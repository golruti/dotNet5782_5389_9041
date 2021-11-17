using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DalObject;
using IBL.BO;
using IDAL.DO;
using IDAl;
using static IBL.BO.Enums;
using Drone = IBL.BO.Drone;
using Customer = IBL.BO.Customer;
using Parcel = IBL.BO.Parcel;

namespace IBL
{
    class BL : IBL
    {
        private IDAL.IDal dal;
        private List<DroneForList> drones;
        private static Random rand = new Random();

        public BL()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneForList>();

            //foreach (var drone in dal.GetDrones())
            //{
            //    drones.Add(new DroneForList
            //    {
            //        Id = drone.Id,
            //        Model = drone.Model,
            //        MaxWeight = (Enums.WeightCategories)drone.MaxWeight,
            //        DeliveryId = 0,
            //        Battery = 1

            //    });
            //}

        }


        //public Drone CreateLogicDrone(int idDrone)
        //{
        //    var dalDrones = dal.GetDrones();
        //    foreach (var drone in dalDrones)
        //    {

        //    }
        //}

        

        public void AddBaseStation(int id,string name, double longitude, double latitude,int chargingStations)
        {
            BaseStation tempBaseStation = new BaseStation(id, name, longitude, latitude,chargingStations);

            Station station = new Station(tempBaseStation.Id, tempBaseStation.Name, tempBaseStation.Location.Longitude, tempBaseStation.Location.Latitude, chargingStations);
            dal.InsertStation(station);
            
        }



        public void AddDrone(int id, string model, int maxWeight, double longitude, double latitude)
        {
            Drone tempDrone = new Drone(id, model,(Enums.WeightCategories)maxWeight, DroneStatuses.Maintenance, rand.Next(20, 41), longitude, latitude);
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDrone.Id, tempDrone.Model, (IDAL.DO.Enum.WeightCategories)tempDrone.MaxWeight);
            dal.InsertDrone(drone);
        }

        public void AddCustomer(int id, string name, string phone, double longitude, double latitude)
        {
            Customer tempCustomer = new Customer(id,name,phone,longitude,latitude);
            IDAL.DO.Customer customer = new IDAL.DO.Customer(tempCustomer.Id, tempCustomer.Name, tempCustomer.Phone, tempCustomer.Location.Longitude, tempCustomer.Location.Latitude);
            dal.InsertCustomer(customer);
        }

        public void AddParcel(int idSender, int idReceiver, int weight, int priority)
        {
            Parcel tempParcel = new Parcel(idSender, idReceiver, (Enums.WeightCategories)weight, (Enums.Priorities)priority);
            IDAl.DO.Parcel parcel = new IDAL.DO.Parcel(tempParcel.SenderId, tempParcel.ReceiverId, (IDAL.DO.Enum.WeightCategories)tempParcel.Weight, (IDAL.DO.Enum.Priorities)tempParcel.Priority, null, DateTime.Now, new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0));
            dal.InsertParcel(parcel);
        }


        public void UpdateParcelScheduled(int idxParcel)
        {
            throw new NotImplementedException();
        }

        public void UpdateParcelPickedUp(int idxParcel)
        {
            throw new NotImplementedException();
        }

        public void UpdateParcelDelivered(int idxParcel)
        {
            throw new NotImplementedException();
        }

        public bool TryAddDroneCarge(int droneId)
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveDroneCarge(int droneId)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int idxStation)
        {
            throw new NotImplementedException();
        }

        public BO.Drone GetDrone(int idxDrone)
        {
            throw new NotImplementedException();
        }

        public BO.Customer GetCustomer(int idxCustomer)
        {
            throw new NotImplementedException();
        }

        public BO.Parcel GetParcel(int idxParcel)
        {
            throw new NotImplementedException();
        }

        public List<Station> GetStations()
        {
            throw new NotImplementedException();
        }

        public List<BO.Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public List<BO.Drone> GetDrones()
        {
            throw new NotImplementedException();
        }

        public List<BO.Parcel> GetParcels()
        {
            throw new NotImplementedException();
        }

        public List<BO.Parcel> UnassignedPackages()
        {
            throw new NotImplementedException();
        }

        public List<Station> GetAvaStations()
        {
            throw new NotImplementedException();
        }

        public void InsertDrone(Drone drone)
        {
            throw new NotImplementedException();
        }

        public void AddDrone(int idDrone, string modelDrone, int maxWeightDrone, int longitudeDrone, int latitudeDrone)
        {
            throw new NotImplementedException();
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
    }
}
