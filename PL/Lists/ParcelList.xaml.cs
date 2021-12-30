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


namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelList.xaml
    /// </summary>
    public partial class ParcelList : UserControl
    {
        BlApi.IBL bl;
        Action<TabItem> addTab;
        ListCollectionView listCollectionView;
        ObservableCollection<ParcelForList> parcelForLists;
        int[] day = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 };
        int[] month = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
        int[] year = { 2000, 2001, 2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011, 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025 };
        
        public ParcelList(BlApi.IBL bl, Action<TabItem> addTab)
        {
            InitializeComponent();
            this.bl = bl;
            this.addTab = addTab;
            parcelForLists = new ObservableCollection<ParcelForList>(bl.GetParcelForList());
            listCollectionView = new ListCollectionView(parcelForLists);
            listCollectionView.Filter += FilterParcel;
            ParcelesListView.DataContext = listCollectionView;
            ParcelStatuses.DataContext = Enum.GetValues(typeof(Enums.ParcelStatuses));
            DayF.DataContext= day;
            DayT.DataContext = day;
            MonthF.DataContext = month;
            MonthT.DataContext = month;
            YearF.DataContext = year;
            YearT.DataContext = year;
        }

        private void RefreshFilter()
        {
            listCollectionView.Filter -= FilterParcel;
            listCollectionView.Filter += FilterParcel;
        }

        private bool FilterParcel(object obj)
        {
            if (obj is ParcelForList parcel)
            {
                if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && YearT.SelectedItem != null && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }
                else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && YearT.SelectedItem != null && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }

                else if (ParcelStatuses.SelectedItem != null && ReceiveId.SelectedItem != null && YearT.SelectedItem != null && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return parcel.Status == status && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }
                else if (SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && YearT.SelectedItem != null && YearF.SelectedItem != null)
                {

                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }
                else if (ReceiveId.SelectedItem != null && YearT.SelectedItem != null && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }
                else if (SenderId.SelectedItem != null && YearT.SelectedItem != null && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return parcel.SendCustomer == SenderId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }
                else if (ParcelStatuses.SelectedItem != null && YearT.SelectedItem != null && YearF.SelectedItem != null)
                {

                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }
                else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && YearT.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));

                    return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && YearT.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));

                    return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }

                else if (ParcelStatuses.SelectedItem != null && ReceiveId.SelectedItem != null && YearT.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));

                    return parcel.Status == status && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if (SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && YearT.SelectedItem != null)
                {

                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));

                    return parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if (ReceiveId.SelectedItem != null && YearT.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));

                    return parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if (SenderId.SelectedItem != null && YearT.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));

                    return parcel.SendCustomer == SenderId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if (ParcelStatuses.SelectedItem != null && YearT.SelectedItem != null)
                {

                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));

                    return parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && YearF.SelectedItem!=null )
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    
                     return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested>dateTime;
                    
                }
                else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null  && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));

                    return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem  && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }

                else if (ParcelStatuses.SelectedItem != null  && ReceiveId.SelectedItem != null && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));

                    return parcel.Status == status &&  parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if ( SenderId.SelectedItem != null && ReceiveId.SelectedItem != null && YearF.SelectedItem != null)
                {
                   
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));

                    return  parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if ( ReceiveId.SelectedItem != null && YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));

                    return parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if ( SenderId.SelectedItem != null &&  YearF.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));

                    return  parcel.SendCustomer == SenderId.SelectedItem  && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }
                else if (ParcelStatuses.SelectedItem != null && YearF.SelectedItem != null)
                {
                    
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));

                    return parcel.ReceiveCustomer == ReceiveId.SelectedItem && bl.GetBLParcel(parcel.Id).Requested > dateTime;

                }

                else if (ParcelStatuses.SelectedItem != null && SenderId.SelectedItem != null && ReceiveId.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    return parcel.Status == status  && parcel.ReceiveCustomer == ReceiveId.SelectedItem;
                }
                else if (ParcelStatuses.SelectedItem != null  && ReceiveId.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                    return parcel.Status == status && parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem;
                }
                else if ( SenderId.SelectedItem != null && ReceiveId.SelectedItem != null)
                {
                    
                    return  parcel.SendCustomer == SenderId.SelectedItem && parcel.ReceiveCustomer == ReceiveId.SelectedItem;
                }
                else if ( YearT.SelectedItem != null && YearF.SelectedItem != null)
                {

                    DateTime dateTimeT = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    DateTime dateTimeF = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return  bl.GetBLParcel(parcel.Id).Requested > dateTimeF && bl.GetBLParcel(parcel.Id).Requested < dateTimeT;

                }
                else if ( ReceiveId.SelectedItem != null )
                {
                    
                    return parcel.ReceiveCustomer == ReceiveId.SelectedItem ;

                }
                else if ( SenderId.SelectedItem != null)
                {
                    
                    return parcel.SendCustomer == SenderId.SelectedItem ;

                }
                else if (ParcelStatuses.SelectedItem != null)
                {
                    Enums.ParcelStatuses status = (Enums.ParcelStatuses)ParcelStatuses.SelectedItem;
                   
                    return parcel.Status == status ;

                }
                else if ( YearT.SelectedItem != null )
                {
                   
                    DateTime dateTime = new DateTime(int.Parse(YearT.Text), int.Parse(MonthT.Text), int.Parse(DayT.Text));
                    
                    return   bl.GetBLParcel(parcel.Id).Requested < dateTime;

                }
                else if ( YearF.SelectedItem != null)
                {
                   
                    DateTime dateTime = new DateTime(int.Parse(YearF.Text), int.Parse(MonthF.Text), int.Parse(DayF.Text));
                    return  bl.GetBLParcel(parcel.Id).Requested > dateTime ;

                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Updates the skimmer list
        /// </summary>
        private void RefreshParcelList()
        {
            
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

        
        /// <summary>
        /// Opens a window for adding a skimmer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddParcelWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Parcel(bl, RefreshParcelList);
            tabItem.Header = "Add parcel";
            this.addTab(tabItem);
        }

        /// <summary>
        /// window winder that allows you to update the details of the glider that you double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParcelListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedParcel = ParcelesListView.SelectedItem as BO.ParcelForList;
            TabItem tabItem = new TabItem();
            tabItem.Content = new Parcel(selectedParcel, this.bl, RefreshParcelList);
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
