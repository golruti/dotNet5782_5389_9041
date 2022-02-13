using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer = BO.Customer;
using Drone = BO.Drone;
using Parcel = BO.Parcel;
using BaseStation = BO.BaseStation;
using User = BO.User;



namespace BlApi
{
    public interface IBL
    {
        public void deleteBLParcel(int parcelId);
        public void DeleteBLDrone(int droneId);
        public void deleteBLBaseStation(int stationId);
        public void AddBaseStation(BaseStation tempBaseStation);
        void AddDrone(Drone tempDrone, int stationId = -1);
        public void AddCustomer(Customer tempCustomer);
        public void AddParcel(Parcel tempParcel);

        public BaseStation GetBLBaseStation(int id);
        public Drone GetBLDrone(int id);
        public Customer GetBLCustomer(int id);
        public Parcel GetBLParcel(int parcelId);

        public IEnumerable<BaseStationForList> GetBaseStationForList();
        public IEnumerable<BaseStationForList> GetBaseStationForList(Predicate<BaseStationForList> predicate);
        public IEnumerable<BaseStationForList> GetAvaBaseStationForList();
        public IEnumerable<DroneForList> GetDroneForList();
        public IEnumerable<DroneForList> GetDroneForList(Predicate<DroneForList> predicate);
        IEnumerable<DroneInCharging> GetDronesInCharging();

        //public IEnumerable<DroneForList> GetDroneForList(Enums.WeightCategories weight, Enums.DroneStatuses status);
        //public IEnumerable<DroneForList> GetDroneForList(Enums.WeightCategories weight);
        //public IEnumerable<DroneForList> GetDroneForList(Enums.DroneStatuses status);
        public IEnumerable<CustomerForList> GetCustomerForList();
        public IEnumerable<CustomerForList> GetCustomerForList(Predicate<CustomerForList> predicate);
        public IEnumerable<ParcelForList> GetParcelForList();
        public IEnumerable<ParcelForList> GetParcelForList(Predicate<ParcelForList> predicate);
        public IEnumerable<DroneInCharging> GetDronesInCharging(int droneId);

        //public IEnumerable<Parcel> GetAllParcels();
        public CustomerForList GetCustomerForList(string name);
        public void UpdateBaseStation(int id, string name, int chargeSlote);
        public void UpdateDroneModel(int id, string model);
        void UpdateRelease(int droneId);
        void UpdateCharge(int droneId);
        void AssignParcelToDrone(int droneId);
        void UpdateCustomer(int customerId, string name, string phone);
        public void ParcelCollection(int Id);
        public void UpdateParcelDelivered(int id);
        public Enums.ParcelStatuses GetParcelStatusByDrone(int droneId);

        public void DeleteBLCustomer(int customerId);

        public void UpdateParcelScheduled(int parcelId, int droneId);





        public void AddUser(User tempUser);
        public bool IsExistClient(int userId, string password);
        public bool IsExistEmployee(int userId, string password);
   


        public void DeleteUser(User tempUser);
    }
}





