using System;
using IDAL.DO;
using DalObject;
namespace ConsoleUI
{
    class Program
    {
        public enum Options { Insert, Update, Disply, View, Exit };
        public enum Insert { Base_station, Drone, Customer, Parcel };
        public enum Update
        {
            Assign_a_package_to_a_drone,
            Package_assembly_by_drone,
            Delivery_of_a_package_to_the_destination,
            Sending_a_drone_for_charging_at_a_base_station,
            Releasing_a_drone_from_charging
        };
        public enum Disply { Base_station, Drone, Customer, Parcel };
        public enum View { Base_stations, Drone, Customers, Parcel, Parcels_not_drone, Base_stations_vacant };

        static void Main(string[] args)
        {
            int choice = 0;
            int choice2 = 0;

            int id = 1;
            DalObject.DalObject dal = new DalObject.DalObject();

            while (choice2 != 5)
            {
                Console.Write("------------\n");
                foreach (var option in System.Enum.GetNames(typeof(Options)))
                {
                    Console.Write((id++) + "--- " + option + "\n");
                }
                Console.Write("------------\n");
                id = 1;
                choice2 = int.Parse(Console.ReadLine());

                switch (choice2)
                {
                    case 1:
                        foreach (var option in System.Enum.GetNames(typeof(Insert)))
                        {

                            Console.Write((id++) + "--- " + option + "\n");
                        }
                        id = 1;
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                dal.InsertStation(MainFunction.GetStation());
                                break;
                            case 2:
                                dal.InsertDrone(MainFunction.GetDrone());
                                break;

                            case 3:
                                dal.InsertCustomer(MainFunction.GetCustomer());
                                break;
                            case 4:
                                dal.InsertParcel(MainFunction.GetParcel());
                                break;
                            default:
                                break;
                        }
                        break;

                    case 2:
                        foreach (var option in System.Enum.GetNames(typeof(Update)))
                        {
                            Console.Write((id++) + "--- " + option + "\n");
                        }
                        id = 1;
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                dal.UpdateParcelScheduled(MainFunction.EnterId());
                                Console.Write("##OK##\n");
                                break;
                            case 2:
                                dal.UpdateParcelPickedUp(MainFunction.EnterId());
                                Console.Write("##OK##\n");
                                break;
                            case 3:
                                dal.UpdateParcelDelivered(MainFunction.EnterId());
                                Console.Write("##OK##\n");
                                break;
                            case 4:
                                if (!dal.TryAddDroneCarge(MainFunction.EnterId()))
                                    Console.WriteLine("Skimmer does not exist or there is no available charging station\n");
                                else
                                    Console.WriteLine("##OK##");
                                break;
                            case 5:
                                if (!dal.TryRemoveDroneCarge(MainFunction.EnterId()))
                                    Console.WriteLine("The drone is not charging\n");
                                else
                                    Console.WriteLine("##OK##");
                                break;
                            default:
                                break;
                        }
                        break;

                    case 3:
                        foreach (var option in System.Enum.GetNames(typeof(Disply)))
                        {
                            Console.Write((id++) + "--- " + option + "\n");
                        }
                        id = 1;
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                Console.WriteLine(dal.GetStation(MainFunction.EnterId()));
                                break;
                            case 2:
                                Console.WriteLine(dal.GetDrone(MainFunction.EnterId()));
                                break;
                            case 3:
                                Console.WriteLine(dal.GetCustomer(MainFunction.EnterId()));
                                break;
                            case 4:
                                Console.WriteLine(dal.GetParcel(MainFunction.EnterId()));
                                break;
                            default:
                                break;
                        }
                        break;

                    case 4:
                        foreach (var option in System.Enum.GetNames(typeof(View)))
                        {
                            Console.Write((id++) + "--- " + option + "\n");
                        }
                        id = 1;
                        choice = int.Parse(Console.ReadLine());
                        switch (choice)
                        {
                            case 1:
                                foreach (var station in dal.GetStations())
                                {
                                    Console.Write(station);
                                }
                                break;
                            case 2:
                                foreach (var drone in dal.GetDrones())
                                {
                                    Console.Write(drone);
                                }
                                break;
                            case 3:
                                foreach (var customer in dal.GetCustomers())
                                {
                                    Console.Write(customer);
                                }
                                break;
                            case 4:
                                foreach (var parcel in dal.GetParcels())
                                {
                                    Console.Write(parcel);
                                }
                                break;
                            case 5:
                                foreach (var parcel in dal.UnassignedPackages())
                                {
                                    Console.Write(parcel);
                                }
                                break;
                            case 6:
                                foreach (var station in dal.GetAvaStations())
                                {
                                    Console.Write(station);
                                }
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}

