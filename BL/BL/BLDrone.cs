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
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Add a drone to the list of drones(DAL list and local list)
        /// </summary>
        /// <param name="tempDrone">The customer for Adding</param>
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
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
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
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
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
                            throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
                        }
                    }
                }
            }
        }

        //---------------------------------------------Delete ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// The function gets a drone ID and tries to delete it( from DAL list and local list)
        /// </summary>
        /// <param name="station"></param>
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
                throw new KeyNotFoundException("Delete drone -BL-" + ex.Message);
            }
            //local list
            drones.Remove(deleteDrone);

            // Release charge if the drone was charging
            if (deleteDrone.Status == Enums.DroneStatuses.Maintenance)
            {
                UpdateRelease(droneId);
            }
        }

        //---------------------------------------------Show item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// etrieves the requested drone from the data and converts it to BL drone
        /// </summary>
        /// <param name="id">The requested drone</param>
        /// <returns>A Bl drone to print</returns>
        public Drone GetBLDrone(int id)
        {
            try
            {
                return mapDrone(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get drone -BL-" + ex.Message);
            }
        }


        /// <summary>
        /// Convert a DAL drone to BL drone
        /// </summary>
        /// <param name="id">The drone to convert</param>
        /// <returns>The converted drone</returns>
        private Drone mapDrone(int id)
        {
            DroneForList droneToList = drones.FirstOrDefault(drone => drone.Id == id);
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

        //--------------------------------------------Show list---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of drones from BL
        /// </summary>
        /// <returns>A list of drones to print</returns
        public IEnumerable<DroneForList> GetDroneForList()
        {
            var s = drones;
            return drones;
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of DroneForList that maintain the predicate</returns>
        public IEnumerable<DroneForList> GetDroneForList(Predicate<DroneForList> predicate)
        {
            return GetDroneForList().Where(s => predicate(s));
        }


        //--------------------------------------------Update------------------------------------------------------------------------------------------
        /// <summary>
        /// update the model of drone
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <param name="model">the new model</param>
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
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }


            //update local list
            DeleteBLDrone(tempDroneForList.Id);
            tempDroneForList.Model = model;
            drones.Add(tempDroneForList);
        }


        /// <summary>
        /// Collection of a parcel by drone
        /// //הרחפן אסף את החבילה
        /// </summary>
        /// <param name="droneID">id of drone</param>
        public void ParcelCollection(int droneID)
        {
            DroneForList droneForList = drones.FirstOrDefault(drone => drone.Id == droneID);
            DO.Parcel parcel;
            DO.Customer customer;

            if (droneForList != null)
            {
                if (droneForList.Status == DroneStatuses.Delivery)
                {
                    int parcelId = -1;
                    foreach (var tempParcel in dal.GetParcels().Where(p => p.IsDeleted == false))
                    {
                        if (tempParcel.Droneld == droneID && tempParcel.PickedUp == null)
                        {
                            parcelId = tempParcel.Id;
                            break;
                        }
                    }

                    if (parcelId != -1)
                    {
                        lock (dal) { parcel = dal.GetParcel(parcelId); }
                        lock (dal) { customer = dal.GetCustomer(parcel.TargetId); }
                        Location location = new Location() { Longitude = customer.Longitude, Latitude = customer.Latitude };
                        droneForList.Battery -= minBattery(droneForList.Location, location, droneForList.Status, droneForList.MaxWeight) + 1;
                        droneForList.Location = location;
                        lock (dal) { dal.UpdateParcelPickedUp(parcel.Id); }
                    }
                    else
                    {
                        throw new ArgumentNullException("not find the parcel -BL-");
                    }
                }
                else
                {
                    throw new ArgumentNullException("the drone not in delivery");
                }
            }
            else
            {
                throw new ArgumentNullException("not find the drone -BL-");
            }
        }


        /// <summary>
        /// The parcel arrived at the destination
        /// //החבילה הגיעה ליעד
        /// </summary>
        /// <param name="droneId"></param>
        public void UpdateParcelDelivered(int droneId)
        {
            DroneForList drone = GetBLDroneInList(droneId);
            IEnumerable<DO.Parcel> parcels;
            lock (dal) { parcels = dal.GetParcels(p => p.IsDeleted == false); }

            if (drone.ParcelDeliveredId != -1)
            {
                DO.Parcel parcel = new DO.Parcel();

                foreach (DO.Parcel item in parcels)
                {
                    if (item.Id == drone.ParcelDeliveredId)
                    {
                        if (item.PickedUp !=null && item.Delivered== null)
                        {
                            parcel = item;
                            break;
                        }
                        else
                        {
                            throw new ArgumentNullException("not find the drone -BL-");
                        }
                    }
                }

                //update local drone list
                Customer customer = GetBLCustomer(parcel.TargetId);
                drone.Status = DroneStatuses.Available;
                drone.Battery -= minBattery(drone.Location, customer.Location, drone.Status, drone.MaxWeight) + 1;
                drone.Location = customer.Location;
                drone.ParcelDeliveredId = -1;
                //update data list
                lock (dal) { dal.UpdateParcelDelivered(parcel.Id); }
            }
            else
            {
                throw new ArgumentNullException("the drone not in delivery -BL-");
            }
        }


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
                        throw new KeyNotFoundException(ex.Message);
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
        /// Sending a drone for charging
        /// </summary>
        /// <param name="droneId">id of drone</param>
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
                        throw new KeyNotFoundException(ex.Message);
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
        /// Release drone from charging
        /// </summary>
        /// <param name="droneId">id of drone</param>
        /// <param name="time">the time the drone was in charge</param>
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
                        throw new KeyNotFoundException(ex.Message);
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


        /// <summary>
        /// Assign a parcel to a drone
        /// //שיוך רחפן לחבילה
        /// </summary>
        /// <param name="id">id of drone</param>
        public void AssignParcelToDrone(int id)
        {
            Drone drone = GetBLDrone(id);

            if (drone.Equals(default(Drone)) || drone.Status != DroneStatuses.Available)
                throw new KeyNotFoundException("drone not exist  or not available -BL-");

            int parcelId = -1;
            bool isExist = false;
            Priorities priority = Priorities.Regular;
            WeightCategories weight = WeightCategories.Light;
            double distance = double.MaxValue;
            IEnumerable<DO.Parcel> parcels;
            lock (dal) { parcels = dal.GetParcels(parcel => parcel.IsDeleted == false && parcel.Scheduled != null); }

            foreach (DO.Parcel parcel in parcels)
            {
                if (parcel.Scheduled == null)
                {
                    var batteryWayToSender = minBattery(drone.Location, GetBLCustomer(parcel.SenderId).Location, DroneStatuses.Available, drone.MaxWeight);
                    var batteryWayToTarget = minBattery(GetBLCustomer(parcel.SenderId).Location, GetBLCustomer(parcel.TargetId).Location, DroneStatuses.Delivery, (WeightCategories)parcel.Weight);
                    if ((batteryWayToSender + batteryWayToTarget) < drone.Battery && (WeightCategories)parcel.Weight <= drone.MaxWeight)
                    {
                        if ((Priorities)parcel.Priority > priority)
                        {
                            parcelId = parcel.Id;
                            priority = (Priorities)parcel.Priority;
                            isExist = true;
                        }
                        else
                        {
                            if ((WeightCategories)parcel.Weight > weight && (Priorities)parcel.Priority == priority)
                            {
                                parcelId = parcel.Id;
                                weight = (WeightCategories)parcel.Weight;
                                isExist = true;
                            }
                            else
                            {
                                if (this.distance(drone.Location.Latitude, GetBLCustomer(parcel.SenderId).Location.Latitude, drone.Location.Longitude, GetBLCustomer(parcel.SenderId).Location.Longitude) < distance && (Enums.WeightCategories)parcel.Weight == weight && (Enums.Priorities)parcel.Priority == priority)
                                {
                                    isExist = true;
                                    parcelId = parcel.Id;
                                    distance = this.distance(drone.Location.Latitude, GetBLCustomer(parcel.SenderId).Location.Latitude, drone.Location.Longitude, GetBLCustomer(parcel.SenderId).Location.Longitude);
                                }
                                else
                                {
                                    if (isExist == false)
                                    {
                                        isExist = true;
                                        parcelId = parcel.Id;
                                        priority = (Enums.Priorities)parcel.Priority;
                                        weight = (Enums.WeightCategories)parcel.Weight;
                                        distance = this.distance(drone.Location.Latitude, GetBLCustomer(parcel.SenderId).Location.Latitude, drone.Location.Longitude, GetBLCustomer(parcel.SenderId).Location.Longitude);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (isExist == false)
                throw new NoParcelFoundForConnectionToTheDroneException("-BL-");
            else
            {
                UpdateDroneStatus(drone.Id, DroneStatuses.Delivery, drone.Battery, parcelId, drone.Location.Longitude, drone.Location.Latitude);
                UpdateParcelScheduled(parcelId, drone.Id);
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

        public void StartDroneSimulator(int droneId, Action update, Func<bool> checkStop)
        {
            new DroneSimulator(this, droneId, update, checkStop);
        }
    }
}
