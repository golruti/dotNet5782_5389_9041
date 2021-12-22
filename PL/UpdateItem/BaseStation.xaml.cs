using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStation.xaml
    /// </summary>
    public partial class BaseStation 
    {

        BlApi.IBL bl;
        Action<TabItem> addTab;
        private string newName;
        private string newNum;
        private BO.BaseStationForList baseStationForList;
        private BO.Drone droneForList;
        Action refreshBaseStationList;
        ObservableCollection<DroneForList> droneForLists;
        public BaseStation(BlApi.IBL bl, Action<TabItem> addTab, Action<TabItem> removeTab, BaseStationForList baseStationForList, Action refreshBaseStationList)
        {
            InitializeComponent();
            this.bl = bl;
            this.addTab = addTab;
            //baseStationForList = bl.GetBLBaseStatiog(baseStationForList.Id);
            droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList().Where(item => item.Location == bl.GetBLBaseStation(baseStationForList.Id).Location && item.Status == Enums.DroneStatuses.Maintenance));
            DronesListView.DataContext = droneForLists;
            this.refreshBaseStationList = refreshBaseStationList;
            //this.droneForList = bl.GetBLDrone(droneForList.Id);
            //this.DataContext = droneForList;

        }

        
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedDrone = (sender as ContentControl).DataContext as BO.DroneForList;

            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone(selectedDrone, this.bl, RefreshDroneList);
            tabItem.Header = "Update drone";
            tabItem.Visibility = Visibility.Visible;
            this.addTab(tabItem);

        }
        private void RefreshDroneList()
        {
            
            DronesListView.DataContext = droneForLists;
        }

        private void Close_Page(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            TabItem tabItem = null;
            while (tmp.GetType() != typeof(TabControl))
            {
                if (tmp.GetType() == typeof(TabItem))
                    tabItem = (tmp as TabItem);
                tmp = ((FrameworkElement)tmp).Parent;
            }
            if (tmp is TabControl tabControl)
                tabControl.Items.Remove(tabItem);

            this.refreshBaseStationList();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (update_name.Text != null && update_num_of_charging_ports.Text != null)
                {
                    bl.UpdateBaseStation(baseStationForList.Id, update_name.Text, int.Parse(update_num_of_charging_ports.Text));
                    (sender as Button).IsEnabled = false;
                }
                else if(update_name.Text != null)
                {
                    bl.UpdateBaseStation(baseStationForList.Id, update_name.Text,baseStationForList.AvailableChargingPorts);
                    (sender as Button).IsEnabled = false;
                }
                else
                {
                    bl.UpdateBaseStation(baseStationForList.Id, baseStationForList.Name, int.Parse(update_num_of_charging_ports.Text));
                    (sender as Button).IsEnabled = false;
                }
                if (MessageBox.Show("The base station has been updated successfully!", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The base station could not be updated, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The base station could not be updated, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The base station could not be updated, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The base station could not be updated, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The base station could not be updated, {ex.Message}");
            }
        }
        private void update_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            newName = (sender as TextBox).Text;
        }

        private void update_num_of_charging_ports_TextChanged(object sender, TextChangedEventArgs e)
        {
            newNum = (sender as TextBox).Text;
        }
    }
}
