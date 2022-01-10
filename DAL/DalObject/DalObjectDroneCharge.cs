using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DAL
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a drone charge to the list of drones charge
        /// </summary>
        /// </summary>
        /// <param name="droneCharge">The drone charge for Adding</param>
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            if (!GetDroneCharge(droneCharge.DroneId).GetType().Equals(default))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone - DAL");
            droneCharge.IsDeleted = false;
            DataSource.droneCharges.Add(droneCharge.StationId, droneCharge);
        }

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        // <summary>
        /// Sending a drone for charging at a base station By changing the drone mode and adding a record of a drone battery charging entity
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns if the base station is available to receive the glider</returns>
        public void UpdateCharge(int droneId)
        {
            var drone = GetDrone(droneId);
            if (drone.GetType().Equals(default))
                throw new KeyNotFoundException("Get drone -DAL-: There is no suitable customer in data"); ;
            var station = GetBaseStations().FirstOrDefault(s => s.ChargeSlote > GetDronesCharges().Count(dc => dc.StationId == s.Id));
            if (station.GetType().Equals(default))
                throw new KeyNotFoundException("Get drone -DAL-: There is no suitable customer in data");

            DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
            AddDroneCharge(droneCharge);
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// Release drone from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother drone released from charging</returns>
        public void DeleteDroneCharge(int droneId)
        {
            if (!DataSource.droneCharges.Remove(droneId))
                throw new KeyNotFoundException("Delete drone charge -DAL-: There is no suitable drone charge in data");

            var deletedDroneCharge = GetDroneCharge(droneId);
            DataSource.droneCharges.Remove(deletedDroneCharge.DroneId);
            deletedDroneCharge.IsDeleted = true;
            DataSource.droneCharges.Add(deletedDroneCharge.DroneId, deletedDroneCharge);
        }

        /// <summary>
        /// update release
        /// </summary>
        /// <param name="id"></param>
        public void UpdateRelease(int id)
        {
            DeleteDroneCharge(id);
        }

        //---------------------------------------------Show item-----------------------------------------------------------------------------------------
        /// The function returns a specific drone charge
        /// </summary>
        /// <param name="droneId">Drone ID</param>
        /// <returns>The specific drone charge</returns>
        public DroneCharge GetDroneCharge(int droneId)
        {
            var droneCharge = DataSource.droneCharges.Values.FirstOrDefault(dc => dc.DroneId == droneId && !(dc.IsDeleted));
            if (droneCharge.GetType().Equals(default))
                throw new KeyNotFoundException("Get drone -DAL-: There is no suitable customer in data");
            return droneCharge;
        }

        //---------------------------------------------Show list--------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns the drones charge list
        /// </summary>
        /// <returns>The drones charge list</returns>
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            return DataSource.droneCharges.Values.Where(droneCharge => !(droneCharge.IsDeleted)).ToList();
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of DroneCharge that maintain the predicate</returns>
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate)
        {
            return DataSource.droneCharges.Values.Where
                (droneCharge => predicate(droneCharge) && !(droneCharge.IsDeleted)).ToList();
        }
    }
}
