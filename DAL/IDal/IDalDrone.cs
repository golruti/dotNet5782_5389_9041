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

        public void InsertDrone(Drone drone);
        public Drone GetDrone(int idDrone);
        public IEnumerable<Drone> GetDrones();
        public void TryAddDroneCarge(int droneId);
        public void TryRemoveDroneCarge(int droneId);
        public void UpdateParcelScheduled(int idxParcel);
        public void DeleteDrone(int id);

    }
}
