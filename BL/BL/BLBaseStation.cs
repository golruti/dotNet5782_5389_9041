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
        //--------------------------------------------Adding---------------------------------------------------------------------------------------------
        public void AddBaseStation(BaseStation tempBaseStation)
        {
            try
            {
                lock (dal)
                {
                    dal.AddBaseStation(new DO.BaseStation()
                    {
                        Id = tempBaseStation.Id,
                        Name = tempBaseStation.Name,
                        Longitude = Math.Round(tempBaseStation.Location.Longitude),
                        Latitude = Math.Round(tempBaseStation.Location.Latitude),
                        ChargeSlote = tempBaseStation.AvailableChargingPorts + tempBaseStation.DronesInCharging.Count(),
                        AvailableChargingPorts = tempBaseStation.AvailableChargingPorts,
                        IsDeleted = false
                    });
                }
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }
        }

        //---------------------------------------------Update -----------------------------------------------------------------------------------------
        public void UpdateBaseStation(int id, string name = "-1", int chargeSlote = -1)
        {
            DO.BaseStation tempBaseStation;
            try
            {
                lock (dal)
                {
                    tempBaseStation = dal.GetBaseStation(id);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get base station -BL-" + ex.Message, ex);
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

            try
            {
                lock (dal)
                {
                    dal.AddBaseStation(new DO.BaseStation()
                    {
                        Id = id,
                        Name = name,
                        Longitude = tempBaseStation.Longitude,
                        Latitude = tempBaseStation.Latitude,
                        ChargeSlote = chargeSlote,
                        AvailableChargingPorts = GetDronesInCharging(tempBaseStation.Id).Count() + tempBaseStation.AvailableChargingPorts,
                        IsDeleted = false
                    });
                }
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }
        }

        //---------------------------------------------Show item--------------------------------------------------------------------------------------------
        public BaseStation GetBLBaseStation(int id)
        {
            try
            {
                lock (dal)
                {
                    return mapBaseStation(dal.GetBaseStation(id));
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get base station -BL-" + ex.Message, ex);
            }
        }

        //---------------------------------------------Show list -------------------------------------------------------------------------------------------
        public IEnumerable<BaseStationForList> GetBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            lock (dal)
            {
                foreach (var baseStation in dal.GetBaseStations(s => s.IsDeleted == false))
                {
                    BaseStationsForList.Add(new BaseStationForList()
                    {
                        Id = baseStation.Id,
                        Name = baseStation.Name,
                        AvailableChargingPorts = baseStation.AvailableChargingPorts,
                        UsedChargingPorts = baseStation.ChargeSlote - baseStation.AvailableChargingPorts
                    });
                }
            }

            if (BaseStationsForList.Count() == 0)
                return Enumerable.Empty<BaseStationForList>();
            var t = BaseStationsForList;
            return BaseStationsForList;

        }


        public IEnumerable<BaseStationForList> GetBaseStationForList(Predicate<BaseStationForList> predicate)
        {
            return GetBaseStationForList().Where(station => predicate(station));
        }


        public IEnumerable<BaseStationForList> GetAvaBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            lock (dal)
            {
                foreach (var baseStation in dal.GetAvaBaseStations().Where(s => s.IsDeleted == false))
                {
                    BaseStationsForList.Add(new BaseStationForList()
                    {
                        Id = baseStation.Id,
                        Name = baseStation.Name,
                        AvailableChargingPorts = baseStation.ChargeSlote - countFullChargeSlots(baseStation.Id),
                        UsedChargingPorts = countFullChargeSlots(baseStation.Id)
                    });
                }
            }

            if (BaseStationsForList.Count() == 0)
                return Enumerable.Empty<BaseStationForList>();
            return BaseStationsForList;
        }



        //---------------------------------------------Delete ------------------------------------------------------------------------------------------
        public void deleteBLBaseStation(int stationId)
        {
            //Delete drone in charge if any
            BaseStation station;
            IEnumerable<int> dronesIds;
            lock (dal) { station = mapBaseStation(dal.GetBaseStation(stationId)); }
            lock (dal) { dronesIds = station.DronesInCharging.Select(d => d.Id); }
            lock (dal) { dronesIds.ToList().ForEach(id => UpdateRelease(id)); }

            try
            {
                lock (dal)
                {
                    dal.DeleteBaseStation(stationId);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Delete base station -BL-" + ex.Message, ex);
            }
        }



        //--------------------------------------------Initialize the parcel list---------------------------------------------------------------------
        /// <summary>
        /// Convert a DAL station to BL satation
        /// </summary>
        /// <param name="station">The sation to convert</param>
        /// <returns>The converted station</returns>
        internal BaseStation mapBaseStation(DO.BaseStation station)
        {
            return new BaseStation()
            {
                Id = station.Id,
                Name = station.Name,
                Location = new Location() { Latitude = Math.Round(station.Latitude), Longitude = Math.Round(station.Longitude) },
                AvailableChargingPorts = station.AvailableChargingPorts,
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
            IEnumerable<DO.DroneCharge> droneCharges;
            lock (dal)
            {
                droneCharges = dal.GetDronesCharges(droneCharge => droneCharge.StationId == stationId && droneCharge.IsDeleted == false);
            }
            return droneCharges.Count();
        }
    }
}
