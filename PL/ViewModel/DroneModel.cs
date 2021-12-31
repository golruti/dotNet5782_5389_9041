using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class DroneModel
    {
        private BlApi.IBL bl;
        private PO.Drone droneInList;
        private Random rand;
        private Action refreshDroneList;
        Action<TabItem> addTab;
        Action<TabItem> closeTab;
    }
}
