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
        BaseStationViewModel baseStationViewModel;

        public BaseStation()
        {
            InitializeComponent();
            baseStationViewModel = new BaseStationViewModel();
            this.DataContext = baseStationViewModel;
            Add_grid.Visibility = Visibility.Visible;
        }

        public BaseStation(BaseStationForList baseStationForList)
        {
            InitializeComponent();
            baseStationViewModel = new BaseStationViewModel(baseStationForList);
            this.DataContext = baseStationViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }

        private void DeleteBaseStation(object sender, RoutedEventArgs e)
        {
            try
            {
                PO.ListsModel.Bl.deleteBLBaseStation(baseStationViewModel.BaseStationInList.Id);
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show($"The station does not exist and could not be deleted");
            }
            catch (Exception)
            {
                MessageBox.Show($"The station was not delete.");
            }
            if (MessageBox.Show("the station was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        private void add_Station_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(longitude.Text) < -90 || double.Parse(longitude.Text) > 90 || double.Parse(latitude.Text) < -90 || double.Parse(latitude.Text) > 90)
            {
                MessageBox.Show("Location not in the middle");
                return;
            }

            try
            {
                PO.ListsModel.Bl.AddBaseStation(new BO.BaseStation()
                {
                    Id = int.Parse(Id.Text),
                    AvailableChargingPorts = int.Parse(Num_of_charging_positions.Text),
                    DronesInCharging = new List<DroneInCharging>(),
                    Location = new Location() { Longitude = double.Parse(longitude.Text), Latitude = double.Parse(latitude.Text) },
                    Name = Name.Text
                }); ;

                if (MessageBox.Show("the station was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The station was not add, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"No charge for sending for charging");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"A key already exists");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The station was not add, {ex.Message}");
            }
            catch (Exception)
            {
                MessageBox.Show($"The station was not add");
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
            try
            {
                var selectedDrone = DronesListView.SelectedItem as PO.DroneInCharging;
                if (!selectedDrone.Equals(null))
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Content = new Drone(PO.ListsModel.Bl.GetBLDrone(selectedDrone.Id));
                    tabItem.Header = "Update drone";
                    tabItem.Visibility = Visibility.Visible;
                    Tabs.AddTab(tabItem);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The station could not be found in the database, {ex.Message}");
            }
            catch (Exception)
            {
                MessageBox.Show($"The station can not be displayed");
            }
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

            PO.ListsModel.RefreshStations();
            // baseStationViewModel.RefreshStationsList();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (update_name.Text != null && update_num_of_charging_ports.Text != null)
                {
                    PO.ListsModel.Bl.UpdateBaseStation(baseStationViewModel.BaseStationInList.Id, update_name.Text, int.Parse(update_num_of_charging_ports.Text));
                    (sender as Button).IsEnabled = false;
                }
                else if (update_name.Text != null)
                {
                    PO.ListsModel.Bl.UpdateBaseStation(baseStationViewModel.BaseStationInList.Id, update_name.Text, baseStationViewModel.BaseStationInList.AvailableChargingPorts);
                    (sender as Button).IsEnabled = false;
                }
                else
                {
                    PO.ListsModel.Bl.UpdateBaseStation(baseStationViewModel.BaseStationInList.Id, baseStationViewModel.BaseStationInList.Name, int.Parse(update_num_of_charging_ports.Text));
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
            catch (ArithmeticException ex)
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
            catch (Exception)
            {
                MessageBox.Show($"The base station could not be updated");
            }
        }
    }
}
