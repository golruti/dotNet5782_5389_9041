using BO;
using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone
    {
        DroneViewModel droneViewModel;

        /// <summary>
        /// Constructor for "Add Drone" page.
        /// </summary>
        public Drone()
        {
            InitializeComponent();
            droneViewModel = new DroneViewModel();
            this.DataContext = droneViewModel;
            Add_grid.Visibility = Visibility.Visible;
            DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
            StationsId.DataContext = ListsModel.Bl.GetBaseStationForList().Select(item => item.Id);
        }

        /// <summary>
        /// Constructor for "Update Drone" page.
        /// </summary>
        public Drone(DroneForList droneForList)
        {
            InitializeComponent();
            droneViewModel = new DroneViewModel(droneForList);
            this.DataContext = droneViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Constructor for "Update Drone" page.
        /// </summary>
        public Drone(BO.Drone droneForList)
        {
            InitializeComponent();
            droneViewModel = new DroneViewModel(droneForList);
            this.DataContext = droneViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Deleting a drone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDrone(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.DeleteBLDrone(droneViewModel.DroneInList.Id);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show($"The drone was not found and not deleted ");

            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show($"The drone was not found and not deleted ");
            }
            catch (Exception)
            {
                MessageBox.Show($"The drone was not deleted ");
            }
            if (MessageBox.Show("the drone was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        /// <summary>
        /// Adding a new drone.
        /// </summary>
        /// <param name="sender"></paramz
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.AddDrone(new BO.Drone()
                {
                    Id = int.Parse(Id.Text),
                    Model = Model.Text,
                    MaxWeight = (BO.Enums.WeightCategories)DroneWeights.SelectedItem,
                    Status = BO.Enums.DroneStatuses.Maintenance,
                    Battery = droneViewModel.Rand.Next(20, 40),
                    Delivery = null,
                    Location = new Location()
                    {
                        Longitude = ListsModel.Bl.GetBLBaseStation(int.Parse(StationsId.Text)).Location.Longitude,
                        Latitude = ListsModel.Bl.GetBLBaseStation(int.Parse(StationsId.Text)).Location.Latitude
                    }
                }, int.Parse(StationsId.Text));

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
        /// sending drone to delivery.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendingDroneForDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.AssignParcelToDrone(droneViewModel.DroneInList.Id);
                if (MessageBox.Show("The drone is sent for delivery", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    droneViewModel.RefreshDroneInList();
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");

            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
        }


        /// <summary>
        /// Release drone from charging.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.UpdateRelease(droneViewModel.DroneInList.Id);
                if (MessageBox.Show("The drone succeed to free itself from charging", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    droneViewModel.RefreshDroneInList();
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone not succeed to free itself from charging, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone not succeed to free itself from charging, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone not succeed to free itself from charging, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone not succeed to free itself from charging, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone not succeed to free itself from charging, {ex.Message}");
            }
        }

        /// <summary>
        /// Sending drone to charging.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendingDroneForCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.UpdateCharge(droneViewModel.DroneInList.Id);
                if (MessageBox.Show("The drone was sent for loading", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    droneViewModel.RefreshDroneInList();
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
        }

        /// <summary>
        /// Collection percel by drone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.ParcelCollection(droneViewModel.DroneInList.Id);
                if (MessageBox.Show("The drone is sent for delivery", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    droneViewModel.RefreshDroneInList();
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
        }

        /// <summary>
        /// The drone reached its destination and אthe parcel was collected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.UpdateParcelDelivered(droneViewModel.DroneInList.Id);
                if (MessageBox.Show("he parcel was successfully delivered", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    droneViewModel.RefreshDroneInList();
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"Sorry, the parcel was not delivered, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"Sorry, the parcel was not delivered, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"Sorry, the parcel was not delivered, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Sorry, the parcel was not delivered, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, the parcel was not delivered, {ex.Message}");
            }
        }

        /// <summary>
        /// Update drone information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.UpdateDroneModel(droneViewModel.DroneInList.Id, droneViewModel.DroneInList.Model);
                if (MessageBox.Show("The drone model has been updated successfully!", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    droneViewModel.RefreshDroneInList();
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone model could not be updated, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone model could not be updated, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone model could not be updated, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone model could not be updated, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone model could not be updated, {ex.Message}");
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

        /// <summary>
        ///  View a specific drone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void parcelByDrone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (droneViewModel.DroneInList.Delivery.Id != 0)
            {
                var selectedParcel = droneViewModel.DroneInList.Delivery;
                if (!selectedParcel.Equals(null))
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Content = new Parcel(selectedParcel.Id);
                    tabItem.Header = "Update Parcel";
                    tabItem.Visibility = Visibility.Visible;
                    Tabs.AddTab(tabItem);
                }
            }
        }

        /// <summary>
        /// Starting a drone simulator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Automatic_Click(object sender, RoutedEventArgs e)
        {
            droneViewModel.StartDroneSimulator();
        }

        /// <summary>
        /// Close the tab and stops the simulator activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Page(object sender, RoutedEventArgs e)
        {
            Tabs.RemoveTab(sender, e);

            if (droneViewModel.worker != null && droneViewModel.worker.IsBusy)
                droneViewModel.worker.CancelAsync();
        }

        /// <summary>
        /// Close the tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Page_notDo(object sender, RoutedEventArgs e)
        {
            Tabs.RemoveTab(sender, e);
        }
    }
}
