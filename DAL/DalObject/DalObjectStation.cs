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
            if (!(uniqueIDTaxCheck(DataSource.stations, station.Id)))
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a station - DAL");
            }
            DataSource.stations.Add(station);
        }

        //---------------------------------------------Update--------------------------------------------------------------------------------------------
        /// delete base station from list
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBaseStation(int id)
        {
            var station = DataSource.stations.FirstOrDefault(s => s.Id == id);
            if (station.Equals(default(BaseStation)))
                throw new KeyNotFoundException("Delete station -DAL-: There is no suitable station in data");
            DataSource.stations.Remove(station);
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------
        /// <returns>base station</returns>
        public BaseStation GetBaseStation(int idStation)
        {
            BaseStation station = DataSource.stations.FirstOrDefault(station => station.Id == idStation);
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
            return DataSource.stations.ToList();
        }

        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            return DataSource.stations.Where(station => predicate(station)).ToList();
        }
    }
}
