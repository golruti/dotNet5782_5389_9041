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
            DataSource.stations.Add(station);
        }

        /// <summary>
        /// Add a drone to the array of existing drones
        /// </summary>
        /// <param name="drone">struct of drone</param>
        public void InsertDrone(Drone drone)
        {
            DataSource.drones.Add(drone);
        }

        /// <summary>
        /// Add a customer to the array of existing customers
        /// </summary>
        /// <param name="customer">struct of customer</param>
        public void InsertCustomer(Customer customer)
        {
            DataSource.customers.Add(customer);
        }

        /// <summary>
        /// Receipt of parcel for shipment.
        /// </summary>
        /// <param name="parcel">struct of parcel</param>
        public void InsertParcel(Parcel parcel)
        {
            DataSource.parcels.Add(parcel);
        }



        
        /// <summary>
        /// Package assembly by drone
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelPickedUp(int idParcel)
        {
            //אסיפת חבילה עי רחפן
            for (int i = 0; i < DataSource.parcels.Count(); ++i)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    //עדכון זמן איסוף
                    Parcel p = DataSource.parcels[i];
                    p.PickedUp = DateTime.Now;
                    DataSource.parcels[i] = p;

                    //עדכון מצב הרחפן למשלוח ..צריך להעביר לבל 
                    for (int j = 0; j < DataSource.drones.Count(); ++j)
                    {
                        if (DataSource.drones[j].Id == DataSource.parcels[i].Droneld)
                        {
                            Drone d = DataSource.drones[j];
                            d.Status = IDAL.DO.Enum.DroneStatuses.Delivery;
                            DataSource.drones[j] = d;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Delivery of a parcel to the destination
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        /// //אספקת חבילה ליעד
        public void UpdateParcelDelivered(int idParcel)
        {
            for (int i = 0; i < DataSource.parcels.Count; ++i)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    //עדכון זמן אספקת חבילה
                    Parcel p = DataSource.parcels[i];
                    p.Delivered = DateTime.Now;
                    DataSource.parcels[i] = p;

                    //עדכון מצב הרחפן לפנוי..צריך להעביר לבל
                    for (int j = 0; j < DataSource.drones.Count(); ++j)
                    {
                        if (DataSource.drones[j].Id == DataSource.parcels[i].Droneld)
                        {
                            Drone d = DataSource.drones[j];
                            d.Status = IDAL.DO.Enum.DroneStatuses.Available;
                            DataSource.drones[j] = d;
                            break;
                        }
                    }
                }
                break;
            }
        }



        /// <summary>
        /// Sending a drone for charging at a base station By changing the drone mode and adding a record of a drone battery charging entity
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns if the base station is available to receive the glider</returns>
        /// 
       //	שליחת רחפן לטעינה בתחנת-בסיס
        public bool TryAddDroneCarge(int droneId)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default))
                return false;

            //	יש לוודא שתחנת הבסיס פנויה לקבל את הרחפן לטעינה
            var station = DataSource.stations.FirstOrDefault(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id));
            if (station.Equals(default))
                return false;

            // הוספת רשומה של ישות טעינת סוללת רחפן
            DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
            DataSource.droneCharges.Add(droneCharge);
            // צריך להעביר לבל..שינוי מצב הרחפן
            drone.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
            return true;
        }

        /// <summary>
        /// Release drone from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother drone released from charging</returns>
        /// 
        //	צריך להיות בבל ..שחרור רחפן מטעינה בתחנת-בסיס
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
        public Station GetStation(int idStation)
        {
            var station = DataSource.stations.FirstOrDefault(s => s.Id == idStation);
            return station;
        }

        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int idDrone)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == idDrone);
            return drone;
        }

        /// <summary>
        /// Removes a customer from an array of customers by id
        /// </summary>
        /// <param name="idxCustomer">struct of customer</param>
        /// <returns>customer</returns>
        public Customer GetCustomer(int idCustomer)
        {
            var customer = DataSource.customers.FirstOrDefault(c => c.Id == idCustomer);
            return customer;
        }

        /// <summary>
        /// Exits a parcel from an array of parcels by id
        /// </summary>
        /// <param name="idxParcel">struct ofo parcel</param>
        /// <returns>parcel</returns>
        public Parcel GetParcel(int idParcel)
        {
            var parcel = DataSource.parcels.FirstOrDefault(p => p.Id == idParcel);
            return parcel;
        }


        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<Station> GetStations()
        {
            List<Station> tempStations = new();
            tempStations = DataSource.stations;
            return tempStations;
        }

        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            List<Customer> tempCustomers = new();
            tempCustomers = DataSource.customers;
            return tempCustomers;
        }

        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        public IEnumerable<Drone> GetDrones()
        {
            List<Drone> tempDrones = new();
            tempDrones = DataSource.drones;
            return tempDrones;
        }

        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            List<Parcel> tempParcels = new();
            tempParcels = DataSource.parcels;
            return tempParcels;
        }



        /// <summary>
        /// Displays a list of packages that have not yet been assigned to the glider
        /// </summary>
        /// <returns>array of parcels that have not yet been assigned to the glider</returns>
        public Parcel[] UnassignedPackages()
        {
            int amountOfParcel = 0, j = 0;
            for (int i = 0; i < DataSource.parcels.Count(); i++)
            {
                if (DataSource.parcels[i].Droneld != 0)
                {
                    ++amountOfParcel;
                }
            }

            Parcel[] parcels = new Parcel[amountOfParcel];
            for (int i = 0; i < DataSource.Config.IndParcel; i++)
            {
                if (DataSource.parcels[i].Droneld != 0)
                {
                    Parcel source = DataSource.parcels[i];
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
