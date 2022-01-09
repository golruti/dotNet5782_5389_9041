﻿using System;
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

            if (GetParcel(parcel.SenderId).GetType().Equals(default))
                throw new KeyNotFoundException("Add parcel -DAL-:Sender not exist");
            if (GetParcel(parcel.TargetId).GetType().Equals(default))
                throw new KeyNotFoundException("Add parcel -DAL-:Target not exist");
            if (!GetParcel(parcel.Id).GetType().Equals(default))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a parcel - DAL");
            parcel.IsDeleted = false;
            DataSource.parcels.Add(parcel.Id, parcel);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Exits a parcel from an array of parcels by id
        /// </summary>
        /// <param name="idxParcel">struct ofo parcel</param>
        /// <returns>parcel</returns>
        public Parcel GetParcel(int idParcel)
        {
            Parcel parcel = DataSource.parcels.Values.FirstOrDefault(parcel => parcel.Id == idParcel);
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
            Parcel parcel = DataSource.parcels.Values.FirstOrDefault(parcel => predicate(parcel) && !(parcel.IsDeleted));
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
            return DataSource.parcels.Values.Where(parcel => !(parcel.IsDeleted)).ToList();
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Parsel that maintain the predicate</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            return DataSource.parcels.Values.Where
                (parcel => predicate(parcel) && !(parcel.IsDeleted)).ToList();
        }


        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        
        /// <summary>
        ///Assigning a parcel to a drone
        /// </summary>
        /// <param name="idParcel">Id of the parcel</param>
        public void UpdateParcelPickedUp(Parcel Parcel)
        {
            Parcel parcel = GetParcels().FirstOrDefault(parcel => parcel.Id == Parcel.Id);
            if (parcel.GetType().Equals(default))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable customer in data");
            }
            parcel.PickedUp = DateTime.Now;

            DeleteParcel(parcel.Id);
            AddParcel(parcel);
        }

        /// <summary>
        /// Delivery of a parcel to the destination
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelDelivered(Parcel Parcel)
        {
            Parcel parcel = GetParcels().FirstOrDefault(parcel => parcel.Id == Parcel.Id);
            if (parcel.GetType().Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable customer in data");
            }
            parcel.Delivered = DateTime.Now;
            DeleteParcel(parcel.Id);
            AddParcel(parcel);
        }

        /// <summary>
        /// update parcel assembly by drone
        /// </summary>
        /// <param name="parcel">the parcel to update</param>
        public void UpdateSupply(Parcel parcel)
        {
            parcel.Scheduled = DateTime.Now;
            DeleteParcel(parcel.Id);
            AddParcel(parcel);
        }

        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        /// <summary>
        /// The function deletes a particular parcel
        /// </summary>
        /// <param name="id"></param>
        public void DeleteParcel(int id)
        {
            if (!DataSource.parcels.Remove(id))
                throw new KeyNotFoundException("Delete parcel -DAL-: There is no suitable parcel in data");

            var deletedParcel = GetParcel(id);
            DataSource.parcels.Remove(deletedParcel.Id);
            deletedParcel.IsDeleted = true;
            DataSource.parcels.Add(deletedParcel.Id, deletedParcel);
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
