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
        private BlApi.IBL bl;
        private BO.Drone droneInList;
        private Action refreshDroneList;
        private string newModel;       
        private Random rand;

        

        public Drone(BlApi.IBL bl, Action refreshDroneList)
        {
            InitializeComponent();
            this.bl = bl;

            StationsId.DataContext = (bl.GetBaseStationForList()).Select(s => s.Id);
            this.refreshDroneList = refreshDroneList;
            DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
        }
        public Drone(DroneForList droneForList, BlApi.IBL bl, Action refreshDroneList)
        {
            InitializeComponent();

            this.bl = bl;
            this.refreshDroneList = refreshDroneList;
            this.droneInList = bl.GetBLDrone(droneForList.Id);
            this.DataContext = droneForList;

            if (droneForList.Status == Enums.DroneStatuses.Available)
            {
                Button sendingDroneForChargingBtn = new Button();
                sendingDroneForChargingBtn.Content = "Sending a drone for charging";
                sendingDroneForChargingBtn.Click += SendingDroneForCharging_Click;
                sendingDroneForChargingBtn.IsEnabled = true;
                sendingDroneForChargingBtn.Background = Brushes.DarkCyan;
                sendingDroneForChargingBtn.Height = 40;
                sendingDroneForChargingBtn.Width = 130;
                
                sendingDroneForChargingBtn.HorizontalAlignment = HorizontalAlignment.Center;
                ButtonsGroup.Children.Add(sendingDroneForChargingBtn);

                Button SendingDroneForDelivery = new Button();
                SendingDroneForDelivery.Content = " Connecting a parcel to a drone";
                SendingDroneForDelivery.Click += SendingDroneForDelivery_Click;
                SendingDroneForDelivery.IsEnabled = true;
                SendingDroneForDelivery.Background = Brushes.DarkCyan;
                SendingDroneForDelivery.Height = 40;
                SendingDroneForDelivery.Width = 130;
                SendingDroneForDelivery.HorizontalAlignment = HorizontalAlignment.Center;
                ButtonsGroup.Children.Add(SendingDroneForDelivery);
            }
            else if (droneForList.Status == Enums.DroneStatuses.Maintenance)
            {
                Button ReleaseDroneFromCharging = new Button();
                ReleaseDroneFromCharging.Content = "Release drone from charging";
                ReleaseDroneFromCharging.Click += ReleaseDroneFromCharging_Click;
                ReleaseDroneFromCharging.IsEnabled = true;
                ReleaseDroneFromCharging.Background = Brushes.DarkCyan;
                ReleaseDroneFromCharging.Height = 40;
                ReleaseDroneFromCharging.Width = 130;
                ReleaseDroneFromCharging.HorizontalAlignment = HorizontalAlignment.Center;

                ButtonsGroup.Children.Add(ReleaseDroneFromCharging);
            }

            if (bl.GetParcelStatusByDrone(droneForList.Id) == Enums.ParcelStatuses.Associated)
            {
                Button ParcelCollection = new Button();
                ParcelCollection.Content = "Parcel collection";
                ParcelCollection.Click += ParcelCollection_Click;
                ParcelCollection.IsEnabled = true;
                ParcelCollection.Background = Brushes.DarkCyan;
                ParcelCollection.Height = 40;
                ParcelCollection.Width = 130;
                ParcelCollection.HorizontalAlignment = HorizontalAlignment.Center;

                ButtonsGroup.Children.Add(ParcelCollection);
            }

            else if (bl.GetParcelStatusByDrone(droneForList.Id) == Enums.ParcelStatuses.Collected)
            {
                Button ParcelDelivery = new Button();
                ParcelDelivery.Content = "Parcel delivery";
                ParcelDelivery.Click += ParcelDelivery_Click;
                ParcelDelivery.Background = Brushes.DarkCyan;
                ParcelDelivery.Height = 40;
                ParcelDelivery.Width = 130;
                ParcelDelivery.HorizontalAlignment = HorizontalAlignment.Center;
                ButtonsGroup.Children.Add(ParcelDelivery);
            }
        }


        /// <summary>
        /// Puts the new Harhan on the list and updates the details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddDrone(new BO.Drone(int.Parse(Id.Text), Model.Text, (BO.Enums.WeightCategories)DroneWeights.SelectedItem, BO.Enums.DroneStatuses.Maintenance, rand.Next(20, 41), bl.GetBLBaseStation(int.Parse(StationsId.Text)).Location.Longitude, bl.GetBLBaseStation(int.Parse(StationsId.Text)).Location.Latitude));


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
                bl.AssignParcelToDrone(droneInList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone is sent for delivery", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
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
        /// release drone from charging
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateRelease(droneInList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone succeed to free itself from charging", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
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
                bl.UpdateCharge(droneInList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone was sent for loading", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
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
                bl.PackageCollection(droneInList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone is sent for delivery", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
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
                bl.PackageDelivery(droneInList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("he parcel was successfully delivered", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
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
        /// close the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            this.refreshDroneList();
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
        /// update the drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateDroneModel(droneInList.Id, update_model.Text);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone model has been updated successfully!", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
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
        /// update the model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_model_TextChanged(object sender, TextChangedEventArgs e)
        {
            newModel = (sender as TextBox).Text;
        }
    }
}
