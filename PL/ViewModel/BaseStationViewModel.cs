using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class BaseStationViewModel
    {
        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public Action RefreshStationsList { get; private set; }
        public PO.BaseStation BaseStationInList { get; set; }
        public DroneListViewModel DronesList { get; set; }
        public IEnumerable<PO.DroneInCharging> DronesInCharging { get; set; }


        public BaseStationViewModel(BlApi.IBL bl, Action<TabItem> addTab, BO.BaseStationForList baseStationForList, Action refreshBaseStationList)
        {
            Bl = bl;
            AddTab = addTab;
            BaseStationInList = PO.ConvertFunctions.BOBaseStationToPO(Bl.GetBLBaseStation(baseStationForList.Id));
            RefreshStationsList = refreshBaseStationList;
            DronesList = new DroneListViewModel(bl, addTab);
            DronesInCharging = (IEnumerable<DroneInCharging>)new List<DroneInCharging>(PO.ConvertFunctions.BODroneInCharging(bl.DronesInCharging(BaseStationInList.Id)));
        }

        public BaseStationViewModel(BlApi.IBL bl, Action refreshBaseStationList)
        {
            Bl = bl;
            RefreshStationsList = refreshBaseStationList;
        }
    }
}
