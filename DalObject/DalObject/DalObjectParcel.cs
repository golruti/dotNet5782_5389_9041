using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    public partial class DalObject
    {
        //--------------------------------------------Adding----------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
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
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int idParcel)
        {
            Parcel parcel = DataSource.parcels.FirstOrDefault(parcel => parcel.Id == idParcel);
            return parcel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(Predicate<Parcel> predicate)
        {
            Parcel parcel = DataSource.parcels.FirstOrDefault(parcel => predicate(parcel) && !(parcel.IsDeleted));
            return parcel;
        }

        //--------------------------------------------Show list---------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            IEnumerable<Parcel> parcels = new List<Parcel>();
            parcels = DataSource.parcels;
            return parcels;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            IEnumerable<Parcel> parcels = new List<Parcel>();
            parcels = DataSource.parcels.Where(parcel => predicate(parcel));
            return parcels;
        }


        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelPickedUp(int parcelId)
        {
            Parcel parcel = GetParcel(parcelId);
            if (parcel.Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }

            DeleteParcel(parcel.Id);
            parcel.PickedUp = DateTime.Now;
            AddParcel(parcel);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelDelivered(int parcelId)
        {
            Parcel parcel = GetParcel(parcelId);
            if (parcel.Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable customer in data");
            }
            DeleteParcel(parcel.Id);
            parcel.Delivered = DateTime.Now;
            parcel.Droneld = -1;
            AddParcel(parcel);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelScheduled(int parcelId, int droneId)
        {
            DeleteParcel(parcelId);
            var parcel = GetParcel(parcelId);
            parcel.Scheduled = DateTime.Now;
            parcel.Droneld = droneId;
            AddParcel(parcel);
        }


        //--------------------------------------------Delete-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            Parcel deletedParcel = GetParcel(id);
            if (deletedParcel.Equals(default(Parcel)))
                throw new KeyNotFoundException("Delete parcel -DAL-: There is no suitable parcel in data");
            if (deletedParcel.Scheduled != null && deletedParcel.Delivered == null)
                throw new TheParcelIsAssociatedAndCannotBeDeleted();
            else
            {
                DataSource.parcels.Remove(deletedParcel);
                deletedParcel.IsDeleted = true;
                DataSource.parcels.Add(deletedParcel);
            }
        }

        //------------------------------------------Private auxiliary functions--------------
        /// <summary>
        /// Auxiliary function that returns the running number For parcel ID and advances it.
        /// </summary>
        /// <returns></returns>
        private int RunNumberForParcel()
        {
            return (DataSource.Config.Index)++;
        }
    }
}
