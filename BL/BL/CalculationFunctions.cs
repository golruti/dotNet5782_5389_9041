using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL
    {

        /// <summary>
        /// The function initialize the location of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="drone">specific drone</param>
        /// <returns>location of the drone </returns>
        private Location findDroneLocation(DroneForList drone)
        {
            if (isDroneMakesDelivery(drone.Id))
            {
                if (findParcelState(drone.Id) == parcelState.associatedNotCollected)
                {
                    DO.Customer senderCustomer2 = new DO.Customer();
                    try
                    {
                        lock (dal) { senderCustomer2 = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == dal.GetParcels().FirstOrDefault(parcel => parcel.IsDeleted == false && parcel.Droneld == drone.Id).SenderId); }

                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException("Get customer/parcel -BL-" + ex.Message, ex);
                    }

                    DO.BaseStation nearStation = nearestBaseStation(senderCustomer2.Longitude, senderCustomer2.Latitude);
                    return new Location() { Longitude = Math.Round(nearStation.Longitude), Latitude = Math.Round(nearStation.Latitude) };
                }
                else if (findParcelState(drone.Id) == parcelState.collectedNotDelivered)
                {
                    DO.Customer senderCustomer = new DO.Customer();
                    try
                    {
                        lock (dal) { senderCustomer = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == dal.GetParcels().FirstOrDefault(parcel => parcel.IsDeleted == false && parcel.Droneld == drone.Id).SenderId); }
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException("Get customer/parcel -BL-" + ex.Message, ex);
                    }
                    lock (dal) { senderCustomer = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == dal.GetParcels().FirstOrDefault(parcel => parcel.IsDeleted == false && parcel.Droneld == drone.Id).SenderId); }
                    return new Location() { Longitude = Math.Round(senderCustomer.Longitude), Latitude = Math.Round(senderCustomer.Latitude) };
                }
            }
            else
            {
                if (drone.Status == Enums.DroneStatuses.Maintenance)
                {
                    int randNumber1;
                    lock (dal) { randNumber1 = rand.Next(dal.GetBaseStations().Where(s => s.IsDeleted == false).Count()); }
                    DO.BaseStation randomBaseStation;
                    lock (dal) { randomBaseStation = dal.GetBaseStations().Where(s => s.IsDeleted == false).ToList()[randNumber1]; }
                    return new Location() { Longitude = Math.Round(randomBaseStation.Longitude), Latitude = Math.Round(randomBaseStation.Latitude) };
                }
            }
            List<DO.Customer> x;
            lock (dal)
            {
                x = dal.GetCustomers((customer) =>
                (dal.GetParcels(parcel => (parcel.Delivered != null) && (customer.Id == parcel.TargetId))).Where(c => c.IsDeleted == false).Any()).ToList();
            }
            int randNumber = rand.Next(x.Count());
            if (x.Count() == 0)
            {
                return new Location() { Latitude = 10, Longitude = 20 };
            }
            else
            {
                var randomCustomerProvided = x.ToList()[randNumber];
                return new Location() { Longitude = Math.Round(randomCustomerProvided.Longitude), Latitude = Math.Round(randomCustomerProvided.Latitude) };
            }
        }

        /// <summary>
        /// The function initialize the status of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>status of the drone</returns>
        private Enums.DroneStatuses findfDroneStatus(int droneId)
        {
            if (isDroneMakesDelivery(droneId))
                return BO.Enums.DroneStatuses.Delivery;

            if (isDroneMaintenance(droneId))
                return BO.Enums.DroneStatuses.Maintenance;

            return BO.Enums.DroneStatuses.Available;
        }

        /// <summary>
        ///  Boolean function that returns if the drone is charging
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        private bool isDroneMaintenance(int droneId)
        {
            IEnumerable<DO.DroneCharge> droneCharges;
            lock (dal) { droneCharges = dal.GetDronesCharges().Where(dc => dc.IsDeleted == false); }

            return !droneCharges.FirstOrDefault(dc => dc.DroneId == droneId).Equals(default(DO.DroneCharge));
        }


        /// <summary>
        /// Boolean function that returns if the drone makes a delivery
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>If the drone makes a delivery</returns>
        private bool isDroneMakesDelivery(int droneId)
        {
            IEnumerable<DO.Parcel> parcels;
            parcels = dal.GetParcels(p => p.IsDeleted == false);
            foreach (var parcel in parcels)
            {

                if (parcel.Droneld == droneId &&
                    parcel.Delivered == null)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// The function initialize the battery of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="drone"></param>
        /// <returns>battery of the drone</returns>
        private double findDroneBattery(DroneForList drone)
        {
            if (isDroneMakesDelivery(drone.Id))
            {
                double minDroneTarget;
                double minTargetBaseStation;
                try
                {
                    lock (dal)
                    {
                        minDroneTarget = minBattery(drone.Location,
                            new Location()
                            {
                                Longitude = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude,
                                Latitude = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude
                            },
                             drone.Status, drone.MaxWeight);
                    }
                }
                catch (KeyNotFoundException ex)
                {
                    throw new KeyNotFoundException("Drone battery -BL-" + ex.Message, ex);
                }

                try
                {
                    lock (dal)
                    {
                        minTargetBaseStation = minBattery(
                         new Location()
                         {
                             Longitude = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude,
                             Latitude = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Latitude
                         },
                        new Location()
                        {
                            Latitude = nearestBaseStation(dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude).Latitude,

                            Longitude = nearestBaseStation(dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude).Longitude
                        },
                        Enums.DroneStatuses.Available, drone.MaxWeight);
                    }
                }
                catch (ArgumentNullException)
                {
                    minTargetBaseStation = 100;
                }

                return rand.Next((int)(minDroneTarget + minTargetBaseStation), 100);
            }
            else if (drone.Status == Enums.DroneStatuses.Maintenance)
            {
                return rand.Next(0, 20);
            }

            double minDroneBaseStation;
            try
            {
                lock (dal)
                {
                    minDroneBaseStation = minBattery(
                        new Location()
                        {
                            Longitude = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude,
                            Latitude = dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude
                        },
                       new Location()
                       {
                           Latitude = nearestBaseStation(dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude).Latitude,
                           Longitude = nearestBaseStation(dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomers().FirstOrDefault(customer => customer.IsDeleted == false && customer.Id == drone.ParcelDeliveredId).Longitude).Longitude
                       },
                       Enums.DroneStatuses.Available, drone.MaxWeight);
                }
            }
            catch (ArgumentNullException )
            {
                minDroneBaseStation = 100;
            }
            return rand.Next((int)(minDroneBaseStation), 101);
        }

        /// <summary>
        /// The function receives the location of a drone and returns the nearest base station to it
        /// </summary>
        /// <param name="LongitudeSenderCustomer">Location of the drone</param>
        /// <param name="LatitudeSenderCustomer">Location of the drone</param>
        /// <returns> The nearest base station</returns>
        internal DO.BaseStation nearestBaseStation(double LongitudeSenderCustomer, double LatitudeSenderCustomer)
        {
            var minDistance = double.MaxValue;
            DO.BaseStation nearestBaseStation = default(DO.BaseStation);
            IEnumerable<DO.BaseStation> avaStations;
            avaStations = dal.GetAvaBaseStations().Where(s => s.IsDeleted == false);

            foreach (var BaseStation in avaStations)
            {
                if (distance(LatitudeSenderCustomer, BaseStation.Latitude, LongitudeSenderCustomer, BaseStation.Longitude) < minDistance)
                {
                    minDistance = distance(LatitudeSenderCustomer, BaseStation.Latitude, LongitudeSenderCustomer, BaseStation.Longitude);
                    nearestBaseStation = BaseStation;
                }
            }
            if (nearestBaseStation.Equals(default(DO.BaseStation)))
                throw new ArgumentNullException("Get nearst base station available -BL-");

            return nearestBaseStation;
        }

        /// <summary>
        /// calculate the battary of the drone after charge
        /// </summary>
        /// <param name="time">how much time the drone was in charge</param>
        /// <returns>sum of battary after charge</returns>
        private double batteryCalculationInCharging(int time)
        {
            lock (dal) { return time * dal.GetElectricityUse()[4]; }
        }
    }
}
