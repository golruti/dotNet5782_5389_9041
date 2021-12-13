using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DalObject
{
    public partial class DalObject
    {
        //--------------------------------------------Adding-------------------------------------------------------------------------------------------
        /// <summary>
        /// Receipt of parcel for shipment.
        /// </summary>
        /// <param name="parcel">struct of parcel</param>
        public void InsertParcel(Parcel parcel)
        {
            if (!uniqueIDTaxCheck(DataSource.customers, parcel.SenderId))
                throw new KeyNotFoundException("Add parcel -DAL-:Sender not exist");
            if (!uniqueIDTaxCheck(DataSource.customers, parcel.TargetId))
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
            Parcel parcel = DataSource.parcels.First(parcel => parcel.Id == idParcel);
            if (parcel.GetType().Equals(default))
                throw new Exception("Get parcel -DAL-: There is no suitable customer in data");
            return parcel;
        }

        //--------------------------------------------Show list---------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            return DataSource.parcels.Select(parcel => parcel.Clone()).ToList();
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
        public void DeleteParcel(int id)
        {
            var parcel = DataSource.parcels.FirstOrDefault(d => d.Id == id);
            if (parcel.Equals(default(Parcel)))
                throw new Exception("Delete parcel -DAL-: There is no suitable customer in data");
            DataSource.parcels.Remove(parcel);
        }

        /// <summary>
        /// Package assembly by drone
        /// </summary>
        /// <param name="idParcel">Id of the parcel</param>
        public void UpdateParcelPickedUp(int idParcel)
        {
            for (int i = 0; i < DataSource.parcels.Count(); ++i)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    Parcel p = DataSource.parcels[i];
                    p.PickedUp = DateTime.Now;
                    DataSource.parcels[i] = p;
                }
            }
        }

        /// <summary>
        /// Delivery of a parcel to the destination
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelDelivered(int idParcel)
        {
            for (int i = 0; i < DataSource.parcels.Count; ++i)
            {
                if (DataSource.parcels[i].Id == idParcel)
                {
                    Parcel p = DataSource.parcels[i];
                    p.Delivered = DateTime.Now;
                    DataSource.parcels[i] = p;
                    break;
                }
            }
        }

        static int Index = 0;
        int IncreastNumberIndea()
        {
            return ++Index;
        }

        /// <summary>
        /// the function update that the parcel picked up
        /// </summary>
        /// <param name="parcel">update parcel</param>
        public void UpdatePickedUp(Parcel parcel)
        {
            Parcel tempParcel = DataSource.parcels.Find(item => parcel.Id == item.Id);
            DataSource.parcels.Remove(parcel);
            parcel.PickedUp = DateTime.Now;
            DataSource.parcels.Add(parcel);
        }

        /// <summary>
        /// update Delivery of a package by skimmer
        /// </summary>
        /// <param name="parcel">the parcel to update</param>
        public void UpdateSupply(Parcel parcel)
        {
            Parcel tempParcel = new Parcel();
            try
            {
                tempParcel = GetParcel(parcel.Id);
            }
            catch
            {
                throw new Exception("Get parcel -DAL-: There is no suitable parcel in data");
            }         
            tempParcel.Delivered = parcel.Delivered;
            DataSource.parcels.Add(tempParcel);
        }
    }
}
