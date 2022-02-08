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
      

        //public void RefreshDronesinChargeList()
        //{
        //    DronesInChargeList = new ListCollectionView((System.Collections.IList)ConvertFunctions.BODroneInChargingTOPO(Bl.GetDronesInCharging(stationId)));
        //}


        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
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


        public DroneInChargeListViewModel(BlApi.IBL bl, Action<TabItem> addTab, int stationId)
        {
            this.Bl = bl;
            this.AddTab = addTab;
            DronesInChargeList = new ListCollectionView(PO.ListsModel.dronesCharging);
            this.stationId = stationId;
        }
    }
}
