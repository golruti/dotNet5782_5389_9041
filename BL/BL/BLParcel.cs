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
        public void AddParcel(Parcel tempParcel)
        {
            try
            {
                lock (dal)
                {
                    dal.AddParcel(new DO.Parcel()
                    {
                        Id = tempParcel.Id,
                        SenderId = tempParcel.CustomerSender.Id,
                        TargetId = tempParcel.CustomerReceives.Id,
                        Weight = (DO.Enum.WeightCategories)tempParcel.Weight,
                        Priority = (DO.Enum.Priorities)tempParcel.Priority,
                        Droneld = -1,
                        Requested = tempParcel.Requested,
                        Scheduled = null,
                        PickedUp = null,
                        Delivered = null,
                        IsDeleted = false
                    });
                }
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }
        }

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
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
            lock (dal) { parcels = dal.GetParcels(parcel => parcel.IsDeleted == false && parcel.Scheduled == null); }

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


        public void UpdateParcelScheduled(int parcelId, int droneId)
        {
            try
            {
                lock (dal) { dal.UpdateParcelScheduled(parcelId, droneId); }
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message, ex);
            }
        }



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
                        if (item.PickedUp != null && item.Delivered == null)
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


        //---------------------------------------------Show item---------------------------------------------------------------------
        public Parcel GetBLParcel(int parcelId)
        {
            try
            {
                lock (dal) { return mapParcel(dal.GetParcel(parcelId)); }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get parcel by id -BL-" + ex.Message, ex);
            }
        }

        //--------------------------------------------Show list----------------------------------------------------------------------            
        public IEnumerable<ParcelForList> GetParcelForList()
        {
            List<ParcelForList> ParcelsForList = new List<ParcelForList>();
            IEnumerable<DO.Parcel> parcels;
            lock (dal) { parcels = dal.GetParcels(p => p.IsDeleted == false); }

            foreach (var parcel in parcels)
            {
                try
                {
                    ParcelsForList.Add(new ParcelForList()
                    {
                        Id = parcel.Id,
                        Weight = (Enums.WeightCategories)parcel.Weight,
                        Priority = (Enums.Priorities)parcel.Priority,
                        SendCustomer = getSendCustomerName(parcel),
                        ReceiveCustomer = getReceiveCustomer(parcel),
                        Status = getParcelStatus(parcel)
                    });
                }
                catch (ArgumentNullException ex)
                {
                    throw new ArgumentNullException("Add Parcel for list -BL-" + ex.Message, ex);
                }
            }
            if (ParcelsForList.Count() == 0)
                return Enumerable.Empty<ParcelForList>();
            return ParcelsForList;
        }

        public IEnumerable<ParcelForList> GetParcelForList(Predicate<ParcelForList> predicate)
        {
            return GetParcelForList().Where(parcel => predicate(parcel));
        }


        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        public void deleteBLParcel(int parcelId)
        {
            var parcel = GetBLParcel(parcelId);
            if (parcel.Scheduled != null && parcel.Delivered == null)
                throw new TheParcelIsAssociatedAndCannotBeDeleted();
            try
            {
                lock (dal) { dal.DeleteParcel(parcelId); }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Delete parcel -BL-" + ex.Message, ex);
            }
            catch (DO.TheParcelIsAssociatedAndCannotBeDeleted ex)
            {
                throw new TheParcelIsAssociatedAndCannotBeDeleted("Delete parcel -BL-" + ex.Message, ex);
            }
        }


        //--------------------------------------------Initialize the parcel --------------------------------------------------------

        public Enums.ParcelStatuses GetParcelStatusByDrone(int parcelId)
        {
            Parcel parcel;
            try
            {
                lock (dal) { parcel = mapParcel(dal.GetParcel(parcelId)); }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get parcel by id -BL-" + ex.Message, ex);
            }

            if (parcel.Delivered != null)
                return Enums.ParcelStatuses.Provided;
            if (parcel.PickedUp != null)
                return Enums.ParcelStatuses.Collected;
            if (parcel.Scheduled != null)
                return Enums.ParcelStatuses.Associated;
            return Enums.ParcelStatuses.Created;
        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to BL parcel 
        /// </summaryparfcel
        /// <returns>A list of parcels to print</returns>
        private IEnumerable<Parcel> GetAllParcels()
        {
            lock (dal) { return dal.GetParcels(p => p.IsDeleted == false).Select(Parcel => GetBLParcel(Parcel.Id)); }
        }


        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of BaseStationForList that maintain the predicate</returns>
        private IEnumerable<Parcel> GetAllParcels(Predicate<Parcel> predicate)
        {
            return GetAllParcels().Where(parcel => predicate(parcel));
        }



        /// <summary>
        /// Convert a DAL parcel to BL parcel
        /// </summary>
        /// <param name="parcel">he parcel to convert</param>
        /// <returns>The converted parcel</returns>
        private Parcel mapParcel(DO.Parcel parcel)
        {
            if (parcel.Equals(default(DO.Parcel)))
                throw new KeyNotFoundException("not found parcel -BL-");
            var tmpDrone = drones.FirstOrDefault(drone => drone.Id == parcel.Droneld);
            try
            {
                lock (dal)
                {
                    return new Parcel()
                    {
                        Id = parcel.Id,
                        CustomerReceives = mapCustomerInParcel(dal.GetCustomer(parcel.TargetId)),
                        CustomerSender = mapCustomerInParcel(dal.GetCustomer(parcel.SenderId)),
                        Weight = (Enums.WeightCategories)parcel.Weight,
                        Priority = (Enums.Priorities)parcel.Priority,
                        Scheduled = parcel.Scheduled,
                        PickedUp = parcel.PickedUp,
                        Requested = parcel.Requested,
                        Delivered = parcel.Delivered,
                        DroneParcel = tmpDrone != default ? mapDroneWithParcel(tmpDrone) : null
                    };
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get parcel by id -BL-" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Convert a DAL parcel to Parcel In Transfer
        /// </summary>
        /// <param name="parcelId">The requested parcel to convert</param>
        /// <returns>The converted parcel</returns>
        internal ParcelByTransfer createParcelInTransfer(int parcelId)
        {
            DO.Parcel parcel;
            DO.Customer sender;
            DO.Customer target;

            lock (dal) { parcel = dal.GetParcel(parcelId); }
            if (parcel.Equals(default(DO.Parcel)))
                throw new KeyNotFoundException("not found parcel -BL-");

            lock (dal) { sender = dal.GetCustomer(parcel.SenderId); }
            lock (dal) { target = dal.GetCustomer(parcel.TargetId); }

            return new ParcelByTransfer
            {
                Id = parcelId,
                Weight = (WeightCategories)parcel.Weight,
                Priority = (Priorities)parcel.Priority,
                IsDestinationParcel = parcel.PickedUp != null,
                SenderLocation = new Location() { Longitude = sender.Longitude, Latitude = sender.Latitude },
                TargetLocation = new Location() { Longitude = target.Longitude, Latitude = target.Latitude },
                Distance = distance(sender.Latitude, sender.Longitude, sender.Latitude, sender.Longitude),
                Sender = new CustomerDelivery() { Id = sender.Id, Name = sender.Name },
                Target = new CustomerDelivery() { Id = target.Id, Name = target.Name }
            };
        }



        /// <summary>
        /// Convert a BL parcel to Parcel At Customer
        /// </summary>
        /// <param name="parcel">The parcel to convert</param>
        /// <param name="type">The type of the customer</param>
        /// <returns>The converted parcel</returns>
        private ParcelToCustomer parcelToParcelAtCustomer(Parcel parcel, string type)
        {
            ParcelToCustomer newParcel = new ParcelToCustomer
            {
                Id = parcel.Id,
                Weight = parcel.Weight,
                Priority = parcel.Priority,
                Status = parcel.Scheduled == default ? default : parcel.PickedUp == default ? ParcelStatuses.Associated : parcel.Scheduled == default ? ParcelStatuses.Collected : ParcelStatuses.Provided
            };

            if (type == "sender")
            {
                newParcel.Customer = new CustomerDelivery()
                {
                    Id = parcel.CustomerReceives.Id,
                    Name = parcel.CustomerReceives.Name
                };
            }
            else
            {
                newParcel.Customer = new CustomerDelivery()
                {
                    Id = parcel.CustomerSender.Id,
                    Name = parcel.CustomerSender.Name
                };
            }
            return newParcel;
        }

        /// <summary>
        /// The function returns the parcel status of a particular drone
        /// </summary>
        /// <param name="DroneId"></param>
        /// <returns>The parcel status</returns>
        private parcelState findParcelState(int DroneId)
        {
            IEnumerable<DO.Parcel> parcels;
            lock (dal) { parcels = dal.GetParcels(p => p.IsDeleted == false); }
            foreach (var parcel in parcels)
            {
                if (parcel.Scheduled != null && parcel.PickedUp == null)
                {
                    return parcelState.associatedNotCollected;
                }
                if (parcel.PickedUp != null && parcel.Delivered == null)
                {
                    return parcelState.collectedNotDelivered;
                }
            }
            return parcelState.DroneNotAssociated;
        }

        /// <summary>
        /// The function returns the name of the customer who sent the parcel
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>The name of the sending customer</returns>
        private string getSendCustomerName(DO.Parcel parcel)
        {
            DO.Customer senderCustomerName;
            lock (dal) { senderCustomerName = dal.GetCustomers().FirstOrDefault(c => c.Id == parcel.SenderId); }

            if (senderCustomerName.Equals(default(DO.Customer)))
            {
                throw new ArgumentNullException("Get recieve customer  -BL-");
            }
            return senderCustomerName.Name;
        }

        /// <summary>
        /// The function returns the name of the customer who should receive the package
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>The name of the receiving customer</returns>
        private string getReceiveCustomer(DO.Parcel parcel)
        {
            DO.Customer receiveCustomerName;
            lock (dal) { receiveCustomerName = dal.GetCustomers().FirstOrDefault(c => c.Id == parcel.TargetId); }
            if (receiveCustomerName.Equals(default(DO.Customer)))
            {
                throw new ArgumentNullException("Get recieve customer  -BL-");
            }
            return receiveCustomerName.Name;
        }

        /// <summary>
        /// The function returns the status of the parcel-
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>The parcel status</returns>
        private ParcelStatuses getParcelStatus(DO.Parcel parcel)
        {
            if (parcel.Delivered != null)
                return Enums.ParcelStatuses.Provided;
            if (parcel.PickedUp != null)
                return Enums.ParcelStatuses.Collected;
            if (parcel.Scheduled != null)
                return Enums.ParcelStatuses.Associated;
            return Enums.ParcelStatuses.Created;
        }

        /// <summary>
        /// The function initializes the ID of the parcel that is in the drone - if any
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>the ID of the parcel</returns>
        private int findParceDeliveredlId(int droneId)
        {
            IEnumerable<DO.Parcel> parcels;
            parcels = dal.GetParcels(p => p.IsDeleted == false && p.Delivered == null);
            foreach (var parcel in parcels)
            {
                if (parcel.Droneld == droneId)
                {
                    return parcel.Id;
                }
            }
            return -1;
        }
    }
}
