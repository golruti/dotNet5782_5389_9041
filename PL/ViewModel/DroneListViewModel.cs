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
        public DroneListViewModel(BlApi.IBL bl, Action<TabItem> addTab)
        {
            this.Bl = bl;
            this.AddTab = addTab;
            Items = new ObservableCollection<DroneForList>(ConvertFunctions.BODroneForListToPO(bl.GetDroneForList()));
            DronesList = new ListCollectionView(Items);
            DroneWeights = Enum.GetValues(typeof(Enums.WeightCategories));
            DroneStatuses = Enum.GetValues(typeof(Enums.DroneStatuses));
        }
        public void RefreshDroneList()
        {
            Items.Clear();
            foreach (var item in ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList()))
                Items.Add(item);
        }
        public ObservableCollection<DroneForList> Items { get; set; }
        public Array DroneStatuses { get; set; }
        public Array DroneWeights { get; set; }
        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        private ListCollectionView dronesList;
        public ListCollectionView DronesList
        {
            get { return dronesList; }
            set
            {
                dronesList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DronesList)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
