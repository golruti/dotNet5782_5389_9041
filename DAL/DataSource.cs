using System;
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
        public static Random Rand = new Random();

        static internal Drone[] drones = new Drone[10];
        static internal Station[] stations = new Station[5];
        static internal Customer[] customers = new Customer[100];
        static internal parcel[] parcels = new parcel[1000];
        static internal List<DroneCharge> droneCharges = new List<DroneCharge>();
        internal class Config
        {
            public static int IndDrone = 0;
            public static int IndStation = 0;
            public static int IndCustomer = 0;
            public static int IndParcel = 0;
        }

        /// <summary>
        /// The function initializes 2 base stations, 10 customers, 5 drones and 10 parcels
        /// </summary>
        public static void Initialize()
        {

            //stations
            for (int i = 0; i < 2; ++i)
            {
                stations[Config.IndStation].Id = Config.IndStation;
                stations[Config.IndStation].Name = $"station {Config.IndStation}";
                stations[Config.IndStation].ChargeSlote = Rand.Next() + 1;
                stations[Config.IndStation].Lattitude = Rand.Next(181) + Rand.NextDouble();
                stations[Config.IndStation].Longitude = Rand.Next(91) + Rand.NextDouble();
                ++Config.IndStation;
            }

            //customers
            for (int i = 0; i < 10; ++i)
            {
                customers[Config.IndCustomer].Id = Config.IndCustomer;
                customers[Config.IndCustomer].Name = $"customer {Config.IndCustomer}";
                customers[Config.IndCustomer].Phone = $"05{Rand.Next(10000000, 100000000)}";
                customers[Config.IndCustomer].Lattitude = Rand.Next(181) + Rand.NextDouble();
                customers[Config.IndCustomer].Longitude = Rand.Next(91) + Rand.NextDouble();
                ++Config.IndCustomer;
            }

            //drones
            for (int i = 0; i < 5; ++i)
            {
                drones[Config.IndDrone].Id = Config.IndDrone;
                drones[Config.IndDrone].Model = $"drone {Config.IndDrone}";
                drones[Config.IndDrone].MaxWeight = (IDAL.DO.Enum.WeightCategories)Rand.Next(0, 3);
                drones[Config.IndDrone].Status = (IDAL.DO.Enum.DroneStatuses)Rand.Next(0, (int)IDAL.DO.Enum.DroneStatuses.Delivery);
                drones[Config.IndDrone].Battery = Rand.Next(101);
                ++Config.IndDrone;
            }

            //parcel
            for (int i = 0; i < 10; ++i)
            {
                parcels[Config.IndParcel].Id = Config.IndParcel;
                parcels[Config.IndParcel].SenderId = Rand.Next(Config.IndCustomer);
                parcels[Config.IndParcel].TargetId = Rand.Next(Config.IndCustomer);
                while (parcels[Config.IndParcel].SenderId == parcels[Config.IndParcel].TargetId)
                {
                    parcels[Config.IndParcel].TargetId = Rand.Next(Config.IndCustomer);
                }
                parcels[Config.IndParcel].Weight = (WeightCategories)(Rand.Next(0, 3));
                parcels[Config.IndParcel].Priority = (Priorities)(Rand.Next(0, 3));
                parcels[Config.IndParcel].Droneld = Config.IndParcel;
                parcels[Config.IndParcel].Requested = DateTime.Now;
                parcels[Config.IndParcel].Scheduled = DateTime.Now.AddDays(2);
                parcels[Config.IndParcel].PickedUp = DateTime.Now.AddDays(30);
                parcels[Config.IndParcel].Delivered = DateTime.Now.AddDays(32);
                ++Config.IndParcel;
            }
        }
    }
}


