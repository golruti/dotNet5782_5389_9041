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
    public class DroneListViewModel : INotifyPropertyChanged
    {
        public DroneListViewModel(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            this.Bl = bl;
            this.AddTab = addTab;
            this.RemoveTab = removeTab;
            //DronesForList = new ObservableCollection<DroneForList>(ConvertFunctions.BODroneForListToPO(bl.GetDroneForList()));
            ListCollectionView = new ListCollectionView(new ObservableCollection<DroneForList>(ConvertFunctions.BODroneForListToPO(bl.GetDroneForList())));
            DroneWeights = Enum.GetValues(typeof(Enums.WeightCategories));
            DroneStatuses = Enum.GetValues(typeof(Enums.DroneStatuses));
        }
        public void RefreshDroneList()
        {
            //DronesForList = new ObservableCollection<DroneForList>(ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList()));
            ListCollectionView = new ListCollectionView(new ObservableCollection<DroneForList>(ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList())));          
        }

        public Array DroneStatuses { get; set; }
        public Array DroneWeights { get; set; }
        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<object, RoutedEventArgs> RemoveTab { get; private set; }
        private ListCollectionView listCollectionView;
        public ListCollectionView ListCollectionView
        {
            get { return listCollectionView; }
            set
            {
                listCollectionView = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListCollectionView)));
            }
        }

        //private ObservableCollection<PO.DroneForList> dronesForList;
        public event PropertyChangedEventHandler PropertyChanged;
        //public ObservableCollection<PO.DroneForList> DronesForList
        //{
        //    get { return dronesForList; }
        //    set
        //    {
        //        dronesForList = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DronesForList)));
        //    }
        //}
    }
}
