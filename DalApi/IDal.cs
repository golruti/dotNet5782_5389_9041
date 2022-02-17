using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enum;

namespace DalApi
{
    /// <summary>
    /// DAL layer interface - the data layer.
    /// </summary>
    public interface IDal
    {
        // In this layer:
        // All the functions that return lists - return all the existing objects in the data, even those that have been logically deleted.
        // And the return functions are individual - return only if the object is not deleted.


        #region Drone
        /// <summary>
        /// Adds a drone to the list of existing customers in the data.
        /// </summary>
        /// <param name="drone"></param>
        public void AddDrone(Drone drone);

        /// <summary>
        /// Returns a specific drone by drone ID only if active in the system
        /// </summary>
        /// <param name="idDrone"></param>
        /// <returns> The specific drone(active)</returns>
        public Drone GetDrone(int idDrone);

        /// <summary>
        /// Returns the list of existing drones in the data.
        /// </summary>
        /// <returns>existing drones in the data</returns>
        public IEnumerable<Drone> GetDrones();

        /// <summary>
        ///  Deletes a specific drone from the data.
        /// </summary>
        /// <param name="droneId"></param>
        public void DeleteDrone(int droneId);
        #endregion

        #region Base station
        /// <summary>
        /// Adds a station to the list of existing stations in the data.
        /// </summary>
        /// <param name="station"></param>
        public void AddBaseStation(BaseStation station);

        /// <summary>
        ///  Returns a specific sttion by station ID only if active in the system.
        /// </summary>
        /// <param name="idStation"></param>
        /// <returns>The specific station(active) </returns>
        public BaseStation GetBaseStation(int idStation);

        /// <summary>
        /// Returns the list of existing stations in the data.
        /// </summary>
        /// <returns>the list of existing stations in the data</returns>
        public IEnumerable<BaseStation> GetBaseStations();

        /// <summary>
        /// Returns the list of existing stations in the data that meet the predicate conditions.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>stations in the data that meet the predicate conditions</returns>
        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate);

        /// <summary>
        /// Returns the list of available free stations in the data.
        /// </summary>
        /// <returns>available free stations in the data</returns>
        public IEnumerable<BaseStation> GetAvaBaseStations();

        /// <summary>
        /// Deletes a specific station from the data.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBaseStation(int id);
        #endregion

        #region Drone charge
        /// <summary>
        /// Adds a drone charge to the list of existing customers in the data.
        /// </summary>
        /// <param name="droneCharge"></param>
        public void AddDroneCharge(DroneCharge droneCharge);

        /// <summary>
        /// Returns a specific drone charge by drone ID only if active in the system.
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>The specific drone charge(active</returns>
        public DroneCharge GetDroneCharge(int droneId);

        /// <summary>
        /// Returns the list of existing drone charges in the data
        /// </summary>
        /// <returns>existing drone charges in the data</returns>
        public IEnumerable<DroneCharge> GetDronesCharges();

        /// <summary>
        /// Returns the list of existing drone charges in the data that meet the predicate conditions.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>drone charges in the data that meet the predicate conditions</returns>
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate);

        /// <summary>
        /// Deletes a specific drone charge from the data.
        /// </summary>
        /// <param name="droneId"></param>
        public void DeleteDroneCharge(int droneId);

        /// <summary>
        /// Releases drone from charging.
        /// </summary>
        /// <param name="droneId"></param>
        void UpdateRelease(int droneId);

        /// <summary>
        /// Sends a drone for charging.
        /// </summary>
        /// <param name="droneId"></param>
        /// <param name="baseStationId"></param>
        public void UpdateCharge(int droneId, int baseStationId);
        #endregion

        #region Parcel
        /// <summary>
        /// Adds a parcel to the list of existing customers in the data.
        /// </summary>
        /// <param name="parcel"></param>
        public void AddParcel(Parcel parcel);

        /// <summary>
        /// Returns a specific parcel by parcel ID only if active in the system.
        /// </summary>
        /// <param name="idParcel"></param>
        /// <returns>The specific parcel</returns>
        public Parcel GetParcel(int idParcel);

        /// <summary>
        ///  Returns the list of existing parcels in the data.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns> existing prcels in the data</returns>
        public IEnumerable<Parcel> GetParcels();

        /// <summary>
        /// Returns the list of existing parcels in the data that meet the predicate conditions.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>parcels in the data that meet the predicate conditions</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate);

        /// <summary>
        /// Deletes a specific parcel from the data.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteParcel(int id);

        /// <summary>
        /// Updates and notes a drone for a parcel that should be sent to the destination.
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="droneId"></param>
        public void UpdateParcelScheduled(int parcelId, int droneId);

        /// <summary>
        /// Sends and updates the package assembly from the origin by the drone.
        /// </summary>
        /// <param name="parcelId"></param>
        public void UpdateParcelPickedUp(int parcelId);

        /// <summary>
        /// Updates the arrival of the parcel to the destination.
        /// </summary>
        /// <param name="parcelId"></param>
        public void UpdateParcelDelivered(int parcelId);
        #endregion

        #region Customer
        /// <summary>
        /// Adds a customer to the list of existing customers in the data.
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer);

        /// <summary>
        /// Returns a specific customer by customer ID only if active in the system.
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns>The specific customer(active) (active)</returns>
        public Customer GetCustomer(int idCustomer);

        /// <summary>
        /// Returns the list of existing stations in the data.
        /// </summary>
        /// <returns>existing customers in the data</returns>
        public IEnumerable<Customer> GetCustomers();

        /// <summary>
        /// Returns the list of existing customers in the data that meet the predicate conditions.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>customers in the data that meet the predicate conditions</returns>
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> predicate);

        /// <summary>
        /// Deletes a specific customer from the data.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteCustomer(int id);
        #endregion

        #region Electricity
        /// <summary>
        /// Returns an array that includes the rate at which a battery is wasted traveling from place to place,
        /// And the battery charge rate.
        /// </summary>
        /// <returns>array that includes the rate at which a battery is wasted and the battery charge rate</returns>
        public double[] GetElectricityUse();
        #endregion

        #region User
        /// <summary>
        /// Adds a User to the list of existing customers in the data.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user);

        /// <summary>
        /// Checks if a particular user exists in the system
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="access"></param>
        /// <returns>true if there is and false if not</returns>
        public bool ExistUser(int userName, string password, Access access);

        /// <summary>
        /// Returns a specific user by userId, password and access only if active in the system.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="access"></param>
        /// <returns>The specific user(active)</returns>
        public User GetUser(int userId, string password, Access access);

        /// <summary>
        ///  Returns the list of existing users in the data.
        /// </summary>
        /// <returns> existing users in the data</returns>
        public IEnumerable<User> GetUsers();

        /// <summary>
        ///  Returns the list of existing users in the data that meet the predicate conditions.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>xisting users in the data that meet the predicate conditions</returns>
        public IEnumerable<User> GetUsers(Predicate<User> predicate);

        /// <summary>
        /// Deletes a specific user from the data.
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User user);
        #endregion
    }
}
