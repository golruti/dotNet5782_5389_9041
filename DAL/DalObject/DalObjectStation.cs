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


        //------------------------------------------Display------------------------------------------
        /// <summary>
        /// Removes a station from an array of stations by id
        /// </summary>
        /// <param name="idxStation">struct of station</param>
        /// <returns>base station</returns>
        public BaseStation GetStation(int idStation)
        {
            BaseStation station = DataSource.stations.First(station => station.Id == idStation);
            if (station.GetType().Equals(default))
                throw new Exception("Get station -DAL-: There is no suitable customer in data");
            return station;
        }



        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<BaseStation> GetBaseStations()
        {

            return DataSource.stations.Select(station => station.Clone()).ToList();
        }


        /// <summary>
        /// Display base stations with available charging stations
        /// </summary>
        /// <returns>array of stations</returns>
        /// //●	הצגת  תחנות-בסיס עם עמדות טעינה פנויות
        /// //לבדוק אם הפונקציה מתאימה לליסטים
        public IEnumerable<BaseStation> GetAvaBaseStations()
        {
            return DataSource.stations
                         .Where(s => s.ChargeSlote > DataSource.droneCharges.Count(dc => dc.StationId == s.Id))
                         .ToList();
        }
    }
}
