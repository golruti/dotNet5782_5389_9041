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
        public DroneViewModel(BO.DroneForList droneInList)
        {
            this.DroneInList = ConvertFunctions.BODroneToPO(ListsModel.Bl.GetBLDrone(droneInList.Id));

            this.Rand = new Random();

            BO.Drone tempDrone = ListsModel.Bl.GetBLDrone(droneInList.Id);
            if (tempDrone.Status == BO.Enums.DroneStatuses.Delivery)
                this.parcelsByDrone = ConvertFunctions.BOParcelByTransferToPO(tempDrone.Delivery);
        }

        public DroneViewModel(BO.Drone droneInList)
        {
            this.DroneInList = ConvertFunctions.BODroneToPO(ListsModel.Bl.GetBLDrone(droneInList.Id));
            this.Rand = new Random();

            if (droneInList.Status == BO.Enums.DroneStatuses.Delivery)
                this.parcelsByDrone = ConvertFunctions.BOParcelByTransferToPO(droneInList.Delivery);
        }

        public DroneViewModel()
        {
            this.Rand = new Random();
            this.DroneInList = new PO.Drone();
            this.ParcelStutus = (Enums.ParcelStatuses)ListsModel.Bl.GetParcelStatusByDrone(DroneInList.Id);
            StationsId = ((ListsModel.Bl.GetBaseStationForList()).Select(s => s.Id));
            DroneWeights = (IEnumerable<Enums.WeightCategories>)Enum.GetValues(typeof(Enums.WeightCategories));
        }


        public Random Rand { get; private set; }
        public Enums.ParcelStatuses ParcelStutus { get; private set; }
        public IEnumerable<int> StationsId { get; set; }
        public IEnumerable<Enums.WeightCategories> DroneWeights { get; set; }
        private PO.Drone droneInList;
        public PO.ParcelByTransfer parcelsByDrone { get; set; }



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

        public void RefreshDroneInList()
        {
            var blDrones = ListsModel.Bl.GetDroneForList();
            if (blDrones.Count() != 0)
            {
                DroneInList = PO.ConvertFunctions.BODroneToPO(ListsModel.Bl.GetBLDrone(droneInList.Id));
                PO.ListsModel.RefreshDrones();
            }
        }
    }
}
