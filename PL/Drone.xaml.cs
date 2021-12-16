﻿using IBL.BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone
    {
        private IBL.IBL bl;
        private IBL.BO.Drone droneForList;

        public Drone(DroneForList droneForList, IBL.IBL bl)
        {
            InitializeComponent();

            this.bl = bl;
            this.droneForList = bl.GetBLDrone(droneForList.Id);
            this.DataContext = droneForList;

            if (droneForList.Status == Enums.DroneStatuses.Available)
            {
                Button sendingDroneForChargingBtn = new Button();
                sendingDroneForChargingBtn.Content = "Sending a drone for charging";
                sendingDroneForChargingBtn.Click += SendingDroneForCharging_Click;
                sendingDroneForChargingBtn.IsEnabled = true;
                ButtonsGroup.Children.Add(sendingDroneForChargingBtn);

                Button SendingDroneForDelivery = new Button();
                SendingDroneForDelivery.Content = "Sending a drone for delivery";
                SendingDroneForDelivery.Click += SendingDroneForCharging_Click;
                SendingDroneForDelivery.IsEnabled = true;
                ButtonsGroup.Children.Add(SendingDroneForDelivery);
            }
            else if (droneForList.Status == Enums.DroneStatuses.Maintenance)
            {
                Button ReleaseDroneFromCharging = new Button();
                ReleaseDroneFromCharging.Content = "Release drone from charging";
                ReleaseDroneFromCharging.Click += ReleaseDroneFromCharging_Click;
                ButtonsGroup.Children.Add(ReleaseDroneFromCharging);
            }

            if (bl.GetParcelStatusByDrone(droneForList.Id) == Enums.ParcelStatuses.Associated)
            {
                Button ParcelCollection = new Button();
                ParcelCollection.Content = "Parcel collection";
                ParcelCollection.Click += ParcelCollection_Click;
                ButtonsGroup.Children.Add(ParcelCollection);
            }

            else if (bl.GetParcelStatusByDrone(droneForList.Id) == Enums.ParcelStatuses.Collected)
            {
                Button ParcelDelivery = new Button();
                ParcelDelivery.Content = "Parcel delivery";
                ParcelDelivery.Click += ParcelDelivery_Click;
                ButtonsGroup.Children.Add(ParcelDelivery);
            }
        }



        //שחרורו רחפן מטעינה
        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            label_time.Visibility = Visibility.Visible;
            textBox_time.Visibility = Visibility.Visible;
            button_time.Visibility = Visibility.Visible;
        }

        //שחרור רחפן מטעינה
        private void Button_ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ReleaseDroneFromRecharge(droneForList.Id, int.Parse(textBox_time.Text));
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone succeed to free itself from charging", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone not succeed to free itself from charging, {ex.Message}");
            }
            catch
            {
                MessageBox.Show($"The glider was unable to free itself from loading, ");
            }
        }

        //שליחת רחפן לטעינה
        private void SendingDroneForCharging_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SendDroneToRecharge(droneForList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone was sent for loading", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch
            {
                MessageBox.Show($"The drone could not be sent for loading, ");
            }
        }


        //שליחת הרחפן למשלוח
        private void SendingDroneForDelivery(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AssignParcelToDrone(droneForList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone is sent for delivery", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch
            {
                MessageBox.Show($"The drone could not be shipped, ");
            }            
        }


        //איסוף חבילה עי רחפן
        private void ParcelCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.PackageCollection(droneForList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone is sent for delivery", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone could not be shipped, {ex.Message}");
            }
            catch
            {
                MessageBox.Show($"The drone could not be shipped, ");
            }
        }



        //-----------------------------------------------

        private void ParcelDelivery_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //-----------------------------------------------









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
        }

        private void textID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}