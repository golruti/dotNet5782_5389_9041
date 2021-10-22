using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.Initialize();
        }

        //insert
        public void InsertStation(Station station)
        {
            DataSource.stations[DataSource.Config.IndStation++] = station;
        }

        public void InsertDrone(Drone drone)
        {
            DataSource.drones[DataSource.Config.IndDrone++] = drone;
        }
        public void InsertCustomer(Customer customer)
        {
            DataSource.customers[DataSource.Config.IndCustomer++] = customer;
        }

        public void InsertParsel(Parsel parsel)
        {
            DataSource.parsels[DataSource.Config.IndParsel++] = parsel;
            DataSource.parsels[DataSource.Config.IndParsel].Id = DataSource.Config.IndParsel;
        }





        //שיוך חבילה לרחפן
        public void UpdateParseךScheduled(int idxParsel)
        {
            for (int i = 0; i < DataSource.Config.IndDrone; ++i)
            {
                if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
                {
                    DataSource.parsels[idxParsel].Scheduled = new DateTime();
                    DataSource.parsels[idxParsel].Droneld = DataSource.drones[i].Id;
                    DataSource.drones[i].Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
                    DataSource.drones[i].MaxWeight = DataSource.parsels[idxParsel].Weight;
                    break;
                }
            }
        }
        //אסיפת חבילה עי רחפן
        public void UpdateParselPickedUp(int idxParsel)
        {
            DataSource.parsels[idxParsel].PickedUp = DateTime.Now;
            DataSource.drones[DataSource.parsels[idxParsel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Delivery;
        }


        //אספקת חבילה ליעד
        public void UpdateParselDelivered(int idxParsel)
        {
            DataSource.parsels[idxParsel].Delivered = DateTime.Now;
            DataSource.drones[DataSource.parsels[idxParsel].Droneld].Status = IDAL.DO.Enum.DroneStatuses.Available;
        }






        //drone charge
        public bool TryAddDroneCarge(int droneId)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default))
                return false;

            var station = DataSource.stations.FirstOrDefault(s => s.ChargeSlote < DataSource.droneCharges.Count(dc => dc.StationId == s.Id));
            if (station.Equals(default))
                return false;

            DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
            DataSource.droneCharges.Add(droneCharge);
            drone.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;
            return true;
        }
        public bool TryRemoveDroneCarge(int droneId)
        {
            var droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId);
            if (droneCharge.Equals(default))
                return false;

            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default))
                return false;

            DataSource.droneCharges.Remove(droneCharge);
            drone.Status = IDAL.DO.Enum.DroneStatuses.Available;
            return true;
        }



        //פונקציות השולפות לפי אינדקס
        public Station GetStation(int idxStation)
        {
            return DataSource.stations[idxStation];
        }
        public Drone GetDrone(int idxDrone)
        {
            return DataSource.drones[idxDrone];
        }
        public Customer GetCustomer(int idxCustomer)
        {
            return DataSource.customers[idxCustomer];
        }
        public Parsel GetParsel(int idxParsel)
        {
            return DataSource.parsels[idxParsel];
        }



        //פונקציות השולפות את המערך המלא
        public Station[] GetStations()
        {
            Station[] stations = new Station[DataSource.Config.IndStation];
            for (int i = 0; i < DataSource.Config.IndStation; i++)
            {
                Station source = DataSource.stations[i];
                stations[i] = source.Clone();
            }
            return stations;
        }
        public Customer[] GetCustomers()
        {
            Customer[] customers = new Customer[DataSource.Config.IndCustomer];
            for (int i = 0; i < DataSource.Config.IndCustomer; i++)
            {
                Customer source = DataSource.customers[i];
                customers[i] = source.Clone();
            }
            return customers;
        }

        public Drone[] GetDrones()
        {
            Drone[] drones = new Drone[DataSource.Config.IndDrone];
            for (int i = 0; i < DataSource.Config.IndDrone; i++)
            {
                Drone source = DataSource.drones[i];
                drones[i] = source.Clone();
            }
            return drones;
        }

        public Parsel[] GetParsels()
        {
            Parsel[] parsels = new Parsel[DataSource.Config.IndParsel];
            for (int i = 0; i < DataSource.Config.IndParsel; i++)
            {
                Parsel source = DataSource.parsels[i];
                parsels[i] = source.Clone();
            }
            return parsels;
        }



        //החזרת רשימת חבילות שלא שויכו לרחפן

        public Parsel[] UnassignedPackages()
        {
            int amountOfParsel = 0, j = 0;
            for (int i = 0; i < DataSource.Config.IndParsel; i++)
            {
                if (DataSource.parsels[i].Droneld != 0)
                {
                    ++amountOfParsel;
                }
            }

            Parsel[] parsels = new Parsel[amountOfParsel];
            for (int i = 0; i < DataSource.Config.IndParsel; i++)
            {
                if (DataSource.parsels[i].Droneld != 0)
                {
                    Parsel source = DataSource.parsels[i];
                    parsels[j] = source.Clone();
                    ++j;
                }
            }
            return parsels;
        }


        //	הצגת  תחנות-בסיס עם עמדות טעינה פנויות
        public Station[] GetAvaStations()
        {
            return DataSource.stations
                         .Where(s => s.ChargeSlote < DataSource.droneCharges.Count(dc => dc.StationId == s.Id))
                         .ToArray();
        }
    }
}
