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
            Customer senderId = DataSource.customers.FirstOrDefault(c => c.Id == parcel.SenderId);
            Customer targetId = DataSource.customers.FirstOrDefault(c => c.Id == parcel.TargetId);

            if (senderId.Equals(default(Customer)))
                throw new KeyNotFoundException("Add parcel -DAL-:Sender not exist");
            if (targetId.Equals(default(Customer)))
                throw new KeyNotFoundException("Add parcel -DAL-:Target not exist");
            if (!(uniqueIDTaxCheck(DataSource.parcels, parcel.Id)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a parcel - DAL");
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
            if (parcel.GetType().Equals(default))
                throw new KeyNotFoundException("Get parcel -DAL-: There is no suitable customer in data");
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
            Parcel parcel = DataSource.parcels.FirstOrDefault(parcel => predicate(parcel));
            if (parcel.GetType().Equals(default))
                throw new KeyNotFoundException("Get parcel -DAL-: There is no suitable customer in data");
            return parcel;
        }

        //--------------------------------------------Show list---------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            return DataSource.parcels.ToList();
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Parsel that maintain the predicate</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            return DataSource.parcels.Where(drone => predicate(drone)).ToList();
        }

        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a particular parcel
        /// </summary>
        /// <param name="id"></param>
        public void DeleteParcel(int id)
        {
            var parcel = DataSource.parcels.FirstOrDefault(d => d.Id == id);
            if (parcel.Equals(default(Parcel)))
                throw new KeyNotFoundException("Delete parcel -DAL-: There is no suitable customer in data");
            DataSource.parcels.Remove(parcel);
        }

        /// <summary>
        ///Assigning a parcel to a drone
        /// </summary>
        /// <param name="idParcel">Id of the parcel</param>
        public void UpdateParcelPickedUp(int idParcel)
        {
            Parcel parcel = DataSource.parcels.FirstOrDefault(parcel => parcel.Id == idParcel);
            if (parcel.Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable customer in data");
            }
            parcel.PickedUp = DateTime.Now;
            try
            {
                DeleteParcel(parcel.Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            try
            {
                AddParcel(parcel);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Delivery of a parcel to the destination
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelDelivered(int idParcel)
        {
            Parcel parcel = DataSource.parcels.FirstOrDefault(parcel => parcel.Id == idParcel);
            if (parcel.Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable customer in data");
            }
            parcel.Delivered = DateTime.Now;
            try
            {
                DeleteParcel(parcel.Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            try
            {
                AddParcel(parcel);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// update parcel assembly by drone
        /// </summary>
        /// <param name="parcel">the parcel to update</param>
        public void UpdateSupply(Parcel parcel)
        {
            parcel.Scheduled = DateTime.Now;
            try
            {
                DeleteParcel(parcel.Id);
            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            try
            {
                AddParcel(parcel);
            }
            catch (ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                throw ex;
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
