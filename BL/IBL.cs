using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer = IBL.BO.Customer;
using Drone = IBL.BO.Drone;
using Parcel = IBL.BO.Parcel;

namespace IBL
{
    public interface IBL
    {
        public void AddBaseStation(BO.BaseStation tempBaseStation);
        public void AddDrone(Drone tempDrone);
        public void AddParcel(Parcel tempParcel);
        void AddCustomer(Customer tempCustomer);
        public void UpdateParcelScheduled(int idxParcel);
        public void UpdateParcelPickedUp(int idxParcel);
        public void UpdateParcelDelivered(int idxParcel);
        public bool TryAddDroneCarge(int droneId);
        public bool TryRemoveDroneCarge(int droneId);
        public IDAL.DO.BaseStation GetStation(int idxStation);
        public Drone GetDrone(int idxDrone);
        public Customer GetCustomer(int idxCustomer);
        public Parcel GetParcel(int idxParcel);
        public List<IDAL.DO.BaseStation> GetStations();
        public List<Customer> GetCustomers();
        public List<Drone> GetDrones();
        public List<Parcel> GetParcels();
        public List<Parcel> UnassignedPackages();
        public List<IDAL.DO.BaseStation> GetAvaStations();
        void UpdateDrone(int droneId, string model);
        void UpdateBaseStation(int stationlId, string name, int chargeSlote);
        void UpdateCustomer(int customerId, string name, string phone);
        void SendDroneToRecharge(int droneId);
        void UpdateDroneLocation(int id);
    }
}
