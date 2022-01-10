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
            if (DataSource.drones.ContainsKey(drone.Id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone - DAL");
            DataSource.drones.Add(drone.Id, drone);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        public Drone GetDrone(int id)
        {
            if (!DataSource.drones.ContainsKey(id))
                throw new KeyNotFoundException("Get customer -DAL-: There is no suitable customer in data");
            return DataSource.drones[id];
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        public IEnumerable<Drone> GetDrones()
        {
            return DataSource.drones.Values.ToList();
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Drone that maintain the predicate</returns>
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate)
        {
            return DataSource.drones.Values.Where(drone => predicate(drone)).ToList();
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific drone
        /// </summary>
        /// <param name="droneId">drone ID</param>
        public void DeleteDrone(int id)
        {
            if (!DataSource.drones.Remove(id))
                throw new KeyNotFoundException("Delete drone -DAL-: There is no suitable customer in data");
        }
    }
}
