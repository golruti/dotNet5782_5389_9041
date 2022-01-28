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
        public ObservableCollection<ParcelForList> Items { get; set; }
        public ParcelListViewModel(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            this.Bl = bl;
            this.AddTab = addTab;
            this.RemoveTab = removeTab;
            parcelsForList = new ListCollectionView((System.Collections.IList)ConvertFunctions.BOParcelForListToPO(bl.GetParcelForList()));
            Items = new ObservableCollection<ParcelForList>(ConvertFunctions.BOParcelForListToPO(bl.GetParcelForList()));
            parcelsForList = new ListCollectionView(Items);
        }
        public void RefreshParcelList()
        {
            Items.Clear();
            foreach (var item in ConvertFunctions.BOParcelForListToPO(Bl.GetParcelForList()))
                Items.Add(item);
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
