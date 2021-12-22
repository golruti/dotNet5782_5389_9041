using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleUI
{
    public class MainFunction
    {
        ///// <summary>
        ///// The function asks the user to enter station details
        ///// </summary>
        ///// <returns>station</returns>
        //public static Station GetStation()
        //{
        //    Station tempStation = new Station();
        //    Console.Write("Enter id ");
        //    tempStation.Id = int.Parse(Console.ReadLine());

        //    Console.Write("Enter name ");
        //    tempStation.Name = Console.ReadLine();

        //    Console.Write("Enter longitude ");
        //    tempStation.Longitude = double.Parse(Console.ReadLine());

        //    Console.Write("Enter lattitude ");
        //    tempStation.Lattitude = double.Parse(Console.ReadLine());

        //    Console.Write("Enter chargeSlote ");
        //    tempStation.ChargeSlote = int.Parse(Console.ReadLine());
        //    Console.Write("##OK##\n\n");

        //    return tempStation;
        //}



        ///// <summary>
        ///// The function asks the user to enter drone details
        ///// </summary>
        ///// <returns>drone</returns>
        //public static Drone GetDrone()
        //{
        //    int tempChoice;
        //    Drone tempDrone = new Drone();

        //    Console.Write("Enter id ");
        //    tempDrone.Id = int.Parse(Console.ReadLine());

        //    Console.Write("Enter model ");
        //    tempDrone.Model = Console.ReadLine(); ;

        //    Console.Write("Enter bettery ");
        //    tempDrone.Battery = double.Parse(Console.ReadLine());

        //    Console.Write("Enter max weight: 1--light,2--heavy,3--medium  ");
        //    tempChoice = int.Parse(Console.ReadLine());
        //    if (tempChoice == 1)
        //    {
        //        tempDrone.MaxWeight = WeightCategories.Light;
        //    }
        //    else if (tempChoice == 2)
        //    {
        //        tempDrone.MaxWeight = WeightCategories.Medium;
        //    }
        //    else
        //    {
        //        tempDrone.MaxWeight = WeightCategories.Heavy;
        //    }

        //    Console.Write("Enter status: 1--available,2--maintenance,3--delivery  ");
        //    tempChoice = int.Parse(Console.ReadLine());
        //    if (tempChoice == 1)
        //    {
        //        tempDrone.Status = DroneStatuses.Available;
        //    }
        //    else if (tempChoice == 2)
        //    {
        //        tempDrone.Status = DroneStatuses.Delivery;
        //    }
        //    else
        //    {
        //        tempDrone.Status = DroneStatuses.Maintenance;
        //    }

        //    Console.Write("##OK##\n\n");
        //    return tempDrone;
        //}



        ///// <summary>
        ///// The function asks the user to enter customer details
        ///// </summary>
        ///// <returns>customer</returns>
        //public static Customer GetCustomer()
        //{
        //    Customer tempCustomer = new Customer();

        //    Console.Write("Enter id ");
        //    tempCustomer.Id = int.Parse(Console.ReadLine());

        //    Console.Write("Enter name ");
        //    tempCustomer.Name = Console.ReadLine();

        //    Console.Write("Enter phone ");
        //    tempCustomer.Phone = Console.ReadLine();

        //    Console.Write("Enter longitude ");
        //    tempCustomer.Longitude = double.Parse(Console.ReadLine());

        //    Console.Write("Enter lattitude ");
        //    tempCustomer.Longitude = double.Parse(Console.ReadLine());

        //    Console.Write("##OK##\n\n");
        //    return tempCustomer;
        //}


        ///// <summary>
        ///// The function asks the user to enter parcel details
        ///// </summary>
        ///// <returns>parcel</returns>
        //public static Parcel GetParcel()
        //{
        //    int tempChoice;
        //    Parcel tempParcel = new Parcel();

        //    Console.Write("Enter id ");
        //    tempParcel.Id = int.Parse(Console.ReadLine());

        //    Console.Write("Enter sender id ");
        //    tempParcel.SenderId = int.Parse(Console.ReadLine());

        //    Console.Write("Enter terget id ");
        //    tempParcel.TargetId = int.Parse(Console.ReadLine());

        //    Console.Write("Enter drone id ");
        //    tempParcel.Droneld = int.Parse(Console.ReadLine());

        //    Console.Write("Enter the weight: 1--light,2--heavy,3--medium  ");
        //    tempChoice = int.Parse(Console.ReadLine());
        //    if (tempChoice == 1)
        //    {
        //        tempParcel.Weight = WeightCategories.Light;
        //    }
        //    else if (tempChoice == 2)
        //    {
        //        tempParcel.Weight = WeightCategories.Medium;
        //    }
        //    else
        //    {
        //        tempParcel.Weight = WeightCategories.Heavy;
        //    }

        //    Console.Write("Enter prioruty: 1--regular,2--fast,3--emergency  ");
        //    tempChoice = int.Parse(Console.ReadLine());
        //    if (tempChoice == 1)
        //    {
        //        tempParcel.Priority = Priorities.Regular;
        //    }
        //    else if (tempChoice == 2)
        //    {
        //        tempParcel.Priority = Priorities.Fast;
        //    }
        //    else
        //    {
        //        tempParcel.Priority = Priorities.Fast;
        //    }

        //    tempParcel.Requested = DateTime.Now;

        //    Console.Write("##OK##\n\n");
        //    return tempParcel;
        //}


        ///// <summary>
        ///// ask from the user too press number for id
        ///// </summary>
        ///// <returns>id</returns>
        //public static int EnterId()
        //{
        //    Console.Write("Enter a number ");
        //    int Id = int.Parse(Console.ReadLine());
        //    return Id;
        //}

    }
}
