using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXml
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddBaseStation(BaseStation station)
        {
            if (!GetBaseStation(station.Id).Equals(default(BaseStation)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a station - DAL");
            station.IsDeleted = false;

            AddItem(baseStationsPath, station);
        }


        //---------------------------------------------Show item----------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation GetBaseStation(int idStation)
        {
            return GetItem<BaseStation>(baseStationsPath, idStation);
        }

        //---------------------------------------------Show list----------------------------------------------------------------------------------------
        /// <returns>array of station</returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStations()
        {         
            return GetList<BaseStation>(baseStationsPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate)
        {
            return GetBaseStations().Where(item => predicate(item));
        }

 
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<BaseStation> GetAvaBaseStations()
        {
            return GetBaseStations(station => station.ChargeSlote > GetDronesCharges(charge => charge.StationId == station.Id).Count());
        }

        //---------------------------------------------Delete--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteBaseStation(int id)
        {
            BaseStation deletedStation = GetBaseStation(id);
            if (deletedStation.Equals(default(BaseStation)))
                throw new KeyNotFoundException("Delete station -DAL-: There is no suitable station in data");

            UpdateItem(baseStationsPath, id, nameof(BaseStation.IsDeleted), true);
        }
    }
}