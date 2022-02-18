using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using static BO.Enums;

namespace BL
{
    partial class BL
    {


        //--------------------------------------------Simulator----------------------------------------------------------------------------------------

        public void StartDroneSimulator(int droneId, Action update, Func<bool> checkStop)
        {
            new DroneSimulator(this, droneId, update, checkStop);
        }



        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        public void AddDrone(Drone tempDrone, int stationId = -1)
        {

            //dal list
            try
            {

                lock (dal)
                {
                    dal.AddDrone(new DO.Drone()
                    {
                        Id = tempDrone.Id,
                        MaxWeight = (DO.Enum.WeightCategories)tempDrone.MaxWeight,
                        Model = tempDrone.Model,
                        IsDeleted = false
                    });
                }
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }

            //local list
            try
            {
                drones.Add(new DroneForList()
                {
                    Id = tempDrone.Id,
                    Model = tempDrone.Model,
                    MaxWeight = tempDrone.MaxWeight,
                    Battery = tempDrone.Battery,
                    Status = tempDrone.Status,
                    Location = new Location() { Latitude = Math.Round(tempDrone.Location.Longitude), Longitude = Math.Round(tempDrone.Location.Latitude) },
                    ParcelDeliveredId = -1
                });
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }

            //If the drone was inserted as a drone in the charge - update the charge
            if (tempDrone.Status == Enums.DroneStatuses.Maintenance)
            {
                BaseStation station;
                DO.BaseStation dalStation;
                if (stationId == -1)
                {
                    station = mapBaseStation(nearestBaseStation(tempDrone.Location.Longitude, tempDrone.Location.Latitude));
                }
                else
                {
                    lock (dal) { station = mapBaseStation(dal.GetBaseStation(stationId)); }
                }

                lock (dal) { dalStation = dal.GetBaseStation(station.Id); }
                lock (dal) { dal.DeleteBaseStation(dalStation.Id); }
                dalStation.AvailableChargingPorts--;
                lock (dal) { dal.AddBaseStation(dalStation); }

                lock (dal)
                {
                    if (dal.GetDroneCharge(tempDrone.Id).Equals(default(DO.DroneCharge)))
                    {
                        try
                        {
                            dal.AddDroneCharge(new DO.DroneCharge()
                            {
                                DroneId = tempDrone.Id,
                                StationId = station.Id,
                                Time = DateTime.Now,
                                IsDeleted = false
                            });
                        }
                        catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
                        {
                            throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
                        }
                    }
                }
            }
        }

