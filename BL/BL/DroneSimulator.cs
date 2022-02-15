using BO;
using System;
using System.Collections.Generic;
using System.Threading;
using static BO.Enums;

namespace BL
{
    internal class DroneSimulator
    {
        DroneForList drone;
        BaseStation station;
        Parcel parcel;

        ParcelByTransfer senderAndTarget;
        Action update;
        private BL bl;
        double distance;
        const int STEP = 2000;/*2*/
        const int DELAY = 500;/*500*/
        private const double TIME_STEP = DELAY / 1000.0;/*1000.0*/

        public DroneSimulator(BL bl, int droneId, Action update, Func<bool> checkStop)
        {
            this.bl = bl;
            this.update = update;
            drone = bl.GetBLDroneInList(droneId);


            while (!checkStop())
            {
                try
                {
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
                }
                catch (Exception)
                {
                    break;
                }
                update();
                Thread.Sleep(1000);
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
                catch (Exception)
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
            //צריך לשלוח את הרחפן לטעינה דרך הפונקציה- כדי ליצור דרון צארג
            //drone.Status = DroneStatuses.Available;
            bl.UpdateChargeSimulator(drone.Id);
            while (drone.Battery < 100)
            {
                if (!sleepDelayTime()) break;
                lock (bl) drone.Battery = Math.Min(100.0, drone.Battery + bl.chargingRate * TIME_STEP);
                update();

            }


            
            bl.UpdateRelease(drone.Id);
            station = null;
        }

        private void DeliveryDrone()
        {
            senderAndTarget = bl.createParcelInTransfer(drone.ParcelDeliveredId);
            distance = bl.distance(drone.Location.Latitude, senderAndTarget.SenderLocation.Latitude,
                drone.Location.Longitude, senderAndTarget.SenderLocation.Longitude);
            while (distance > 0.003)
            {
                if (!sleepDelayTime()) break;
                CalculateLocationAndBattary(bl.available, senderAndTarget.SenderLocation);
                distance = bl.distance(drone.Location.Latitude, senderAndTarget.SenderLocation.Latitude, drone.Location.Longitude, senderAndTarget.SenderLocation.Longitude);
                var t = senderAndTarget.SenderLocation;

                update();
            }

            parcel = bl.GetBLParcel(senderAndTarget.Id);
            if (parcel.PickedUp.Equals(null))
                bl.ParcelCollection(drone.Id);

            distance = bl.distance(drone.Location.Latitude, senderAndTarget.TargetLocation.Latitude,
                 drone.Location.Longitude, senderAndTarget.TargetLocation.Longitude);
            double elec = senderAndTarget.Weight == WeightCategories.Heavy ? bl.heavyWeight : senderAndTarget.Weight == WeightCategories.Light ? bl.lightWeight : bl.mediumWeight;
            while (distance > 0.005)
            {
                if (!sleepDelayTime()) break;
                CalculateLocationAndBattary(elec, senderAndTarget.TargetLocation);
                var t = bl.GetParcelForList();
                distance = bl.distance(drone.Location.Latitude, senderAndTarget.TargetLocation.Latitude, drone.Location.Longitude,senderAndTarget.TargetLocation.Longitude);
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
