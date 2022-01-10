﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using static DO.Enum;

namespace DAL
{
    static class DataSource
    {
        public static Random Rand = new Random();

        static internal Dictionary<int, Drone> drones = new();
        static internal Dictionary<int, BaseStation> stations = new();
        static internal Dictionary<int, Customer> customers = new();
        static internal Dictionary<int, Parcel> parcels = new();
        static internal Dictionary<int, DroneCharge> droneCharges = new();

        internal class Config
        {
            static public double free = 0.001;
            static public double CarriesLightWeigh = 0.002;
            static public double CarriesMediumWeigh = 0.005;
            static public double CarriesHeavyWeight = 0.006;
            static public double ChargingRate = 20;
            static public int Index = 0;
        }

        /// <summary>
        /// The function initializes 2 base stations, 10 customers, 5 drones and 10 parcels
        /// </summary>
        public static void Initialize()
        {
            //stations
            for (int i = 0; i < 2; ++i)
            {
                BaseStation tempStation = new BaseStation();
                tempStation.Id = i;
                tempStation.Name = $"station {i}";
                tempStation.ChargeSlote = Rand.Next(50);
                tempStation.Latitude = Rand.Next(-89, 89) + Rand.NextDouble();
                tempStation.Longitude = Rand.Next(-89, 89) + Rand.NextDouble();
                tempStation.IsDeleted = false;
                stations[tempStation.Id] = tempStation;

            }

            //customers
            for (int i = 0; i < 10; ++i)
            {
                Customer tempCustomer = new Customer();
                tempCustomer.Id = i;
                tempCustomer.Name = $"customer {i}";
                tempCustomer.Phone = $"05{Rand.Next(10000000, 100000000)}";
                tempCustomer.Latitude = Rand.Next(-89, 89) + Rand.NextDouble();
                tempCustomer.Longitude = Rand.Next(-89, 89) + Rand.NextDouble();
                tempCustomer.IsDeleted = false;
                customers[tempCustomer.Id] = tempCustomer;
            }

            //drones
            for (int i = 0; i < 5; ++i)
            {
                Drone tempDrone = new Drone();
                tempDrone.Id = i;
                tempDrone.Model = $"drone {i}";
                tempDrone.MaxWeight = (DO.Enum.WeightCategories)Rand.Next(0, 3);
                tempDrone.IsDeleted = false;
                drones[tempDrone.Id] = tempDrone;
            }
            
            
            //parcel
            for (int i = 0; i < 10; ++i)
            {
                Parcel tempParcel = new Parcel();

                tempParcel.Id = (Config.Index)++;
                tempParcel.SenderId = Rand.Next(customers.Count());
                if (i == 3)
                {
                    tempParcel.TargetId = Rand.Next(customers.Count());
                    while (tempParcel.SenderId == tempParcel.TargetId)
                    {
                        tempParcel.TargetId = Rand.Next(customers.Count());
                    }
                    tempParcel.Scheduled = DateTime.Now.AddDays(-10);
                    tempParcel.PickedUp = DateTime.Now.AddDays(-5);
                    tempParcel.Delivered = DateTime.Now.AddDays(-2);
                }

                tempParcel.Weight = (WeightCategories)(Rand.Next(0, 3));
                tempParcel.Priority = (Priorities)(Rand.Next(0, 3));
                tempParcel.Droneld = i;
                tempParcel.Requested = DateTime.Now;
                tempParcel.Droneld = -1;
                if (i == 0)
                {
                    tempParcel.Droneld = i;
                    tempParcel.Scheduled = DateTime.Now.AddDays(2);
                    tempParcel.PickedUp = DateTime.Now.AddDays(30);
                }
                if (i == 1)
                {
                    tempParcel.Droneld = i;
                    tempParcel.Scheduled = DateTime.Now.AddDays(2);
                }
                tempParcel.IsDeleted = false;
                parcels[tempParcel.Id] = tempParcel;
            }
        }
    }
}


