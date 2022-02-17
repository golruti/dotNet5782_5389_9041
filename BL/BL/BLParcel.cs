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
        /// Add a parcel to the list of purcels
        /// </summary>
        /// <param name="tempParcel">The parcel for Adding</param>
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
        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function gets a customer ID and tries to delete it
        /// </summary>
        /// <param name="station"></param>
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

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function updates the details when the parcel is associated with the drone
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="droneId"></param>
        /// <param name="dateTime"></param>
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

        //---------------------------------------------Show item----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieves the requested parcel from the data and converts it to BL parcel
        /// </summary>
        /// <param name="parcelId">The requested parcel</param>
        /// <returns>A Bl parcel to print</returns>
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

        //--------------------------------------------Show list--------------------------------------------------------------------            
        /// <summary>
        /// he function returns the parcel list from DAL to the ParcelForList list
        /// </summary>
        /// <returns>The list of ParcelForList parcels</returns>
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

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of ParcelForList that maintain the predicate</returns>
        public IEnumerable<ParcelForList> GetParcelForList(Predicate<ParcelForList> predicate)
        {
            return GetParcelForList().Where(parcel => predicate(parcel));
        }



        //--------------------------------------------Initialize the parcel --------------------------------------------------------

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
        /// The function returns the status of the package according to the drone to which it is associated
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
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
            if (sender.Equals(default(DO.Customer)) || target.Equals(default(DO.Customer)))
                throw new KeyNotFoundException("not found sender customer/target customer -BL-");

            return new ParcelByTransfer
            {
                Id = parcelId,
                Weight = (WeightCategories)parcel.Weight,
                Priority = (Priorities)parcel.Priority,
                IsDestinationParcel = !parcel.PickedUp.Equals(null),
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
            parcels = dal.GetParcels(p => p.IsDeleted == false);
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
