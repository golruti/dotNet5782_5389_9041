using BO;
using System;
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
        const int STEP = 2000;
        const int DELAY = 500;
        private const double TIME_STEP = DELAY / 1000.0;
        Func<bool> checkStop;

        public DroneSimulator(BL bl, int droneId, Action update, Func<bool> checkStop)
        {
            this.bl = bl;
            this.update = update;
            this.checkStop = checkStop;
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
                    //Connect a drone to the parcel.
                    bl.AssignParcelToDrone(drone.Id);
                }
                catch (Exception)
                {
                    //If no parcel suitable for the conditions of the drone has been found
                    //- the drone is sent for loading.
                    if (drone.Battery < 100)
                    {
                        try
                        {
                            bl.UpadateChargaSimulator(drone.Id);
                        }
                        catch(NoStationAvailableForCharging)
                        {
                            //If no station is found close to charging -
                            //the simulator will keep trying again and again until space becomes available.
                        }
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

            //The process of "driving" the drone  to the station.
            while (distance > 0.001)
            {
                if (checkStop())
                    return;
                if (!sleepDelayTime())
                    break;
                CalculateLocationAndBattary(bl.available, station.Location);
                distance = bl.distance(drone.Location.Latitude, station.Location.Latitude, drone.Location.Longitude, station.Location.Longitude);
                update();
            }

            //The process of loading the drone as long as it is not loaded to the maximum.
            while (drone.Battery < 100)
            {
                if (checkStop())
                    return;
                if (!sleepDelayTime())
                    break;
                lock (bl) drone.Battery = Math.Min(100.0, drone.Battery + bl.chargingRate * TIME_STEP);
                update();
            }
            bl.UpdateRelease(drone.Id);
        }







        private void DeliveryDrone()
        {
            //The process of "driving" the drone  to the customer of origin.
            senderAndTarget = bl.createParcelInTransfer(drone.ParcelDeliveredId);
            distance = bl.distance(drone.Location.Latitude, senderAndTarget.SenderLocation.Latitude,
                drone.Location.Longitude, senderAndTarget.SenderLocation.Longitude);
            while (distance > 0.003)
            {
                if (checkStop())
                    return;
                if (!sleepDelayTime())
                    break;
                CalculateLocationAndBattary(bl.available, senderAndTarget.SenderLocation);
                distance = bl.distance(drone.Location.Latitude, senderAndTarget.SenderLocation.Latitude, drone.Location.Longitude, senderAndTarget.SenderLocation.Longitude);
                update();
            }


            //Collect the package from the customer of origin
            parcel = bl.GetBLParcel(senderAndTarget.Id);
            if (parcel.PickedUp == null)
                bl.ParcelCollection(drone.Id);

            distance = bl.distance(drone.Location.Latitude, senderAndTarget.TargetLocation.Latitude,
                 drone.Location.Longitude, senderAndTarget.TargetLocation.Longitude);
            double elec = senderAndTarget.Weight == WeightCategories.Heavy ? bl.heavyWeight : senderAndTarget.Weight == WeightCategories.Light ? bl.lightWeight : bl.mediumWeight;

            //The process of "driving" the drone to the target.
            while (distance > 0.005)
            {
                if (checkStop())
                    return;
                if (!sleepDelayTime())
                    break;
                CalculateLocationAndBattary(elec, senderAndTarget.TargetLocation);
                distance = bl.distance(drone.Location.Latitude, senderAndTarget.TargetLocation.Latitude, drone.Location.Longitude, senderAndTarget.TargetLocation.Longitude);
                update();
            }

            //The drone reached its destination
            bl.UpdateParcelDelivered(drone.Id);
        }








        /// <summary>
        /// Calculates and moves at any moment the battery and the exact location of the drone.
        /// </summary>
        /// <param name="elecricity"></param>
        /// <param name="targetLocation"></param>
        private void CalculateLocationAndBattary(double elecricity, Location targetLocation)
        {
            double delta = distance < STEP ? distance : STEP;
            double proportion = delta / distance;
            drone.Battery = Math.Max(0.0, drone.Battery - delta * elecricity);
            double lat = drone.Location.Latitude + (targetLocation.Latitude - drone.Location.Latitude) * proportion;
            double lon = drone.Location.Longitude + (targetLocation.Longitude - drone.Location.Longitude) * proportion;
            lock (bl) drone.Location = new() { Latitude = lat, Longitude = lon };
            var x = bl.GetBLDrone(drone.Id);
        }

        /// <summary>
        /// Stay time for display.
        /// </summary>
        /// <returns></returns>
        private static bool sleepDelayTime()
        {
            try { Thread.Sleep(DELAY); } catch (ThreadInterruptedException) { return false; }
            return true;
        }
    }
}
