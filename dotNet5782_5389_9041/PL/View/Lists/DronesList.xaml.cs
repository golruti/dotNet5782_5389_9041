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
    public partial class DronesList
    {
        DroneListViewModel droneListViewModel;
        
        public DronesList(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            InitializeComponent();
            droneListViewModel = new DroneListViewModel(bl, addTab);
            droneListViewModel.DronesList.Filter = FilterDrone;
            this.DataContext = droneListViewModel;
            droneListViewModel.DronesList.GroupDescriptions.Add(new PropertyGroupDescription("Status"));
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(DronesListView.DataContext);
            //PropertyGroupDescription groupDescription = new PropertyGroupDescription("Status");
            ////view.GroupDescriptions.Add(groupDescription);
        }


        private bool FilterDrone(object obj)
        {
            if (obj is DroneForList drone)
            {
                if (DroneStatuses.SelectedItem != null && DroneWeights.SelectedItem != null)
                {
                    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                    return drone.Status == status && drone.MaxWeight == weight;
                }
                else if (DroneWeights.SelectedItem != null)
                {
                    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                    return drone.MaxWeight == weight;
                }
                else if (DroneStatuses.SelectedItem != null)
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
        /// Updates the drone list
        /// </summary>
        private void RefreshDroneList()
        {
            droneListViewModel.RefreshDroneList();
            RefreshFilter();
        }

        
        private void RefreshFilter()
        {
            droneListViewModel.DronesList.Refresh();
        }
        /// <summary>
        /// Changes the list of skimmers according to the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneDtatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }
        /// <summary>
        /// Changes the list of skimmers according to the weight selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneWeights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }


        /// <summary>
        /// Opens a window for adding a skimmer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddDroneWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone(droneListViewModel.Bl, RefreshDroneList);
            tabItem.Header = "Add drone";
            droneListViewModel.AddTab(tabItem);
        }

        /// <summary>
        /// window winder that allows you to update the details of the glider that you double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedDrone = DronesListView.SelectedItem as PO.DroneForList;

            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone(ConvertFunctions.PODroneForListToBO(selectedDrone), droneListViewModel.Bl, RefreshDroneList);
            tabItem.Header = "Update drone";
            tabItem.Visibility = Visibility.Visible;
            this.droneListViewModel.AddTab(tabItem);
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
