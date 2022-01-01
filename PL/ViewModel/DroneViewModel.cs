using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class DroneViewModel
    {
        public BlApi.IBL Bl { get; private set; }
        public Action RefreshDronesList { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public Random Rand { get; private set; }
        public PO.Drone DroneInList { get; set; }


        public DroneViewModel(BlApi.IBL bl, Action refreshDronesList)
        {
            this.Bl = bl;
            this.RefreshDronesList = refreshDronesList;
            this.DroneInList = new PO.Drone();
            this.Rand = new Random();
        }
        public DroneViewModel(BO.DroneForList droneInList, BlApi.IBL bl, Action refreshDronesList)
        {
            this.Bl = bl;
            this.RefreshDronesList = refreshDronesList;
            this.DroneInList = ConvertFunctions.BODroneToPO(bl.GetBLDrone(droneInList.Id));
            this.Rand = new Random();
        }

    }
}
