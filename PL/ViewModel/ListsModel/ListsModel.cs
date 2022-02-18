using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PO;
using Singleton;

namespace PL.ViewModel
{
    /// <summary>
    /// Static auxiliary department for refreshing the lists on display.
    /// </summary>
    static public class ListsModel
    {
        static public BlApi.IBL Bl;
        static public ObservableCollection<DroneForList> drones { get; set; }
        static public ObservableCollection<BaseStationForList> stations { get; set; }
        static public ObservableCollection<CustomerForList> customers { get; set; }
        static public ObservableCollection<ParcelForList> parcels { get; set; }



        static ListsModel()
        {
            Bl = BlApi.BlFactory.GetBl();
            drones = ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList());
            stations = ConvertFunctions.BOBaseStationForListToPO(Bl.GetAvaBaseStationForList());
            customers = ConvertFunctions.BOCustomerForListToPO(Bl.GetCustomerForList());
            parcels = ConvertFunctions.BOParcelForListToPO(Bl.GetParcelForList());
        }

        /// <summary>
        /// Refreshing the list of drones on display.
        /// </summary>
        static public void RefreshDrones()
        {
            drones.Clear();
            foreach (var item in ConvertFunctions.BODroneForListToPO(Bl.GetDroneForList()))
                drones.Add(item);
        }
        /// <summary>
        /// Refreshing the specipic drone on display for the simulator.
        /// </summary>
        /// <param name="droneId"></param>
        static public void RefreshDrone(int droneId)
        {
            var dronePO = drones.FirstOrDefault(d => d.Id == droneId);
            drones.Remove(dronePO);

            var droneNew = ConvertFunctions.BODorneForListToPO(Bl.GetDroneForList().FirstOrDefault(d => d.Id == droneId));
            drones.Add(droneNew);
        }

        /// <summary>
        /// Refreshing the list of stations on display.
        /// </summary>
        static public void RefreshStations()
        {
            stations.Clear();
            foreach (var item in ConvertFunctions.BOBaseStationForListToPO(Bl.GetBaseStationForList()))
                stations.Add(item);
        }

        /// <summary>
        /// Refreshing the list of customers on display.
        /// </summary>
        static public void RefreshCustomers()
        {
            customers.Clear();
            foreach (var item in ConvertFunctions.BOCustomerForListToPO(Bl.GetCustomerForList()))
                customers.Add(item);
        }

        /// <summary>
        /// Refreshing the list of parcels on display.
        /// </summary>
        static public void RefreshParcels()
        {
            parcels.Clear();
            foreach (var item in ConvertFunctions.BOParcelForListToPO(Bl.GetParcelForList()))
                parcels.Add(item);
        }
    }
}
