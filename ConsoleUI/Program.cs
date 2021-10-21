using System;
using IDAL.DO;
using DalObject;
namespace ConsoleUI
{
    class Program
    {
        public enum Options { insert, update, disply, view };
        public enum Insert { base_station, drone, customer, package };
        public enum Update { collect_package, deliver_package, drone, release_drone };
        public enum Disply { base_station, drone, customer, package };
        public enum View { base_stations, drone, customers, packages, packages_not_drone, base_stations_vacant }



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
                        id = 0;
                        choice = int.Parse(Console.ReadLine());

                        switch (choice)
                        {

                            case 1:
                              DalObject.DalObject.InsertStation(MainFunction.getStation());
                                break;
                            case 2:
                                DalObject.DalObject.InsertDrone(MainFunction.getDrone());
                                break;
                            case 3:
                                DalObject.DalObject.InsertCustomer(MainFunction.getCastomer());
                                break;
                            case 4:
                                DalObject.DalObject.InsertParsel(MainFunction.getParsel());
                                int x = 10;
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
                                // code block
                                break;
                            case 2:
                                // code block
                                break;
                            default:
                                // code block
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
                            default:
                                // code block
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
                            default:
                                // code block
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

