using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject: IDal.IDal
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





        /// Assigning a parcel to a drone
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        //public void UpdateParcelScheduled(int idxParcel)
        //{
        //    for (int i = 0; i < DataSource.drones.Count(); ++i)
        //    {
        //        if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
        //        {
        //            DataSource.parcels[idxParcel].Scheduled = new DateTime();
        //            DataSource.parcels[idxParcel].Droneld = DataSource.drones[i].Id;
        //            DataSource.drones[i].Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
        //            DataSource.drones[i].MaxWeight = DataSource.parcels[idxParcel].Weight;
        //            break;
        //        }
        //    }
        //}


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
                            //d.Status = IDAL.DO.Enum.DroneStatuses.Delivery;
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
                            //d.Status = IDAL.DO.Enum.DroneStatuses.Available;
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
            //drone.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
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
            //drone.Status = IDAL.DO.Enum.DroneStatuses.Available;
            return true;
        }


        /// <summary>
        /// Removes a station from an array of stations by id
        /// </summary>
        /// <param name="idxStation">struct of station</param>
        /// <returns>base station</returns>
        public Station GetStation(int idStation)
        {
            return DataSource.stations.First(station => station.Id == idStation);
        }

        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int idDrone)
        {
            return DataSource.drones.First(drone => drone.Id == idDrone);
        }

        /// <summary>
        /// Removes a customer from an array of customers by id
        /// </summary>
        /// <param name="idxCustomer">struct of customer</param>
        /// <returns>customer</returns>
        public Customer GetCustomer(int idCustomer)
        {
            return DataSource.customers.First(customer => customer.Id == idCustomer);
        }

        /// <summary>
        /// Exits a parcel from an array of parcels by id
        /// </summary>
        /// <param name="idxParcel">struct ofo parcel</param>
        /// <returns>parcel</returns>
        public Parcel GetParcel(int idParcel)
        {
            return DataSource.parcels.First(parcel => parcel.Id == idParcel);
        }


        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<Station> GetStations()
        {

            return DataSource.stations.Select(station =>station.Clone()).ToList();
        }

        /// <summary>
        /// The function prepares a new array of all existing customers
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return DataSource.customers.Select(customer => customer.Clone()).ToList();
        }

        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        public IEnumerable<Drone> GetDrones()
        {
            return DataSource.drones.Select(drone => drone.Clone()).ToList();
        }

        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            return DataSource.parcels.Select(parcel => parcel.Clone()).ToList();
        }



        //מפה אין שינויים!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        /// <summary>
        /// Displays a list of packages that have not yet been assigned to the glider
        /// </summary>
        /// <returns>array of parcels that have not yet been assigned to the glider</returns>
        /// 
        //	הצגת רשימת חבילות שעוד לא שויכו לרחפן
        public IEnumerable<Parcel> UnassignedPackages()
        {
            return new List<Parcel>(DataSource.parcels.Where(parcel => parcel.Droneld == 0).ToList());
        }


        /// <summary>
        /// Display base stations with available charging stations
        /// </summary>
        /// <returns>array of stations</returns>
        /// //●	הצגת  תחנות-בסיס עם עמדות טעינה פנויות
        /// // הפונקציה לא מתאימה לליסטים!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public IEnumerable<Station> GetAvaStations()
        {
            return DataSource.stations
                         .Where(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id))
                         .ToList();
        }




       public double[] DronePowerConsumptionRequest()
        {
            return (new double[5]{
                DataSource.Config.vacant,
                 DataSource.Config.CarriesLightWeigh,
                  DataSource.Config.CarriesMediumWeigh,
                   DataSource.Config.CarriesHeavyWeight,
                    DataSource.Config.ChargingRatel
                  });
        }
    }
}
