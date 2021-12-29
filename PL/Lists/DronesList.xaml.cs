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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DronesList
    {
        BlApi.IBL bl;
        Action<TabItem> addTab;

        ObservableCollection<DroneForList> droneForLists;
        ListCollectionView listCollectionView;

        public DronesList(BlApi.IBL bl, Action<TabItem> addTab)
        {
            InitializeComponent();
            this.bl = bl;
            this.addTab = addTab;

            droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList());
            listCollectionView = new ListCollectionView(droneForLists);
            listCollectionView.Filter += FilterDrone;
            DronesListView.DataContext = listCollectionView;

            DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
            DroneStatuses.DataContext = Enum.GetValues(typeof(Enums.DroneStatuses));
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
            droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList());
            listCollectionView = new ListCollectionView(droneForLists);
            listCollectionView.Filter += FilterDrone;
            DronesListView.DataContext = listCollectionView;

            //if (DroneStatuses.SelectedItem != null && DroneWeights.SelectedItem != null)
            //{
            //    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
            //    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
            //    droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList(weight,status));
            //    DronesListView.DataContext = droneForLists;
            //}
            //else if (DroneWeights.SelectedItem != null)
            //{
            //    Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
            //    droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList(weight));
            //    DronesListView.DataContext = droneForLists;
            //}
            //else if (DroneStatuses.SelectedItem != null)
            //{
            //    Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
            //    droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList(status));
            //    DronesListView.DataContext = droneForLists;
            //}         
            //else
            //{
            //    droneForLists = new ObservableCollection<DroneForList>(bl.GetDroneForList());
            //    DronesListView.DataContext = droneForLists;
            //}
        }

        /// <summary>
        /// Changes the list of skimmers according to the weight selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneWeights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();

            //if (DroneWeights.SelectedItem == null)
            //{
            //    DronesListView.DataContext = bl.GetDroneForList();
            //}
            //else
            //{
            //    if (DroneStatuses.SelectedItem != null)
            //    {
            //        Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
            //        Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
            //        DronesListView.DataContext = bl.GetDroneForList(weight,status);
            //    }
            //    else { 
            //        Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
            //        DronesListView.DataContext = bl.GetDroneForList(weight);
            //    }
            //}
        }
        private void RefreshFilter()
        {
            listCollectionView.Filter -= FilterDrone;
            listCollectionView.Filter += FilterDrone;
        }
        /// <summary>
        /// Changes the list of skimmers according to the selected status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DroneDtatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();

            //if (DroneStatuses.SelectedItem == null)
            //{
            //    DronesListView.ItemsSource = bl.GetDroneForList();
            //}
            //else
            //{
            //    if(DroneWeights.SelectedItem != null)
            //    {
            //        Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
            //        Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;

            //        DronesListView.DataContext = bl.GetDroneForList( weight,status);
            //    }
            //    else
            //    {
            //        Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
            //        DronesListView.ItemsSource = bl.GetDroneForList(status);
            //    }
            //}

        }

        /// <summary>
        /// Opens a window for adding a skimmer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddDroneWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new AddDrone(bl, RefreshDroneList);
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
            var selectedDrone = (sender as ContentControl).DataContext as BO.DroneForList;

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
