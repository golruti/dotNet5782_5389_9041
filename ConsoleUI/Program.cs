using System;
using IDAL.DO;
using DalObject;
namespace ConsoleUI
{
    class Program
    {
        public enum Options { insert, update, disply, view };
        public enum Insert { base_station, skimmer, customer, package };
        public enum Update { collect_package, deliver_package, skimmer, release_skimmer };
        public enum Disply { base_station, skimmer, customer, package };
        public enum View { base_stations, skimmers, customers, packages, packages_not_skimmer, base_stations_vacant }



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
                id = 0;
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
                                DalObject.DalObject.InsertStation(getStation());




                                break;
                            case 2:
                                // code block
                                break;
                            default:
                                // code block
                                break;
                        }
                        break;


                    case 2:

                        foreach (var item in System.Enum.GetNames(typeof(Update)))
                        {
                            Console.Write(item + "-" + (id++) + "\n");
                        }
                        id = 0;
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
                        id = 0;
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
                        id = 0;
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


                        Station getStation()
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
                }


            }


        }


    }
}

