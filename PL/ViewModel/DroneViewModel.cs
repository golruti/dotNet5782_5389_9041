using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class DroneViewModel : INotifyPropertyChanged
    {
        public BlApi.IBL Bl { get; private set; }
        public Action RefreshDronesList { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public Random Rand { get; private set; }
        public Enums.ParcelStatuses ParcelStutus { get; private set; }
        public Array StationsId { get; set; }
        private PO.Drone droneInList;

        public PO.Drone SourceDroneInList
        {
            get { return droneInList; }
            set
            {
                droneInList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(droneInList)));
            }
        }
        public PO.Drone DroneInList
        {
            get { return droneInList; }
            set
            {
                droneInList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DroneInList)));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        //StationsId.DataContext = (bl.GetBaseStationForList()).Select(s => s.Id);

        public DroneViewModel(BlApi.IBL bl, Action refreshDronesList)
        {
            this.Bl = bl;
            this.RefreshDronesList = refreshDronesList;
            this.DroneInList = new PO.Drone();
            this.SourceDroneInList = new PO.Drone();
            this.Rand = new Random();
            this.ParcelStutus = (Enums.ParcelStatuses)Bl.GetParcelStatusByDrone(DroneInList.Id);
            StationsId = ((bl.GetBaseStationForList()).Select(s => s.Id)).ToArray();
        }

        public DroneViewModel(BO.DroneForList droneInList, BlApi.IBL bl, Action refreshDronesList)
        {
            this.Bl = bl;
            this.RefreshDronesList = refreshDronesList;
            this.DroneInList = ConvertFunctions.BODroneToPO(bl.GetBLDrone(droneInList.Id));
            this.SourceDroneInList = ConvertFunctions.BODroneToPO(bl.GetBLDrone(droneInList.Id));
            this.Rand = new Random();
        }

        public void RefreshDroneInList()
        {
            DroneInList = PO.ConvertFunctions.BODroneToPO(Bl.GetBLDrone(droneInList.Id));
        }
    }
}
