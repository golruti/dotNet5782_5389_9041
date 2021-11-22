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



        //--------------------------------------------תחנות בסיס עם עמדות טעינה פתוחות-------------------------------------------------------------------------------------------
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


        private int numOfUsedChargingPorts(int idBaseStation)
        {
            int countUsedChargingPorts = 0;
            foreach (var BaseStation in dal.GetAvaBaseStations())
            {
                ++countUsedChargingPorts;
            }
            return countUsedChargingPorts;
        }

        //--------------------------------------------הוספת תחנת בסיס-------------------------------------------------------------------------------------------

        public void AddBaseStation(BaseStation tempBaseStation)
        {


            IDAL.DO.BaseStation baseStation = new IDAL.DO.BaseStation(tempBaseStation.Id, tempBaseStation.Name, tempBaseStation.Location.Longitude, tempBaseStation.Location.Latitude, tempBaseStation.AvailableChargingPorts);
            dal.InsertStation(baseStation);
        }


        public void UpdateBaseStation(int id, string name, int chargeSlote)
        {
            IDAL.DO.BaseStation tempBaseStation = dal.GetStation(id);
            //dal.DeleteBaseStation(id);
            IDAL.DO.BaseStation station = new IDAL.DO.BaseStation(id, name, tempBaseStation.Longitude, tempBaseStation.Latitude, chargeSlote);
            dal.InsertStation(station);
        }


        public BaseStation GetBLBaseStation(int baseStationId)
        {
            BaseStation newBaseStation = new BaseStation();
            IDAL.DO.BaseStation dataBaseStation = dal.GetStation(baseStationId);
            newBaseStation.Id = dataBaseStation.Id;
            newBaseStation.Name = dataBaseStation.Name;
            newBaseStation.Location.Latitude = dataBaseStation.Longitude;
            newBaseStation.Location.Longitude = dataBaseStation.Longitude;
            newBaseStation.AvailableChargingPorts = dataBaseStation.ChargeSlote - dal.CountFullChargeSlots(dataBaseStation.Id);
            newBaseStation.DronesInCharging = findDronesInCharing(dataBaseStation.Id);
            return newBaseStation;
        }
    }
}
