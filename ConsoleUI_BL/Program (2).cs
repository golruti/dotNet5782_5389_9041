using System;

namespace ConsoleUI_BL
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

        private static void Main(string[] args)
        {
            int choice = 0;
            int choice2 = 0;

            int id = 1;
            IBL bl= new BL();
            IDal.IDal dal = new DalObject.DalObject();
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
                                IBL.InsertStation(MainFunction.GetStation());
                                break;
                            case 2:
                                IBL.InsertDrone(MainFunction.GetDrone());
                                break;

                            case 3:
                                IBL.InsertCustomer(MainFunction.GetCustomer());
                                break;
                            case 4:
                                IBL.InsertParcel(MainFunction.GetParcel());
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
                                IBL.UpdateParcelScheduled(MainFunction.EnterId());
                                Console.Write("##OK##\n");
                                break;
                            case 2:
                                IBL.UpdateParcelPickedUp(MainFunction.EnterId());
                                Console.Write("##OK##\n");
                                break;
                            case 3:
                                IBL.UpdateParcelDelivered(MainFunction.EnterId());
                                Console.Write("##OK##\n");
                                break;
                            case 4:
                                if (!IBL.TryAddDroneCarge(MainFunction.EnterId()))
                                    Console.WriteLine("Skimmer does not exist or there is no available charging station\n");
                                else
                                    Console.WriteLine("##OK#");
                                break;
                            case 5:
                                if (!IBL.TryRemoveDroneCarge(MainFunction.EnterId()))
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
                                Console.WriteLine(IBL.GetStation(MainFunction.EnterId()));
                                break;
                            case 2:
                                Console.WriteLine(IBL.GetDrone(MainFunction.EnterId()));
                                break;
                            case 3:
                                Console.WriteLine(IBL.GetCustomer(MainFunction.EnterId()));
                                break;
                            case 4:
                                Console.WriteLine(IBL.GetParcel(MainFunction.EnterId()));
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
                                foreach (var station in IBL.GetStations())
                                {
                                    Console.Write(station);
                                }
                                break;
                            case 2:
                                foreach (var drone in IBL.GetDrones())
                                {
                                    Console.Write(drone);
                                }
                                break;
                            case 3:
                                foreach (var customer in IBL.GetCustomers())
                                {
                                    Console.Write(customer);
                                }
                                break;
                            case 4:
                                foreach (var parcel in IBL.GetParcels())
                                {
                                    Console.Write(parcel);
                                }
                                break;
                            case 5:
                                foreach (var parcel in IBL.UnassignedPackages())
                                {
                                    Console.Write(parcel);
                                }
                                break;
                            case 6:
                                foreach (var station in IBL.GetAvaStations())
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
        } }
    }
