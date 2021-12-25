﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

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
            //IDAL.DO.Parcel parcel = new IDAL.DO.Parcel(dal.IncreastNumberIndea(), tempParcel.SenderId, tempParcel.ReceiverId, (IDAL.DO.Enum.WeightCategories)tempParcel.Weight, (IDAL.DO.Enum.Priorities)tempParcel.Priority, null, DateTime.Now, new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0));
            //dal.InsertParcel(parcel);
        }

        /// <summary>
        /// The function updates the details when the פשרבקך is associated with the skimmer
        /// </summary>
        /// <param name="parcelId"></param>
        /// <param name="droneId"></param>
        /// <param name="dateTime"></param>
        public void UpdateParcelAffiliation(int parcelId, int droneId, DateTime dateTime)
        {
            DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.DeleteParcel(parcelId);
            parcel.Droneld = droneId;
            parcel.Scheduled = dateTime;
            dal.AddParcel(parcel);
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
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Get base station -BL-" + ex.Message);
            }
        }

        /// <summary>
        /// The function returns the status of the package according to the skimmer to which it is associated
        /// </summary>
        /// <param name="droneId"></param>
        /// <returns></returns>
        public Enums.ParcelStatuses GetParcelStatusByDrone(int droneId)
        {
            Parcel parcel = mapParcel(dal.GetParcel(parcel => parcel.Droneld == droneId));

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

        /// <summary>
        /// Convert a DAL parcel to Parcel In Transfer
        /// </summary>
        /// <param name="id">The requested parcel to convert</param>
        /// <returns>The converted parcel</returns>
        private ParcelByTransfer createParcelInTransfer(int id)
        {
            DO.Parcel parcel = dal.GetParcel(id);
            DO.Customer sender = dal.GetCustomer(parcel.SenderId);
            DO.Customer target = dal.GetCustomer(parcel.TargetId);
            return new ParcelByTransfer
            {
                Id = id,
                Weight = (Enums.WeightCategories)parcel.Weight,
                Priority = (Enums.Priorities)parcel.Priority,
                ParcelStatus = !parcel.PickedUp.Equals(default),
                SenderLocation = new Location(sender.Longitude, sender.Latitude),
                TargetLocation = new Location(target.Longitude, target.Latitude),
                Distance = distance(sender.Latitude, sender.Longitude, sender.Latitude, sender.Longitude),
                Sender = new CustomerDelivery(sender.Id, sender.Name),
                Target = new CustomerDelivery(target.Id, target.Name)
            };
        }

        /// <summary>
        /// Convert a DAL customer to BL Customer In Parcel
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private CustomerDelivery mapCustomerInParcel(DO.Customer customer)
        {
            return new CustomerDelivery()
            {
                Id = customer.Id,
                Name = customer.Name
            };
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

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of BaseStationForList that maintain the predicate</returns>
        public IEnumerable<ParcelForList> GetParcelForList(Predicate<ParcelForList> predicate)
        {
            return GetParcelForList().Where(parcel => predicate(parcel));
        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to BL parcel 
        /// </summaryparfcel
        /// <returns>A list of parcels to print</returns>
        private IEnumerable<Parcel> getAllParcels()
        {
            return dal.GetParcels().Select(Parcel => GetBLParcel(Parcel.Id));
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
