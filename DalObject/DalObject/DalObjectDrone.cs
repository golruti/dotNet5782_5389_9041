using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDrone(Drone drone)
        {
            if (!GetDrone(drone.Id).Equals(default(Drone)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone - DAL");
            drone.IsDeleted = false;
            DataSource.drones.Add(drone);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            Drone drone = DataSource.drones.FirstOrDefault(drone => (drone.Id == droneId) && !(drone.IsDeleted));
            return drone;
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones()
        {
            IEnumerable<Drone> drones = new List<Drone>();
            drones= DataSource.drones;

            return drones;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate)
        {
            IEnumerable<Drone> drones = new List<Drone>();
            drones = DataSource.drones.Where(drone => predicate(drone) );
            return drones;
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
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
