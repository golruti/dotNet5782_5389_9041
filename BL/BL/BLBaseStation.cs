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

        

        public BaseStationForList GetBaseStationFromList(int id)
        {
            List<BaseStationForList> BaseStationsForList = (List<BaseStationForList>)GetBaseStationForList();
            return BaseStationsForList.First(BaseStationForList =>BaseStationForList.Id  == id);
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
        public BaseStation GetBaseStation(int id)
        {
            IDAL.DO.BaseStation baseStation = dal.GetStation(id);
            BaseStation tempBaseStation(baseStation.Id,baseStation.Name, baseStation.Longitude, baseStation.latitude, baseStation.ChargeSlote);
            return tempBaseStation;
        }

        //כמה עמדות טעינה פנויות
        //public int SeveralAvailablechargingStations(int id)
        //{
        //    BaseStation baseStation = GetBaseStation(id);
        //    int sum = 0;
        //    foreach (var item in drones)
        //    {
        //        if ((int)item.Status == 2 && item.Location.Latitude == baseStation.Location.Latitude && item.Location.Longitude == baseStation.Location.Longitude)
        //        {
        //            ++sum;
        //        }
        //    }

        //    return sum;
        //}
    }
}
