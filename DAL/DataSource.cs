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

        static internal List<Drone> drones = new();
        static internal List<Station> stations = new();
        static internal List<Customer> customers = new();
        static internal List<Parcel> parcels = new();
        static internal List<DroneCharge> droneCharges = new();
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
                Station tempStation = new Station();
                tempStation.Id = i;
                tempStation.Name = $"station {i}";
                tempStation.ChargeSlote = Rand.Next() + 1;
                tempStation.Lattitude = Rand.Next(181) + Rand.NextDouble();
                tempStation.Longitude = Rand.Next(91) + Rand.NextDouble();
                stations.Add(tempStation);
               
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
                customers.Add(new Customer() { Id =5, Name = "jj" });
            }

            //drones
            for (int i = 0; i < 5; ++i)
            {
                Drone tempDrone = new Drone();
                tempDrone.Id = i;
                tempDrone.Model = $"drone {i}";
                tempDrone.MaxWeight = (IDAL.DO.Enum.WeightCategories)Rand.Next(0, 3);
                tempDrone.Status = (IDAL.DO.Enum.DroneStatuses)Rand.Next(0, (int)IDAL.DO.Enum.DroneStatuses.Delivery);
                tempDrone.Battery = Rand.Next(101);
                drones.Add(tempDrone);

           
            }

            //parcel
            for (int i = 0; i < 10; ++i)
            {
                Parcel tempParcel = new Parcel();

                tempParcel.Id = i;
                tempParcel.SenderId = Rand.Next(customers.Count());
                tempParcel.TargetId = Rand.Next(customers.Count());
                while (tempParcel.SenderId == tempParcel.TargetId)
                {
                    tempParcel.TargetId = Rand.Next(customers.Count());
                }
                tempParcel.Weight = (WeightCategories)(Rand.Next(0, 3));
                tempParcel.Priority = (Priorities)(Rand.Next(0, 3));
                tempParcel.Droneld = i;
                tempParcel.Requested = DateTime.Now;
                tempParcel.Scheduled = DateTime.Now.AddDays(2);
                tempParcel.PickedUp = DateTime.Now.AddDays(30);
                tempParcel.Delivered = DateTime.Now.AddDays(32);

            }
        }
    }

}


