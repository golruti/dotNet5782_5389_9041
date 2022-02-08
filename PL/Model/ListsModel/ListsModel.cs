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
        static private BlApi.IBL bl;
        static public ObservableCollection<DroneForList> drones { get; set; }
        static public ObservableCollection<BaseStationForList> stations { get; set; }
        static public ObservableCollection<CustomerForList> customers { get; set; }
        static public ObservableCollection<ParcelForList> parcels { get; set; }
        static public ObservableCollection<DroneInCharging> dronesCharging { get; set; }



        static ListsModel()
        {
            bl = BlApi.BlFactory.GetBl();
            drones = ConvertFunctions.BODroneForListToPO(bl.GetDroneForList());
            stations = ConvertFunctions.BOBaseStationForListToPO(bl.GetAvaBaseStationForList());
            customers = ConvertFunctions.BOCustomerForListToPO(bl.GetCustomerForList());
            parcels = ConvertFunctions.BOParcelForListToPO(bl.GetParcelForList());
            dronesCharging = ConvertFunctions.BODroneInChargingTOPO(bl.GetDronesInCharging());
        }


        static public void RefreshDrones()
        {
            drones.Clear();
            foreach (var item in ConvertFunctions.BODroneForListToPO(bl.GetDroneForList()))
                drones.Add(item);
        }

        static public void RefreshStations()
        {
            stations.Clear();
            foreach (var item in ConvertFunctions.BOBaseStationForListToPO(bl.GetBaseStationForList()))
                stations.Add(item);
        }
        static public void RefreshCustomers()
        {
            customers.Clear();
            foreach (var item in ConvertFunctions.BOCustomerForListToPO(bl.GetCustomerForList()))
                customers.Add(item);

            var t = customers;
        }
        static public void RefreshParcels()
        {
            parcels.Clear();
            foreach (var item in ConvertFunctions.BOParcelForListToPO(bl.GetParcelForList()))
                parcels.Add(item);
        }

        static public void RefreshDronesCharging()
        {
            dronesCharging.Clear();
            foreach (var item in ConvertFunctions.BODroneInChargingTOPO(bl.GetDronesInCharging()))
                dronesCharging.Add(item);
        }
    }
}
