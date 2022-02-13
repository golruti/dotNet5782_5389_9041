using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class DroneInChargeListViewModel : INotifyPropertyChanged
    {

        private int stationId { get;  set; }
        private ListCollectionView dronesInChargeList;
        public ListCollectionView DronesInChargeList
        {
            get { return dronesInChargeList; }
            set
            {
                dronesInChargeList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DronesInChargeList)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        //public DroneInChargeListViewModel(int stationId)
        //{
        //    DronesInChargeList = new ListCollectionView(PO.ListsModel.dronesCharging);
        //    this.stationId = stationId;
        //}
    }
}
