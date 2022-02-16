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
        public DroneListViewModel()
        {
            DronesList = new ListCollectionView(PO.ListsModel.drones);
            DroneWeights = Enum.GetValues(typeof(Enums.WeightCategories));
            DroneStatuses = Enum.GetValues(typeof(Enums.DroneStatuses));
        }


        public Array DroneStatuses { get; set; }
        public Array DroneWeights { get; set; }
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
