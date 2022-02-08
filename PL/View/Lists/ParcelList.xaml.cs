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
        //BlApi.IBL bl;
        //Action<TabItem> addTab;
        //Action<object, RoutedEventArgs> RemoveTab;
        //ListCollectionView listCollectionView;
        //ObservableCollection<ParcelForList> parcelForLists;

        ParcelListViewModel parcelListViewModel;

        public ParcelList(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            InitializeComponent();
            parcelListViewModel = new ParcelListViewModel(bl, addTab, removeTab);
            this.DataContext = parcelListViewModel;
            parcelListViewModel.ParcelsForList.Filter = FilterParcel;
            parcelListViewModel.ParcelsForList.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ParcelForList.SendCustomer)));
            SenderId.DataContext = bl.GetParcelForList().Select(item => item.SendCustomer).Distinct();
            ReceiveId.DataContext = bl.GetParcelForList().Select(item => item.ReceiveCustomer).Distinct();
            ParcelStatuses.DataContext = Enum.GetValues(typeof(Enums.ParcelStatuses));
        }

        /// <summary>
        /// Updates the skimmer list
        /// </summary>
        private void RefreshParcelList()
        {
           // parcelListViewModel.RefreshParcelList();
            PO.ListsModel.RefreshParcels();
            RefreshFilter();
        }

        /// <summary>
        /// Changes the list of skimmers according to the weight selected
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
            tabItem.Content = new Parcel(parcelListViewModel.Bl/*, RefreshParcelList*/);
            tabItem.Header = "Add parcel";
            this.parcelListViewModel.AddTab(tabItem);
        }

        /// <summary>
        /// window winder that allows you to update the details of the glider that you double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedParcel = ParcelesListView.SelectedItem as PO.ParcelForList;
            TabItem tabItem = new TabItem();
            tabItem.Content = new Parcel(selectedParcel.Id, this.parcelListViewModel.Bl, /*RefreshParcelList,*/ parcelListViewModel.AddTab);
            tabItem.Header = "Update Parcel";
            tabItem.Visibility = Visibility.Visible;
            this.parcelListViewModel.AddTab(tabItem);
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
                return (ParcelStatuses.SelectedItem == null || parcel.Status == (PO.Enums.ParcelStatuses)ParcelStatuses.SelectedItem)
                    && (SenderId.SelectedItem == null || parcel.SendCustomer == SenderId.SelectedItem)
                    && (ReceiveId.SelectedItem == null || parcel.ReceiveCustomer == ReceiveId.SelectedItem)
                    && (From.SelectedDate == null||parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate)
                    && (To.SelectedDate == null||parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate);

            //    if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && To.SelectedDate != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && To.SelectedDate != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }

            //    else if (ParcelStatuses.SelectedItem != null && ReceiveId.SelectedItem != null && To.SelectedDate != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.Status == status && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && To.SelectedDate != null && From.SelectedDate != null)
            //    {


            //        return parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (ReceiveId.SelectedItem != null && To.SelectedDate != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (SenderId.SelectedItem != null && To.SelectedDate != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.SendCustomer == SenderId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && To.SelectedDate != null && From.SelectedDate != null)
            //    {


            //        return parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && To.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > To.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && To.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > To.SelectedDate;

            //    }

            //    else if (ParcelStatuses.SelectedItem != null && ReceiveId.SelectedItem != null && To.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.Status == status && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > To.SelectedDate;

            //    }
            //    else if (SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && To.SelectedDate != null)
            //    {



            //        return parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > To.SelectedDate;

            //    }
            //    else if (ReceiveId.SelectedItem != null && To.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > To.SelectedDate;

            //    }
            //    else if (SenderId.SelectedItem != null && To.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.SendCustomer == SenderId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > To.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && To.SelectedDate != null)
            //    {



            //        return parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > To.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }

            //    else if (ParcelStatuses.SelectedItem != null && ReceiveId.SelectedItem != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.Status == status && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }
            //    else if (SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && From.SelectedDate != null)
            //    {



            //        return parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }
            //    else if (ReceiveId.SelectedItem != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }
            //    else if (SenderId.SelectedItem != null && From.SelectedDate != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;


            //        return parcel.SendCustomer == SenderId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null && From.SelectedDate != null)
            //    {



            //        return parcel.ReceiveCustomer == ReceiveId.SelectedItem && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }

            //    else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
            //        return parcel.Status == status && parcel.ReceiveCustomer == ReceiveId.SelectedItem;
            //    }
            //    else if (ParcelStatuses.SelectedItem != null && ReceiveId.SelectedItem != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
            //        return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem;
            //    }
            //    else if (SenderId.SelectedItem != null && ReceiveId.SelectedItem != null)
            //    {

            //        return parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem;
            //    }
            //    else if (To.SelectedDate != null && From.SelectedDate != null)
            //    {


            //        return parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate && parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (ReceiveId.SelectedItem != null)
            //    {

            //        return parcel.ReceiveCustomer == ReceiveId.SelectedItem;

            //    }
            //    else if (SenderId.SelectedItem != null)
            //    {

            //        return parcel.SendCustomer == SenderId.SelectedItem;

            //    }
            //    else if (ParcelStatuses.SelectedItem != null)
            //    {
            //        Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;

            //        return parcel.Status == status;

            //    }
            //    else if (To.SelectedDate != null)
            //    {



            //        return parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested < To.SelectedDate;

            //    }
            //    else if (From.SelectedDate != null)
            //    {


            //        return parcelListViewModel.Bl.GetBLParcel(parcel.Id).Requested > From.SelectedDate;

            //    }
            //    else
            //    {
            //        return true;
            //    }
            }
            return false;
        }


    }
}
