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
using BaseStation = IBL.BO.BaseStation;

namespace IBL
{
    public interface IBL
    {
        void initializeDrones();
        IDAL.DO.BaseStation nearestBaseStation(double LongitudeSenderCustomer, double LatitudeSenderCustomer);
        double Distance(double Latitude1, double Latitude2, double Longitude1, double Longitude2);
        IEnumerable<BaseStationForList> GetBaseStationForList();
        int numOfUsedChargingPorts(int idBaseStation);
        IEnumerable<BaseStationForList> GetAvaBaseStationForList();
        void AddBaseStation(BaseStation tempBaseStation);
        void UpdateBaseStation(int id, string name, int chargeSlote);
        IEnumerable<CustomerForList> GetCustomerForList();
        void AddCustomer(Customer tempCustomer);
        void UpdateCustomer(int id, string name, string phone);
        private bool IsDroneMakesDelivery(int droneId);
        public IEnumerable<DroneForList> GetDroneForList();
        public void AddDrone(Drone tempDrone);
        public void UpdateDrone(int id, string model);
        private Location findDroneLocation(DroneForList drone);
        private Enums.DroneStatuses findfDroneStatus(int droneId);
        private DroneForList getDroneForList(int droneId);
        private Enums.ParcelStatuses findParcelState(int DroneId);
        public IEnumerable<ParcelForList> ParcelForList();
        private string getSendCustomerName(IDAL.DO.Parcel parcel);
        private string getReceiveCustomer(IDAL.DO.Parcel parcel);
        private BO.Enums.ParcelStatuses getStatusCustomer(IDAL.DO.Parcel parcel);
        public IEnumerable<ParcelForList> UnassignedParcelsForList();
        public void AddParcel(Parcel tempParcel);
        private int FindParceDeliveredlId(int droneId);

    }
}




        //public void AddBaseStation(BO.BaseStation tempBaseStation);
        //public void AddDrone(Drone tempDrone);
        //public void AddParcel(Parcel tempParcel);
        //void AddCustomer(Customer tempCustomer);
        //public void UpdateParcelScheduled(int idxParcel);
        //public void UpdateParcelPickedUp(int idxParcel);
        //public void UpdateParcelDelivered(int idxParcel);
        //public bool TryAddDroneCarge(int droneId);
        //public bool TryRemoveDroneCarge(int droneId);
        //public IDAL.DO.BaseStation GetStation(int idxStation);
        //public Drone GetDrone(int idxDrone);
        //public Customer GetCustomer(int idxCustomer);
        //public Parcel GetParcel(int idxParcel);
        //public List<IDAL.DO.BaseStation> GetStations();
        //public List<Customer> GetCustomers();
        //public List<Drone> GetDrones();
        //public List<Parcel> GetParcels();
        //public List<Parcel> UnassignedPackages();
        //public List<IDAL.DO.BaseStation> GetAvaStations();
        //void UpdateDrone(int droneId, string model);
        //void UpdateBaseStation(int stationlId, string name, int chargeSlote);
        //void UpdateCustomer(int customerId, string name, string phone);
        //void SendDroneToRecharge(int droneId);
        //void UpdateDroneLocation(int id);
