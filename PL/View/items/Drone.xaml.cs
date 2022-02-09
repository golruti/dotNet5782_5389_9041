using BO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone
    {
        DroneViewModel droneViewModel;


        public Drone(BlApi.IBL bl/*, Action refreshDroneList*/)
        {
            InitializeComponent();
            droneViewModel = new DroneViewModel(bl/*, refreshDroneList*/);
            this.DataContext = droneViewModel;
            Add_grid.Visibility = Visibility.Visible;
            DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
            StationsId.DataContext = bl.GetBaseStationForList().Select(item => item.Id);
        }



        public Drone(DroneForList droneForList, BlApi.IBL bl/*, Action refreshDroneList*/)
        {
            InitializeComponent();
            droneViewModel = new DroneViewModel(droneForList, bl/*, refreshDroneList*/);
            this.DataContext = droneViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }

        public Drone(BO.Drone droneForList, BlApi.IBL bl/*, Action refreshDroneList*/)
        {
            InitializeComponent();
            droneViewModel = new DroneViewModel(droneForList, bl/*, refreshDroneList*/);
            this.DataContext = droneViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }
        private void DeleteDrone(object sender, RoutedEventArgs e)
        {
            droneViewModel.Bl.DeleteBLDrone(droneViewModel.DroneInList.Id);
            if (MessageBox.Show("the drone was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        /// <summary>
        /// Puts the new Harhan on the list and updates the details
        /// </summary>
        /// <param name="sender"></paramz
        /// <param name="e"></param>
        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneViewModel.Bl.AddDrone( new BO.Drone()
                {
                    Id = int.Parse(Id.Text),
                    Model = Model.Text,
                    MaxWeight = (BO.Enums.WeightCategories)DroneWeights.SelectedItem,
                    Status = BO.Enums.DroneStatuses.Maintenance,
                    Battery = droneViewModel.Rand.Next(20, 40),
                    Delivery = null,
                    Location = new Location()
                    {
                        Longitude = droneViewModel.Bl.GetBLBaseStation(int.Parse(StationsId.Text)).Location.Longitude,
                        Latitude = droneViewModel.Bl.GetBLBaseStation(int.Parse(StationsId.Text)).Location.Latitude
                    }
                   
                });

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
        /// sending drone to delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendingDroneForDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneViewModel.Bl.AssignParcelToDrone(droneViewModel.DroneInList.Id);
                (sender as Button).IsEnabled = false;
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
            //  droneViewModel.RefreshDroneInList();
            PO.ListsModel.RefreshDrones();
        }

        private void Close_Page_notDo(object sender, RoutedEventArgs e)
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
        }

        /// <summary>
        /// release drone from charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneViewModel.Bl.UpdateRelease(droneViewModel.DroneInList.Id);
                (sender as Button).IsEnabled = false;
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
        /// sending drone to charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendingDroneForCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneViewModel.Bl.UpdateCharge(droneViewModel.DroneInList.Id);
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
        /// collection percel by drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneViewModel.Bl.PackageCollection(droneViewModel.DroneInList.Id);
                (sender as Button).IsEnabled = false;
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
        /// Package delivery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneViewModel.Bl.PackageDelivery(droneViewModel.DroneInList.Id);
                (sender as Button).IsEnabled = false;
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
        /// update the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                droneViewModel.Bl.UpdateDroneModel(droneViewModel.DroneInList.Id, droneViewModel.DroneInList.Model);
                //(sender as Button).IsEnabled = false;
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

        private void parcelByDrone_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var selectedParcel = parcelByDrone.SelectedItem as PO.ParcelByTransfer;
            //TabItem tabItem = new TabItem();
            //tabItem.Content = new Parcel(selectedParcel.Id, this.droneViewModel.Bl, /*RefreshParcelList,*/ droneViewModel.AddTab);
            //tabItem.Header = "Update Parcel";
            //tabItem.Visibility = Visibility.Visible;
            //this.droneViewModel.AddTab(tabItem);
        }
    }
}
