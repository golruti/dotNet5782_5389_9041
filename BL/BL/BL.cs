using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    partial class BL : IBL
    {
        private IDAL.IDal dal;
        private List<DroneForList> drones;
        private static Random rand = new Random();
        private enum parcelState { DroneNotAssociated, associatedNotCollected, collectedNotDelivered }

        /// <summary>
        /// constructor
        /// </summary>
        public BL()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneForList>();
            initializeDrones();
            double[] arr = dal.GetElectricityUse();
            double available = arr[0];
            double lightWeight = arr[1];
            double mediumWeight = arr[2];
            double heavyWeight = arr[3];
            double chargingRate = arr[4];
        }

        /// <summary>
        /// The function initializes the list of drones stored in BL
        /// </summary>
        private void initializeDrones()
        {
            foreach (var drone in dal.GetDrones())
            {
                drones.Add(new DroneForList
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (Enums.WeightCategories)drone.MaxWeight,
                });
            }

            foreach (var drone in drones)
            {
                drone.Status = findfDroneStatus(drone.Id);
            }

            foreach (var drone in drones)
            {
                drone.ParcelDeliveredId = findParceDeliveredlId(drone.Id);
            }

            foreach (var drone in drones)
            {
                drone.Location = findDroneLocation(drone);
            }

            foreach (var drone in drones)
            {
                drone.Battery = findDroneBattery(drone);
            }
        }

        /// <summary>
        /// The function calculates the minimum charge the glider needs to get 
        /// from the place of origin to the destination
        /// </summary>
        /// <param name="exit">location of exit</param>
        /// <param name="target">location of target</param>
        /// <param name="status"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private double minBattery(Location exit, Location target, Enums.DroneStatuses status, Enums.WeightCategories weight)
        {
            double distance = Distance(exit.Latitude, target.Latitude, exit.Longitude, target.Longitude) / 1000;
            if (status == Enums.DroneStatuses.Available)
            {
                return distance * (dal.GetElectricityUse()[0]);
            }
            else if (status == Enums.DroneStatuses.Delivery)
            {
                if (weight == Enums.WeightCategories.Light)
                {
                    return distance * (dal.GetElectricityUse()[1]);
                }
                else if (weight == Enums.WeightCategories.Medium)
                {
                    return distance * (dal.GetElectricityUse()[2]);
                }
                else if (weight == Enums.WeightCategories.Heavy)
                {
                    return distance * (dal.GetElectricityUse()[3]);
                }
            }
            throw new Exception("It is not possible to calculate the drone distance in maintenance");
        }

        public void AssignPackageToSkimmer(object d)
        {
            throw new NotImplementedException();
        }

        public void UpdateBaseStation(int stationlId, string name, int chargeSlote)
        {
            throw new NotImplementedException();
        }

        public void UpdateDrone(int droneId, string model)
        {
            throw new NotImplementedException();
        }

        public void GetStation(int stationId)
        {
            throw new NotImplementedException();
        }
    }
}


///// <summary>
///// Assigning a parcel to a drone
///// </summary>
///// <param name="idxParcel">Id of the parcel</param>
//public void UpdateParcelScheduled(int idxParcel)
//{
//    for (int i = 0; i < DataSource.drones.Count; ++i)
//    {
//        if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
//        {
//            Parcel p = DataSource.parcels[idxParcel];
//            p.Scheduled = new DateTime();
//            p.Droneld = DataSource.drones[i].Id;
//            DataSource.parcels[idxParcel] = p;

//            Drone d = DataSource.drones[i];
//            d.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;

//            d.MaxWeight = DataSource.parcels[idxParcel].Weight;

//            DataSource.drones[DataSource.drones.Count] = d;
//            break;
//        }
//    }
//}
