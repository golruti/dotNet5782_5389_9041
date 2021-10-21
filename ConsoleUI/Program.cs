using System;
using IDAL.DO;
using DalObject;
namespace ConsoleUI
{
    class Program
    {
        public enum Options { insert, update, disply, view };
        public enum Insert { base_station, drone, customer, package };
        public enum Update
        {
            Assign_a_package_to_a_skimmer,
            Package_assembly_by_skimmer,
            Delivery_of_a_package_to_the_destination,
            Sending_a_skimmer_for_charging_at_a_base_station,
            Releasing_a_skimmer_from_charging
        };
        public enum Disply { base_station, drone, customer, package };
        public enum View { base_stations, drone, customers, packages, packages_not_drone, base_stations_vacant };



        static void Main(string[] args)
        {

            int choice = 0;
            int id = 1;

            while (choice != 5)
            {
                foreach (var item in System.Enum.GetNames(typeof(Options)))
                {
                    Console.Write(item + "-" + (id++) + "\n");
                }
                id = 1;
                choice = int.Parse(Console.ReadLine());


                switch (choice)
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
                                DalObject.DalObject.InsertStation(MainFunction.GetStation());
                                break;
                            case 2:
                                DalObject.DalObject.InsertDrone(MainFunction.GetDrone());
                                break;
                            case 3:
                                DalObject.DalObject.InsertCustomer(MainFunction.GetCustomer());
                                break;
                            case 4:
                                DalObject.DalObject.InsertParsel(MainFunction.GetParsel());

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
                                DalObject.DalObject.UpdateParseךScheduled(MainFunction.GetIdOfParcel());
                                break;
                            case 2:
                                DalObject.DalObject.UpdateParselPickedUp(MainFunction.GetIdOfParcel());
                                break;
                            case 3:
                                DalObject.DalObject.UpdateParselDelivered(MainFunction.GetIdOfParcel());
                                break;
                            case 4:
                                // code block
                                break;
                            case 5:
                                // code block
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
                                // code block
                                break;
                            case 2:
                                // code block
                                break;
                            case 3:
                                // code block
                                break;
                            case 4:
                                // code block
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
                                // code block
                                break;
                            case 2:
                                // code block
                                break;
                            case 3:
                                // code block
                                break;
                            case 4:
                                // code block
                                break;
                            case 5:
                                // code block
                                break;
                            case 6:
                                // code block
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

