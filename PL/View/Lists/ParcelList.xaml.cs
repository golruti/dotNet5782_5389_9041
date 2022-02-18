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

        public bool ShouldFilter { get; set; } = true;

        public bool IsInCustomerMode { get; set; } = false;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerId"></param>
        public ParcelList(int? customerId = null)
        {
            InitializeComponent();
            parcelListViewModel = new ParcelListViewModel(customerId);
            this.DataContext = parcelListViewModel;
            parcelListViewModel.ParcelsForList.Filter += FilterParcel;
            SenderId.DataContext = ListsModel.Bl.GetParcelForList().Select(item => item.SendCustomer).Distinct();
            ReceiveId.DataContext = ListsModel.Bl.GetParcelForList().Select(item => item.ReceiveCustomer).Distinct();
            ParcelStatuses.DataContext = Enum.GetValues(typeof(Enums.ParcelStatuses));
            if (customerId != null)
            {
                Buttons.Visibility = Visibility.Collapsed;
                IsInCustomerMode = true;
            }
            else
                Buttons.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Show "Add Parcel" window.
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
        /// View a specific parcel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedParcel = ParcelesListView.SelectedItem as PO.ParcelForList;
            if (selectedParcel != null)
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Parcel(selectedParcel.Id, IsInCustomerMode);
                tabItem.Header = "Update Parcel";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
            }
        }


        /// <summary>
        /// Filter for filtering the parcels list.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterParcel(object obj)
        {
            if (!ShouldFilter) return true;

            if (obj is PO.ParcelForList parcel)
            {
                return (parcelListViewModel.Customer == null || parcel.SendCustomer == parcelListViewModel.Customer.Name || parcel.ReceiveCustomer == parcelListViewModel.Customer.Name)
                    && (ParcelStatuses.SelectedItem == null || parcel.Status == (PO.Enums.ParcelStatuses)ParcelStatuses.SelectedItem)
                    && (SenderId.SelectedItem == null || parcel.SendCustomer == SenderId.SelectedItem.ToString())
                    && (ReceiveId.SelectedItem == null || parcel.ReceiveCustomer == ReceiveId.SelectedItem.ToString())
                    && (From.SelectedDate == null || ListsModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate)
                    && (To.SelectedDate == null || ListsModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate);
            }
            return false;
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

        /// <summary>
        /// Displays the list without filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            ShouldFilter = checkBox.IsChecked != true;
            RefreshFilter();
        }

        private void RefreshFilter()
        {
            parcelListViewModel.ParcelsForList.Refresh();
        }


        /// <summary>
        /// View parcels by groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            parcelListViewModel.ParcelsForList.GroupDescriptions.Clear();
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
    }
}
