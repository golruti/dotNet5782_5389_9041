using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using Customer = BO.Customer;
using Drone = BO.Drone;
using Parcel = BO.Parcel;
using BaseStation = BO.BaseStation;
using User = BO.User;
using static BO.Enums;

namespace BlApi
{
    /// <summary>
    /// BL layer interface - the logic layer.
    /// </summary>
    public interface IBL
    {
        // In this layer:
        //All the functions - return only if the object is not deleted.
        #region Drone Simulator
        /// <summary>
        /// Runs the simulator.
        /// </summary>
        /// <param name="droneId">drone ID number</param>
        /// <param name="update">function for updating the changes</param>
        /// <param name="checkStop">function for stopping the simulator process</param>
        public void StartDroneSimulator(int droneId, Action update, Func<bool> checkStop);
        #endregion

        #region Drone
        /// <summary>
        /// Adds a drone to the list of existing customers in the data.
        /// </summary>
        /// <param name="tempDrone"></param>
        /// <param name="stationId"></param>
        void AddDrone(Drone tempDrone, int stationId = -1);

        /// <summary>
        /// Returns a specific drone by drone ID only if active in the system
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The specific drone(active)</returns>
        public Drone GetBLDrone(int id);

        /// <summary>
        /// Returns the list of active drones and converts them to type "DroneForList"
        /// </summary>
        /// <returns> The "DroneForList" list</returns>
        public IEnumerable<DroneForList> GetDroneForList();

        /// <summary>
        ///  Returns the list of active drones that meet the predicate conditions and converts them to type "DroneForList"
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>active drones that meet the predicate conditions and converts them to type "DroneForList"</returns>
        public IEnumerable<DroneForList> GetDroneForList(Predicate<DroneForList> predicate);

        /// <summary>
        ///  Deletes a specific drone from the data.
        /// </summary>
        /// <param name="droneId"></param>
        public void DeleteBLDrone(int droneId);

        /// <summary>
        /// Updates a model of drone identified by a drone identification number.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        public void UpdateDroneModel(int id, string model);
        #endregion

        #region Base Station
        /// <summary>
        /// Adds a station to the list of existing stations in the data.
        /// </summary>
        /// <param name="tempBaseStation"></param>
        public void AddBaseStation(BaseStation tempBaseStation);

        /// <summary>
        ///  Returns a specific sttion by station ID only if active in the system.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The specific station(active) </returns>
        public BaseStation GetBLBaseStation(int id);

        /// <summary>
        /// Returns the list of active stations and converts them to type "StationForList"
        /// </summary>
        /// <returns> The "StationForList" list</returns>
        public IEnumerable<BaseStationForList> GetBaseStationForList();

        /// <summary>
        ///  Returns the list of active stations that meet the predicate conditions and converts them to type "StationForList"
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>active stations that meet the predicate conditions and converts them to type "StationForList"</returns>
        public IEnumerable<BaseStationForList> GetBaseStationForList(Predicate<BaseStationForList> predicate);

        /// <summary>
        /// Returns the list of active available stations and converts them to type "StationForList"
        /// </summary>
        /// <returns> The available "StationForList" list</returns>
        public IEnumerable<BaseStationForList> GetAvaBaseStationForList();

        /// <summary>
        ///  Deletes a specific station.
        /// </summary>
        /// <param name="stationId"></param>
        public void deleteBLBaseStation(int stationId);

        /// <summary>
        /// Updates a model and/ or charge Slotes of a station by station ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="chargeSlote"></param>
        public void UpdateBaseStation(int id, string name, int chargeSlote);
        #endregion

        #region Drone In Charge
        /// <summary>
        /// Returns the list of active drone charges and converts them to type "DroneInCharge"
        /// </summary>
        /// <returns>active drone charges and converts them to type "DroneInCharge" </returns>
        IEnumerable<DroneInCharging> GetDronesInCharging();

