using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DAL
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            if (!GetDroneCharge(droneCharge.DroneId).Equals(default(DroneCharge)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone charge - DAL");
            droneCharge.IsDeleted = false;
            DataSource.droneCharges.Add( droneCharge);
        }

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCharge(int droneId, int baseStationId)
        {
            var drone = GetDrone(droneId);
            if (drone.Equals(default(DroneCharge)))
                throw new KeyNotFoundException("Get drone charge -DAL-: There is no suitable drone charge in data"); ;
            var station = GetBaseStation(baseStationId);
            if (station.Equals(default(BaseStation)))
                throw new KeyNotFoundException("Get station -DAL-: There is no suitable station in data");

            DroneCharge droneCharge = new DroneCharge() { DroneId = droneId, StationId = station.Id, Time = DateTime.Now, IsDeleted = false };
            DeleteBaseStation(station.Id);
            station.AvailableChargingPorts--;
            AddBaseStation(station);
            AddDroneCharge(droneCharge);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateRelease(int droneId)
        {
            DeleteDroneCharge(droneId);

            var station = GetBaseStation(GetDroneCharge(droneId).StationId);
            DeleteBaseStation(station.Id);
            station.AvailableChargingPorts++;
            AddBaseStation(station);
        }

        //---------------------------------------------Show item-----------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneId)
        {
            DroneCharge droneCharge = DataSource.droneCharges.FirstOrDefault(dc => dc.DroneId == droneId && !(dc.IsDeleted));
            return droneCharge;
        }

        //---------------------------------------------Show list--------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            IEnumerable<DroneCharge> droneCharges = new List<DroneCharge>();
            droneCharges = DataSource.droneCharges;
            return droneCharges;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate)
        {
            IEnumerable<DroneCharge> droneCharges = new List<DroneCharge>();
            droneCharges = DataSource.droneCharges.Where
                (droneCharge => predicate(droneCharge) );
            return droneCharges;
        }


        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(int droneId)
        {
            DroneCharge deletedDroneCharge = GetDroneCharge(droneId);
            if (deletedDroneCharge.Equals(default(DroneCharge)))
                throw new KeyNotFoundException("Delete drone charge -DAL-: There is no suitable drone charge in data");
            else
            {
                DataSource.droneCharges.Remove(deletedDroneCharge);
                deletedDroneCharge.IsDeleted = true;
                DataSource.droneCharges.Add(deletedDroneCharge);
            }
        }
    }
}
