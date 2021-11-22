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


        //--------------------------------------------הצגת רשימת רחפנים לרשימה---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public IEnumerable<DroneForList> GetDroneForList()
        {
            return drones;
        }

        //-----------add
        public void AddDrone(Drone tempDrone)
        {

            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDrone.Id, tempDrone.Model, (IDAL.DO.Enum.WeightCategories)tempDrone.MaxWeight);
            dal.InsertDrone(drone);
        }

        public void AddDroneForList(Drone drone)
        {
            //DroneForList droneForList(drone.Id,drone.Model, drone.MaxWeight, drone.Battery, drone.Status, drone.Longitude, drone.Latitude);
            //drones.Add(droneForList);
        }
        //--------------------------------------------עידכון------------------------------------------------------------------------------------------


        //-------------------------עידכון מודל
        public void UpdateDrone(int id, string model)
        {
            DroneForList tempDroneForList = drones.Find(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Model = model;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDroneForList.Id, tempDroneForList.Model, (IDAL.DO.Enum.WeightCategories)tempDroneForList.MaxWeight);
            dal.InsertDrone(drone);
        }

        //---------------------שליחת רחפן לטעינה
        public void SendDroneToRecharge(int droneId)
        {
            DroneForList tempDroneForList =getDroneForList(droneId);
            Drone tempDrone = new Drone(tempDroneForList.Id, tempDroneForList.Model,tempDroneForList.MaxWeight, tempDroneForList.Status, tempDroneForList.Battery, tempDroneForList.Location.Longitude, tempDroneForList.Location.Latitude);
            int baseStationId=0;
            double distance = double.MaxValue;
            if ((int)tempDrone.Status==0)
            {
               
                foreach (var item in GetAvaBaseStationForList())
                {
                    double tempDistance = Distance(tempDrone.Location.Latitude, GetBaseStation(item.Id).Location.Latitude, tempDrone.Location.Longitude, GetBaseStation(item.Id).Location.Longitude);
                    if(tempDistance<distance)
                    {
                        baseStationId = item.Id;
                        distance = tempDistance;
                    }
                }
                if(BatteryCalculation(distance)<tempDrone.Battery)
                {
                    UpdateDroneStatus(droneId, DroneStatuses.Maintenance,tempDrone.Battery- BatteryCalculation(distance), GetBaseStation(baseStationId).Location.Latitude, GetBaseStation(baseStationId).Location.Latitude);
                    
                }
                else
                {

                }
            }
            else
            {

            }
        }

        public void ReleaseDroneFromRecharge(int droneId,double time)
        {

        }

        //}


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
            //else //אם הרחפן לא מבצע משלוח
            //{
            //    if (drone.Status == Enums.DroneStatuses.Maintenance)
            //    {
                    //מגריל מיקום מבין התחנות הקיימות
                    //??לבדוק אם זה דווקא תחנות פנוית או ההיפך....
                    int randNumber1 = rand.Next(dal.GetBaseStations().Count());
                    var randomBaseStation = (dal.GetBaseStations().ToList())[randNumber1];
                    return new Location(randomBaseStation.Longitude, randomBaseStation.Latitude);
            //    }
            //}

            ////אם הרחפן פנוי ולא שויך עי שום חבילה
            //int randNumber = rand.Next(dal.GetCustomersProvided().Count());    
            //var randomCustomerProvided = (dal.GetCustomersProvided().ToList())[randNumber];
            //return new Location(randomCustomerProvided.Longitude, randomCustomerProvided.Latitude);
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

        private List<DroneInCharging> findDronesInCharing(int id)
        {
            List<int> list = dal.GetDronechargingInStation(id);
            List<DroneInCharging> droneInChargings = new List<DroneInCharging>();
            DroneForList droneToList;
            foreach (var idDrone in list)
            {
                droneToList = drones.FirstOrDefault(item => (item.Id == idDrone));
                if (!droneToList.Equals(default(DroneForList)))
                {
                    droneInChargings.Add(new DroneInCharging() { Id = idDrone, Battery = droneToList.Battery });
                }
            }
            return droneInChargings;
        }
    }
}
