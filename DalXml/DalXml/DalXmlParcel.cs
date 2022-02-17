using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DO.Enum;
using Enum = System.Enum;

namespace DAL
{
    internal partial class DalXml
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
            AddItem(parcelsPath, parcel);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int idParcel)
        {
            return GetItem<Parcel>(parcelsPath, idParcel);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(Predicate<Parcel> predicate)
        {
            return GetParcels().FirstOrDefault(item => predicate(item));
        }

        //--------------------------------------------Show list-------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels()
        {
            return GetList<Parcel>(parcelsPath);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {

            return GetParcels().Where(item => predicate(item));
        }


        //--------------------------------------------Update--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelPickedUp(int parcelId)
        {
            if (GetParcel(parcelId).Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }

            UpdateItem(parcelsPath, parcelId, nameof(Parcel.PickedUp), DateTime.Now);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelDelivered(int parcelId)
        {
            if (GetParcel(parcelId).Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }
            UpdateItem(parcelsPath, parcelId, nameof(Parcel.Delivered), DateTime.Now);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateParcelScheduled(int parcelId, int droneId)
        {
            if (GetParcel(parcelId).Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }
            UpdateItem(parcelsPath, parcelId, nameof(Parcel.Scheduled), DateTime.Now);
            UpdateItem(parcelsPath, parcelId, nameof(Parcel.Droneld), droneId);
        }


        //--------------------------------------------Delete--------------------------------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeleteParcel(int id)
        {
            Parcel deletedParcel = GetParcel(id);
            if (deletedParcel.Equals(default(Parcel)))
                throw new KeyNotFoundException("Delete parcel -DAL-: There is no suitable parcel in data");
            if (deletedParcel.Scheduled != null && deletedParcel.Delivered == null)
                throw new TheParcelIsAssociatedAndCannotBeDeleted();
            UpdateItem(parcelsPath, id, nameof(Parcel.IsDeleted), true);
        }

        //------------------------------------------Private auxiliary functions--------------
        /// <summary>
        /// Auxiliary function that returns the running number For parcel ID and advances it.
        /// </summary>
        /// <returns></returns>
        private int RunNumberForParcel()
        {
            XDocument document = XDocument.Load(ConfigPath);

            XElement indexElement = document.Root.Element("Index");
            int index = int.Parse(indexElement.Value);
            indexElement.SetValue(index + 1);

            document.Save(ConfigPath);
            return index;
        }
    }
}
