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
        public void AddBaseStation(int id, string name, double longitude, double latitude, int availableChargingStations);
        public void AddDrone(int id, string model, int maxWeight, double longitude, double latitude);
        public void AddCustomer(int id, string name, string phone, double longitude, double latitude);
        public void AddParcel(int idSender, int idReceiver, int weight, int priority);
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
        void AddDrone(int idDrone, string modelDrone, int maxWeightDrone, int longitudeDrone, int latitudeDrone);
        void AddCustomer(int idCustomer, string nameCustomer, string phoneCustomer, int longitudeCustomer, int latitudeCustomer);
       
    }
}
