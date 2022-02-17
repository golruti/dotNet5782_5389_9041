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
        /// <summary>
        /// Add a base station to the array of stations
        /// </summary>
        /// <param name="station">struct of station</param>

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddBaseStation(BaseStation station)
        {
            if (!GetBaseStation(station.Id).Equals(default(BaseStation)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a station - DAL");
            station.IsDeleted = false;
            DataSource.stations.Add(station);
        }


        //---------------------------------------------Show item----------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific station
        /// </summary>
        /// <param name="idStation"></param>
        /// <returns>station id</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int idStation)
        {
            BaseStation station = DataSource.stations.FirstOrDefault(station => station.Id == idStation && !(station.IsDeleted));
            return station;
        }

        //---------------------------------------------Show list----------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStations()
        {
            IEnumerable<BaseStation> baseStations = new List<BaseStation>();
            baseStations = DataSource.stations;
            return baseStations;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            IEnumerable<BaseStation> baseStations = new List<BaseStation>();
            baseStations = DataSource.stations.Where(station => predicate(station));
            return baseStations;
        }

        /// <summary>
        /// Display base stations with available charging stations
        /// </summary>
        /// <returns>array of stations</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetAvaBaseStations()
        {
            IEnumerable<BaseStation> baseStations = new List<BaseStation>();
            baseStations = DataSource.stations
                         .Where(station => station.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == station.Id));
            return baseStations;
        }
        //---------------------------------------------Delete--------------------------------------------------------------------------------------------
        /// delete base station from list
        /// </summary>
        /// <param name="id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int id)
        {
            BaseStation deletedStation = GetBaseStation(id);
            if (deletedStation.Equals(default(BaseStation)))
                throw new KeyNotFoundException("Delete station -DAL-: There is no suitable station in data");
            else
            {
                DataSource.stations.Remove(deletedStation);
                deletedStation.IsDeleted = true;
                DataSource.stations.Add(deletedStation);
            }
        }
    }
}
