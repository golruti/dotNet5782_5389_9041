using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
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
            IDAL.DO.BaseStation baseStation = new IDAL.DO.BaseStation(tempBaseStation.Id, tempBaseStation.Name, tempBaseStation.Location.Longitude, tempBaseStation.Location.Latitude, tempBaseStation.AvailableChargingPorts);
            try
            {
                dal.InsertStation(baseStation);
            }
            catch (IDAL.DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
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
        private BaseStation mapBaseStation(IDAL.DO.BaseStation station)
        {
            return new BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = new Location(station.Latitude, station.Longitude),
                AvailableChargingPorts = station.ChargeSlote - dal.CountFullChargeSlots(station.Id),
                DronesInCharging = DronesInCharging(station.Id)
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
                    AvailableChargingPorts = (baseStation.ChargeSlote) - numOfUsedChargingPorts(baseStation.Id),
                    UsedChargingPorts = numOfUsedChargingPorts(baseStation.Id)
                });
            }
            return BaseStationsForList;
        }

        /// <summary>
        /// The function return the base stations with available charging stations 
        /// </summary>
        /// <returns> List of Free base stations</returns>
        public IEnumerable<BaseStationForList> GetAvaBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            foreach (var baseStation in dal.GetAvaBaseStations())
            {
                BaseStationsForList.Add(new BaseStationForList()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    AvailableChargingPorts = (baseStation.ChargeSlote) - numOfUsedChargingPorts(baseStation.Id),
                    UsedChargingPorts = numOfUsedChargingPorts(baseStation.Id)
                });
            }
            return BaseStationsForList;
        }

        /// <summary>
        /// The function returns the number of charging stations occupied at a particular base station
        /// </summary>
        /// <param name="idBaseStation"> Station ID</param>
        /// <returns> The num of used charging ports</returns>
        private int numOfUsedChargingPorts(int idBaseStation)
        {
            int countUsedChargingPorts = 0;
            foreach (var BaseStation in dal.GetAvaBaseStations())
            {
                ++countUsedChargingPorts;
            }
            return countUsedChargingPorts;
        }


        //public void UpdateBaseStation(int id, string name, int chargeSlote)
        //{
        //    IDAL.DO.BaseStation tempBaseStation = dal.GetStation(id);
        //    dal.DeleteBaseStation(id);
        //    IDAL.DO.BaseStation station = new IDAL.DO.BaseStation(id, name, tempBaseStation.Longitude, tempBaseStation.Latitude, chargeSlote);
        //    dal.InsertStation(station);
        //}

    }
}
