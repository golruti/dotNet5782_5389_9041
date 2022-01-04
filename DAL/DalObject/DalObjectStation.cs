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
        /// Add a base station to the array of stations
        /// </summary>
        /// <param name="station">struct of station</param>
        public void AddBaseStation(BaseStation station)
        {
            if (DataSource.stations.ContainsKey(station.Id))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a station - DAL");
            DataSource.stations.Add(station.Id, station);
        }

        //---------------------------------------------Update--------------------------------------------------------------------------------------------
        /// delete base station from list
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBaseStation(int id)
        {
            if (!DataSource.stations.Remove(id))
                throw new KeyNotFoundException("Delete station -DAL-: There is no suitable station in data");
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific station
        /// </summary>
        /// <param name="idStation"></param>
        /// <returns>station id</returns>
        public BaseStation GetBaseStation(int idStation)
        {
            BaseStation station = DataSource.stations.Values.FirstOrDefault(station => station.Id == idStation);
            if (station.GetType().Equals(default))
                throw new KeyNotFoundException("Get station -DAL-: There is no suitable customer in data");
            return station;
        }

        //---------------------------------------------Show list----------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<BaseStation> GetBaseStations()
        {
            return DataSource.stations.Values.ToList();
        }

        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            return DataSource.stations.Values.Where(station => predicate(station)).ToList();
        }


        /// <summary>
        /// Display base stations with available charging stations
        /// </summary>
        /// <returns>array of stations</returns>
        public IEnumerable<BaseStation> GetAvaBaseStations()
        {
            return DataSource.stations.Values
                         .Where(s => s.ChargeSlote > DataSource.droneCharges.Values.Count(dc => dc.StationId == s.Id))
                         .ToList();
        }

    }
}
