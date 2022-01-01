using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class BaseStationViewModel
    {
        BlApi.IBL bl;
        private PO.BaseStation baseStation;
        Action refreshBaseStationList;
        Action<TabItem> addTab;
        Action<TabItem> closeTab;

        private string newName;
        private string newNum;

        ObservableCollection<DroneForList> droneList;
    }
}
