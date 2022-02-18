using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class BaseStationViewModel
    {
        public BaseStationViewModel()
        {

        }
        public BaseStationViewModel(BO.BaseStationForList baseStationForList)
        {
            BaseStationInList = PO.ConvertFunctions.BOBaseStationToPO(ListsModel.Bl.GetBLBaseStation(baseStationForList.Id));
        }

      
        public PO.BaseStation BaseStationInList { get; set; }
    }
}
