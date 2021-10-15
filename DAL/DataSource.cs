using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

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
                customers[Config.indCustomer].Phone = $"05 {rand.Next()} {}|";

                ++Config.indCustomer;
            }
        }
    }

}
    

