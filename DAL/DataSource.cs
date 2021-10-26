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
                Station station = new Station();
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
                Customer customer = new Customer();
                customer.Id = Config.IndCustomer;
                customer.Name = $"customer {Config.IndCustomer}";
                customer.Phone = $"05{Rand.Next(10000000, 100000000)}";
                customer.Lattitude = Rand.Next(181) + Rand.NextDouble();
                customer.Longitude = Rand.Next(91) + Rand.NextDouble();
                ++Config.IndCustomer;
                customers.Add(new Customer() { Id = 5, Name = "jj" });
            }

            //drones
            for (int i = 0; i < 5; ++i)
            {
                drones.Add(new Drone()
                {
                    Id = i,
                    Model = $"drone {i}",
                    MaxWeight = (IDAL.DO.Enum.WeightCategories)Rand.Next(0, 3),
                    Status = (IDAL.DO.Enum.DroneStatuses)Rand.Next(0, (int)IDAL.DO.Enum.DroneStatuses.Delivery),
                    Battery = Rand.Next(101)
                });
            }

            //parcel
            for (int i = 0; i < 10; ++i)
            {


                parcels.Add(new parcel()
                {
                    Id = i,
                    SenderId = Rand.Next(customers.Count),
                    TargetId = Rand.Next(customers.Count),
                   Weight = (WeightCategories)(Rand.Next(0, 3)),
                   Priority = (Priorities)(Rand.Next(0, 3)),
                   Droneld = Config.IndParcel,
                   Requested = DateTime.Now,
                   Scheduled = DateTime.Now.AddDays(2),
                   PickedUp = DateTime.Now.AddDays(30),
                   Delivered = DateTime.Now.AddDays(32)
                });

                 while (parcels[i].SenderId == parcels[i].TargetId)
                 {
                    parcels[i].TargetId = Rand.Next(customers.Count);
                 }
            }
        }
    }

}


