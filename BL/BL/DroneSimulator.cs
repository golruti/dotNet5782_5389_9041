using BO;
using System;
using System.Collections.Generic;
using System.Threading;
using static BO.Enums;

namespace BL
{
    internal class DroneSimulator
    {
        Drone drone;
        BaseStation station;
        Parcel parcel;
        Action update;
        private BL bl;
        double distance;
        const int STEP = 2;
        const int DELAY = 500;
        private const double TIME_STEP = DELAY / 1000.0;

        public DroneSimulator(BL bl, int droneId, Action update, Func<bool> checkStop)
        {
            this.bl = bl;
            this.update = update;
            drone = bl.GetBLDrone(droneId);

            while (!checkStop())
            {
                //bl.UpdateDroneModel(drone.Id, "M-" + DateTime.Now.Second);
                switch (drone.Status)
                {
                    case DroneStatuses.Available:
                        AvaibleDrone();
                        break;
                    case DroneStatuses.Maintenance:
                        MaintenanceDrone();
                        break;
                    case DroneStatuses.Delivery:
                        DeliveryDrone();
                        break;
                    default:
                        break;
                }
                update();
                //Thread.Sleep(5000);
            }
        }
        private void AvaibleDrone()
        {
            lock (bl)
            {
                try
                {
                    bl.AssignParcelToDrone(drone.Id);
                }
                catch (KeyNotFoundException)
                {
                    if (drone.Battery < 100)
                    {
                        drone.Status = DroneStatuses.Maintenance;
                    }
                }
            }
        }

        private void MaintenanceDrone()
        {
            lock (bl)
            {
                station = bl.mapBaseStation(bl.nearestBaseStation(drone.Location.Longitude, drone.Location.Latitude));
                distance = bl.distance(drone.Location.Latitude, station.Location.Latitude, drone.Location.Longitude, station.Location.Longitude);
            }

            while (distance > 0.001)
            {
                if (!sleepDelayTime()) break;
                CalculateLocationAndBattary(bl.available, station.Location);
                distance = bl.distance(drone.Location.Latitude, station.Location.Latitude, drone.Location.Longitude, station.Location.Longitude);

                update();
            }
            while (drone.Battery < 100)
            {
                if (!sleepDelayTime()) break;
                lock (bl) drone.Battery = Math.Min(1.0, drone.Battery + bl.chargingRate * TIME_STEP);
                update();
            }
            bl.UpdateRelease(drone.Id);
            station = null;
        }

        private void DeliveryDrone()
        {
            distance = bl.distance(drone.Location.Latitude, drone.Delivery.SenderLocation.Latitude,
                drone.Location.Longitude, drone.Delivery.SenderLocation.Longitude);
            while (distance > 0.003)
            {
                if (!sleepDelayTime()) break;
                CalculateLocationAndBattary(bl.available, station.Location);
                distance = bl.distance(drone.Location.Latitude, drone.Delivery.TargetLocation.Latitude, drone.Location.Longitude, drone.Delivery.TargetLocation.Longitude);

                update();
            }

            parcel = bl.GetBLParcel(drone.Delivery.Id);
            if (parcel.PickedUp.Equals(null))
                bl.ParcelCollection(drone.Id);

            distance = bl.distance(drone.Location.Latitude, drone.Delivery.TargetLocation.Latitude,
                 drone.Location.Longitude, drone.Delivery.TargetLocation.Longitude);
            double elec = drone.Delivery.Weight == WeightCategories.Heavy ? bl.heavyWeight : drone.Delivery.Weight == WeightCategories.Light ? bl.lightWeight : bl.mediumWeight;
            while (distance > 0.005)
            {
                if (!sleepDelayTime()) break;
                CalculateLocationAndBattary(elec, station.Location);
                distance = bl.distance(drone.Location.Latitude, drone.Delivery.TargetLocation.Latitude, drone.Location.Longitude, drone.Delivery.TargetLocation.Longitude);
                update();
            }
            bl.UpdateParcelDelivered(drone.Id);
        }

        private void CalculateLocationAndBattary(double elecricity, Location targetLocation)
        {
            double delta = distance < STEP ? distance : STEP;
            double proportion = delta / distance;

            drone.Battery = Math.Max(0.0, drone.Battery - delta * elecricity);
            double lat = drone.Location.Latitude + (targetLocation.Latitude - drone.Location.Latitude) * proportion;
            double lon = drone.Location.Longitude + (targetLocation.Longitude - drone.Location.Longitude) * proportion;
            lock (bl) drone.Location = new() { Latitude = lat, Longitude = lon };
        }
        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
    }
}
