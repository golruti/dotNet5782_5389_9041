using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    class DataSource
    {
        static internal Drone[] drones = new Drone[10];
        static internal Station[] stations = new Station[5];
        static internal Customer[] customers = new Customer[100];
        static internal Parsel[] parseles = new Parsel[1000];

        internal class Config
        {
            public static int indDrone = 0;
            public static int indStation = 0;
            public int indCustomer = 0;
            public int indParsel = 0;
            public int ContinuousNumber;
        }

        public static void initialize()
        {


        }
    }
}
