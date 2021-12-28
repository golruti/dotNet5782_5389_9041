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
            DO.Parcel parcel = new DO.Parcel(tempParcel.CustomerSender.Id, tempParcel.CustomerReceives.Id, (DO.Enum.WeightCategories)tempParcel.Weight, (DO.Enum.Priorities)tempParcel.Priority, tempParcel.DroneParcel.Id, DateTime.Now, null, null, null);
            try
            {
                dal.AddParcel(parcel);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
            }
        }
        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function gets a customer ID and tries to delete it
        /// </summary>
        /// <param name="station"></param>
        private void deleteBLParcel(int parcelId)
        {
            try
            {
                dal.DeleteParcel(parcelId);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Delete parcel -BL-" + ex.Message);
            }
        }

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function updates the details when the parcel is associated with the skimmer
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="droneId"></param>
        /// <param name="dateTime"></param>
        public void UpdateParcelAffiliation(int parcelId, int droneId, DateTime dateTime)
        {
            DO.Parcel parcel;
            try
            {
               parcel = dal.GetParcel(parcelId);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Delete parcel -BL-" + ex.Message);
            }
            
            dal.DeleteParcel(parcelId);
            parcel.Droneld = droneId;
            parcel.Scheduled = dateTime;

            try
            {
                dal.AddParcel(parcel);
            }
            catch (DO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw new ThereIsAnObjectWithTheSameKeyInTheListException(ex.Message);
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
                return mapParcel(dal.GetParcel(parcelId));
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get parcel by id -BL-" + ex.Message);
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
            foreach (var parcel in dal.GetParcels())
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
                    throw new ArgumentNullException("Add Parcel for list -BL-" + ex.Message);
                }
            }
            return ParcelsForList;
        }
        
        public IEnumerable<ParcelForList> GetParcelForList(Predicate<ParcelForList> predicate)
        {
            return GetParcelForList().Where(parcel => predicate(parcel));
        }


        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, string idReceiver, Priorities priority)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, Priorities priority)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status  && parcel.Priority == priority);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status );
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender );
        }

        

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, Priorities priority)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.Priority == priority);
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, Priorities priority)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender  && parcel.Priority == priority);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(ParcelStatuses status, string idReceiver, Priorities priority)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender( string idReceiver, Priorities priority)
        {
            return GetParcelForList(parcel =>  parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(string idReceiver)
        {
            return GetParcelForList(parcel =>parcel.ReceiveCustomer == idReceiver );
        }
        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(ParcelStatuses status, string idReceiver)
        {
            return GetParcelForList(parcel => parcel.Status == status && parcel.ReceiveCustomer == idReceiver );
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, string idReceiver)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver );
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, string idReceiver)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender  && parcel.ReceiveCustomer == idReceiver);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, string idReceiver, Priorities priority,DateTime from,DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested>from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, string idReceiver, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver  && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status  && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(ParcelStatuses status,  string idReceiver, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.Status == status && parcel.ReceiveCustomer == idReceiver && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, string idReceiver, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender &&  parcel.ReceiveCustomer == idReceiver && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, Priorities priority, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

       

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(ParcelStatuses status, Priorities priority, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, Priorities priority, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(ParcelStatuses status, string idReceiver, Priorities priority, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

       

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, Priorities priority, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.Status == status  && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(string idReceiver, Priorities priority, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel =>  parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, string idReceiver, Priorities priority, DateTime from, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender  && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, string idReceiver, Priorities priority, DateTime from)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from );
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, string idReceiver, DateTime from)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver  && GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender,DateTime from)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status  && GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(ParcelStatuses status, string idReceiver, DateTime from)
        {
            return GetParcelForList(parcel => parcel.Status == status && parcel.ReceiveCustomer == idReceiver && GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, string idReceiver, DateTime from)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.ReceiveCustomer == idReceiver && GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, string idSender, Priorities priority, DateTime from)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status  && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status, Priorities priority, DateTime from)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, Priorities priority, DateTime from)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutSender(ParcelStatuses status, string idReceiver, Priorities priority, DateTime from)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from);
        }

       

        public IEnumerable<ParcelForList> GetParcelForList( string idSender, string idReceiver, Priorities priority, DateTime from)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender  && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested > from);
        }
        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom(ParcelStatuses status, string idSender, string idReceiver, Priorities priority, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom(ParcelStatuses status, string idSender, string idReceiver,  DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.ReceiveCustomer == idReceiver  && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom( string idSender, string idReceiver, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.ReceiveCustomer == idReceiver && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFromAndSender(ParcelStatuses status,  string idReceiver, DateTime to)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.ReceiveCustomer == idReceiver && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom(ParcelStatuses status, string idSender,  DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status  && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom(ParcelStatuses status, string idSender, Priorities priority, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Status == status && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFromAndSender(ParcelStatuses status, string idReceiver, Priorities priority, DateTime to)
        {
            return GetParcelForList(parcel =>  parcel.Status == status && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom( string idSender, string idReceiver, Priorities priority, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested < to);
        }


        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom(ParcelStatuses status, Priorities priority, DateTime to)
        {
            return GetParcelForList(parcel => parcel.Status == status && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested < to);
        }
        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom(string idSender, Priorities priority, DateTime to)
        {
            return GetParcelForList(parcel => parcel.SendCustomer == idSender && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested < to);
        }
        public IEnumerable<ParcelForList> GetParcelForListWithOutFromAndSender(string idReceiver, Priorities priority, DateTime to)
        {
            return GetParcelForList(parcel => parcel.ReceiveCustomer == idReceiver && parcel.Priority == priority && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList(DateTime from, DateTime to)
        {
            return GetParcelForList().Where(parcel => GetBLParcel(parcel.Id).Requested > from && GetBLParcel(parcel.Id).Requested < to);
        }

        public IEnumerable<ParcelForList> GetParcelForList(Priorities priority)
        {
            return GetParcelForList(parcel => parcel.Priority == priority);
        }
       
        public IEnumerable<ParcelForList> GetParcelForList(ParcelStatuses status)
        {
            return GetParcelForList(parcel => parcel.Status == status);
        }
        public IEnumerable<ParcelForList> GetParcelForList(DateTime from)
        {
            return GetParcelForList().Where(parcel => GetBLParcel(parcel.Id).Requested > from);
        }

        public IEnumerable<ParcelForList> GetParcelForListWithOutFrom(DateTime to)
        {
            return GetParcelForList().Where(parcel => GetBLParcel(parcel.Id).Requested < to);
        }
       

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to BL parcel 
        /// </summaryparfcel
        /// <returns>A list of parcels to print</returns>
        private IEnumerable<Parcel> getAllParcels()
        {
            return (dal.GetParcels()).Select(Parcel => GetBLParcel(Parcel.Id));
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of BaseStationForList that maintain the predicate</returns>
        public IEnumerable<Parcel> GetAllParcels(Predicate<Parcel> predicate)
        {
            return getAllParcels().Where(parcel => predicate(parcel));
        }


        //--------------------------------------------Initialize the parcel list--------------------------------------------------------

        /// <summary>
        /// The function returns the status of the package according to the skimmer to which it is associated
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Enums.ParcelStatuses GetParcelStatusByDrone(int droneId)
        {
            Parcel parcel;
            try
            {
                parcel = mapParcel(dal.GetParcel(droneId));
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get parcel by id -BL-" + ex.Message);
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
            var tmpDrone = drones.Values.FirstOrDefault(drone => drone.Id == parcel.Droneld);
            try
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
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get parcel by id -BL-" + ex.Message);
            }
        }

        /// <summary>
        /// Convert a DAL parcel to Parcel In Transfer
        /// </summary>
        /// <param name="id">The requested parcel to convert</param>
        /// <returns>The converted parcel</returns>
        private ParcelByTransfer createParcelInTransfer(int id)
        {
            DO.Parcel parcel;
            DO.Customer sender;
            DO.Customer target;
            try
            {
                parcel = dal.GetParcel(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get parcel by id -BL-" + ex.Message);
            }
            try
            {
                sender = dal.GetCustomer(parcel.SenderId);
                target = dal.GetCustomer(parcel.TargetId);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException("Get customer by SenderId/TargetId -BL-" + ex.Message);
            }

            return new ParcelByTransfer
            {
                Id = id,
                Weight = (Enums.WeightCategories)parcel.Weight,
                Priority = (Enums.Priorities)parcel.Priority,
                IsDestinationParcel = !parcel.PickedUp.Equals(default),
                SenderLocation = new Location(sender.Longitude, sender.Latitude),
                TargetLocation = new Location(target.Longitude, target.Latitude),
                Distance = distance(sender.Latitude, sender.Longitude, sender.Latitude, sender.Longitude),
                Sender = new CustomerDelivery(sender.Id, sender.Name),
                Target = new CustomerDelivery(target.Id, target.Name)
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
                Status = parcel.Scheduled == default ? default : parcel.PickedUp == default ? ParcelStatuses.Associated : parcel.Scheduled == default ?
                                                                                                                 ParcelStatuses.Collected : ParcelStatuses.Provided
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
            foreach (var parcel in dal.GetParcels())
            {
                if (!(parcel.Scheduled.Equals(null)) && parcel.PickedUp.Equals(null))
                {
                    return parcelState.associatedNotCollected;
                }
                if (!parcel.PickedUp.Equals(null) && parcel.Delivered.Equals(null))
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
            if ((dal.GetCustomers().First(customer => customer.Id == parcel.SenderId)).Name.Equals(default(string)))
            {
                throw new ArgumentNullException("Get sender customer  -BL-");
            }
            return (dal.GetCustomers().First(customer => customer.Id == parcel.SenderId)).Name;
        }

        /// <summary>
        /// The function returns the name of the customer who should receive the package
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>The name of the receiving customer</returns>
        private string getReceiveCustomer(DO.Parcel parcel)
        {
            if ((dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.TargetId)).Name.Equals(default(string)))
            {
                throw new ArgumentNullException("Get recieve customer  -BL-");
            }
            return (dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.TargetId)).Name;
        }

        /// <summary>
        /// The function returns the status of the parcel-
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns>The parcel status</returns>
        private BO.Enums.ParcelStatuses getParcelStatus(DO.Parcel parcel)
        {
            if (!(parcel.Requested.Equals(null)))
            {
                return Enums.ParcelStatuses.Created;
            }
            else if (parcel.Scheduled.Equals(null) && parcel.PickedUp.Equals(null) && parcel.Delivered.Equals(null))
            {
                return Enums.ParcelStatuses.Associated;
            }
            else if (!(parcel.PickedUp.Equals(null)) && parcel.Delivered.Equals(null))
            {
                return Enums.ParcelStatuses.Collected;
            }
            else
            {
                return Enums.ParcelStatuses.Provided;
            }
        }

        /// <summary>
        /// The function initializes the ID of the parcel that is in the drone - if any
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns>the ID of the parcel</returns>
        private int findParceDeliveredlId(int droneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                if (parcel.Droneld == droneId)
                {
                    return parcel.Id;
                }
            }
            return default(int);
        }
    }
}
