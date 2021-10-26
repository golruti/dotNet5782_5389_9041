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

        static internal List<Drone> drones = new List<Drone>();
        static internal List<Station> stations = new List<Station>();
        static internal List<Customer> customers = new List<Customer>();
        static internal List<parcel> parcels = new List<parcel>();
        static internal List<DroneCharge> droneCharges = new List<DroneCharge>();
        internal class Config
        {
        }

        /// <summary>
        /// The function initializes 2 base stations, 10 customers, 5 drones and 10 parcels
        /// </summary>
        public static void Initialize()
        {

            //stations
            for (int i = 0; i < 2; ++i)
            {
                Station station=new Station();
                Station.Id = Config.IndStation;
                Station.Name = $"station {Config.IndStation}";
                Station.ChargeSlote = Rand.Next() + 1;
                Station.Lattitude = Rand.Next(181) + Rand.NextDouble();
                Station.Longitude = Rand.Next(91) + Rand.NextDouble();
                stations.push(station);
                ++Config.IndStation;
            }

            //customers
            for (int i = 0; i < 10; ++i)
            {
                Customer customer=new Customer();
                customer.Id = Config.IndCustomer;
                customer.Name = $"customer {Config.IndCustomer}";
                customer.Phone = $"05{Rand.Next(10000000, 100000000)}";
                customer.Lattitude = Rand.Next(181) + Rand.NextDouble();
                customer.Longitude = Rand.Next(91) + Rand.NextDouble();
                ++Config.IndCustomer;
                customers.push(customer);
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


