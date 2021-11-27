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

        //--------------------------------------------הוספת תחנת בסיס-------------------------------------------------------------------------------------------
        public void AddBaseStation(BaseStation tempBaseStation)
        {
            IDAL.DO.BaseStation baseStation = new IDAL.DO.BaseStation(tempBaseStation.Id, tempBaseStation.Name, tempBaseStation.Location.Longitude, tempBaseStation.Location.Latitude, tempBaseStation.AvailableChargingPorts);
            dal.InsertStation(baseStation);
        }


        //---------------------------------------------הצגת תחנת בסיס לפי ID ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public BaseStation GetBLBaseStation(int id)
        { 
                return mapBaseStation(dal.GetStation(id));
        }

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


        //---------------------------------------------הצגת רשימת תחנות בסיס לרשימה ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public IEnumerable<BaseStationForList> GetBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            foreach (var baseStation in dal.GetBaseStations())
            {
                BaseStationsForList.Add(new BaseStationForList()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    AvailableChargingPorts = numOfUsedChargingPorts(baseStation.Id),
                    UsedChargingPorts = (baseStation.ChargeSlote) - numOfUsedChargingPorts(baseStation.Id)
                });
            }
            return BaseStationsForList;
        }

        //--------------------------------------------רשימת תחנות בסיס עם עמדות טעינה פנויות-------------------------------------------------------------------------------------------
        public IEnumerable<BaseStationForList> GetAvaBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            foreach (var baseStation in dal.GetAvaBaseStations())
            {
                BaseStationsForList.Add(new BaseStationForList()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    AvailableChargingPorts = numOfUsedChargingPorts(baseStation.Id),
                    UsedChargingPorts = (baseStation.ChargeSlote) - numOfUsedChargingPorts(baseStation.Id)
                });
            }
            return BaseStationsForList;
        }

        //מספר תחנות פנויות
        private int numOfUsedChargingPorts(int idBaseStation)
        {
            int countUsedChargingPorts = 0;
            foreach (var BaseStation in dal.GetAvaBaseStations())
            {
                ++countUsedChargingPorts;
            }
            return countUsedChargingPorts;
        }


        //public void AddBaseStation(BaseStation tempBaseStation)
        //{
            

        //public void UpdateBaseStation(int id, string name, int chargeSlote)
        //{
        //    IDAL.DO.BaseStation tempBaseStation = dal.GetStation(id);
        //    dal.DeleteBaseStation(id);
        //    IDAL.DO.BaseStation station = new IDAL.DO.BaseStation(id, name, tempBaseStation.Longitude, tempBaseStation.Latitude, chargeSlote);
        //    dal.InsertStation(station);
        //}
        //public BaseStation GetBaseStation(int id)
        //{
        //    IDAL.DO.BaseStation baseStation = dal.GetStation(id);
        //    BaseStation tempBaseStation(baseStation.Id, baseStation.Name, baseStation.Longitude, baseStation.latitude, baseStation.ChargeSlote);
        //    return tempBaseStation;
        //}


        //    return sum;
        //}
    }
}
