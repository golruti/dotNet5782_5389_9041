using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enum;

namespace DalApi
{
    public interface IDal
    {
        #region Base station
        public void AddBaseStation(BaseStation station);
        public BaseStation GetBaseStation(int idStation);
        public IEnumerable<BaseStation> GetBaseStations();
        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate);
        public IEnumerable<BaseStation> GetAvaBaseStations();
        public void DeleteBaseStation(int id);
        void UpdateRelease(int droneId);
        public void UpdateCharge(int droneId, int baseStationId);
        #endregion

        #region Customer
        public void AddCustomer(Customer customer);
        public Customer GetCustomer(int idCustomer);
        public Customer GetCustomer(Predicate<Customer> predicate);
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate);
        public void DeleteCustomer(int id);
        #endregion

        #region Drone
        public void AddDrone(Drone drone);
        public Drone GetDrone(int idDrone);
        public IEnumerable<Drone> GetDrones();
        public void DeleteDrone(int droneId);
        #endregion

        #region Drone charge
        public void AddDroneCharge(DroneCharge droneCharge);
        public DroneCharge GetDroneCharge(int droneId);
        public IEnumerable<DroneCharge> GetDronesCharges();
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate);
        public void DeleteDroneCharge(int droneId);
        #endregion

        #region parcel
        public void AddParcel(Parcel parcel);
        public Parcel GetParcel(int idParcel);
        public Parcel GetParcel(Predicate<Parcel> predicate);
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate);
        public void UpdateParcelPickedUp(int parcelId);
        public void UpdateParcelDelivered(int parcelId);
        public void UpdateParcelScheduled(int parcelId, int droneId);

        public void DeleteParcel(int id);
        #endregion

        #region User
        public void AddUser(User user);
        public bool ExistUser(int userName, string password, Access access);
        public User GetUser(int userId, string password, Access access);
        public User GetCUser(Predicate<User> predicate, Access access);
        public IEnumerable<User> GetUsers();
        public IEnumerable<User> GetUsers(Predicate<User> predicate);
        public void DeleteUser(User user);
        #endregion
        public double[] GetElectricityUse();
    }
}
