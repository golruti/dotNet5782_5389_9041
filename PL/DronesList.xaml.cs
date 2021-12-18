﻿using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DronesList
    {
        IBL.IBL bl;
        Action<TabItem> addTab;


        ObservableCollection<DroneForList> droneForLists;
        public DronesList(IBL.IBL bl, Action<TabItem> addTab, Action<TabItem> removeTab)
        {
            InitializeComponent();
            this.bl = bl;
            this.addTab = addTab;

            droneForLists= new ObservableCollection<DroneForList>(bl.GetDroneForList());
            DronesListView.DataContext = droneForLists;
            DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
            DroneStatuses.DataContext = Enum.GetValues(typeof(Enums.DroneStatuses));
        }

        /// <summary>
        /// Updates the skimmer list
        /// </summary>
        private void RefreshDroneList()
        {
            if (DroneStatuses.SelectedItem != null && DroneWeights.SelectedItem != null)
            {
                Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList(drone => drone.Status == status && drone.MaxWeight == weight));
                DronesListView.DataContext = droneForLists;
            }
            else if (DroneWeights.SelectedItem != null)
            {
                Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList(drone => drone.MaxWeight == weight));
                DronesListView.DataContext = droneForLists;
            }
            else if (DroneStatuses.SelectedItem != null)
            {
                Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList(drone => drone.Status == status));
                DronesListView.DataContext = droneForLists;
            }
            
            else
            {
                droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList());
                DronesListView.DataContext = droneForLists;
            }
        }

        /// <summary>
        /// Changes the list of skimmers according to the weight selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneWeights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneWeights.SelectedItem == null)
            {
                DronesListView.DataContext = bl.GetDroneForList();
            }
            else
            {
                if (DroneStatuses.SelectedItem != null)
                {
                    Enums.WeightCategories tempWeight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                    DronesListView.DataContext = bl.GetDroneForList(drone => drone.Status == status && drone.MaxWeight == tempWeight);
                }
                else { 
                    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                    DronesListView.DataContext = bl.GetDroneForList(drone => drone.MaxWeight == weight);
                }
            }
        }

        /// <summary>
        /// Changes the list of skimmers according to the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneDtatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneStatuses.SelectedItem == null)
            {
                DronesListView.ItemsSource = bl.GetDroneForList();
            }
            else
            {
                if(DroneWeights.SelectedItem != null)
                {
                    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                    
                    DronesListView.DataContext = bl.GetDroneForList(drone => drone.MaxWeight == weight && drone.Status == status);
                }
                else
                {
                    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                    DronesListView.ItemsSource = bl.GetDroneForList(drone => drone.Status == status);
                }
            }
            
        }

        /// <summary>
        /// Opens a window for adding a skimmer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddDroneWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new AddDrone(bl,RefreshDroneList);
            tabItem.Header = "Add drone";
            this.addTab(tabItem);
        }

        /// <summary>
        /// window winder that allows you to update the details of the glider that you double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedDrone = (sender as ContentControl).DataContext as IBL.BO.DroneForList;

            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone(selectedDrone, this.bl, RefreshDroneList);
            tabItem.Header = "Update drone";
            tabItem.Visibility = Visibility.Visible;
            this.addTab(tabItem);

        }

        /// <summary>
        /// the function close the page
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
        }
    }
}
