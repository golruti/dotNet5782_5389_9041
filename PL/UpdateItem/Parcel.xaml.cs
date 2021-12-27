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

namespace PL.UpdateItem
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel : UserControl
    {
        private BlApi.IBL bl;
        private BO.Parcel parcelInList;
        Action refreshDroneList;
        private string newModel;
        public Parcel(ParcelForList parcelForList, BlApi.IBL bl, Action refreshDroneList)
        {
            InitializeComponent();

            this.bl = bl;
            this.refreshDroneList = refreshDroneList;

            this.parcelInList = bl.GetBLParcel(parcelForList.Id);
            this.DataContext = parcelForList;

            if (parcelForList.Status != Enums.ParcelStatuses.Provided )
            {
                Button ShowDrone = new Button();
                ShowDrone.Content = "Details of the drone";
                ShowDrone.Click += ShowDrone_Click;
                ShowDrone.IsEnabled = true;
                ShowDrone.Background = Brushes.DarkCyan;
                ShowDrone.Height = 40;
                ShowDrone.Width = 130;
                ShowDrone.HorizontalAlignment = HorizontalAlignment.Center;
                ButtonsGroup.Children.Add(ShowDrone);

                
            }
            
        }

        private void ShowDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateCharge(parcelInList.Id);
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

        private void SendingDroneForDelivery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AssignParcelToDrone(parcelInList.Id);
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
                bl.UpdateRelease(parcelInList.Id);
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
       

        /// <summary>
        /// collection percel by drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.PackageCollection(parcelInList.Id);
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
                bl.PackageDelivery(parcelInList.Id);
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

        ///// <summary>
        ///// update the drone
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Update_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        bl.UpdateDroneModel(parcelInList.Id, update_model.Text);
        //        (sender as Button).IsEnabled = false;
        //        if (MessageBox.Show("The drone model has been updated successfully!", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
        //        {
        //            Close_Page(sender, e);
        //        }
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        MessageBox.Show($"The drone model could not be updated, {ex.Message}");
        //    }
        //    catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
        //    {
        //        MessageBox.Show($"The drone model could not be updated, {ex.Message}");
        //    }
        //    catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
        //    {
        //        MessageBox.Show($"The drone model could not be updated, {ex.Message}");
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        MessageBox.Show($"The drone model could not be updated, {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"The drone model could not be updated, {ex.Message}");
        //    }
        //}

        ///// <summary>
        ///// update the model
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void update_model_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    newModel = (sender as TextBox).Text;
        //}
    }
}
