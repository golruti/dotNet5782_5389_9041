﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static IDAL.DO.Enum;

namespace DalObject
{
    public class DataSource
    {
        public static Random rand = new Random();

        static internal Drone[] drones = new Drone[10];
        static internal Station[] stations = new Station[5];
        static internal Customer[] customers = new Customer[100];
        static internal Parsel[] parseles = new Parsel[1000];

        internal class Config
        {
            public static int indDrone = 0;
            public static int indStation = 0;
            public static int indCustomer = 0;
            public static int indParsel = 0;
            public static int ContinuousNumber;
        }

        public static void initialize()
        {

            //stations
            for (int i = 0; i < 2; ++i)
            {
                stations[Config.indStation].Id = Config.indStation;
                stations[Config.indStation].Name = $"station {Config.indStation}";
                stations[Config.indStation].ChargeSlote = rand.Next() + 1;
                stations[Config.indStation].Lattitude = rand.Next(181) + rand.NextDouble();
                stations[Config.indStation].Longitude = rand.Next(91) + rand.NextDouble();
                ++Config.indStation;
            }

            //customers
            for (int i = 0; i < 10; ++i)
            {
                customers[Config.indCustomer].Id = Config.indCustomer;
                customers[Config.indCustomer].Name = $"customer {Config.indCustomer}";
                customers[Config.indCustomer].Phone = $"05 {rand.Next(100000000)}|";
                customers[Config.indCustomer].Lattitude = rand.Next(181) + rand.NextDouble();
                customers[Config.indCustomer].Longitude = rand.Next(91) + rand.NextDouble();
                ++Config.indCustomer;
            }

            //drones
            for (int i = 0; i < 5; ++i)
            {
                drones[Config.indDrone].Id = Config.indDrone;
                drones[Config.indDrone].Model = $"drone {Config.indDrone}";
                drones[Config.indDrone].MaxWeight = (IDAL.DO.Enum.WeightCategories)rand.Next(0,3);
                drones[Config.indDrone].Status = (IDAL.DO.Enum.DroneStatuses)rand.Next(0, 3);
                drones[Config.indDrone].Battery =rand.Next(101);


                ++Config.indDrone;
            }

            //parcel
            for (int i = 0; i < 10; ++i)
            {
                parseles[Config.indParsel].Id = Config.indParsel;
                parseles[Config.indParsel].SenderId = rand.Next( Config.indCustomer);
                parseles[Config.indParsel].TargetId = rand.Next(Config.indCustomer);
                while(parseles[Config.indParsel].SenderId== parseles[Config.indParsel].TargetId)
                {
                    parseles[Config.indParsel].TargetId = rand.Next(Config.indCustomer);
                }
                parseles[Config.indParsel].Weight = (WeightCategories)(rand.Next(0,3));
                parseles[Config.indParsel].Priority = (Priorities)(rand.Next(0, 3));
                parseles[Config.indParsel].Id = rand.Next(Config.indDrone);
                parseles[Config.indParsel].Requested = DateTime.Now;
                parseles[Config.indParsel].Scheduled = DateTime.Now.AddDays(2);
                parseles[Config.indParsel].PickedUp = DateTime.Now.AddDays(30);
                parseles[Config.indParsel].Delivered = DateTime.Now.AddDays(32);
                ++Config.indParsel;
            }

        }
    }

}
    

