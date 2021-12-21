using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public  interface IDal
    {
        public bool uniqueIDTaxCheck<T>(IEnumerable<T> lst, int id);
        public double[] GetElectricityUse();

        public void InsertStation(BaseStation station);
        public BaseStation GetStation(int idStation);
        public IEnumerable<BaseStation> GetBaseStations();
        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate);
        void UpdateRelease(int droneId);
        public void DeleteBaseStation(int id);

        public void InsertCustomer(Customer customer);
        public Customer GetCustomer(int idCustomer);
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate);
        public void DeleteCustomer(int id);
        public Customer customerByDrone(int ParcelDeliveredId);
        public Customer FindSenderCustomerByDroneId(int DroneId);

        public void InsertDrone(Drone drone);
        public Drone GetDrone(int idDrone);
        public IEnumerable<Drone> GetDrones();
        public void TryAddDroneCarge(int droneId);
        public void TryRemoveDroneCarge(int droneId);
        public void DeleteDrone(int droneId);


        public void InsertDroneCharge(DroneCharge droneCharge);
        public DroneCharge GetDroneCharge(int droneId);
        public IEnumerable<DroneCharge> GetDronesCharges();
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate);
        public int CountFullChargeSlots(int id);
        public DroneCharge GetDronesCharge(int id);

        public void InsertParcel(Parcel parcel);
        public Parcel GetParcel(int idParcel);
        public Parcel GetParcelByDrone(int droneId);
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate);
        public void UpdateParcelPickedUp(int idParcel);
        public void UpdateParcelDelivered(int idParcel);
        public void DeleteParcel(int id);
        public void UpdatePickedUp(Parcel parcel);
        public void UpdateSupply(Parcel parcel);
    }
}
