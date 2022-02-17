using PO;
using System;
using System.Collections;
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
        public const string NONE_VALUE = "None";
        public DroneListViewModel()
        {
            DronesList = new ListCollectionView(PO.ListsModel.drones);
            DroneWeights = Enum.GetValues(typeof(Enums.WeightCategories)).OfType<object>().Union(new List<object>() { NONE_VALUE });
            DroneStatuses = Enum.GetValues(typeof(Enums.DroneStatuses)).OfType<object>().Union(new List<object>() { NONE_VALUE });
        }


        public IEnumerable DroneStatuses { get; set; }
        public IEnumerable DroneWeights { get; set; }
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