        //---------------------------------------------Delete ---------------------------------------------------------------------------------------------------------
        public void DeleteBLDrone(int droneId)
        {
            //DAL list
            var deleteDrone = GetBLDroneInList(droneId);
            if (deleteDrone.Equals(default(DroneForList)))
                throw new ArgumentNullException("delete drone -BL-:There is not drone with same id i data");
            try
            {
                lock (dal)
                {
                    dal.DeleteDrone(droneId);
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Delete drone -BL-" + ex.Message, ex);
            }
            //local list
            drones.Remove(deleteDrone);

            // Release charge if the drone was charging
            if (deleteDrone.Status == Enums.DroneStatuses.Maintenance)
            {
                UpdateRelease(droneId);
            }
        }

        //---------------------------------------------Show item-------------------------------------------------------------------------------------------------------
        public Drone GetBLDrone(int id)
        {
            try
            {
                return mapDrone(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get drone -BL-" + ex.Message, ex);
            }
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------------------
        public IEnumerable<DroneForList> GetDroneForList()
        {
            var s = drones;
            return drones;
        }

        public IEnumerable<DroneForList> GetDroneForList(Predicate<DroneForList> predicate)
        {
            return GetDroneForList().Where(s => predicate(s));
        }


        //--------------------------------------------Update-----------------------------------------------------------------------------------------------------------
        public void UpdateDroneModel(int id, string model)
        {
            DroneForList tempDroneForList = drones.FirstOrDefault(drone => drone.Id == id);
            if (tempDroneForList == null)
                throw new ArgumentNullException("update drone -BL-:There is not drone with same id i data");

            //update DAL list
            try
            {
                lock (dal) { dal.DeleteDrone(id); }
                lock (dal)
                {
                    dal.AddDrone(new DO.Drone()
                    {
                        Id = tempDroneForList.Id,
                        MaxWeight = (DO.Enum.WeightCategories)tempDroneForList.MaxWeight,
                        Model = tempDroneForList.Model,
                        IsDeleted = false
                    });
                }
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message, ex);
            }

            //update local list
            tempDroneForList.Model = model;
        }


        public void UpdateCharge(int droneId)
        {
            DroneForList tempDrone = GetBLDroneInList(droneId);
            int baseStationId = -1;
            Location location = new Location() { };
            double distance = double.MaxValue;
            IEnumerable<DO.BaseStation> availableStations;
            lock (dal) { availableStations = dal.GetAvaBaseStations().Where(s => s.IsDeleted == false); }


            if (tempDrone.Status == DroneStatuses.Available)
            {
                foreach (var item in availableStations)
                {
                    double tempDistance = this.distance(tempDrone.Location.Latitude, item.Latitude, tempDrone.Location.Longitude, item.Longitude);
                    if (tempDistance < distance)
                    {
                        baseStationId = item.Id;
                        distance = tempDistance;
                        location = new Location() { Longitude = item.Longitude, Latitude = item.Latitude };
                    }
                }
                if (minBattery(tempDrone.Location, location, tempDrone.Status, tempDrone.MaxWeight) < tempDrone.Battery)
                {
                    UpdateDroneStatus(droneId, DroneStatuses.Maintenance, tempDrone.Battery - minBattery(tempDrone.Location, location, tempDrone.Status, tempDrone.MaxWeight), GetBLBaseStation(baseStationId).Location.Latitude, GetBLBaseStation(baseStationId).Location.Latitude);
                    tempDrone.Location = GetBLBaseStation(baseStationId).Location;
                    try
                    {
                        lock (dal) { dal.UpdateCharge(droneId, baseStationId); }
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException(ex.Message, ex);
                    }
                }
                else
                {
                    throw new ArgumentNullException("the drone not have enough battery  -BL-");
                }
            }
            else
            {
                throw new ArgumentNullException("the drone not available -BL-");
            }
        }


        /// <summary>
        /// Sends drone for charging - with changes in logic for drone simulator.
        /// </summary>
        /// <param name="droneId"></param>
        internal void UpadateChargaSimulator(int droneId)
        {
            DroneForList tempDrone = GetBLDroneInList(droneId);
            int baseStationId = -1;
            Location location = new Location() { };
            double distance = double.MaxValue;
            IEnumerable<DO.BaseStation> availableStations;
            lock (dal) { availableStations = dal.GetAvaBaseStations().Where(s => s.IsDeleted == false); }

            if (tempDrone.Status == DroneStatuses.Available)
            {
                foreach (var item in availableStations)
                {
                    double tempDistance = this.distance(tempDrone.Location.Latitude, item.Latitude, tempDrone.Location.Longitude, item.Longitude);
                    if (tempDistance < distance)
                    {
                        baseStationId = item.Id;
                        distance = tempDistance;
                        location = new Location() { Longitude = item.Longitude, Latitude = item.Latitude };
                    }
                }
                if (minBattery(tempDrone.Location, location, tempDrone.Status, tempDrone.MaxWeight) < tempDrone.Battery)
                {
                    UpdateDroneStatus(droneId, DroneStatuses.Maintenance, tempDrone.Battery - minBattery(tempDrone.Location, location, tempDrone.Status, tempDrone.MaxWeight), tempDrone.Location.Latitude, tempDrone.Location.Latitude);
                    try
                    {
                        lock (dal) { dal.UpdateCharge(droneId, baseStationId); }
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException(ex.Message, ex);
                    }
                }
                else
                {
                    throw new NoStationAvailableForCharging("the drone not have enough battery  -BL-");
                }
            }
            else
            {
                throw new ArgumentNullException("the drone not available -BL-");
            }
        }


        public void UpdateRelease(int droneId)
        {
            DroneForList drone = drones.FirstOrDefault(drone => drone.Id == droneId);
            if (!drone.Equals(default(DroneForList)))
            {
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    TimeSpan time = (TimeSpan)(DateTime.Now - GetDroneInChargByID(droneId).Time);
                    double totalTime = drone.Battery + batteryCalculationInCharging(time.Seconds);
                    drone.Battery = totalTime < 100 ? totalTime : 100;
                    drone.Status = DroneStatuses.Available;
                    try
                    {
                        lock (dal) { dal.UpdateRelease(droneId); }
                    }
                    catch (KeyNotFoundException ex)
                    {
                        throw new KeyNotFoundException(ex.Message, ex);
                    }
                }
                else
                {
                    throw new ArgumentNullException("the drone not maintenance -BL-");
                }
            }
            else
            {
                throw new KeyNotFoundException("the drone not found -BL-");
            }
        }



        //--------------------------------------------Initialize the drone list--------------------------------------------------------

        /// <summary>
        /// etrieves the requested drone from the data and converts it to BL drone
        /// </summary>
        /// <param name="id">The requested dron</param>
        /// <returns>A Bl drone for list</returns>
        internal DroneForList GetBLDroneInList(int id)
        {
            var drone = drones.FirstOrDefault(d => d.Id == id);
            return drone;
        }


        /// <summary>
        /// update status of drone(Only from the local list)
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <param name="status">the new status</param>
        /// <param name="battery">battery of the drone</param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        private void UpdateDroneStatus(int id, DroneStatuses status, double battery, int parcelIdDeliverd, double longitude, double latitude)
        {
            DroneForList tempDroneForList = drones.First(drone => drone.Id == id);

            if (tempDroneForList.Equals(default(DroneForList)))
                throw new ArgumentNullException("update drone -BL-:There is not drone with same id i data");

            //local list
            drones.Remove(tempDroneForList);
            tempDroneForList.Status = status;
            tempDroneForList.Battery = battery;
            tempDroneForList.ParcelDeliveredId = parcelIdDeliverd;
            tempDroneForList.Location.Longitude = longitude;
            tempDroneForList.Location.Latitude = latitude;
            drones.Add(tempDroneForList);
        }

        /// <summary>
        /// update status of drone(Only from the local list)
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <param name="status">the new status</param>
        /// <param name="battery">battery of the drone</param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        private void UpdateDroneStatus(int id, DroneStatuses status, double battery, double longitude, double latitude)
        {
            DroneForList tempDroneForList = drones.First(drone => drone.Id == id);
            tempDroneForList.Status = status;
            tempDroneForList.Battery = battery;
            tempDroneForList.Location.Longitude = longitude;
            tempDroneForList.Location.Latitude = latitude;
        }

        private Drone mapDrone(int id)
        {
            DroneForList droneToList = drones.FirstOrDefault(drone => drone.Id == id);
            var t = drones;
            if (droneToList == null)
                throw new ArgumentNullException("Map drone -BL-:There is not drone with same id i data");
            return new Drone()
            {
                Id = droneToList.Id,
                Model = droneToList.Model,
                MaxWeight = droneToList.MaxWeight,
                Status = droneToList.Status,
                Battery = droneToList.Battery,
                Location = droneToList.Location,
                Delivery = droneToList.ParcelDeliveredId != -1 ? createParcelInTransfer(droneToList.ParcelDeliveredId) : null
            };
        }
    }
}
