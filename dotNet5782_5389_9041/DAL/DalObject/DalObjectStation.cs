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
            if (!GetBaseStation(station.Id).GetType().Equals(default))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a station - DAL");
            station.IsDeleted = false;
            DataSource.stations.Add(station.Id, station);
        }

        //---------------------------------------------Delete--------------------------------------------------------------------------------------------
        /// delete base station from list
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBaseStation(int id)
        {
            if (!DataSource.stations.Remove(id))
                throw new KeyNotFoundException("Delete station -DAL-: There is no suitable station in data");

            var deletedStation = GetBaseStation(id);
            DataSource.stations.Remove(deletedStation.Id);
            deletedStation.IsDeleted = true;
            DataSource.stations.Add(deletedStation.Id, deletedStation);
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific station
        /// </summary>
        /// <param name="idStation"></param>
        /// <returns>station id</returns>
        public BaseStation GetBaseStation(int idStation)
        {
            BaseStation station = DataSource.stations.Values.FirstOrDefault(station => station.Id == idStation && !(station.IsDeleted));
            if (station.GetType().GetType().Equals(default))
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
            return DataSource.stations.Values.Where(station => !(station.IsDeleted)).ToList();
        }

        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            return DataSource.stations.Values.Where
                (station => predicate(station) && !(station.IsDeleted)).ToList();
        }

        /// <summary>
        /// Display base stations with available charging stations
        /// </summary>
        /// <returns>array of stations</returns>
        public IEnumerable<BaseStation> GetAvaBaseStations()
        {
            return DataSource.stations.Values
                         .Where(station => station.ChargeSlote > DataSource.droneCharges.Values.Count(dc => dc.StationId == station.Id && !(station.IsDeleted)))
                         .ToList();
        }

    }
}
