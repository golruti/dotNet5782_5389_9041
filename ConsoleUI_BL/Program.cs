using System;
using System.Collections.Generic;
using IBL;
using IBL.BO;
using static IBL.BO.Enums;

namespace ConsoleUI_BL
{
    class Program
    {
        private static IBL.IBL bl;
        private static Random rand = new Random();

        static void Main(string[] args)
        {
            bl = new IBL.BL();
            ShowMenu();
        }

        enum MenuOptions { EXIT, ADD, UPDATE, SHOWONE, SHOWLIST }
        enum EntityOptions { EXIT, BASESTATION, DRONE, CUSTOMER, PARCEL }
        enum UpdateOptions { EXIT,DRONE, BASESTATION,CUSTOMER, RECHARGE, REKEASE, SCHEDULED }
        enum ListOptions { Exit, BaseStations, Drones, Customers, Parcels, UnAssignmentParcels, AvailableChargingStations }
        private static void ShowMenu()
        {
            MenuOptions menuOption;
            do
            {
                Console.WriteLine("Welcome!");
                Console.WriteLine("option:\n 1-Add,\n 2-Update,\n 3-Show item,\n 4-Show list,\n 0-Exit");
                menuOption = (MenuOptions)int.Parse(Console.ReadLine());
                switch (menuOption)
                {
                    case MenuOptions.ADD:
                        MenuAddOptions();
                        break;
                    case MenuOptions.UPDATE:
                        MenuUpdateOptions();
                        break;
                    case MenuOptions.SHOWONE:
                        MenuShowOneOptions();
                        break;
                    case MenuOptions.SHOWLIST:
                        MenuShowListOptions();
                        break;
                    case MenuOptions.EXIT:
                        break;
                }
            } while (menuOption != MenuOptions.EXIT);
        }

        private static void MenuShowListOptions()
        {
            Console.WriteLine(
                "List option:\n 1-Base Stations,\n 2-Drones,\n 3-Customers,\n 4-Parcels,\n 5-UnAssignment Parcels,\n 6-Available Charging Stations,\n 0-Exit");
            ListOptions listOptions = (ListOptions)int.Parse(Console.ReadLine());
            switch (listOptions)
            {
                case ListOptions.BaseStations:
                    foreach (var item in bl.GetBaseStationForList())
                    {
                        Console.WriteLine(item);
                    }
                    break;
                case ListOptions.Drones:
                    foreach (var item in bl.GetDroneForList())
                    {
                        Console.WriteLine(item);
                    }

                    break;
                case ListOptions.Customers:
                    foreach (var item in bl.GetCustomerForList())
                    {
                        Console.WriteLine(item);
                    }

                    break;
                case ListOptions.Parcels:
                    foreach (var item in bl.GetParcelForList())
                    {
                        Console.WriteLine(item);
                    }

                    break;
                case ListOptions.UnAssignmentParcels:
                    foreach (var item in bl.UnassignedParcelsForList())
                    {
                        Console.WriteLine(item);
                    }

                    break;
                case ListOptions.AvailableChargingStations:
                    foreach (var item in bl.GetAvaBaseStationForList())
                    {
                        Console.WriteLine(item);
                    }

                    break;
                case ListOptions.Exit:
                    break;
            }
        }

        private static void MenuShowOneOptions()
        {
            EntityOptions entityOption;
            Console.WriteLine("View item option:\n 1-Base Station,\n 2-Drone,\n 3-Customer,\n 4-Parcel\n, 0-Exit");
            entityOption = (EntityOptions)int.Parse(Console.ReadLine());
            Console.WriteLine($"Enter a requested {entityOption} id");
            int requestedId;
            int.TryParse(Console.ReadLine(), out requestedId);
            switch (entityOption)
            {
                case EntityOptions.BASESTATION:
                    BaseStation baseStation = bl.GetBLBaseStation(requestedId);
                    Console.WriteLine(baseStation);
                    ShowList(baseStation.DronesInCharging);
                    break;
                case EntityOptions.DRONE:
                    Console.WriteLine(bl.GetBLDrone(requestedId));
                    break;
                case EntityOptions.CUSTOMER:
                    Customer customer = bl.GetCustomer(requestedId);
                    Console.WriteLine(customer);
                    break;
                case EntityOptions.PARCEL:
                    Console.WriteLine(bl.GetBLParcel(requestedId));
                    break;
                case EntityOptions.EXIT:
                    break;
            }
        }

