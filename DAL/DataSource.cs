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
                stations[Config.indStation].Lattitude = rand.Next(181);
                stations[Config.indStation].Longitude = rand.Next(91);
                ++Config.indStation;
            }

            //customers
            for (int i = 0; i < 10; ++i)
            {
                customers[Config.indCustomer].Id = Config.indCustomer;
                customers[Config.indCustomer].Name = $"customer {Config.indCustomer}";

                ++Config.indCustomer;
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
    

