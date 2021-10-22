using System;
using IDAL.DO;
using DalObject;
namespace ConsoleUI
{
    class Program
    {
        public enum Options { insert, update, disply, view };
        public enum Insert { base_station, drone, customer, parcel };
        public enum Update
        {
            Assign_a_package_to_a_skimmer,
            Package_assembly_by_skimmer,
            Delivery_of_a_package_to_the_destination,
            Sending_a_skimmer_for_charging_at_a_base_station,
            Releasing_a_skimmer_from_charging
        };
        public enum Disply { base_station, drone, customer, parcel };
        public enum View { base_stations, drone, customers, parsel, parsels_not_drone, base_stations_vacant };



        static void Main(string[] args)
        {

            int choice = 0;
            int choice2 = 0;

            int id = 1;
            DalObject.DalObject dal = new DalObject.DalObject();

            while (choice2 != 5)
            {
                foreach (var item in System.Enum.GetNames(typeof(Options)))
                {
                    Console.Write(item + "-" + (id++) + "\n");
                }
                id = 1;
                choice2 = int.Parse(Console.ReadLine());


                switch (choice2)
                {

                    case 1:
                        foreach (var item in System.Enum.GetNames(typeof(Insert)))
                        {

                            Console.Write(item + "-" + (id++) + "\n");
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
                                dal.InsertParsel(MainFunction.GetParsel());

                                break;
                            default:
                                break;
                        }
                        break;


                    case 2:
                        foreach (var item in System.Enum.GetNames(typeof(Update)))
                        {
                            Console.Write(item + "-" + (id++) + "\n");
                        }
                        id = 1;
                        choice = int.Parse(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                dal.UpdateParseךScheduled(MainFunction.GetId());
                                break;
                            case 2:
                                dal.UpdateParselPickedUp(MainFunction.GetId());
                                break;
                            case 3:
                                dal.UpdateParselDelivered(MainFunction.GetId());
                                break;
                            case 4:
                                if (!dal.TryAddDroneCarge(MainFunction.GetId()))
                                    Console.WriteLine("X");
                                else
                                    Console.WriteLine("V");
                                break;
                            case 5:
                                if (!dal.TryRemoveDroneCarge(MainFunction.GetId()))
                                    Console.WriteLine("X");
                                else
                                    Console.WriteLine("V");
                                break;

                            default:
                                break;
                        }
                        break;




                    case 3:
                        foreach (var item in System.Enum.GetNames(typeof(Disply)))
                        {
                            Console.Write(item + "-" + (id++) + "\n");
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
                                Console.WriteLine(dal.GetParsel(MainFunction.EnterId()));
                                break;
                            default:
                                break;
                        }
                        break;




                    case 4:
                        foreach (var item in System.Enum.GetNames(typeof(View)))
                        {
                            Console.Write(item + "-" + (id++) + "\n");
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
                                foreach (var parsel in dal.GetParsels())
                                {
                                    Console.Write(parsel);
                                }
                                break;
                            case 5:
                                foreach (var parsel in dal.UnassignedPackages())
                                {
                                    Console.Write(parsel);
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

