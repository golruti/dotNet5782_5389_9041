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
        //------------------------------------------Add------------------------------------------
        /// <summary>
        /// Add a drone charge to the list of drones charge
        /// </summary>
        /// </summary>
        /// <param name="droneCharge">The drone charge for Adding</param>
        public void InsertDroneCharge(DroneCharge droneCharge)
        {           
            if (!(uniqueIDTaxCheck(DataSource.droneCharges, droneCharge.DroneId)))
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a Drone Charge - DAL");
            }
            DataSource.droneCharges.Add(droneCharge);
        }

        // <summary>
        /// Sending a drone for charging at a base station By changing the drone mode and adding a record of a drone battery charging entity
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns if the base station is available to receive the glider</returns>
        public void TryAddDroneCarge(int droneId)
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
                InsertDroneCharge(droneCharge);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw ex;
            }
        }

        //---------------------------------------------Return item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns a specific drone charge
        /// </summary>
        /// <param name="droneId">Drone ID</param>
        /// <returns>The specific drone charge</returns>
        public DroneCharge GetDroneCharge(int droneId)
        {
            var droneCharge = DataSource.droneCharges.First(dc => dc.DroneId == droneId);
            if (droneCharge.GetType().Equals(default))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Get drone -DAL-: There is no suitable customer in data");
            return droneCharge;
        }

        //---------------------------------------------Return list----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns the drones charge list
        /// </summary>
        /// <returns>The drones charge list</returns>
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            return DataSource.droneCharges.Select(drone => drone.Clone()).ToList();
        }


        /// <summary>
        /// Release drone from charging at base station
        /// </summary>
        /// <param name="droneId">Id of the drone</param>
        /// <returns>Returns the mother drone released from charging</returns>
        public void TryRemoveDroneCarge(int droneId)
        {
            var drone = DataSource.drones.FirstOrDefault(d => d.Id == droneId);
            if (drone.Equals(default(Drone)))
                throw new Exception("Get drone -DAL-: There is no suitable customer in data");
            DataSource.droneCharges.Remove(GetDroneCharge(droneId));
        }


        /// <summary>
        /// The function returns the list of indexes to skimmers that are charging at a particular station
        /// </summary>
        /// <param name="baseStationId"></param>
        /// <returns>The Index list</returns>
        public List<int> GetDronechargingInStation(int baseStationId)
        {
            List<int> list = new List<int>();
            foreach (var item in DataSource.droneCharges)
            {
                if (item.StationId == baseStationId)
                    list.Add(item.DroneId);
            }
            return list;
        }

        /// <summary>
        /// The function returns how many stations are occupied at a particular station
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The number of stations occupied</returns>
        public int CountFullChargeSlots(int id)
        {
            int count = 0;
            foreach (DroneCharge item in DataSource.droneCharges)
            {
                if (item.StationId == id)
                    ++count;
            }
            return count;
        }


        /// <summary>
        /// update release
        /// </summary>
        /// <param name="id"></param>
        public void UpdateRelease(int id)
        {          
            DataSource.droneCharges.RemoveAll(item=>id == item.DroneId);
        }
    }
}
