using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using PL.ViewModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStation.xaml
    /// </summary>
    public partial class BaseStation 
    {
        private string newName;
        private string newNum;
        private IEnumerable<BO.DroneInCharging> dronesInCharging;
        //
        //
        //

        BaseStationViewModel baseStationViewModel;

        public BaseStation(BlApi.IBL bl, Action refreshBaseStationList)
        {
            InitializeComponent();
            baseStationViewModel = new BaseStationViewModel(bl,refreshBaseStationList);
            this.DataContext = baseStationViewModel;
            Add_grid.Visibility = Visibility.Visible;
        }
        
        public BaseStation(BlApi.IBL bl, Action<TabItem> addTab, BaseStationForList baseStationForList, Action refreshBaseStationList)
        {
            InitializeComponent();
            baseStationViewModel = new BaseStationViewModel(bl, addTab, baseStationForList, refreshBaseStationList);
            this.DataContext = baseStationViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }

        private void RefreshDroneInChargeList()
        {
            dronesInCharging = baseStationViewModel.Bl.DronesInCharging(baseStationViewModel.BaseStationInList.Id);
            DronesListView.DataContext = dronesInCharging;

        }
        private void DeleteBaseStation(object sender, RoutedEventArgs e)
        {
            baseStationViewModel.Bl.deleteBLBaseStation(baseStationViewModel.BaseStationInList.Id);
            if (MessageBox.Show("the customer was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                baseStationViewModel.Bl.AddBaseStation(new BO.BaseStation(int.Parse(Id.Text),Name.Text,double.Parse(longitude.Text), double.Parse(latitude.Text),int.Parse(Num_of_charging_positions.Text)));

                if (MessageBox.Show("the drone was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
        }

       

        /// <summary>
        /// Input filter for ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    
    private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedDrone = (sender as ContentControl).DataContext as BO.DroneForList;

            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone(selectedDrone, baseStationViewModel.Bl, baseStationViewModel.DronesList.RefreshDroneList);
            tabItem.Header = "Update drone";
            tabItem.Visibility = Visibility.Visible;
            baseStationViewModel.AddTab(tabItem);

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

            baseStationViewModel.RefreshStationsList();


        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (update_name.Text != null && update_num_of_charging_ports.Text != null)
                {
                    baseStationViewModel.Bl.UpdateBaseStation(baseStationViewModel.BaseStationInList.Id, update_name.Text, int.Parse(update_num_of_charging_ports.Text));
                    (sender as Button).IsEnabled = false;
                }
                else if(update_name.Text != null)
                {
                    baseStationViewModel.Bl.UpdateBaseStation(baseStationViewModel.BaseStationInList.Id, update_name.Text, baseStationViewModel.BaseStationInList.AvailableChargingPorts);
                    (sender as Button).IsEnabled = false;
                }
                else
                {
                    baseStationViewModel.Bl.UpdateBaseStation(baseStationViewModel.BaseStationInList.Id, baseStationViewModel.BaseStationInList.Name, int.Parse(update_num_of_charging_ports.Text));
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
