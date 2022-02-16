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
using PL.ViewModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : UserControl
    {
        ParcelListViewModel parcelListViewModel;

        public ParcelList(int? customerId = null)
        {
            InitializeComponent();
            parcelListViewModel = new ParcelListViewModel(customerId);
            this.DataContext = parcelListViewModel;
            parcelListViewModel.ParcelsForList.Filter += FilterParcel;
            parcelListViewModel.ParcelsForList.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ParcelForList.SendCustomer)));
            SenderId.DataContext = PO.ListsModel.Bl.GetParcelForList().Select(item => item.SendCustomer).Distinct();
            ReceiveId.DataContext = PO.ListsModel.Bl.GetParcelForList().Select(item => item.ReceiveCustomer).Distinct();
            ParcelStatuses.DataContext = Enum.GetValues(typeof(Enums.ParcelStatuses));
            if (customerId != null)
                Buttons.Visibility = Visibility.Collapsed;
            else
                Buttons.Visibility = Visibility.Visible;
                //customerId != null ? Buttons.Visibility = Visibility.Collapsed : Buttons.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Changes the list of drones according to the weight selected
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name="e"></param>
        private void ParcelStatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }

        private void SenderId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }

        private void ReceiveId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }

        private void time_to_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }

        private void time_from_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }


        private void To_time_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }

        private void From_time_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshFilter();
        }
        /// <summary>
        /// Opens a window for adding a skimmer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddParcelWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Parcel();
            tabItem.Header = "Add parcel";
            Tabs.AddTab(tabItem);
        }

        /// <summary>
        /// window winder that allows you to update the details of the glider that you double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedParcel = ParcelesListView.SelectedItem as PO.ParcelForList;
            if (!selectedParcel.Equals(null))
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Parcel(selectedParcel.Id);
                tabItem.Header = "Update Parcel";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
            }
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

        private void MoveToSender(object sender, RoutedEventArgs e)
        {
            parcelListViewModel.ParcelsForList.GroupDescriptions.Clear();
            parcelListViewModel.ParcelsForList.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ParcelForList.SendCustomer)));
        }

        private void MoveToRecive(object sender, RoutedEventArgs e)
        {
            parcelListViewModel.ParcelsForList.GroupDescriptions.Clear();
            parcelListViewModel.ParcelsForList.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ParcelForList.ReceiveCustomer)));
        }
        private void RefreshFilter()
        {
            parcelListViewModel.ParcelsForList.Refresh();
        }

        private bool FilterParcel(object obj)
        {
            if (obj is PO.ParcelForList parcel)
            {
                return (parcelListViewModel.Customer == null || parcel.SendCustomer == parcelListViewModel.Customer.Name || parcel.ReceiveCustomer == parcelListViewModel.Customer.Name)
                    && (ParcelStatuses.SelectedItem == null || parcel.Status == (PO.Enums.ParcelStatuses)ParcelStatuses.SelectedItem)
                    && (SenderId.SelectedItem == null || parcel.SendCustomer == SenderId.SelectedItem.ToString())
                    && (ReceiveId.SelectedItem == null || parcel.ReceiveCustomer == ReceiveId.SelectedItem.ToString())
                    && (From.SelectedDate == null || PO.ListsModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate)
                    && (To.SelectedDate == null || PO.ListsModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate);
            }
            return false;
        }


    }
}