        private static void MenuUpdateOptions()
        {
            Console.WriteLine("Update option:\n 1-model of drone,\n 2-Pickedup,\n 3-Delivery,\n 4-Recharge,\n 5-Release,\n 0-Exit");
            UpdateOptions updateOptions;
            updateOptions = (UpdateOptions)int.Parse(Console.ReadLine());
            int parcelId, droneId, stationlId, customerId;
            string name;
            switch (updateOptions)
            {
                case UpdateOptions.DRONE:
                    Console.WriteLine("Enter IDs for drone and model:");
                    droneId = int.Parse(Console.ReadLine());
                    string model = (Console.ReadLine());
                    bl.UpdateDrone(droneId, model);
                    break;

                case UpdateOptions.BASESTATION:
                    Console.WriteLine("Enter number of station and name and/or sum of Charging positions and sum with loaded skimmers:");
                    stationlId = int.Parse(Console.ReadLine());
                    name = (Console.ReadLine());
                    int chargeSlote = int.Parse(Console.ReadLine());
                    bl.UpdateBaseStation(stationlId, name, chargeSlote);
                    break;

                case UpdateOptions.CUSTOMER:
                    Console.WriteLine("Enter ID and name and/or phone:");
                    customerId = int.Parse(Console.ReadLine());
                    name = (Console.ReadLine());
                    string phone = (Console.ReadLine());
                    bool correctName = true;
                    do
                    {
                        Console.WriteLine("Enter phone");
                        phone = Console.ReadLine();
                        if (!(phone[0] == '+' || phone[0] == '*' || char.IsDigit(phone[0])))
                            foreach (char item in phone.Substring(1))
                                if (!char.IsDigit(item))
                                    correctName = false;
                    } while (!correctName);
                    bl.UpdateCustomer(customerId, name, phone);
                    break;

                case UpdateOptions.RECHARGE:
                    Console.WriteLine("Enter IDs for drone:");
                    droneId = int.Parse(Console.ReadLine());
                    bl.SendDroneToRecharge(droneId);
                    break;

                case UpdateOptions.REKEASE:
                    Console.WriteLine("Enter ID for drone and the time in charging:");
                    droneId = int.Parse(Console.ReadLine());
                    int time = int.Parse(Console.ReadLine());
                    bl.ReleaseDroneFromRecharge(droneId,time);

                    break;
                case UpdateOptions.SCHEDULED:
                    Console.WriteLine("nter ID for drone");
                    droneId = int.Parse(Console.ReadLine());
                    bl.UpdateScheduled(d);
                    break;
                case UpdateOptions.EXIT:
                    break;
            }
        }

        private static void MenuAddOptions()
        {
            EntityOptions entityOption;
            Console.WriteLine("Adding option:\n 1-Base Station,\n 2-Drone,\n 3-Customer,\n 4-Parcel");
            entityOption = (EntityOptions)int.Parse(Console.ReadLine());
            int id;
            double longitude, latitude;
            string name;
            switch (entityOption)
            {
                case EntityOptions.BASESTATION:
                    Console.WriteLine("Enter Id and Location and Number of charging positions add Name :");
                    int chargingStations;

                    if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out longitude) && double.TryParse(Console.ReadLine(), out latitude) && int.TryParse(Console.ReadLine(), out chargingStations))
                    {
                        name = Console.ReadLine();
                        bl.AddBaseStation(new BaseStation(id, name, longitude, latitude, chargingStations));
                    }
                    else
                    {

                    }
                    break;

                case EntityOptions.DRONE:
                    Console.WriteLine("Enter Id and id of station and max weight drone (for light press 1 for Medium press 2 for Heavy press 3 )  add model:");
                    int maxWeight, stationId;

                    if (int.TryParse(Console.ReadLine(), out id) && int.TryParse(Console.ReadLine(), out stationId) && Enum.TryParse(Console.ReadLine(), out maxWeight))
                    {
                        String model = Console.ReadLine();
                        bl.GetStation(stationId);

                        bl.AddDrone(new Drone(id, model, (Enums.WeightCategories)maxWeight, DroneStatuses.Maintenance, rand.Next(20, 41), bl.GetStation(stationId).Longitude, bl.GetStation(stationId).Latitude));
                    }
                    else
                    {

                    }
                    break;
                case EntityOptions.CUSTOMER:

                    Console.WriteLine("Enter Id and Location add Name and Phone:");
                    if (int.TryParse(Console.ReadLine(), out id) && double.TryParse(Console.ReadLine(), out latitude) && double.TryParse(Console.ReadLine(), out longitude))
                    {

                        name = Console.ReadLine();
                        string phone;
                        bool correctName = true;
                        do
                        {
                            Console.WriteLine("Enter phone");
                            phone = Console.ReadLine();
                            if (!(phone[0] == '+' || phone[0] == '*' || char.IsDigit(phone[0])))
                                foreach (char item in phone.Substring(1))
                                    if (!char.IsDigit(item))
                                        correctName = false;
                        } while (!correctName);
                        bl.AddCustomer(new Customer(id, name, phone, longitude, latitude));
                    }
                    else
                    {

                    }
                    break;
                case EntityOptions.PARCEL:
                    Console.WriteLine("Enter Idof the sender and of the reciver and the weight of the Parcel(for light press 1 for Medium press 2 for Heavy press 3 ) and the priority(for Regular  press 1 for Emergency press 2 for Fast press 3) :");
                    int idSender, idReceiver, weight, priority;
                    if (int.TryParse(Console.ReadLine(), out idSender) && int.TryParse(Console.ReadLine(), out idReceiver) && Enum.TryParse(Console.ReadLine(), out weight) && Enum.TryParse(Console.ReadLine(), out priority))
                    {

                        bl.AddParcel(new Parcel(idSender, idReceiver, (Enums.WeightCategories)weight, (Enums.Priorities)priority));
                    }
                    break;
                case EntityOptions.EXIT:
                    break;
            }
            return;
        }

        private static void ShowList<T>(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}
