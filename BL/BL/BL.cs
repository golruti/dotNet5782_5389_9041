using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using IBL.BO;
using static IBL.BO.Enums;

namespace IBL
{
    partial class BL : IBL
    {

        private IDAL.IDal dal;
        private List<DroneForList> drones;
        private static Random rand = new Random();
        private enum parcelState { DroneNotAssociated, associatedNotCollected, collectedNotDelivered }


        public BL()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneForList>();
            initializeDrones();
        }



        private void initializeDrones()
        {

            foreach (var drone in dal.GetDrones())
            {
                drones.Add(new DroneForList
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = (Enums.WeightCategories)drone.MaxWeight,
                });
            }

            foreach (var drone in drones)
            {
                drone.Status = findfDroneStatus(drone.Id);
            }

            foreach (var drone in drones)
            {
                drone.ParcelDeliveredId = findParceDeliveredlId(drone.Id);
            }

            foreach (var drone in drones)
            {
                drone.Location = findDroneLocation(drone);
            }

            foreach (var drone in drones)
            {
                drone.Battery = findDroneBattery(drone);
            }

        }
        //        private double findDroneBattery(DroneForList drone)
        //        {

        //            if (IsDroneMakesDelivery(drone.Id))  //אם הרחפן מבצע משלוח, הרחפן שויך והחבילה לא סופקה
        //            {
        //                return
        //            }

        //            else if (drone.Status == Enums.DroneStatuses.Maintenance)
        //            {
        //                return rand.Next(0, 20);
        //            }

        //            //הרחפן פנוי
        //            return

        //        }

        //        private double minimalBattery(double distance, Enums.WeightCategories weight)
        //        {
        //            return distance * Enum.GetName(typeof(WeightCategories)).GetValue(0);
        //        }


        //            if (IsDroneMakesDelivery(newDrone.Id))  //אם הרחפן מבצע משלוח, הרחפן שויך והחבילה לא סופקה
        //        {
        //            if (findParcelState(newDrone.Id) == parcelState.associatedNotCollected)
        //            {
        //                //עדכון מיקום רחפן לתחנה הקרובה ביותר לשולח
        //                IDAL.DO.Customer senderCustomer = FindSenderCustomerByDroneId(newDrone.Id);
        //        IDAL.DO.BaseStation soonStation = soonBaseStation(senderCustomer.Longitude, senderCustomer.Latitude);
        //        newDrone.Location = new Location(soonStation.Longitude, soonStation.Latitude);

        //        //newDrone.Battery =
        //    }
        //            else if (findParcelState(newDrone.Id) == parcelState.collectedNotDelivered)
        //            {
        //                //עדכון מיקום רחפן למיקום השולח

        //                IDAL.DO.Customer senderCustomer = FindSenderCustomerByDroneId(newDrone.Id);
        //    newDrone.Location = new Location(senderCustomer.Longitude, senderCustomer.Latitude);

        //    // newDrone.Battery =
        //}
        //        }

        //        else //אם הרחפן לא מבצע משלוח
        //{
        //    if (newDrone.Status == Enums.DroneStatuses.Maintenance)
        //    {
        //        int randNumber = rand.Next(dal.GetBaseStations().Count());
        //        var randomBaseStation = dal.GetById<IDAL.DO.BaseStation>((List<IDAL.DO.BaseStation>)dal.GetCustomerProvided(), randNumber);
        //        newDrone.Location = new Location(randomBaseStation.Longitude, randomBaseStation.Latitude);

        //        newDrone.Battery = rand.Next(0, 20);
        //    }
        //    else if (newDrone.Status == Enums.DroneStatuses.Available)
        //    {
        //        // newDrone.Battery =

        //        int randNumber = rand.Next(dal.GetBaseStations().Count());
        //        var randomBaseStation = dal.GetById<IDAL.DO.BaseStation>((List<IDAL.DO.BaseStation>)dal.GetBaseStations(), randNumber);
        //        newDrone.Location = new Location(randomBaseStation.Longitude, randomBaseStation.Latitude);
        //        //newDrone.Location =
        //    }
        //}


        //    }


       



















        private IDAL.DO.BaseStation nearestBaseStation(double LongitudeSenderCustomer, double LatitudeSenderCustomer)
        {
            var minDistance = double.MaxValue;
            var nearestBaseStation = default(IDAL.DO.BaseStation);
            foreach (var BaseStation in dal.GetBaseStations())
            {
                if (Distance(LongitudeSenderCustomer, LatitudeSenderCustomer, BaseStation.Latitude, BaseStation.Longitude) < minDistance)
                {
                    minDistance = Distance(LongitudeSenderCustomer, LatitudeSenderCustomer, BaseStation.Latitude, BaseStation.Longitude);
                    nearestBaseStation = BaseStation;
                }
            }
            if (nearestBaseStation.Equals(default(IDAL.DO.BaseStation)))
            {
                throw new Exception();
            }
            return nearestBaseStation;
        }


        private double Distance(double Latitude1, double Latitude2, double Longitude1, double Longitude2)
        {
            var Coord1 = new GeoCoordinate(Latitude1, Longitude2);
            var Coord2 = new GeoCoordinate(Latitude2, Longitude2);
            return Coord1.GetDistanceTo(Coord2);
        }



        //public void UpdateParcelScheduled(int idxParcel)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateParcelPickedUp(int idxParcel)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateParcelDelivered(int idxParcel)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool TryAddDroneCarge(int droneId)
        //{
        //    throw new NotImplementedException();
        //}

        //public bool TryRemoveDroneCarge(int droneId)
        //{
        //    throw new NotImplementedException();
        //}

        //public IDAL.DO.BaseStation GetStation(int idxStation)
        //{
        //    throw new NotImplementedException();
        //}

        //public BO.Drone GetDrone(int idxDrone)
        //{
        //    throw new NotImplementedException();
        //}

        //public BO.Customer GetCustomer(int idxCustomer)
        //{
        //    throw new NotImplementedException();
        //}

        //public BO.Parcel GetParcel(int idxParcel)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<IDAL.DO.BaseStation> GetStations()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<BO.Customer> GetCustomers()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<BO.Drone> GetDrones()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<BO.Parcel> GetParcels()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<BO.Parcel> UnassignedPackages()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<IDAL.DO.BaseStation> GetAvaStations()
        //{
        //    throw new NotImplementedException();
        //}

        //public void InsertDrone(Drone drone)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddDrone(int idDrone, string modelDrone, int maxWeightDrone, int longitudeDrone, int latitudeDrone)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddCustomer(int idCustomer, string nameCustomer, string phoneCustomer, int longitudeCustomer, int latitudeCustomer)
        //{
        //    throw new NotImplementedException();
        //}






    }
}


    ///// <summary>
    ///// Assigning a parcel to a drone
    ///// </summary>
    ///// <param name="idxParcel">Id of the parcel</param>
    //public void UpdateParcelScheduled(int idxParcel)
    //{
    //    for (int i = 0; i < DataSource.drones.Count; ++i)
    //    {
    //        if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
    //        {
    //            Parcel p = DataSource.parcels[idxParcel];
    //            p.Scheduled = new DateTime();
    //            p.Droneld = DataSource.drones[i].Id;
    //            DataSource.parcels[idxParcel] = p;

    //            Drone d = DataSource.drones[i];
    //            d.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;

    //            d.MaxWeight = DataSource.parcels[idxParcel].Weight;

    //            DataSource.drones[DataSource.drones.Count] = d;
    //            break;
    //        }
    //    }
    //}
