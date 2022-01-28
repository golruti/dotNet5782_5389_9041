using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXML
    {
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            DalFactory.GetDal().AddDroneCharge(new DroneCharge(droneCharge.DroneId, droneCharge.StationId));
        }
        public DroneCharge GetDroneCharge(int droneId)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            throw new NotImplementedException();

        }
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate)
        {
            throw new NotImplementedException();
        }
        public void DeleteDroneCharge(int droneId)
        {
            throw new NotImplementedException();
        }

       

    }
}
