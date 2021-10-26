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
               


                stations.Add(new Station(){ Id = i, Name = $"station {i}", ChargeSlote = Rand.Next() + 1, Lattitude = Rand.Next(181) + Rand.NextDouble() , Longitude = Rand.Next(91) + Rand.NextDouble() });
                
            }

            //customers
            for (int i = 0; i < 10; ++i)
            {
          
                customers.Add(new Customer() { Id = i, Name = $"customer {i}", Phone = $"05{Rand.Next(10000000, 100000000)}" , Lattitude = Rand.Next(181) + Rand.NextDouble() , Longitude = Rand.Next(91) + Rand.NextDouble() });
            }

            //drones
            for (int i = 0; i < 5; ++i)
            {
              
            }

            //parcel
            for (int i = 0; i < 10; ++i)
            {
                Parcel tempParcel = new Parcel();

                tempParcel.Id = il;
                tempParcel.SenderId = Rand.Next(customers.Count());
                tempParcel.TargetId = Rand.Next(customers.Count());
                while (tempParcel.SenderId == tempParcel.TargetId)
                {
                    tempParcel.TargetId = Rand.Next(customers.Count());
                }
                tempParcel.Weight = (WeightCategories)(Rand.Next(0, 3));
                tempParcel.Priority = (Priorities)(Rand.Next(0, 3));
                tempParcel.Droneld = Config.IndParcel;
                tempParcel.Requested = DateTime.Now;
                tempParcel.Scheduled = DateTime.Now.AddDays(2);
                tempParcel.PickedUp = DateTime.Now.AddDays(30);
                tempParcel.Delivered = DateTime.Now.AddDays(32);

            }
        }
    }
}


