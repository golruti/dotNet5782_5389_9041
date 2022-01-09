﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BO;
using Singleton;
using static BO.Enums;


namespace BL
{
    sealed partial class BL : Singleton.Singleton<BL>, BlApi.IBL
    {
        private static DalApi.IDal dal;
        static private Dictionary<int, DroneForList> drones = new();
        private static Random rand = new Random();
        private enum parcelState { DroneNotAssociated, associatedNotCollected, collectedNotDelivered }

        /// <summary>
        /// constructor
        /// </summary>
        private BL()
        {
            dal = DalApi.DalFactory.GetDal();
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
                drones.Add(drone.Id, new DroneForList
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (Enums.WeightCategories)drone.MaxWeight,
                });
            }

            foreach (var drone in drones)
            {
                drone.Value.Status = findfDroneStatus(drone.Value.Id);
                if (drone.Value.Status == DroneStatuses.Maintenance)
                {
                    //dal.AddDroneCharge(new DO.DroneCharge(drone.Value.Id, rand.Next(1)));

                }

            }

            foreach (var drone in drones)
            {
                drone.Value.ParcelDeliveredId = findParceDeliveredlId(drone.Value.Id);
            }

            foreach (var drone in drones)
            {
                drone.Value.Location = findDroneLocation(drone.Value);
            }

            foreach (var drone in drones)
            {
                drone.Value.Battery = findDroneBattery(drone.Value);
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
            double distance = this.distance(exit.Latitude, target.Latitude, exit.Longitude, target.Longitude) / 1000;
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
            throw new KeyNotFoundException("It is not possible to calculate the drone distance in maintenance");
        }
    }
}


