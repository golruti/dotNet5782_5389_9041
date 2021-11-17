using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class  BL
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


        private int numOfUsedChargingPorts(int idBaseStation)
        {
            int countUsedChargingPorts = 0;
            foreach (var BaseStation in dal.GetAvaBaseStations())
            {
                ++countUsedChargingPorts;
            }
            return countUsedChargingPorts;
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

        public void AddBaseStation(int id, string name, double longitude, double latitude, int chargingStations)
        {
            BO.BaseStation tempBaseStation = new BO.BaseStation(id, name, longitude, latitude, chargingStations);

            IDAL.DO.BaseStation station = new IDAL.DO.BaseStation(tempBaseStation.Id, tempBaseStation.Name, tempBaseStation.Location.Longitude, tempBaseStation.Location.Latitude, chargingStations);
            dal.InsertStation(station);

        }
    }
}
