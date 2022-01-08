using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class ParcelListViewModel
    {
        
        ListCollectionView listCollectionView;
        //ObservableCollection<ParcelForList> parcelForLists;
        
        
        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public Action<object, RoutedEventArgs> RemoveTab { get; private set; }
        private ListCollectionView parcelsForList;
        public event PropertyChangedEventHandler PropertyChanged;
        public ParcelListViewModel(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            this.Bl = bl;
            this.AddTab = addTab;
            this.RemoveTab = removeTab;
            //new ObservableCollection<int>()
            parcelsForList = new ListCollectionView((System.Collections.IList)ConvertFunctions.BOParcelForListToPO(bl.GetParcelForList()));
        }
        public void RefreshParcelList()
        {
            parcelsForList = new ListCollectionView((System.Collections.IList)ConvertFunctions.BOParcelForListToPO(Bl.GetParcelForList()));
        }


       
        public ListCollectionView ParcelsForList
        {
            get { return parcelsForList; }
            set
            {
                parcelsForList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelsForList)));
            }
        }
    }
}
