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

        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a base station to the array of stations
        /// </summary>
        /// <param name="station">struct of station</param>
        public void InsertStation(BaseStation station)
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

            DataSource.stations.Remove(GetStation(id));
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------
        /// <returns>base station</returns>
        public BaseStation GetStation(int idStation)
        {
            BaseStation station = DataSource.stations.First(station => station.Id == idStation);
            if (station.GetType().Equals(default))
                throw new Exception("Get station -DAL-: There is no suitable customer in data");
            return station;
        }

        //---------------------------------------------Show list----------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<BaseStation> GetBaseStations()
        {
            return DataSource.stations.Select(station => station.Clone());
        }

        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            return DataSource.stations.Where(station => predicate(station)).ToList();
        }
    }
}
