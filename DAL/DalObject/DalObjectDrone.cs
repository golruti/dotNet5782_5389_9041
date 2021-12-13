using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a drone to the array of existing drones
        /// </summary>
        /// <param name="drone">struct of drone</param>
        public void InsertDrone(Drone drone)
        {
            if (!(uniqueIDTaxCheck(DataSource.drones, drone.Id)))
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone - DAL");
            }
            DataSource.drones.Add(drone);
        }

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific drone
        /// </summary>
        /// <param name="droneId">drone ID</param>
        public void DeleteDrone(int droneId)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default(Drone)))
                throw new Exception("Delete drone -DAL-: There is no suitable customer in data");
            DataSource.drones.Remove(drone);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int idDrone)
        {
            Drone drone = DataSource.drones.First(drone => drone.Id == idDrone);
            if (drone.GetType().Equals(default))
                throw new Exception("Get drone -DAL-: There is no suitable customer in data");
            return drone;
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        public IEnumerable<Drone> GetDrones()
        {
            return DataSource.drones.Select(drone => drone.Clone()).ToList();
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Drone that maintain the predicate</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate)
        {
            return DataSource.drones.Where(drone => predicate(drone)).ToList();
        }
    }
}
