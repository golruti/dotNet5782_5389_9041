using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using static IDAL.DO.Enum;



namespace ConsoleUI
{
    public class MainFunction
    {

        public static Station getStation()
        {
            int tempID, tempChargeSlote;
            string tempName;
            double tempLongitude, tempLattitude;
            Station tempStation = new Station();
            Console.Write("Enter id");
            tempID = int.Parse(Console.ReadLine());
            tempStation.Id = tempID;

            Console.Write("Enter name");
            tempName = Console.ReadLine();
            tempStation.Name = tempName;

            Console.Write("Enter longitude");
            tempLongitude = double.Parse(Console.ReadLine());
            tempStation.Longitude = tempLongitude;

            Console.Write("Enter lattitude");
            tempLattitude = double.Parse(Console.ReadLine());
            tempStation.Lattitude = tempLattitude;


            Console.Write("Enter chargeSlote");
            tempChargeSlote = int.Parse(Console.ReadLine());
            tempStation.ChargeSlote = tempChargeSlote;

            return tempStation;
        }




        public static Drone getDrone()
        {
            int tempID, tempChoice;
            string tempModel;
            WeightCategories tempMaxWeight;
            DroneStatuses tempStatus;
            double tempLBettery;
            Drone tempDrone = new Drone();

            Console.Write("Enter id");
            tempID = int.Parse(Console.ReadLine());
            tempDrone.Id = tempID;

            Console.Write("Enter model");
            tempModel = Console.ReadLine();
            tempDrone.Model = tempModel;

            Console.Write("Enter max weight, 1-light,2-heavy,3-medium");
            tempChoice = int.Parse(Console.ReadLine());
            if (tempChoice == 1)
            {
                tempMaxWeight = WeightCategories.light;
            }
            else if (tempChoice == 2)
            {
                tempMaxWeight = WeightCategories.medium;
            }
            else 
            {
                tempMaxWeight = WeightCategories.heavy;
            }    
            tempDrone.MaxWeight = tempMaxWeight;


            Console.Write("Enter bettery");
            tempLBettery = double.Parse(Console.ReadLine());
            tempDrone.Battery = tempLBettery;



            Console.Write("Enter status, 1-available,2- maintenance,3-delivery");
            tempChoice = int.Parse(Console.ReadLine());
            if (tempChoice == 1)
            {
                tempStatus = DroneStatuses.available;
            }
            else if (tempChoice == 2)
            {
                tempStatus = DroneStatuses.delivery;
            }
            else
            {
                tempStatus = DroneStatuses.maintenance;
            }
            return tempDrone;
        }
}
}
