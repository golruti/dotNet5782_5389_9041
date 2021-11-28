﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    partial class BL
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a drone to the list of drones
        /// </summary>
        /// <param name="tempDrone">The customer for Adding</param>
        public void AddDrone(Drone tempDrone)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDrone.Id, tempDrone.Model, (IDAL.DO.Enum.WeightCategories)tempDrone.MaxWeight);
            try
            {
                dal.InsertDrone(drone);
                DroneForList droneForList = new DroneForList(tempDrone.Id, tempDrone.Model, tempDrone.MaxWeight, tempDrone.Battery, tempDrone.Status, tempDrone.Location.Longitude, tempDrone.Location.Latitude);
                drones.Add(droneForList);
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        public void AddDroneForList(Drone drone)
        {
            DroneForList droneForList= new DroneForList(drone.Id, drone.Model, drone.MaxWeight, drone.Battery, drone.Status, drone.Location.Longitude, drone.Location.Latitude);
            drones.Add(droneForList);
        }


        //---------------------------------------------Show item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// etrieves the requested drone from the data and converts it to BL drone
        /// </summary>
        /// <param name="id">The requested drone</param>
        /// <returns>A Bl drone to print</returns>
        public Drone GetBLDrone(int id)
        {
            try
            {
                return MapDrone(id);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Get base station -BL-" + ex.Message);
            }

        }

        /// <summary>
        /// Convert a DAL drone to BL drone
        /// </summary>
        /// <param name="id">The drone to convert</param>
        /// <returns>The converted drone</returns>
        private Drone MapDrone(int id)
        {
            DroneForList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new ArgumentNullException("Map drone -BL-:There is not drone with same id i data");
            return new Drone()
            {
                Id = droneToList.Id,
                Model = droneToList.Model,
                MaxWeight = droneToList.MaxWeight,
                Status = droneToList.Status,
                Battery = droneToList.Battery,
                Location = droneToList.Location,
                Delivery = !droneToList.ParcelDeliveredId.Equals(default) ? CreateParcelInTransfer((int)droneToList.ParcelDeliveredId) : default
            };
        }

        //--------------------------------------------Show list---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Retrieves the list of drones from BL
        /// </summary>
        /// <returns>A list of drones to print</returns
        public IEnumerable<DroneForList> GetDroneForList()
        {
            return drones;
        }



        //--------------------------------------------עדכון------------------------------------------------------------------------------------------
        //עידכון מודל
        public void UpdateDroneModel(int id, string model)
        {
            DroneForList tempDroneForList = drones.Find(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Model = model;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDroneForList.Id, tempDroneForList.Model, (IDAL.DO.Enum.WeightCategories)tempDroneForList.MaxWeight);
            dal.InsertDrone(drone);
        }

        public void UpdateDroneStatus(int id, DroneStatuses status, double battery, double longitude, double latitude)
        {
            DroneForList tempDroneForList = drones.Find(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Status = status;
            tempDroneForList.Battery = battery;
            tempDroneForList.Location.Longitude = longitude;
            tempDroneForList.Location.Latitude = latitude;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
        }





        //---------------------שליחת רחפן לטעינה
        public void SendDroneToRecharge(int droneId)
        {
            DroneForList tempDroneForList = getDroneForList(droneId);
            Drone tempDrone = new Drone(tempDroneForList.Id, tempDroneForList.Model, tempDroneForList.MaxWeight, tempDroneForList.Status, tempDroneForList.Battery, tempDroneForList.Location.Longitude, tempDroneForList.Location.Latitude);
            int baseStationId = 0;
            double distance = double.MaxValue;
            if ((int)tempDrone.Status == 0)
            {

                foreach (var item in dal.GetBaseStations())
                {
                    double tempDistance = Distance(tempDrone.Location.Latitude, item.Latitude, tempDrone.Location.Longitude, item.Longitude);
                    if (tempDistance < distance/* && SeveralAvailablechargingStations(item.Id)>0*/)
                    {
                        baseStationId = item.Id;
                        distance = tempDistance;
                    }
                    else
                    {

                    }
                }
                if (BatteryCalculationOnTraveling(distance) < tempDrone.Battery)
                {
                    UpdateDroneStatus(droneId, DroneStatuses.Maintenance, tempDrone.Battery - BatteryCalculationOnTraveling(distance), GetBLBaseStation(baseStationId).Location.Latitude, GetBLBaseStation(baseStationId).Location.Latitude);
                    dal.AddDroneCarge(droneId, baseStationId);
                }
                else
                {

                }
            }
            else
            {

            }
        }

        //צריך לסדר....!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        double BatteryCalculationOnTraveling(double distance)
        {
            return distance;
        }
        //
        double BatteryCalculationInCharging(int time)
        {
            double battery = time * 0.05;
            if (battery < 100)
            {
                return battery;
            }
            return 100;
        }

        //שחרור רחפן מטעינה
        public void ReleaseDroneFromRecharge(int droneId, int time)
        {
            DroneInCharging droneInCharging = new DroneInCharging();// DroneInCharging(droneId, BatteryCalculationInCharging(time));
            DroneForList drone = drones.Find(item => item.Id == droneId);
            if (drone != null)
            {
                if (drone.Status == (DroneStatuses)2)
                {
                    drones.Remove(drone);
                    droneInCharging.Battery = drone.Battery + BatteryCalculationInCharging(time);
                    drone.Battery = droneInCharging.Battery;
                    drone.Status = (DroneStatuses)0;
                    drones.Add(drone);
                    dal.UpdateRelease(droneId);
                }
                else
                {

                }
            }
            else
            {

            }
        }

        //○	שיוך חבילה לרחפן
        public void AssignPackageToSkimmer(int id)
        {
            Drone drone = new Drone();
            foreach (DroneForList item in drones)
            {
                if (item.Id == id)
                {
                    drone = new Drone(id, item.Model, item.MaxWeight, item.Status, item.Battery, item.Location.Longitude, item.Location.Latitude);
                }
            }
            DroneForList droneForList = drones.Find(item => item.Id == id);
            if (drone != null && drone.Status == DroneStatuses.Available)
            {

                double dronrMaxDistance = drone.Battery - 20;
                List<ParcelForList> parcels = new List<ParcelForList>();
                foreach (IDAL.DO.Parcel item in dal.GetParcels())
                {
                    //do!!!!!!!!!!!!!!
                    double distance = 3;
                    if (enough(distance,drone.Battery, (int)drone.MaxWeight) && (WeightCategories)item.Weight <= drone.MaxWeight)
                    {
                        ParcelForList parcel = new ParcelForList();
                        parcel.Id = parcel.Id;
                        parcel.Weight= (Enums.WeightCategories)item.Weight;
                        parcel.Priority = (Enums.Priorities)item.Priority;
                        parcel.SendCustomer = getSendCustomerName(item);
                        parcel.ReceiveCustomer= getReceiveCustomer(item);
                        parcel.Status= getStatusCustomer(item);
                        parcels.Add(parcel);
                    }
                }
                if (optionalParcels.Count == 0)
                {
                    throw new UnextantException("parcel");
                }
                int chosenId = findParcel(optionalParcels, drone);
                IDAL.DO.Parcel p = new IDAL.DO.Parcel { Id = chosenId, DroneId = drone.Id, Scheduled = DateTime.Now };
                dal.UpdateScheduled(p);
                int index = 0;
                foreach (DroneForList item in Drones)
                {
                    if (item.Id == drone.Id)
                    {
                        break;
                    }
                    ++index;
                }
                Drones[index].DeliveryId = chosenId;
                Drones[index].Status = DroneStatuses.TRANSPORT;
            }
            else
            {

            }
        }

        private bool enough(double distance, double battery, int weight)
        {
            double sumBattery = 0;
            try
            {
                sumBattery = minBattery(distance, weight);
            }
            catch
            {
                
            }
            return sumBattery <= battery;
        }

        private double minBattery(double distance, int weight)
        {
            double sumBattery;
            switch (weight)
            {
                case 0:
                    sumBattery = available * distance;
                    break;
                case 1:
                    sumBattery = lightWeight * distance;
                    break;
                case 2:
                    sumBattery = mediumWeight * distance;
                    break;
                case 3:
                    sumBattery = heavyWeight * distance;
                    break;
                default:
                    throw new Exception();

            }
            return sumBattery;
        }

        //--------------------------------------------Initialize the drone list--------------------------------------------

        /// <summary>
        /// The function initialize the location of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="drone">specific drone</param>
        /// <returns>location of the drone </returns>
        private Location findDroneLocation(DroneForList drone)
        {
            if (IsDroneMakesDelivery(drone.Id))
            {
                if (findParcelState(drone.Id) == parcelState.associatedNotCollected)
                {
                    IDAL.DO.Customer senderCustomer2 = new IDAL.DO.Customer();
                    try
                    {
                        senderCustomer2 = dal.FindSenderCustomerByDroneId(drone.Id);
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new ArgumentNullException("Get base station -BL-" + ex.Message);
                    }

                    IDAL.DO.BaseStation soonStation = nearestBaseStation(senderCustomer2.Longitude, senderCustomer2.Latitude);
                    return new Location(soonStation.Longitude, soonStation.Latitude);
                }
                else if (findParcelState(drone.Id) == parcelState.collectedNotDelivered)
                {
                    IDAL.DO.Customer senderCustomer = new IDAL.DO.Customer();
                    try
                    {
                        senderCustomer = dal.FindSenderCustomerByDroneId(drone.Id);
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new ArgumentNullException("Get base station -BL-" + ex.Message);
                    }
                    senderCustomer = dal.FindSenderCustomerByDroneId(drone.Id);
                    return new Location(senderCustomer.Longitude, senderCustomer.Latitude);
                }
            }
            else
            {
                if (drone.Status == Enums.DroneStatuses.Maintenance)
                {
                    int randNumber1 = rand.Next(dal.GetBaseStations().Count());
                    var randomBaseStation = (dal.GetBaseStations().ToList())[randNumber1];
                    return new Location(randomBaseStation.Longitude, randomBaseStation.Latitude);
                }
            }
            int randNumber = rand.Next(dal.GetCustomersProvided().Count());
            var randomCustomerProvided = (dal.GetCustomersProvided().ToList())[randNumber];
            return new Location(randomCustomerProvided.Longitude, randomCustomerProvided.Latitude);
        }

        /// <summary>
        /// The function initialize the status of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>status of the drone</returns>
        private Enums.DroneStatuses findfDroneStatus(int droneId)
        {
            if (IsDroneMakesDelivery(droneId))
            {
                return BO.Enums.DroneStatuses.Delivery;
            }

            return (Enums.DroneStatuses)rand.Next(System.Enum.GetNames(typeof(Enums.DroneStatuses)).Length);
        }

        /// <summary>
        /// Boolean function that returns if the drone makes a delivery
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>If the drone makes a delivery</returns>
        private bool IsDroneMakesDelivery(int droneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                if (parcel.Droneld == droneId &&
                    !(parcel.Requested.Equals(default(DateTime))) && parcel.Delivered.Equals(default(DateTime)))
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
            if (IsDroneMakesDelivery(drone.Id))
            {
                double minDroneTarget;
                double minTargetBaseStation;
                try
                {
                    minDroneTarget = minBattery(drone.Location,
                        new Location(dal.customerByDrone(drone.ParcelDeliveredId).Longitude, dal.customerByDrone(drone.ParcelDeliveredId).Longitude),
                         drone.Status, drone.MaxWeight);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException("Drone battery -BL-" + ex.Message);
                }

                try
                {
                    minTargetBaseStation = minBattery(
                     new Location(dal.customerByDrone(drone.ParcelDeliveredId).Longitude, dal.customerByDrone(drone.ParcelDeliveredId).Longitude),
                    new Location(nearestBaseStation(dal.customerByDrone(drone.ParcelDeliveredId).Latitude, dal.customerByDrone(drone.ParcelDeliveredId).Longitude).Latitude,
                    nearestBaseStation(dal.customerByDrone(drone.ParcelDeliveredId).Latitude, dal.customerByDrone(drone.ParcelDeliveredId).Longitude).Longitude),
                    Enums.DroneStatuses.Available, drone.MaxWeight);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException("Drone battery -BL-" + ex.Message);
                }

                return rand.Next((int)(minDroneTarget + minTargetBaseStation), 101);
            }
            else if (drone.Status == Enums.DroneStatuses.Maintenance)
            {
                return rand.Next(0, 20);
            }

            double minDroneBaseStation;
            try
            {
                minDroneBaseStation = minBattery(
                    new Location(dal.customerByDrone(drone.ParcelDeliveredId).Longitude, dal.customerByDrone(drone.ParcelDeliveredId).Longitude),
                   new Location(nearestBaseStation(dal.customerByDrone(drone.ParcelDeliveredId).Latitude, dal.customerByDrone(drone.ParcelDeliveredId).Longitude).Latitude,
                   nearestBaseStation(dal.customerByDrone(drone.ParcelDeliveredId).Latitude, dal.customerByDrone(drone.ParcelDeliveredId).Longitude).Longitude),
                   Enums.DroneStatuses.Available, drone.MaxWeight);
                return rand.Next((int)(minDroneBaseStation), 101);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Drone battery -BL-" + ex.Message);
            }
        }

        /// <summary>
        /// The function receives the location of a drone and returns the nearest base station to it
        /// </summary>
        /// <param name="LongitudeSenderCustomer">Location of the drone</param>
        /// <param name="LatitudeSenderCustomer">Location of the drone</param>
        /// <returns> The nearest base station</returns>
        private IDAL.DO.BaseStation nearestBaseStation(double LongitudeSenderCustomer, double LatitudeSenderCustomer)
        {
            var minDistance = double.MaxValue;
            var nearestBaseStation = default(IDAL.DO.BaseStation);
            foreach (var BaseStation in dal.GetBaseStations())
            {
                if (Distance(LatitudeSenderCustomer, BaseStation.Latitude, LongitudeSenderCustomer, BaseStation.Longitude) < minDistance)
                {
                    minDistance = Distance(LatitudeSenderCustomer, BaseStation.Latitude, LongitudeSenderCustomer, BaseStation.Longitude);
                    nearestBaseStation = BaseStation;
                }
            }
            if (nearestBaseStation.Equals(default(IDAL.DO.BaseStation)))
            {
                throw new ArgumentNullException("Get nearst base station -BL-");
            }
            return nearestBaseStation.Clone();
        }    
    }
}
