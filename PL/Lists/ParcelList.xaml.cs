using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Lists
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : UserControl
    {
        BlApi.IBL bl;
        Action<TabItem> addTab;

        ObservableCollection<ParcelForList> parcelForLists;
        //public ParcelList(BlApi.IBL bl, Action<TabItem> addTab)
        //{
        //    InitializeComponent();
        //    this.bl = bl;
        //    this.addTab = addTab;

        //    parcelForLists = new ObservableCollection<ParcelForList>(bl.GetParcelForList());
        //    ParcelesListView.DataContext = parcelForLists;
        //    ParcelStatuses.DataContext = Enum.GetValues(typeof(Enums.ParcelStatuses));
            
        //}
        ///// <summary>
        ///// Updates the skimmer list
        ///// </summary>
        //private void RefreshParcelList()
        //{
        //    //ParcelStatuses,SenderId,ReceiveId,From,To
        //    if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && From.SelectedItem != null && To.SelectedItem != null)
        //    {
               
        //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
        //        parcelForLists = new ObservableCollection<ParcelForList>(bl.GetParcelForList());
        //        ParcelesListView.DataContext = parcelForLists;
        //    }
        //    else if (SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && From.SelectedItem != null && To.SelectedItem != null)
        //    {
                
        //    }
        //    else if (ParcelStatuses.SelectedItem != null  && ReceiveId.SelectedItem != null && From.SelectedItem != null && To.SelectedItem != null)
        //    {
                
        //    }
        //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && From.SelectedItem != null && To.SelectedItem != null)
        //    {
                
        //    }
        //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null  && To.SelectedItem != null)
        //    {
                
        //    }
        //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && From.SelectedItem != null && To.SelectedItem != null)
        //    {
                

        //    }
        //    else
        //    {
                
        //    }
        //}

        ///// <summary>
        ///// Changes the list of skimmers according to the weight selected
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        ////private void DroneWeights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        ////{
        ////    if (DroneWeights.SelectedItem == null)
        ////    {
        ////        DronesListView.DataContext = bl.GetDroneForList();
        ////    }
        ////    else
        ////    {
        ////        if (DroneStatuses.SelectedItem != null)
        ////        {
        ////            Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
        ////            Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
        ////            DronesListView.DataContext = bl.GetDroneForList(weight, status);
        ////        }
        ////        else
        ////        {
        ////            Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
        ////            DronesListView.DataContext = bl.GetDroneForList(weight);
        ////        }
        ////    }
        ////}

        ///// <summary>
        ///// Changes the list of skimmers according to the selected status
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        ////private void DroneDtatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        ////{
        ////    if (DroneStatuses.SelectedItem == null)
        ////    {
        ////        DronesListView.ItemsSource = bl.GetDroneForList();
        ////    }
        ////    else
        ////    {
        ////        if (DroneWeights.SelectedItem != null)
        ////        {
        ////            Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
        ////            Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;

        ////            DronesListView.DataContext = bl.GetDroneForList(weight, status);
        ////        }
        ////        else
        ////        {
        ////            Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
        ////            DronesListView.ItemsSource = bl.GetDroneForList(status);
        ////        }
        ////    }

        ////}

        ///// <summary>
        ///// Opens a window for adding a skimmer
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ShowAddParcelWindow(object sender, RoutedEventArgs e)
        //{
        //    TabItem tabItem = new TabItem();
        //    tabItem.Content = new AddDrone(bl, RefreshParcelList);
        //    tabItem.Header = "Add drone";
        //    this.addTab(tabItem);
        //}

        ///// <summary>
        ///// window winder that allows you to update the details of the glider that you double-clicked
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var selectedDrone = (sender as ContentControl).DataContext as BO.DroneForList;

        //    TabItem tabItem = new TabItem();
        //    tabItem.Content = new Drone(selectedDrone, this.bl, RefreshParcelList);
        //    tabItem.Header = "Update drone";
        //    tabItem.Visibility = Visibility.Visible;
        //    this.addTab(tabItem);

        //}

        ///// <summary>
        ///// the function close the page
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void Close_Page(object sender, RoutedEventArgs e)
        //{
        //    object tmp = sender;
        //    TabItem tabItem = null;
        //    while (tmp.GetType() != typeof(TabControl))
        //    {
        //        if (tmp.GetType() == typeof(TabItem))
        //            tabItem = (tmp as TabItem);
        //        tmp = ((FrameworkElement)tmp).Parent;
        //    }
        //    if (tmp is TabControl tabControl)
        //        tabControl.Items.Remove(tabItem);
        //}
    }

}
