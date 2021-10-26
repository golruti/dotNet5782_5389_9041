using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
        /// <summary>
        /// A constructive function of a department that initializes drones, stations, customers and packages
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// Add a base station to the array of stations
        /// </summary>
        /// <param name="station">struct of station</param>
        public void InsertStation(Station station)
        {
            DataSource.stations[DataSource.Config.IndStation++] = station;
        }

        /// <summary>
        /// Add a drone to the array of existing drones
        /// </summary>
        /// <param name="drone">struct of drone</param>
        public void InsertDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.IndDrone++] = drone;
        }

        /// <summary>
        /// Add a customer to the array of existing customers
        /// </summary>
        /// <param name="customer">struct of customer</param>
        public void InsertCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.IndCustomer++] = customer;
        }

        /// <summary>
        /// Receipt of parcel for shipment.
        /// </summary>
        /// <param name="parcel">struct of parcel</param>
        public void InsertParcel(parcel parcel)
        {
            DataSource.parcels[DataSource.Config.IndParcel++] = parcel;
            DataSource.parcels[DataSource.Config.IndParcel].Id = DataSource.Config.IndParcel;
        }


        /// <summary>
        /// Assigning a parcel to a drone
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelScheduled(int idxParcel)
        {
            for (int i = 0; i < DataSource.Config.IndDrone; ++i)
            {
                if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
                {
                    DataSource.parcels[idxParcel].Scheduled = new DateTime();
                    DataSource.parcels[idxParcel].Droneld = DataSource.drones[i].Id;
                    DataSource.drones[i].Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
                    DataSource.drones[i].MaxWeight = DataSource.parcels[idxParcel].Weight;
                    break;
                }
            }
        }

        /// <summary>
        /// Assigning a parcel to a drone
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelPickedUp(int idxParcel)
        {
            DataSource.parcels[idxParcel].PickedUp = DateTime.Now;
            DataSource.drones[DataSource.parcels[idxParcel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Delivery;
        }


        /// <summary>
        /// Delivery of a parcel to the destination
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelDelivered(int idxParcel)
        {
            DataSource.parcels[idxParcel].Delivered = DateTime.Now;
            DataSource.drones[DataSource.parcels[idxParcel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Available;
        }




        /// <summary>
        /// Sending a drone for charging at a base station By changing the drone mode and adding a record of a drone battery charging entity
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns if the base station is available to receive the glider</returns>
        public bool TryAddDroneCarge(int droneId)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default))
                return false;

            var station = DataSource.stations.FirstOrDefault(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id));
            if (station.Equals(default))
                return false;

            DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
            DataSource.droneCharges.Add(droneCharge);
            drone.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
            return true;
        }

        /// <summary>
        /// Release drone from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother drone released from charging</returns>
        public bool TryRemoveDroneCarge(int droneId)
        {
            if (!DataSource.droneCharges.Any(dc => dc.DroneId == droneId))
                return false;
            var droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId);

            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default))
                return false;

            DataSource.droneCharges.Remove(droneCharge);
            drone.Status = IDAL.DO.Enum.DroneStatuses.Available;
            return true;
        }


        /// <summary>
        /// Removes a station from an array of stations by id
        /// </summary>
        /// <param name="idxStation">struct of station</param>
        /// <returns>base station</returns>
        public Station GetStation(int idxStation)
        {
            var station = DataSource.stations.FirstOrDefault(s => s.Id == idxStation);
            return station;
        }

        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int idxDrone)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == idxDrone);
            return drone;
        }

        /// <summary>
        /// Removes a customer from an array of customers by id
        /// </summary>
        /// <param name="idxCustomer">struct of customer</param>
        /// <returns>customer</returns>
        public Customer GetCustomer(int idxCustomer)
        {
            var customer = DataSource.customers.FirstOrDefault(c => c.Id == idxCustomer);
            return customer;
        }

        /// <summary>
        /// Exits a parcel from an array of parcels by id
        /// </summary>
        /// <param name="idxParcel">struct ofo parcel</param>
        /// <returns>parcel</returns>
        public Parcel GetParcel(int idxParcel)
        {
            var parcel = DataSource.parcels.FirstOrDefault(p => p.Id == idxParcel);
            return parcel;
        }


        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public List<Station> GetStations()
        {
            List<Station> tempStations = new();
            tempStations = DataSource.stations;
            return tempStations;
        }

        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of station</returns>
        public List<Customer> GetCustomers()
        {
            List<Customer> tempCustomers = new();
            tempCustomers = DataSource.customers;
            return tempCustomers;
        }

        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        public List<Drone> GetDrones()
        {
            List<Drone> tempDrones = new();
            tempDrones = DataSource.drones;
            return tempDrones;
        }

        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public List<Parcel> GetParcels()
        {
            List<Parcel> tempParcels = new();
            tempParcels = DataSource.parcels;
            return tempParcels;
        }



        /// <summary>
        /// Displays a list of packages that have not yet been assigned to the glider
        /// </summary>
        /// <returns>array of parcels that have not yet been assigned to the glider</returns>
        public parcel[] UnassignedPackages()
        {
            int amountOfParcel = 0, j = 0;
            for (int i = 0; i < DataSource.Config.IndParcel; i++)
            {
                if (DataSource.parcels[i].Droneld != 0)
                {
                    ++amountOfParcel;
                }
            }

            parcel[] parcels = new parcel[amountOfParcel];
            for (int i = 0; i < DataSource.Config.IndParcel; i++)
            {
                if (DataSource.parcels[i].Droneld != 0)
                {
                    parcel source = DataSource.parcels[i];
                    parcels[j] = source.Clone();
                    ++j;
                }
            }
            return parcels;
        }


        /// <summary>
        /// Display base stations with available charging stations
        /// </summary>
        /// <returns>array of stations</returns>
        public Station[] GetAvaStations()
        {
            return DataSource.stations
                         .Where(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id))
                         .ToArray();
        }
    }
}
