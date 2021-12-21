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
            if (!(uniqueIDTaxCheck(DataSource.droneCharges, droneCharge.DroneId)))
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone charge - DAL");
            }
            DataSource.droneCharges.Add(droneCharge);
        }


        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        // <summary>
        /// Sending a drone for charging at a base station By changing the drone mode and adding a record of a drone battery charging entity
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns if the base station is available to receive the glider</returns>
        public void UpdateCharge(int droneId)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default(Drone)))
                throw new KeyNotFoundException("Get drone -DAL-: There is no suitable customer in data"); ;
            var station = DataSource.stations.FirstOrDefault(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id));
            if (station.Equals(default(BaseStation)))
                throw new KeyNotFoundException("Get drone -DAL-: There is no suitable customer in data");

            DroneCharge droneCharge = new DroneCharge(droneId, station.Id);
            try
            {
                AddDroneCharge(droneCharge);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Release drone from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother drone released from charging</returns>
        public void DeleteDroneCharge(int droneId)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default(Drone)))
                throw new Exception("Get drone -DAL-: There is no suitable customer in data");
            DataSource.droneCharges.Remove(GetDroneCharge(droneId));
        }

        /// <summary>
        /// update release
        /// </summary>
        /// <param name="id"></param>
        public void UpdateRelease(int id)
        {
            DataSource.droneCharges.RemoveAll(item => id == item.DroneId);
        }
        //---------------------------------------------Show item-----------------------------------------------------------------------------------------
        /// The function returns a specific drone charge
        /// </summary>
        /// <param name="droneId">Drone ID</param>
        /// <returns>The specific drone charge</returns>
        public DroneCharge GetDroneCharge(int droneId)
        {
            var droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId);
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
            return DataSource.droneCharges.ToList();
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of DroneCharge that maintain the predicate</returns>
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate)
        {
            return DataSource.droneCharges.Where(droneCharge => predicate(droneCharge)).ToList();
        }
    }
}
