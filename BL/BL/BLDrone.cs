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
        /// Add a drone to the list of drones
        /// </summary>
        /// <param name="tempDrone">The customer for Adding</param>
        public void AddDrone(int id, int stationId, Enums.WeightCategories maxWeight, string model)
        {
            Drone tempDrone = new Drone(id, model, (Enums.WeightCategories)maxWeight, DroneStatuses.Maintenance, rand.Next(20, 41), GetBLBaseStation(stationId).Location.Longitude, GetBLBaseStation(stationId).Location.Latitude);
            DO.Drone drone = new DO.Drone(tempDrone.Id, tempDrone.Model, (DO.Enum.WeightCategories)tempDrone.MaxWeight);
            try
            {
                dal.AddDrone(drone);
                DroneForList droneForList = new DroneForList(tempDrone.Id, tempDrone.Model, tempDrone.MaxWeight, tempDrone.Battery, tempDrone.Status, tempDrone.Location.Longitude, tempDrone.Location.Latitude);
                drones.Add(droneForList);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }

        /// <summary>
        /// add drone to the list in the BL
        /// </summary>
        /// <param name="drone">the drone to add</param>
        public void AddDroneForList(Drone drone)
        {
            DroneForList droneForList = new DroneForList(drone.Id, drone.Model, drone.MaxWeight, drone.Battery, drone.Status, drone.Location.Longitude, drone.Location.Latitude);
            drones.Add(droneForList);
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
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Get base station -BL-" + ex.Message);
            }

        }

        /// <summary>
        /// Convert a DAL drone to BL drone
        /// </summary>
        /// <param name="id">The drone to convert</param>
        /// <returns>The converted drone</returns>
        private Drone mapDrone(int id)
        {
            DroneForList droneToList = drones.FirstOrDefault(item => item.Id == id);
            if (droneToList == default)
                throw new ArgumentNullException("Map drone -BL-:There is not drone with same id i data");
            return new Drone()
            {
                Id = droneToList.Id,
                Model = droneToList.Model,
                MaxWeight = droneToList.MaxWeight,
                Status = droneToList.Status,
                Battery = droneToList.Battery,
                Location = droneToList.Location,
                Delivery = droneToList.ParcelDeliveredId != -1 ? createParcelInTransfer((int)droneToList.ParcelDeliveredId) : default
            };
        }

        //--------------------------------------------Show list---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the list of drones from BL
        /// </summary>
        /// <returns>A list of drones to print</returns
        public IEnumerable<DroneForList> GetDroneForList()
        {
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

        /// <summary>
        /// The function returns all the drones that maintain the status and weight
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<DroneForList> GetDroneForList(WeightCategories weight, DroneStatuses status)
        {
            return GetDroneForList(drone => drone.MaxWeight == weight && drone.Status == status);
        }
        /// <summary>
        /// The function returns all the drones that maintain the weight
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public IEnumerable<DroneForList> GetDroneForList(WeightCategories weight)
        {
            return GetDroneForList(drone => drone.MaxWeight == weight);
        }
        /// <summary>
        /// The function returns all the drones that maintain the status and weight
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<DroneForList> GetDroneForList(DroneStatuses status)
        {
            return GetDroneForList(drone => drone.Status == status);
        }

        //--------------------------------------------Update------------------------------------------------------------------------------------------
        /// <summary>
        /// update the model of drone
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <param name="model">the new model</param>
        public void UpdateDroneModel(int id, string model)
        {
            DroneForList tempDroneForList = drones.First(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Model = model;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
            DO.Drone drone = new DO.Drone(tempDroneForList.Id, tempDroneForList.Model, (DO.Enum.WeightCategories)tempDroneForList.MaxWeight);
            dal.AddDrone(drone);
        }

        /// <summary>
        /// update status of drone
        /// </summary>
        /// <param name="id">id of the drone</param>
        /// <param name="status">the new status</param>
        /// <param name="battery">battery of the drone</param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public void UpdateDroneStatus(int id, DroneStatuses status, double battery, int parcelIdDeliverd, double longitude, double latitude)
        {
            DroneForList tempDroneForList = drones.First(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Status = status;
            tempDroneForList.Battery = battery;
            tempDroneForList.ParcelDeliveredId = parcelIdDeliverd;
            tempDroneForList.Location.Longitude = longitude;
            tempDroneForList.Location.Latitude = latitude;

            drones.Add(tempDroneForList);
        }

        public void UpdateDroneStatus(int id, DroneStatuses status, double battery, double longitude, double latitude)
        {
            DroneForList tempDroneForList = drones.First(item => item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Status = status;
            tempDroneForList.Battery = battery;
            tempDroneForList.Location.Longitude = longitude;
            tempDroneForList.Location.Latitude = latitude;
            drones.Add(tempDroneForList);
        }

        /// <summary>
        /// Collection of a package by skimmer
        /// </summary>
        /// <param name="id">id of drone</param>
        public void PackageCollection(int id)
        {

            if (drones.FirstOrDefault(item => item.Id == id) != default(DroneForList))
            {
                DroneForList droneForList = drones.First(item => item.Id == id);
                if (droneForList.Status == DroneStatuses.Delivery)
                {
                    int parcelId = -1;
                    foreach (var tempParcel in dal.GetParcels())
                    {
                        if (tempParcel.Droneld == id)
                        {
                            parcelId = tempParcel.Id;
                            break;
                        }
                    }

                    if (parcelId != -1)
                    {
                        DO.Parcel parcel = dal.GetParcel(parcelId);
                        DO.Customer customer = dal.GetCustomer(parcel.SenderId);
                        double distance = this.distance(droneForList.Location.Latitude, customer.Latitude, droneForList.Location.Longitude, customer.Longitude);
                        Location location = new Location(customer.Longitude, customer.Latitude);
                        droneForList.Battery -= ((int)minBattery(droneForList.Location, location, droneForList.Status, droneForList.MaxWeight) + 1);
                        droneForList.Location = location;
                        drones.Add(droneForList);
                        dal.UpdateParcelPickedUp(parcel.Id);
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
        /// update the parcel status to package in delivery
        /// </summary>
        /// <param name="id"></param>
        public void PackageDelivery(int id)
        {
            DroneForList drone = new DroneForList();
            int index = 0;
            foreach (DroneForList item in drones)
            {
                if (item.Id == id)
                {
                    drone = item;
                    break;
                }
                ++index;
            }
            if (drone.ParcelDeliveredId != 0)
            {
                DO.Parcel parcel = new DO.Parcel();
                foreach (DO.Parcel item in dal.GetParcels())
                {
                    if (item.Id == drone.ParcelDeliveredId)
                    {
                        if (item.PickedUp != null && item.Delivered == default(DateTime))
                        {
                            parcel = item;
                        }
                        else
                        {
                            throw new ArgumentNullException("not find the drone -BL-");
                        }
                    }
                }
                Customer customer = GetBLCustomer(parcel.TargetId);
                drones[index].Battery -= (minBattery(drone.Location, customer.Location, drone.Status, drone.MaxWeight) + 1);
                drones[index].Location = customer.Location;
                parcel.Delivered = DateTime.Now;
                drones[index].Status = DroneStatuses.Available;
                dal.UpdateSupply(parcel);
            }
            else
            {
                throw new ArgumentNullException("the drone not in delivery -BL-");
            }
        }

        /// <summary>
        /// Sending a skimmer for charging
        /// </summary>
        /// <param name="droneId">id of drone</param>
        public void UpdateCharge(int droneId)
        {
            DroneForList tempDroneForList = drones.First(item => item.Id == droneId);
            Drone tempDrone = new Drone(tempDroneForList.Id, tempDroneForList.Model, tempDroneForList.MaxWeight, tempDroneForList.Status, tempDroneForList.Battery, tempDroneForList.Location.Longitude, tempDroneForList.Location.Latitude);
            int baseStationId = -1;
            Location location = new Location(-1, -1);
            double distance = double.MaxValue;
            if ((int)tempDrone.Status == 0)
            {

                foreach (var item in dal.GetBaseStations())
                {
                    double tempDistance = this.distance(tempDrone.Location.Latitude, item.Latitude, tempDrone.Location.Longitude, item.Longitude);
                    if (tempDistance < distance)
                    {
                        baseStationId = item.Id;
                        distance = tempDistance;
                        location = new Location(item.Longitude, item.Latitude);

                    }

                }
                if (minBattery(tempDrone.Location, location, tempDrone.Status, tempDrone.MaxWeight) < tempDrone.Battery)
                {
                    UpdateDroneStatus(droneId, DroneStatuses.Maintenance, tempDrone.Battery - minBattery(tempDrone.Location, location, tempDrone.Status, tempDrone.MaxWeight), GetBLBaseStation(baseStationId).Location.Latitude, GetBLBaseStation(baseStationId).Location.Latitude);
                    dal.UpdateCharge(droneId);


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
        /// Release skimmer from charging
        /// </summary>
        /// <param name="droneId">id of drone</param>
        /// <param name="time">the time the drone was in charge</param>
        public void UpdateRelease(int droneId)
        {
            DroneInCharging droneInCharging = new DroneInCharging();
            DroneForList drone = drones.First(item => item.Id == droneId);
            if (drone != null)
            {
                if (drone.Status == DroneStatuses.Maintenance)
                {
                    TimeSpan time = (TimeSpan)(DateTime.Now - GetDroneInChargByID(droneId).Time);
                    drones.Remove(drone);
                    droneInCharging.Battery = drone.Battery + batteryCalculationInCharging(time.Hours);
                    drone.Battery = droneInCharging.Battery;
                    drone.Status = (DroneStatuses)0;
                    drones.Add(drone);
                    dal.UpdateRelease(droneId);
                }
                else
                {
                    throw new ArgumentNullException("the drone not maintenance -BL-");
                }
            }
            else
            {
                throw new ArgumentNullException("the drone not found -BL-");
            }
        }

        /// <summary>
        /// Assign a package to a skimmer
        /// </summary>
        /// <param name="id">id of drone</param>
        public void AssignParcelToDrone(int id)
        {
            Drone drone = new Drone();
            foreach (DroneForList item in drones)
            {
                if (item.Id == id)
                {
                    drone = new Drone(id, item.Model, item.MaxWeight, item.Status, item.Battery, item.Location.Longitude, item.Location.Latitude);
                }
            }
            DroneForList droneForList = drones.First(item => item.Id == id);
            if (drone != null && drone.Status == DroneStatuses.Available)
            {
                int parcelId = 0;
                bool exist = false;

                Enums.Priorities priority = Enums.Priorities.Regular;
                Enums.WeightCategories weight = Enums.WeightCategories.Light;
                double distance = double.MaxValue;

                foreach (DO.Parcel item in dal.GetParcels())
                {
                    if (minBattery(drone.Location, GetBLCustomer(item.SenderId).Location, drone.Status, drone.MaxWeight) < drone.Battery && (WeightCategories)item.Weight <= drone.MaxWeight)
                    {
                        if ((Enums.Priorities)item.Priority > priority)
                        {
                            parcelId = item.Id;
                            priority = (Enums.Priorities)item.Priority;
                            exist = true;
                        }
                        else
                        {
                            if ((Enums.WeightCategories)item.Weight > weight && (Enums.Priorities)item.Priority == priority)
                            {
                                parcelId = item.Id;
                                weight = (Enums.WeightCategories)item.Weight;
                                exist = true;
                            }
                            else
                            {
                                if (this.distance(drone.Location.Latitude, GetBLCustomer(item.SenderId).Location.Latitude, drone.Location.Longitude, GetBLCustomer(item.SenderId).Location.Longitude) < distance && (Enums.WeightCategories)item.Weight == weight && (Enums.Priorities)item.Priority == priority)
                                {
                                    exist = true;
                                    parcelId = item.Id;
                                    distance = this.distance(drone.Location.Latitude, GetBLCustomer(item.SenderId).Location.Latitude, drone.Location.Longitude, GetBLCustomer(item.SenderId).Location.Longitude);
                                }
                                else
                                {
                                    if (exist == false)
                                    {
                                        exist = true;
                                        parcelId = item.Id;
                                        priority = (Enums.Priorities)item.Priority;
                                        weight = (Enums.WeightCategories)item.Weight;
                                        distance = this.distance(drone.Location.Latitude, GetBLCustomer(item.SenderId).Location.Latitude, drone.Location.Longitude, GetBLCustomer(item.SenderId).Location.Longitude);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (exist == true)
                {
                    UpdateDroneStatus(drone.Id, DroneStatuses.Delivery, drone.Battery, parcelId, drone.Location.Longitude, drone.Location.Latitude);
                    UpdateParcelAffiliation(parcelId, drone.Id, DateTime.Now);

                }
                else
                {
                    throw new ArgumentNullException("parcel not exist -BL-");
                }

            }
            else
            {
                throw new ArgumentNullException("parcel not exist -BL-");
            }
        }

        //--------------------------------------------Initialize the drone list--------------------------------------------------------
        /// <summary>
        /// The function initialize the location of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="drone">specific drone</param>
        /// <returns>location of the drone </returns>
        private Location findDroneLocation(DroneForList drone)
        {
            if (IsDroneMakesDelivery(drone.Id))
            {
                if (findParcelState(drone.Id) == parcelState.associatedNotCollected)
                {
                    DO.Customer senderCustomer2 = new DO.Customer();
                    try
                    {
                        senderCustomer2 = dal.GetCustomer(customer => customer.Id == (dal.GetParcel(parcel => parcel.Droneld == drone.Id).SenderId));

                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new ArgumentNullException("Get base station -BL-" + ex.Message);
                    }

                    DO.BaseStation soonStation = nearestBaseStation(senderCustomer2.Longitude, senderCustomer2.Latitude);
                    return new Location(soonStation.Longitude, soonStation.Latitude);
                }
                else if (findParcelState(drone.Id) == parcelState.collectedNotDelivered)
                {
                    DO.Customer senderCustomer = new DO.Customer();
                    try
                    {
                        senderCustomer = dal.GetCustomer(customer => customer.Id == (dal.GetParcel(parcel => parcel.Droneld == drone.Id).SenderId));
                    }
                    catch (ArgumentNullException ex)
                    {
                        throw new ArgumentNullException("Get base station -BL-" + ex.Message);
                    }
                    senderCustomer = dal.GetCustomer(customer => customer.Id == (dal.GetParcel(parcel => parcel.Droneld == drone.Id).SenderId));
                    return new Location(senderCustomer.Longitude, senderCustomer.Latitude);
                }
            }
            else
            {
                if (drone.Status == Enums.DroneStatuses.Maintenance)
                {
                    int randNumber1 = rand.Next(dal.GetBaseStations().Count());
                    var randomBaseStation = (dal.GetBaseStations().ToList())[randNumber1];
                    return new Location(randomBaseStation.Longitude, randomBaseStation.Latitude);
                }
            }
            var x =
                dal.GetCustomers((customer) => (
               (dal.GetParcels(parcel => (parcel.Delivered != null) && (customer.Id == parcel.TargetId))).Any()
               )
                );
            int randNumber = rand.Next(x.Count());
            var randomCustomerProvided = x.ToList()[randNumber];
            return new Location(randomCustomerProvided.Longitude, randomCustomerProvided.Latitude);
        }

        /// <summary>
        /// The function initialize the status of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>status of the drone</returns>
        private Enums.DroneStatuses findfDroneStatus(int droneId)
        {
            if (IsDroneMakesDelivery(droneId))
            {
                return BO.Enums.DroneStatuses.Delivery;
            }

            return (Enums.DroneStatuses)rand.Next(System.Enum.GetNames(typeof(Enums.DroneStatuses)).Length - 1);
        }

        /// <summary>
        /// Boolean function that returns if the drone makes a delivery
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>If the drone makes a delivery</returns>
        private bool IsDroneMakesDelivery(int droneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                if (parcel.Droneld == droneId &&
                    !(parcel.Requested.Equals(null)) && parcel.Delivered.Equals(default(DateTime)))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// The function initialize the battery of the drone in the list saved in the BL as required
        /// </summary>
        /// <param name="drone"></param>
        /// <returns>battery of the drone</returns>
        private double findDroneBattery(DroneForList drone)
        {
            if (IsDroneMakesDelivery(drone.Id))
            {
                double minDroneTarget;
                double minTargetBaseStation;
                try
                {
                    minDroneTarget = minBattery(drone.Location,
                        new Location(dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude, dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude),
                         drone.Status, drone.MaxWeight);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException("Drone battery -BL-" + ex.Message);
                }

                try
                {
                    minTargetBaseStation = minBattery(
                     new Location(dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude, dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude),
                    new Location(nearestBaseStation(dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude).Latitude,
                    nearestBaseStation(dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude).Longitude),
                    Enums.DroneStatuses.Available, drone.MaxWeight);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException("Drone battery -BL-" + ex.Message);
                }

                return rand.Next((int)(minDroneTarget + minTargetBaseStation), 101);
            }
            else if (drone.Status == Enums.DroneStatuses.Maintenance)
            {
                return rand.Next(0, 20);
            }

            double minDroneBaseStation;
            try
            {
                minDroneBaseStation = minBattery(
                    new Location(dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude, dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude),
                   new Location(nearestBaseStation(dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude).Latitude,
                   nearestBaseStation(dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Latitude, dal.GetCustomer(customer => customer.Id == drone.ParcelDeliveredId).Longitude).Longitude),
                   Enums.DroneStatuses.Available, drone.MaxWeight);
                return rand.Next((int)(minDroneBaseStation), 101);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException("Drone battery -BL-" + ex.Message);
            }
        }

        /// <summary>
        /// The function receives the location of a drone and returns the nearest base station to it
        /// </summary>
        /// <param name="LongitudeSenderCustomer">Location of the drone</param>
        /// <param name="LatitudeSenderCustomer">Location of the drone</param>
        /// <returns> The nearest base station</returns>
        private DO.BaseStation nearestBaseStation(double LongitudeSenderCustomer, double LatitudeSenderCustomer)
        {
            var minDistance = double.MaxValue;
            var nearestBaseStation = default(DO.BaseStation);
            foreach (var BaseStation in dal.GetBaseStations())
            {
                if (distance(LatitudeSenderCustomer, BaseStation.Latitude, LongitudeSenderCustomer, BaseStation.Longitude) < minDistance)
                {
                    minDistance = distance(LatitudeSenderCustomer, BaseStation.Latitude, LongitudeSenderCustomer, BaseStation.Longitude);
                    nearestBaseStation = BaseStation;
                }
            }
            if (nearestBaseStation.Equals(default(DO.BaseStation)))
            {
                throw new ArgumentNullException("Get nearst base station -BL-");
            }
            return nearestBaseStation;
        }

        /// <summary>
        /// calculate the battary of the drone after charge
        /// </summary>
        /// <param name="time">how much time the drone was in charge</param>
        /// <returns>sum of battary after charge</returns>
        private double batteryCalculationInCharging(int time)
        {
            double battery = time * 0.05;
            if (battery < 100)
            {
                return battery;
            }
            return 100;
        }
    }
}
