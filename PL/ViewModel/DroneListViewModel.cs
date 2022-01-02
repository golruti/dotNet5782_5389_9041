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
            DronesForList = new ObservableCollection<DroneForList>(ConvertFunctions.BODroneForListToPO(bl.GetDroneForList()));
            ListCollectionView = new ListCollectionView(DronesForList);
        }
        public void RefreshDroneList()
        {
            DronesForList = new ObservableCollection<DroneForList>(ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList()));
            ListCollectionView = new ListCollectionView(DronesForList);
            DroneStatuses = Enum.GetValues(typeof(Enums.DroneStatuses));
            DroneStatuses = Enum.GetValues(typeof(Enums.DroneStatuses));
        }

        //DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
        //DroneStatuses.DataContext = Enum.GetValues(typeof(Enums.DroneStatuses));
        public Array DroneStatuses { get; set; }
        public Array DroneWeights { get; set; }

        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<object, RoutedEventArgs> RemoveTab { get; private set; }
        public ListCollectionView ListCollectionView { get; set; }
        private ObservableCollection<PO.DroneForList> dronesForList;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PO.DroneForList> DronesForList
        {
            get { return dronesForList; }
            set
            {
                dronesForList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DronesForList)));
            }
        }
    }
}
