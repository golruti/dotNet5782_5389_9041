using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    partial class BL
    {
        //--------------------------------------------הוספת רחפן-------------------------------------------------------------------------------------------
        public void AddDrone(Drone tempDrone)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDrone.Id, tempDrone.Model, (IDAL.DO.Enum.WeightCategories)tempDrone.MaxWeight);
            dal.InsertDrone(drone);
        }

        public void AddDroneForList(Drone drone)
        {
            DroneForList droneForList(drone.Id, drone.Model, drone.MaxWeight, drone.Battery, drone.Status, drone.Longitude, drone.Latitude);
            drones.Add(droneForList);
        }


        //---------------------------------------------הצגת רחפן לפי ID ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Drone GetBLDrone(int id)
        {
            return MapDrone(id);
        }

        private Drone MapDrone(int id)
        {
            DroneForList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new ArgumentNullException("Map drone -BL-:There is not drone with same id i data");
            return new Drone()
            {
                Id = droneToList.Id,
                Model = droneToList.Model,
                MaxWeight = droneToList.MaxWeight,
                Status = droneToList.Status,
                Battery = droneToList.Battery,
                Location = droneToList.Location,
                Delivery = droneToList.ParcelDeliveredId != null ? CreateParcelInTransfer((int)droneToList.ParcelDeliveredId) : null
            };
        }

        //--------------------------------------------הצגת רשימת רחפנים לרשימה---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public IEnumerable<DroneForList> GetDroneForList()
        {
            return drones;
        }





        //--------------------------------------------עדכון------------------------------------------------------------------------------------------


        //עידכון מודל
        public void UpdateDroneModel(int id, string model)
        {
            DroneForList tempDroneForList = drones.Find(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Model = model;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDroneForList.Id, tempDroneForList.Model, (IDAL.DO.Enum.WeightCategories)tempDroneForList.MaxWeight);
            dal.InsertDrone(drone);
        }

        public void UpdateDroneStatus(int id, DroneStatuses status, double battery, double longitude, double latitude)
        {
            DroneForList tempDroneForList = drones.Find(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Status = status;
            tempDroneForList.Battery = battery;
            tempDroneForList.Location.Longitude = longitude;
            tempDroneForList.Location.Latitude = latitude;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
        }





        //---------------------שליחת רחפן לטעינה
        public void SendDroneToRecharge(int droneId)
        {
            DroneForList tempDroneForList = getDroneForList(droneId);
            Drone tempDrone = new Drone(tempDroneForList.Id, tempDroneForList.Model, tempDroneForList.MaxWeight, tempDroneForList.Status, tempDroneForList.Battery, tempDroneForList.Location.Longitude, tempDroneForList.Location.Latitude);
            int baseStationId=0;
            double distance = double.MaxValue;
            if ((int)tempDrone.Status == 0)
            {

                foreach (var item in dal.GetBaseStations())
                {
                    double tempDistance = Distance(tempDrone.Location.Latitude, item.Latitude, tempDrone.Location.Longitude, item.Longitude);
                    if (tempDistance < distance/* && SeveralAvailablechargingStations(item.Id)>0*/)
                    {
                        baseStationId = item.Id;
                        distance = tempDistance;
                    }
                    else
                    {

                    }
                }
                if (BatteryCalculationOnTraveling(distance) < tempDrone.Battery)
                {
                    UpdateDroneStatus(droneId, DroneStatuses.Maintenance, tempDrone.Battery - BatteryCalculationOnTraveling(distance), GetBLBaseStation(baseStationId).Location.Latitude, GetBLBaseStation(baseStationId).Location.Latitude);
                    dal.AddDroneCarge(droneId, baseStationId);
                }
                else
                {

                }
            }
            else
            {

            }
        }

        //צריך לסדר....!!!!!!!!!!!!!!!!!!!!!!!!!!!!1
        double BatteryCalculationOnTraveling(double distance)
        {
            return distance;
        }

        double BatteryCalculationInCharging(int time)
        {
            double battery = time * 0.05;
            if (battery<100)
            {
                return battery;
            }
            return 100;
        }

        public void ReleaseDroneFromRecharge(int droneId,int time)
        {
            DroneInCharging droneInCharging = new DroneInCharging();// DroneInCharging(droneId, BatteryCalculationInCharging(time));
            DroneForList drone = drones.Find(item => item.Id == droneId);
            if (drone != null)
            {
                if (drone.Status == (DroneStatuses)2)
                {
                    drones.Remove(drone);
                    droneInCharging.Battery = drone.Battery + BatteryCalculationInCharging(time);
                    drone.Battery = droneInCharging.Battery;
                    drone.Status = (DroneStatuses)0;
                    drones.Add(drone);
                    dal.UpdateRelease(droneId);
                }
                else
                {
                   
                }
            }
            else
            {
                
            }
        }

        //----------------------------------------------------------------------------------------לשימוש הקונסטרקטור


        private Location findDroneLocation(DroneForList drone)
        {
            if (IsDroneMakesDelivery(drone.Id))
            {
                if (findParcelState(drone.Id) == parcelState.associatedNotCollected)
                {
                    //עדכון מיקום רחפן לתחנה הקרובה ביותר לשולח
                    IDAL.DO.Customer senderCustomer2 = FindSenderCustomerByDroneId(drone.Id);
                    IDAL.DO.BaseStation soonStation = nearestBaseStation(senderCustomer2.Longitude, senderCustomer2.Latitude);
                    return new Location(soonStation.Longitude, soonStation.Latitude);
                }
                else if (findParcelState(drone.Id) == parcelState.collectedNotDelivered)
                {
                    //עדכון מיקום רחפן למיקום השולח
                    IDAL.DO.Customer senderCustomer2 = FindSenderCustomerByDroneId(drone.Id);
                    return new Location(senderCustomer2.Longitude, senderCustomer2.Latitude);
                }
            }
            else //אם הרחפן לא מבצע משלוח
            {
                if (drone.Status == Enums.DroneStatuses.Maintenance)
                {
                    //מגריל מיקום מבין התחנות הקיימות
                    //??לבדוק אם זה דווקא תחנות פנוית או ההיפך....
                    int randNumber1 = rand.Next(dal.GetBaseStations().Count());
                    var randomBaseStation = (dal.GetBaseStations().ToList())[randNumber1];
                    return new Location(randomBaseStation.Longitude, randomBaseStation.Latitude);
                }
            }
            //אם הרחפן פנוי ולא שויך עי שום חבילה
            int randNumber = rand.Next(dal.GetCustomersProvided().Count());
            var randomCustomerProvided = (dal.GetCustomersProvided().ToList())[randNumber];
            return new Location(randomCustomerProvided.Longitude, randomCustomerProvided.Latitude);
        }

        //פונקציה לשימוש הקונסטרקטור
        //בדיקה אם הרחפן מבצע משלוח
        private bool IsDroneMakesDelivery(int droneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                //חבילה שלא סופקה והרחפן שויך
                if (parcel.Droneld == droneId &&
                    !(parcel.Requested.Equals(default(DateTime))) && parcel.Delivered.Equals(default(DateTime)))
                {
                    return true;
                }
            }
            return false;
        }

        private Enums.DroneStatuses findfDroneStatus(int droneId)
        {

            if (IsDroneMakesDelivery(droneId))  //אם הרחפן מבצע משלוח, הרחפן שויך והחבילה לא סופקה
            {
                return BO.Enums.DroneStatuses.Delivery;
            }

            return (Enums.DroneStatuses)rand.Next(System.Enum.GetNames(typeof(Enums.DroneStatuses)).Length);
        }


        private DroneForList getDroneForList(int droneId)
        {
            return drones.First(drone => drone.Id == droneId);
        }
    }
}
