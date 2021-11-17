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

        //לשימוש הקונסטרקטור
        //מחזיר את סטטוס הרחפן
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

        //--------------------------------------------הצגת רשימת חבילות לרשימה--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<ParcelForList> ParcelForList()
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

        public void AddParcel(int idSender, int idReceiver, int weight, int priority)
        {
            Parcel tempParcel = new Parcel(idSender, idReceiver, (Enums.WeightCategories)weight, (Enums.Priorities)priority);
            IDAl.DO.Parcel parcel = new IDAL.DO.Parcel(tempParcel.SenderId, tempParcel.ReceiverId, (IDAL.DO.Enum.WeightCategories)tempParcel.Weight, (IDAL.DO.Enum.Priorities)tempParcel.Priority, null, DateTime.Now, new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0), new DateTime(0, 0, 0, 0, 0, 0, 0));
            dal.InsertParcel(parcel);
        }

    }
}
