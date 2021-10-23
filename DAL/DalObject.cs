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
        /// A constructive function of a department that initializes skimmers, stations, customers and packages
        /// </summary>
        public DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// Add a base station to the list of stations
        /// </summary>
        /// <param name="station">struct of station</param>
        public void InsertStation(Station station)
        {
            DataSource.stations[DataSource.Config.IndStation++] = station;
        }

        /// <summary>
        /// Add a skimmer to the list of existing skimmers
        /// </summary>
        /// <param name="drone">struct of drone</param>
        public void InsertDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.IndDrone++] = drone;
        }

        /// <summary>
        /// Admission of a new customer to the customer list
        /// </summary>
        /// <param name="customer">struct of customer</param>
        public void InsertCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.IndCustomer++] = customer;
        }

        /// <summary>
        /// Receipt of package for shipment.
        /// </summary>
        /// <param name="parsel">struct of parsel</param>
        public void InsertParsel(Parsel parsel)
        {
            DataSource.parsels[DataSource.Config.IndParsel++] = parsel;
            DataSource.parsels[DataSource.Config.IndParsel].Id = DataSource.Config.IndParsel;
        }


        /// <summary>
        /// Assigning a package to a skimmer
        /// </summary>
        /// <param name="idxParsel">Id of the parsel</param>
        public void UpdateParseךScheduled(int idxParsel)
        {
            for (int i = 0; i < DataSource.Config.IndDrone; ++i)
            {
                if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
                {
                    DataSource.parsels[idxParsel].Scheduled = new DateTime();
                    DataSource.parsels[idxParsel].Droneld = DataSource.drones[i].Id;
                    DataSource.drones[i].Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
                    DataSource.drones[i].MaxWeight = DataSource.parsels[idxParsel].Weight;
                    break;
                }
            }
        }

        /// <summary>
        /// Skimmer package assembly
        /// </summary>
        /// <param name="idxParsel">Id of the parsel</param>
        public void UpdateParselPickedUp(int idxParsel)
        {
            DataSource.parsels[idxParsel].PickedUp = DateTime.Now;
            DataSource.drones[DataSource.parsels[idxParsel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Delivery;
        }


        /// <summary>
        /// Delivery of a package to the destination
        /// </summary>
        /// <param name="idxParsel">Id of the parsel</param>
        public void UpdateParselDelivered(int idxParsel)
        {
            DataSource.parsels[idxParsel].Delivered = DateTime.Now;
            DataSource.drones[DataSource.parsels[idxParsel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Available;
        }




        /// <summary>
        /// Sending a skimmer for charging at a base station By changing the skimmer mode and adding a record of a skimmer battery charging entity
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
        /// Release skimmer from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother skimmer released from charging</returns>
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
        /// Removes a skimmer from an array of skimmers by id
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
        /// Exits a package from an array of packages by id
        /// </summary>
        /// <param name="idxParsel">struct ofo parsel</param>
        /// <returns>parsel</returns>
        public Parsel GetParsel(int idxParsel)
        {
            var parsel = DataSource.parsels.FirstOrDefault(p => p.Id == idxParsel);
            return parsel;
        }


        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public Station[] GetStations()
        {
            Station[] stations = new Station[DataSource.Config.IndStation];
            for (int i = 0; i < DataSource.Config.IndStation; i++)
            {
                Station source = DataSource.stations[i];
                stations[i] = source.Clone();
            }
            return stations;
        }

        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of station</returns>
        public Customer[] GetCustomers()
        {
            Customer[] customers = new Customer[DataSource.Config.IndCustomer];
            for (int i = 0; i < DataSource.Config.IndCustomer; i++)
            {
                Customer source = DataSource.customers[i];
                customers[i] = source.Clone();
            }
            return customers;
        }

        /// <summary>
        /// The function prepares a new array of all existing skimmers
        /// </summary>
        /// <returns>array of drones</returns>
        public Drone[] GetDrones()
        {
            Drone[] drones = new Drone[DataSource.Config.IndDrone];
            for (int i = 0; i < DataSource.Config.IndDrone; i++)
            {
                Drone source = DataSource.drones[i];
                drones[i] = source.Clone();
            }
            return drones;
        }

        /// <summary>
        /// The function prepares a new array of all existing packages
        /// </summary>
        /// <returns>array of parseles</returns>
        public Parsel[] GetParsels()
        {
            Parsel[] parsels = new Parsel[DataSource.Config.IndParsel];
            for (int i = 0; i < DataSource.Config.IndParsel; i++)
            {
                Parsel source = DataSource.parsels[i];
                parsels[i] = source.Clone();
            }
            return parsels;
        }



        /// <summary>
        /// Displays a list of packages that have not yet been assigned to the glider
        /// </summary>
        /// <returns>array of parsels that have not yet been assigned to the glider</returns>
        public Parsel[] UnassignedPackages()
        {
            int amountOfParsel = 0, j = 0;
            for (int i = 0; i < DataSource.Config.IndParsel; i++)
            {
                if (DataSource.parsels[i].Droneld != 0)
                {
                    ++amountOfParsel;
                }
            }

            Parsel[] parsels = new Parsel[amountOfParsel];
            for (int i = 0; i < DataSource.Config.IndParsel; i++)
            {
                if (DataSource.parsels[i].Droneld != 0)
                {
                    Parsel source = DataSource.parsels[i];
                    parsels[j] = source.Clone();
                    ++j;
                }
            }
            return parsels;
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
