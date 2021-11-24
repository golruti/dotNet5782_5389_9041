﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    partial class BL
    {
        //--------------------------------------------הוספת תחנת בסיס-------------------------------------------------------------------------------------------
        public void AddParcel(Parcel tempParcel)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel(dal.IncreastNumberIndea(), tempParcel.SenderId, tempParcel.ReceiverId, (IDAL.DO.Enum.WeightCategories)tempParcel.Weight, (IDAL.DO.Enum.Priorities)tempParcel.Priority, null, DateTime.Now, new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0));
            dal.InsertParcel(parcel);
        }
        //---------------------------------------------חבילה לפי ID ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public Parcel GetBLParcel(int parcelId)
        {
            return mapParcel(dal.GetParcel(parcelId));
        }



        private Parcel mapParcel(IDAL.DO.Parcel parcel)
        {
            var tmpDrone = drones.FirstOrDefault(drone => drone.Id == parcel.Droneld);
            return new Parcel()
            {
                Id = parcel.Id,
                SenderId = parcel.SenderId,
                ReceiverId = parcel.SenderId,
                Weight = (Enums.WeightCategories)parcel.Weight,
                Priority = (Enums.Priorities)parcel.Priority,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Requested = parcel.Requested,
                Delivered = parcel.Delivered,
                DroneParcel = tmpDrone != default ? mapDroneWithParcel(tmpDrone) : null
            };
        }


        //--------------------------------------------הצגת רשימת חבילות לרשימה--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
       public IEnumerable<ParcelForList> GetParcelForList()
        {
            List<ParcelForList> ParcelsForList = new List<ParcelForList>();
            foreach (var parcel in dal.GetParcels())
            {
                ParcelsForList.Add(new ParcelForList()
                {
                    Id = parcel.Id,
                    Weight = (Enums.WeightCategories)parcel.Weight,
                    Priority = (Enums.Priorities)parcel.Priority,
                    SendCustomer = getSendCustomerName(parcel),
                    ReceiveCustomer = getReceiveCustomer(parcel),
                    Status = getStatusCustomer(parcel)
                });
            }
            return ParcelsForList;
        }

        //--------------------------------------------רשימת חבילות שעוד לא שויכו לרחפן--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public IEnumerable<ParcelForList> UnassignedParcelsForList()
        {
            List<ParcelForList> ParcelsForList = new List<ParcelForList>();
            foreach (var parcel in dal.UnassignedParcels())
            {
                ParcelsForList.Add(new ParcelForList()
                {
                    Id = parcel.Id,
                    Weight = (Enums.WeightCategories)parcel.Weight,
                    Priority = (Enums.Priorities)parcel.Priority,
                    SendCustomer = getSendCustomerName(parcel),
                    ReceiveCustomer = getReceiveCustomer(parcel),
                    Status = getStatusCustomer(parcel)
                });
            }
            return ParcelsForList;
        }



        private IEnumerable<Parcel> getAllParcels()
        {
            return dal.GetParcels().Select(Parcel => GetBLParcel(Parcel.Id));
        }

        //לשימוש הקונסטרקטור
        private parcelState findParcelState(int DroneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                //החבילה שויכה ולא נאספה
                if (!(parcel.Scheduled.Equals(default(DateTime))) && parcel.PickedUp.Equals(default(DateTime)))
                {
                    return parcelState.associatedNotCollected;
                }
                //החבילה נאספה אך לא סופקה 
                if (!parcel.PickedUp.Equals(default(DateTime)) && parcel.Delivered.Equals(default(DateTime)))
                {
                    return parcelState.collectedNotDelivered;

                }
            }
            return parcelState.DroneNotAssociated;
        }




        private string getSendCustomerName(IDAL.DO.Parcel parcel)
        {
            if ((dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.SenderId)).Name.Equals(default(string)))
            {
                throw new Exception();
            }
            return (dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.SenderId)).Name;
        }

        private string getReceiveCustomer(IDAL.DO.Parcel parcel)
        {
            if ((dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.TargetId)).Name.Equals(default(string)))
            {
                throw new Exception();
            }
            return (dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.TargetId)).Name;
        }

        private BO.Enums.ParcelStatuses getStatusCustomer(IDAL.DO.Parcel parcel)
        {
            //החבילה נוצרה
            if (!(parcel.Requested.Equals(default(DateTime))))
            {
                return Enums.ParcelStatuses.Created;
            }
            //החבילה שויכה
            else if (parcel.Scheduled.Equals(default(DateTime)) && parcel.PickedUp.Equals(default(DateTime)) && parcel.Delivered.Equals(default(DateTime)))
            {
                return Enums.ParcelStatuses.Associated;
            }
            //החבילה נאספה
            else if (!(parcel.PickedUp.Equals(default(DateTime))) && parcel.Delivered.Equals(default(DateTime)))
            {
                return Enums.ParcelStatuses.Collected;
            }
            //החבילה סופקה
            else
            {
                return Enums.ParcelStatuses.Provided;
            }
        }


        private int findParceDeliveredlId(int droneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                if (parcel.Droneld == droneId)
                {
                    return parcel.Id;
                }
            }
            return 0;
        }



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
                Distance = Distance( sender.Latitude, sender.Longitude, sender.Latitude,sender.Longitude),
                Sender = new CustomerDelivery(sender.Id, sender.Name),
                Target = new CustomerDelivery(target.Id, target.Name)
            };
        }

    }
}