using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singleton;

namespace PO
{
    static public class ListsModel /*: Singleton.Singleton<ListsModel>*/
    {
        static public BlApi.IBL Bl;
        static public ObservableCollection<DroneForList> drones { get; set; }
        static public ObservableCollection<BaseStationForList> stations { get; set; }
        static public ObservableCollection<CustomerForList> customers { get; set; }
        static public ObservableCollection<ParcelForList> parcels { get; set; }
        //static public ObservableCollection<DroneInCharging> dronesCharging { get; set; }



        static ListsModel()
        {
            Bl = BlApi.BlFactory.GetBl();
            drones = ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList());
            stations = ConvertFunctions.BOBaseStationForListToPO(Bl.GetAvaBaseStationForList());
            customers = ConvertFunctions.BOCustomerForListToPO(Bl.GetCustomerForList());
            parcels = ConvertFunctions.BOParcelForListToPO(Bl.GetParcelForList());
            //dronesCharging = ConvertFunctions.BODroneInChargingTOPO(Bl.GetDronesInCharging());
        }


        static public void RefreshDrones()
        {
            drones.Clear();
            foreach (var item in ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList()))
                drones.Add(item);
        }

        static public void RefreshStations()
        {
            stations.Clear();
            foreach (var item in ConvertFunctions.BOBaseStationForListToPO(Bl.GetBaseStationForList()))
                stations.Add(item);
        }
        static public void RefreshCustomers()
        {
            customers.Clear();
            foreach (var item in ConvertFunctions.BOCustomerForListToPO(Bl.GetCustomerForList()))
                customers.Add(item);

           
        }
        static public void RefreshParcels()
        {
            parcels.Clear();
            foreach (var item in ConvertFunctions.BOParcelForListToPO(Bl.GetParcelForList()))
                parcels.Add(item);
        }

    //    static public void RefreshDronesCharging()
    //    {
    //        dronesCharging.Clear();
    //        foreach (var item in ConvertFunctions.BODroneInChargingTOPO(Bl.GetDronesInCharging()))
    //            dronesCharging.Add(item);
    //    }
    }
}
