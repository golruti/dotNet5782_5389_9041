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
                dal.AddBaseStation(baseStation);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }
        //---------------------------------------------Delete ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function gets a station ID and tries to delete it
        /// </summary>
        /// <param name="station"></param>
        public void deleteBLBaseStation(int stationId)
        {
            try
            {
                dal.DeleteBaseStation(stationId);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Delete base station -BL-" + ex.Message);
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
            DO.BaseStation tempBaseStation;
            try
            {
                tempBaseStation = dal.GetBaseStation(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get base station -BL-" + ex.Message);
            }


            if (name == "-1")
            {
                name = tempBaseStation.Name;
            }
            if (chargeSlote == -1)
            {
                chargeSlote = tempBaseStation.ChargeSlote;
            }
            else
            {
                if (chargeSlote < countFullChargeSlots(id))
                    throw new ArithmeticException("Excess number of stations -BL-");
            }
            deleteBLBaseStation(id);
            DO.BaseStation station = new DO.BaseStation(id, name, tempBaseStation.Longitude, tempBaseStation.Latitude, chargeSlote);
            try
            {
                dal.AddBaseStation(station);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
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
                return mapBaseStation(dal.GetBaseStation(id));
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get base station -BL-" + ex.Message);
            }
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
                    AvailableChargingPorts = (baseStation.ChargeSlote) - countFullChargeSlots(baseStation.Id),
                    UsedChargingPorts = countFullChargeSlots(baseStation.Id)
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
            return GetBaseStationForList().Where(station => predicate(station));
        }

        /// <summary>
        /// Returns the stations with with free charging stations
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<BaseStationForList> GetAvaBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            foreach (var baseStation in dal.GetAvaBaseStations())
            {
                BaseStationsForList.Add(new BaseStationForList()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    AvailableChargingPorts = (baseStation.ChargeSlote) - countFullChargeSlots(baseStation.Id),
                    UsedChargingPorts = countFullChargeSlots(baseStation.Id)
                });
            }
            return BaseStationsForList;
        }

        //--------------------------------------------Initialize the parcel list--------------------------------------------------------

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
                AvailableChargingPorts = station.ChargeSlote - countFullChargeSlots(station.Id),
                DronesInCharging = GetDronesInCharging(station.Id)
            };
        }

        /// <summary>
        /// The function returns the amount of charging stations occupied at a particular station
        /// </summary>
        /// <param name="stationId"></param>
        /// <returns></returns>
        private int countFullChargeSlots(int stationId)
        {
            IEnumerable<DO.DroneCharge> droneCharges = dal.GetDronesCharges(droneCharge => droneCharge.StationId == stationId);
            return droneCharges.Count();
        }
    }
}
