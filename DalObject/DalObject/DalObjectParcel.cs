using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Receipt of parcel for shipment.
        /// </summary>
        /// <param name="parcel">struct of parcel</param>
        public void AddParcel(Parcel parcel)
        {
            parcel.Id = RunNumberForParcel();

            if (GetCustomer(parcel.SenderId).Equals(default(Customer)))
                throw new KeyNotFoundException("Add parcel -DAL-:Sender not exist");
            if (GetCustomer(parcel.TargetId).Equals(default(Customer)))
                throw new KeyNotFoundException("Add parcel -DAL-:Target not exist");
            if (!GetParcel(parcel.Id).Equals(default(Parcel)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a parcel - DAL");
            parcel.IsDeleted = false;
            DataSource.parcels.Add(parcel);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Exits a parcel from an array of parcels by id
        /// </summary>
        /// <param name="idxParcel">struct ofo parcel</param>
        /// <returns>parcel</returns>
        public Parcel GetParcel(int idParcel)
        {
            Parcel parcel = DataSource.parcels.FirstOrDefault(parcel => parcel.Id == idParcel);
            return parcel;
        }
        /// <summary>
        /// The function accepts a condition in the predicate and returns 
        /// the parcel that satisfies the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Parcel GetParcel(Predicate<Parcel> predicate)
        {
            Parcel parcel = DataSource.parcels.FirstOrDefault(parcel => predicate(parcel) && !(parcel.IsDeleted));
            return parcel;
        }

        //--------------------------------------------Show list---------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            IEnumerable<Parcel> parcels = new List<Parcel>();
            parcels = DataSource.parcels.Where(parcel => !(parcel.IsDeleted));
            return parcels;
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Parsel that maintain the predicate</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            IEnumerable<Parcel> parcels = new List<Parcel>();
            parcels = DataSource.parcels.Where(parcel => predicate(parcel) && !(parcel.IsDeleted));
            return parcels;
        }


        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        /// <summary>
        ///Assigning a parcel to a drone
        /////החבילה נאספה עי הרחפן
        /// </summary>
        /// <param name="idParcel">Id of the parcel</param>
        public void UpdateParcelPickedUp(int parcelId)
        {
            Parcel parcel = GetParcel( parcelId);
            if (parcel.Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }

            DeleteParcel(parcel.Id);
            parcel.PickedUp = DateTime.Now;
            AddParcel(parcel);
        }

        /// <summary>
        /// Delivery of a parcel to the destination
        /// //הרחפן-החבילה הגיעה ליעד
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelDelivered(int parcelId)
        {
            Parcel parcel = GetParcel( parcelId);
            if (parcel.Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable customer in data");
            }
            DeleteParcel(parcel.Id);
            parcel.Delivered = DateTime.Now;
            parcel.Droneld = -1;
            AddParcel(parcel);
        }


        /// <summary>
        /// update parcel assembly by drone
        /// // החבילה שויכה לרחפן
        /// </summary>
        /// <param name="parcel">the parcel to update</param>
        /// ()supply
        public void UpdateParcelScheduled(int parcelId, int droneId)
        {
            DeleteParcel(parcelId);
            var parcel = GetParcel(parcelId);
            parcel.Scheduled = DateTime.Now;
            parcel.Droneld = droneId;
            AddParcel(parcel);
        }


        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a particular parcel
        /// </summary>
        /// <param name="id"></param>
        public void DeleteParcel(int id)
        {
            Parcel deletedParcel = GetParcel(id);
            if (deletedParcel.Equals(default(Parcel)))
                throw new KeyNotFoundException("Delete parcel -DAL-: There is no suitable parcel in data");
            else
            {
                DataSource.parcels.Remove(deletedParcel);
                deletedParcel.IsDeleted = true;
                DataSource.parcels.Add(deletedParcel);
            }
        }

        //------------------------------------------Private auxiliary functions--------------
        /// <summary>
        /// Auxiliary function that returns the running number and advances it.
        /// </summary>
        /// <returns></returns>
        private int RunNumberForParcel()
        {
            return (DataSource.Config.Index)++;
        }
    }
}
