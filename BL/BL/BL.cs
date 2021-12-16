using System;
using System.Collections.Generic;

using System.Linq;
using IBL.BO;
using Singleton;
using static IBL.BO.Enums;

namespace IBL
{
    sealed partial class BL : Singleton.Singleton<BL>, IBL
    {
        private IDAL.IDal dal;
        private List<DroneForList> drones =new List<DroneForList>();
        private static Random rand = new Random();
        private enum parcelState { DroneNotAssociated, associatedNotCollected, collectedNotDelivered }

        /// <summary>
        /// constructor
        /// </summary>
        private BL()
        {
            dal = Singleton<DalObject.DalObject>.Instance;
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
    }
}



