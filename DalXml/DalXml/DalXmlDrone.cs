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
        //--------------------------------------------Adding---------------------------------------------------------------------------------------------
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone GetDrone(int droneId)
        {
            return GetItem<Drone>(dronesPath, droneId);
        }

        //--------------------------------------------Show list--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones()
        {
            return GetList<Drone>(dronesPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Drone> GetDrones(Predicate<Drone> predicate)
        {
            return GetDrones().Where(item => predicate(item));
        }

        //--------------------------------------------Delete--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDrone(int id)
        {
            if (GetDrone(id).Equals(default(Drone)))
                throw new KeyNotFoundException("Delete drone -DAL: There is no suitable drone in data");

            UpdateItem(dronesPath, id, nameof(Drone.IsDeleted), true);
        }
    }
}