        /// <summary>
        /// Returns the list of active drone charges on a particular station identified by station ID
        /// and converts them to "DroneInCharge" type
        /// </summary>
        /// <returns>active drone charges on a particular station identified by station ID 
        /// and converts them to type "DroneInCharge" </returns>
        public IEnumerable<DroneInCharging> GetDronesInCharging(int stationId);

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
        void UpdateCharge(int droneId);
        #endregion

        #region Parcel
        /// <summary>
        /// Adds a parcel to the list of existing customers in the data.
        /// </summary>
        /// <param name="tempParcel"></param>
        public void AddParcel(Parcel tempParcel);

        /// <summary>
        /// Returns a specific parcel by parcel ID only if active in the system.
        /// </summary>
        /// <param name="idParcel"></param>
        /// <returns>The specific parcel</returns>
        public Parcel GetBLParcel(int parcelId);

        /// <summary>
        /// Returns the list of active parcels and converts them to type "ParcelForList"
        /// </summary>
        /// <returns> The "ParcelForList" list</returns>
        public IEnumerable<ParcelForList> GetParcelForList();

        /// <summary>
        ///  Returns the list of active parcels that meet the predicate conditions and converts them to type "ParcelForList"
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>active parcels that meet the predicate conditions and converts them to type "ParcelForList"</returns>
        public IEnumerable<ParcelForList> GetParcelForList(Predicate<ParcelForList> predicate);

        /// <summary>
        /// Deletes a specific parcel from the data.
        /// </summary>
        /// <param name="id"></param>
        public void deleteBLParcel(int parcelId);

        /// <summary>
        /// Updates and notes a drone for a parcel that should be sent to the destination.
        /// </summary>
        /// <param name="droneId"></param>
        void AssignParcelToDrone(int droneId);

        /// <summary>
        /// Sends and updates the package assembly from the origin by the drone.
        /// </summary>
        /// <param name="parcelId"></param>
        public void ParcelCollection(int Id);


        /// <summary>
        /// Updates the arrival of the parcel to the destination.
        /// </summary>
        /// <param name="droneId"></param>
        public void UpdateParcelDelivered(int droneId);

        /// <summary>
        /// The function returns the status of the parcel according to the drone to which it is associated
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
        public Enums.ParcelStatuses GetParcelStatusByDrone(int droneId);
        #endregion

        #region Customer
        /// <summary>
        /// Adds a customer to the list of existing customers in the data.
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer tempCustomer);

        /// <summary>
        /// Returns a specific customer by customer ID only if active in the system.
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <returns>The specific customer(active) (active)</returns>
        public Customer GetBLCustomer(int id);

        /// <summary>
        /// Returns the list of active customers and converts them to type "GetCustomerForList"
        /// </summary>
        /// <returns> The "GetCustomerForList" list</returns>
        public IEnumerable<CustomerForList> GetCustomerForList();

        /// <summary>
        ///  Returns the list of active customers that meet the predicate conditions and converts them to type "CustomerForList"
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>active customers that meet the predicate conditions and converts them to type "CustomerForList"</returns>
        public IEnumerable<CustomerForList> GetCustomerForList(Predicate<CustomerForList> predicate);

        /// <summary>
        /// Updates name and cell phone number for a customer identified by a customer ID.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        void UpdateCustomer(int customerId, string name, string phone);

        /// <summary>
        /// Deletes a specific customer from the data.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBLCustomer(int customerId);
        #endregion

        #region User
        /// <summary>
        /// Adds a User to the list of existing customers in the data.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User tempUser);

        /// <summary>
        /// Checks if a client user exists in the system
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>true if there is and false if not </returns>
        public bool IsExistClient(int userId, string password);


        /// <summary>
        /// Checks if a emloyee user exists in the system
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>true if there is and false if not </returns>
        public bool IsExistEmployee(int userId, string password);

        /// <summary>
        /// Returns a specific user by userId, password and access only if active in the system.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <param name="access"></param>
        /// <returns>The specific user(active)</returns>
        public User GetUser(int userName, string password, Access access);

        /// <summary>
        /// Deletes a specific user from the data.
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(User tempUser);
        #endregion
    }
}