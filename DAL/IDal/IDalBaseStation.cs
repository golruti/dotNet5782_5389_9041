using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public partial interface IDal
    {
        public void InsertStation(BaseStation station);
        public BaseStation GetStation(int idStation);
        public IEnumerable<BaseStation> GetBaseStations();
        public IEnumerable<BaseStation> GetBaseStations(Predicate<BaseStation> predicate);
        public IEnumerable<BaseStation> GetAvaBaseStations();
        void AddDroneCarge(int droneId, int baseStationId);
        void UpdateRelease(int droneId);
        public void DeleteBaseStation(int id);
    }
}
