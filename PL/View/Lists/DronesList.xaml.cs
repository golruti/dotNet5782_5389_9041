using System;
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
using PO;
using PL.ViewModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DronesList : UserControl
    {
        DroneListViewModel droneListViewModel;

        /// <summary>
        /// onstructor
        /// </summary>
        public DronesList()
        {
            InitializeComponent();
            droneListViewModel = new DroneListViewModel();
            droneListViewModel.DronesList.Filter = FilterDrone;
            this.DataContext = droneListViewModel;
        }

        /// <summary>
        /// Show "Add Drone" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddDroneWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone();
            tabItem.Header = "Add drone";
            Tabs.AddTab(tabItem);
        }

        /// <summary>
        /// View a specific drone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DronesListView.SelectedItem is PO.DroneForList selectedDrone)
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Drone(ConvertFunctions.PODroneForListToBO(selectedDrone));
                tabItem.Header = "Update drone";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
            }
        }

        /// <summary>
        /// Filter for filtering the drones list.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterDrone(object obj)
        {
            object selectedStatus = DroneStatuses.SelectedItem == DroneListViewModel.NONE_VALUE ? null : DroneStatuses.SelectedItem;
            object selectedWeight = DroneWeights.SelectedItem == DroneListViewModel.NONE_VALUE ? null : DroneWeights.SelectedItem;

            if (obj is DroneForList drone)
            {
                if (selectedStatus != null && selectedWeight != null)
                {
                    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                    return drone.Status == status && drone.MaxWeight == weight;
                }
                else if (selectedWeight != null)
                {
                    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                    return drone.MaxWeight == weight;
                }
                else if (selectedStatus != null)
                {
                    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                    return drone.Status == status;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Changes the list of droners according to the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneDtatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }
        /// <summary>
        /// Changes the list of drones according to the weight selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneWeights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }
        /// <summary>
        /// Refresh the list view.
        /// </summary>
        private void RefreshFilter()
        {
            droneListViewModel.DronesList.Refresh();
        }


        /// <summary>
        /// Close the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Page(object sender, RoutedEventArgs e)
        {
            Tabs.RemoveTab(sender, e);
        }

        /// <summary>
        /// View customers by groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.IsChecked == true)
            {
                droneListViewModel.DronesList.GroupDescriptions.Add(new PropertyGroupDescription("Status"));
            }
            else
            {
                droneListViewModel.DronesList.GroupDescriptions.Clear();
            }
        }
    }
}
