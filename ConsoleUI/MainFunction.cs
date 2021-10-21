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

        public static Station GetStation()
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




        public static Drone GetDrone()
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

            Console.Write("Enter bettery");
            tempLBettery = double.Parse(Console.ReadLine());
            tempDrone.Battery = tempLBettery;

            Console.Write("Enter max weight, 1-light,2-heavy,3-medium");
            tempChoice = int.Parse(Console.ReadLine());
            if (tempChoice == 1)
            {
                tempMaxWeight = WeightCategories.Light;
            }
            else if (tempChoice == 2)
            {
                tempMaxWeight = WeightCategories.Medium;
            }
            else
            {
                tempMaxWeight = WeightCategories.Heavy;
            }
            tempDrone.MaxWeight = tempMaxWeight;


            Console.Write("Enter status, 1-available,2- maintenance,3-delivery");
            tempChoice = int.Parse(Console.ReadLine());
            if (tempChoice == 1)
            {
                tempStatus = DroneStatuses.Available;
            }
            else if (tempChoice == 2)
            {
                tempStatus = DroneStatuses.Delivery;
            }
            else
            {
                tempStatus = DroneStatuses.Maintenance;
            }
            return tempDrone;
        }




        public static Customer GetCustomer()
        {
            int tempID;
            string tempName, tempPhone;
            double tempLongitude, tempLattitude;
            Customer tempCustomer = new Customer();

            Console.Write("Enter id");
            tempID = int.Parse(Console.ReadLine());
            tempCustomer.Id = tempID;

            Console.Write("Enter name");
            tempName = Console.ReadLine();
            tempCustomer.Name = tempName;

            Console.Write("Enter phone");
            tempPhone = Console.ReadLine();
            tempCustomer.Phone = tempPhone;

            Console.Write("Enter longitude");
            tempLongitude = double.Parse(Console.ReadLine());
            tempCustomer.Longitude = tempLongitude;

            Console.Write("Enter lattitude");
            tempLattitude = double.Parse(Console.ReadLine());
            tempCustomer.Longitude = tempLattitude;

            return tempCustomer;
        }



        public static Parsel GetParsel()
        {
            int tempID, tempDroneId, tempSenderId, tempTargetId, tempChoice;
            WeightCategories tempWeight;
            Priorities tempPriority;
            Parsel tempParsel = new Parsel();

            Console.Write("Enter id");
            tempID = int.Parse(Console.ReadLine());
            tempParsel.Id = tempID;

            Console.Write("Enter sender id");
            tempSenderId = int.Parse(Console.ReadLine());
            tempParsel.SenderId = tempSenderId;

            Console.Write("Enter terget id");
            tempTargetId = int.Parse(Console.ReadLine());
            tempParsel.TargetId = tempTargetId;

            Console.Write("Enter drone id");
            tempDroneId = int.Parse(Console.ReadLine());
            tempParsel.Droneld = tempDroneId;


            Console.Write("Enter the weight, 1-light,2-heavy,3-medium");
            tempChoice = int.Parse(Console.ReadLine());
            if (tempChoice == 1)
            {
                tempWeight = WeightCategories.Light;
            }
            else if (tempChoice == 2)
            {
                tempWeight = WeightCategories.Medium;
            }
            else
            {
                tempWeight = WeightCategories.Heavy;
            }
            tempParsel.Weight = tempWeight;


            Console.Write("Enter prioruty, 1-regular,2-fast,3-emergency");
            tempChoice = int.Parse(Console.ReadLine());
            if (tempChoice == 1)
            {
                tempPriority = Priorities.Regular;
            }
            else if (tempChoice == 2)
            {
                tempPriority = Priorities.Fast;
            }
            else
            {
                tempPriority = Priorities.Fast;
            }
            tempParsel.Priority = tempPriority;

            tempParsel.Requested = DateTime.Now;
            return tempParsel;
        }




        public static int GetIdOfParcel()
        {
            Console.Write("Enter a package number");
            int droneId = int.Parse(Console.ReadLine());
            return droneId;

        }

    }
}
