using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXml
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a drone to the array of existing drones
        /// </summary>
        /// <param name="drone">struct of drone</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone)
        {
            if (!GetDrone(drone.Id).Equals(default(Drone)))
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone - DAL");
            }

            drone.IsDeleted = false;
            AddItem(dronesPath, drone);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Removes a drone from an array of drones by id
        /// </summary>
        /// <param name="idxDrone">struct of drone</param>
        /// <returns>drone</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            return GetItem<Drone>(dronesPath, droneId);
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing drones
        /// </summary>
        /// <returns>array of drones</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones()
        {
            return GetList<Drone>(dronesPath);
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Drone that maintain the predicate</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate)
        {
            return GetDrones().Where(item => predicate(item));
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific drone
        /// </summary>
        /// <param name="droneId">drone ID</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            if (GetDrone(id).Equals(default(Drone)))
                throw new KeyNotFoundException("Delete drone -DAL: There is no suitable drone in data");

            UpdateItem(dronesPath, id, nameof(Drone.IsDeleted), true);
        }
    }
}
