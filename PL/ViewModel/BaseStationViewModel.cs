using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class BaseStationViewModel
    {
        //public PO.BaseStation SourceBaseStationInList { get; set; }

        public PO.BaseStation BaseStationInList { get; set; }
       // public DroneInChargeListViewModel DronesInChargingList { get; set; }


        public BaseStationViewModel(BO.BaseStationForList baseStationForList)
        {

            //SourceBaseStationInList = PO.ConvertFunctions.BOBaseStationToPO(ListsModel.Bl.GetBLBaseStation(baseStationForList.Id));
            BaseStationInList = PO.ConvertFunctions.BOBaseStationToPO(ListsModel.Bl.GetBLBaseStation(baseStationForList.Id));
            //DronesInChargingList = new DroneInChargeListViewModel(SourceBaseStationInList.Id);
        }

        public BaseStationViewModel()
        {

        }
    }
}
