using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a station to the list of base stations
        /// </summary>
        /// <param name="tempBaseStation">The station for Adding</param>
        public void AddBaseStation(BaseStation tempBaseStation)
        {
            DO.BaseStation baseStation = new DO.BaseStation(tempBaseStation.Id, tempBaseStation.Name, tempBaseStation.Location.Longitude, tempBaseStation.Location.Latitude, tempBaseStation.AvailableChargingPorts);
            try
            {
                dal.InsertStation(baseStation);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        //---------------------------------------------Update ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// update base station 
        /// </summary>
        /// <param name="id">id of the base station</param>
        /// <param name="name">name of the base station</param>
        /// <param name="chargeSlote">sum of charge slote</param>
        public void UpdateBaseStation(int id, string name = "-1", int chargeSlote = -1)
        {
            DO.BaseStation tempBaseStation = dal.GetStation(id);
            dal.DeleteBaseStation(id);
            if (name == "-1")
            {
                name = tempBaseStation.Name;
            }
            if (chargeSlote == -1)
            {
                chargeSlote = tempBaseStation.ChargeSlote;
            }
            DO.BaseStation station = new DO.BaseStation(id, name, tempBaseStation.Longitude, tempBaseStation.Latitude, chargeSlote);
            dal.InsertStation(station);
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested base station from the data and converts it to BL base station
        /// </summary>
        /// <param name="id">The requested base station id</param>
        /// <returns>A Bl base station to print</returns>
        public BaseStation GetBLBaseStation(int id)
        {
            try
            {
                return mapBaseStation(dal.GetStation(id));
            }
            catch (ArgumentNullException ex)
            {

                throw new ArgumentNullException("Get base station -BL-" + ex.Message);
            }
        }

        /// <summary>
        /// Convert a DAL station to BL satation
        /// </summary>
        /// <param name="station">The sation to convert</param>
        /// <returns>The converted station</returns>
        private BaseStation mapBaseStation(DO.BaseStation station)
        {
            return new BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = new Location(station.Latitude, station.Longitude),
                AvailableChargingPorts = station.ChargeSlote - dal.CountFullChargeSlots(station.Id),
                DronesInCharging = dronesInCharging(station.Id)
            };
        }

        //---------------------------------------------Show list ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function returns the base station list from DAL to the BaseStationForList list
        /// </summary>
        /// <returns> The list of BaseStationForList stations </returns>
        public IEnumerable<BaseStationForList> GetBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            foreach (var baseStation in dal.GetBaseStations())
            {
                BaseStationsForList.Add(new BaseStationForList()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    AvailableChargingPorts = (baseStation.ChargeSlote) - dal.CountFullChargeSlots(baseStation.Id),
                    UsedChargingPorts = dal.CountFullChargeSlots(baseStation.Id)
                });
            }
            return BaseStationsForList;
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of BaseStationForList that maintain the predicate</returns>
        public IEnumerable<BaseStationForList> GetBaseStationForList(Predicate<BaseStationForList> predicate)
        {
            return GetBaseStationForList().Where(s => predicate(s));
        }

        public List<int> GetBaseStationForListsId()
        {
            List<int> baseStationsId=new();
            foreach( var item in GetBaseStationForList())
            {
                baseStationsId.Add(item.Id);
            }
            return baseStationsId;
        }
    }
}
