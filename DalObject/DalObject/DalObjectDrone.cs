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
        /// Add a drone to the array of existing drones
        /// </summary>
        /// <param name="drone">struct of drone</param>
        public void AddDrone(Drone drone)
        {
            if (!GetDrone(drone.Id).Equals(default(Drone)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone - DAL");
            drone.IsDeleted = false;
            DataSource.drones.Add(drone);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int droneId)
        {
            Drone drone = DataSource.drones.FirstOrDefault(drone => (drone.Id == droneId) && !(drone.IsDeleted));
            return drone;
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        public IEnumerable<Drone> GetDrones()
        {
            IEnumerable<Drone> drones = new List<Drone>();
            drones= DataSource.drones;

            return drones;
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Drone that maintain the predicate</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate)
        {
            IEnumerable<Drone> drones = new List<Drone>();
            drones = DataSource.drones.Where(drone => predicate(drone) );
            return drones;
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific drone
        /// </summary>
        /// <param name="droneId">drone ID</param>
        public void DeleteDrone(int id)
        {
            Drone deletedDrone = GetDrone(id);
            if(deletedDrone.Equals(default(Drone)))
                throw new KeyNotFoundException("Delete drone -DAL: There is no suitable customer in data");
            else
            {
                DataSource.drones.Remove(deletedDrone);
                deletedDrone.IsDeleted = true;
                DataSource.drones.Add(deletedDrone);
            }
        }
    }
}
