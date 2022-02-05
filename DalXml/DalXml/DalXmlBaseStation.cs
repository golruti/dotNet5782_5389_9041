using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXML
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a base station to the array of stations
        /// </summary>
        /// <param name="station">struct of station</param>
        public void AddBaseStation(BaseStation station)
        {
            if (!GetBaseStation(station.Id).Equals(default(BaseStation)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a station - DAL");
            station.IsDeleted = false;

            AddItem(baseStationsPath, station);
        }


        //---------------------------------------------Show item----------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a specific station
        /// </summary>
        /// <param name="idStation"></param>
        /// <returns>station id</returns>
        public BaseStation GetBaseStation(int idStation)
        {
            return GetItem<BaseStation>(baseStationsPath, idStation);
        }

        //---------------------------------------------Show list----------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing stations
        /// </summary>
        /// <returns>array of station</returns>
        public IEnumerable<BaseStation> GetBaseStations()
        {
            return GetList<BaseStation>(baseStationsPath);
        }

        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            return GetBaseStations().Where(item => predicate(item));
        }

        /// <summary>
        /// Display base stations with available charging stations
        /// </summary>
        /// <returns>array of stations</returns>
        public IEnumerable<BaseStation> GetAvaBaseStations()
        {
            return GetBaseStations(station => station.ChargeSlote > GetDronesCharges(charge => charge.StationId == station.Id).Count());
        }

        //---------------------------------------------Delete--------------------------------------------------------------------------------------------
        /// delete base station from list
        /// </summary>
        /// <param name="id"></param>
        public void DeleteBaseStation(int id)
        {
            BaseStation deletedStation = GetBaseStation(id);
            if (deletedStation.Equals(default(BaseStation)))
                throw new KeyNotFoundException("Delete station -DAL-: There is no suitable station in data");

            UpdateItem(baseStationsPath, id, nameof(BaseStation.IsDeleted), true);
        }
    }