using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    internal partial class DalXml
    {
        //--------------------------------------------Adding--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddDroneCharge(DroneCharge droneCharge)
        {
            var station1 = GetBaseStation(23);

            if (!GetDroneCharge(droneCharge.DroneId).Equals(default(DroneCharge)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a drone charge - DAL");
            droneCharge.IsDeleted = false;

            AddItem(droneChargesPath, droneCharge);
           var station =GetBaseStation(23);

        }

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        public void UpdateCharge(int droneId, int baseStationId)
        {
            var drone = GetDrone(droneId);
            if (drone.Equals(default(DroneCharge)))
                throw new KeyNotFoundException("Get drone charge -DAL-: There is no suitable drone charge in data"); ;
            var station = GetBaseStation(baseStationId);
            if (station.Equals(default(BaseStation)))
                throw new KeyNotFoundException("Get station -DAL-: There is no suitable station in data");

            DroneCharge droneCharge = new DroneCharge() { DroneId = droneId, StationId = station.Id, Time = DateTime.Now, IsDeleted = false };
            AddDroneCharge(droneCharge);
            DeleteBaseStation(station.Id);
            --station.AvailableChargingPorts;         
            AddBaseStation(station);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateRelease(int droneId)
        {
            var station = GetBaseStation(GetDroneCharge(droneId).StationId);
            DeleteDroneCharge(droneId);
            DeleteBaseStation(station.Id);
            station.AvailableChargingPorts++;
            AddBaseStation(station);
        }

        //---------------------------------------------Show item-----------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneCharge GetDroneCharge(int droneId)
        {
            return GetList<DroneCharge>(droneChargesPath).FirstOrDefault(charge => charge.DroneId == droneId && charge.IsDeleted ==false);
        }

        //---------------------------------------------Show list-----------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronesCharges()
        {
            return GetList<DroneCharge>(droneChargesPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<DroneCharge> GetDronesCharges(Predicate<DroneCharge> predicate)
        {

            return GetList<DroneCharge>(droneChargesPath).Where(item => predicate(item));
        }


        //--------------------------------------------Delete--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteDroneCharge(int droneId)
        {
            DroneCharge deletedDroneCharge = GetDroneCharge(droneId);
            if (deletedDroneCharge.Equals(default(DroneCharge)))
                throw new KeyNotFoundException("Delete drone charge -DAL-: There is no suitable drone charge in data");

            XDocument document = XDocument.Load(droneChargesPath);

            document.Root
                    .Elements()
                    .Single(item => !bool.Parse(item.Element(nameof(DroneCharge.IsDeleted)).Value) && droneId == int.Parse(item.Element("DroneId").Value))
                    .SetElementValue(nameof(DroneCharge.IsDeleted), true);

            document.Save(droneChargesPath);
        }

    }
}
