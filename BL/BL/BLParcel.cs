using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
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

        public void UpdateParcelAffiliation(int parcelId, int droneId, DateTime dateTime)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(parcelId);
            dal.DeleteParcel(parcelId);
            parcel.Droneld = droneId;
            parcel.Scheduled = dateTime;
            dal.InsertParcel(parcel);
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
        /// Convert a DAL parcel to BL parcel
        /// </summary>
        /// <param name="parcel">he parcel to convert</param>
        /// <returns>The converted parcel</returns>
        private Parcel mapParcel(IDAL.DO.Parcel parcel)
        {
            var tmpDrone = drones.FirstOrDefault(drone => drone.Id == parcel.Droneld);
            return new Parcel()
            {
                Id = parcel.Id,
                CustomerReceives = MapCustomerInParcel(dal.GetCustomer(parcel.TargetId)),
                CustomerSender = MapCustomerInParcel(dal.GetCustomer(parcel.SenderId)),
                Weight = (Enums.WeightCategories)parcel.Weight,
                Priority = (Enums.Priorities)parcel.Priority,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Requested = parcel.Requested,
                Delivered = parcel.Delivered,
                DroneParcel = tmpDrone != default ? mapDroneWithParcel(tmpDrone) : null
            };
        }

        //--------------------------------------------Show list--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

        public ParcelForList GetParcelFormList(int id)
        {
            List<ParcelForList> parcelsForList = (List<ParcelForList>)GetParcelForList();
            ParcelForList parcelForList = parcelsForList.Find(item => item.Id == id);
            return parcelForList;
        }

        /// <summary>
        /// The function returns the list of parcels that have not yet been associated with the drone
        /// </summary>
        /// <returns>List of parcels not yet associated with the drone</returns>
        public IEnumerable<ParcelForList> UnassignedParcelsForList()
        {
            List<ParcelForList> ParcelsForList = new List<ParcelForList>();
            foreach (var parcel in dal.UnassignedParcels())
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
        /// The function returns the parcel status of a particular drone
        /// </summary>
        /// <param name="DroneId"></param>
        /// <returns>The parcel status</returns>
        private parcelState findParcelState(int DroneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                if (!(parcel.Scheduled.Equals(default(DateTime))) && parcel.PickedUp.Equals(default(DateTime)))
                {
                    return parcelState.associatedNotCollected;
                }
                if (!parcel.PickedUp.Equals(default(DateTime)) && parcel.Delivered.Equals(default(DateTime)))
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
        private string getSendCustomerName(IDAL.DO.Parcel parcel)
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
        private string getReceiveCustomer(IDAL.DO.Parcel parcel)
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
        private BO.Enums.ParcelStatuses getParcelStatus(IDAL.DO.Parcel parcel)
        {
            if (!(parcel.Requested.Equals(default(DateTime))))
            {
                return Enums.ParcelStatuses.Created;
            }
            else if (parcel.Scheduled.Equals(default(DateTime)) && parcel.PickedUp.Equals(default(DateTime)) && parcel.Delivered.Equals(default(DateTime)))
            {
                return Enums.ParcelStatuses.Associated;
            }
            else if (!(parcel.PickedUp.Equals(default(DateTime))) && parcel.Delivered.Equals(default(DateTime)))
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



        /// <summary>
        /// Convert a DAL parcel to Parcel In Transfer
        /// </summary>
        /// <param name="id">The requested parcel to convert</param>
        /// <returns>The converted parcel</returns>
        private ParcelByTransfer CreateParcelInTransfer(int id)
        {
            IDAL.DO.Parcel parcel = dal.GetParcel(id);
            IDAL.DO.Customer sender = dal.GetCustomer(parcel.SenderId);
            IDAL.DO.Customer target = dal.GetCustomer(parcel.TargetId);
            return new ParcelByTransfer
            {
                Id = id,
                Weight = (Enums.WeightCategories)parcel.Weight,
                Priority = (Enums.Priorities)parcel.Priority,
                ParcelStatus = !parcel.PickedUp.Equals(default),
                SenderLocation = new Location(sender.Longitude, sender.Latitude),
                TargetLocation = new Location(target.Longitude, target.Latitude),
                Distance = Distance(sender.Latitude, sender.Longitude, sender.Latitude, sender.Longitude),
                Sender = new CustomerDelivery(sender.Id, sender.Name),
                Target = new CustomerDelivery(target.Id, target.Name)
            };
        }
        /// <summary>
        /// Convert a DAL customer to BL Customer In Parcel
        /// </summary>
        /// <param name="parcel">The customer to convert</param>
        /// <returns>The converted customer</returns>
        private CustomerDelivery MapCustomerInParcel(IDAL.DO.Customer customer)
        {
            return new CustomerDelivery()
            {
                Id = customer.Id,
                Name = customer.Name
            };
        }

        /// <summary>
        /// Retrieves the list of parcels from the data and converts it to BL parcel 
        /// </summaryparfcel
        /// <returns>A list of parcels to print</returns>
        private IEnumerable<Parcel> getAllParcels()
        {
            return dal.GetParcels().Select(Parcel => GetParcel(Parcel.Id));
        }

        /// <summary>
        /// Retrieves the requested parcel from the data and converts it to BL parcel
        /// </summary>
        /// <param name="id">The requested parcel id</param>
        /// <returns>A Bl parcel to print</returns>
        public Parcel GetParcel(int id)
        {
            try
            {
                return mapParcel(dal.GetParcel(id));
            }
            catch (KeyNotFoundException ex)
            {

                throw new KeyNotFoundException(ex.Message);
            }
        }

    }
}
