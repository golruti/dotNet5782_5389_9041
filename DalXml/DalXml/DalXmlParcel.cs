using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static DO.Enum;
using Enum = System.Enum;

namespace DAL
{
    internal partial class DalXml
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
            var t = GetParcel(parcel.Id);
            if (!GetParcel(parcel.Id).Equals(default(Parcel)))
                throw new ThereIsAnObjectWithTheSameKeyInTheListException("Adding a parcel - DAL");

            parcel.IsDeleted = false;
            AddItem(parcelsPath, parcel);
        }

        //--------------------------------------------Show item-------------------------------------------------------------------------------------------
        /// <summary>
        /// Exits a parcel from an array of parcels by id
        /// </summary>
        /// <param name="idxParcel">struct ofo parcel</param>
        /// <returns>parcel</returns>
        public Parcel GetParcel(int idParcel)
        {
            return GetItem<Parcel>(parcelsPath, idParcel);
        }
        /// <summary>
        /// The function accepts a condition in the predicate and returns 
        /// the parcel that satisfies the condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Parcel GetParcel(Predicate<Parcel> predicate)
        {
            return GetParcels().FirstOrDefault(item => predicate(item));
        }

        //--------------------------------------------Show list---------------------------------------------------------------------------------------
        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            return GetList<Parcel>(parcelsPath);
        }

        /// <summary>
        /// The function receives a predicate and returns the list that maintains the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>List of Parsel that maintain the predicate</returns>
        public IEnumerable<Parcel> GetParcels(Predicate<Parcel> predicate)
        {
            return GetParcels().Where(item => predicate(item));
        }


        //--------------------------------------------Update-------------------------------------------------------------------------------------------
        /// <summary>
        ///Assigning a parcel to a drone
        /// </summary>
        /// <param name="idParcel">Id of the parcel</param>
        public void UpdateParcelPickedUp(int parcelId)
        {
            if (GetParcel(parcelId).Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }

            UpdateItem(parcelsPath, parcelId, nameof(Parcel.PickedUp), DateTime.Now);
        }



        /// <summary>
        /// Delivery of a parcel to the destination
        /// </summary>
        /// <param name="idxParcel">Id of the parcel</param>
        public void UpdateParcelDelivered(int parcelId)
        {
            if (GetParcel(parcelId).Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }
            UpdateItem(parcelsPath, parcelId, nameof(Parcel.Delivered), DateTime.Now);
            UpdateItem(parcelsPath, parcelId, nameof(Parcel.Droneld), -1);
        }

        /// <summary>
        /// update parcel assembly by drone
        /// </summary>
        /// <param name="parcel">the parcel to update</param>
        public void UpdateParcelScheduled(int parcelId, int droneId)
        {
            if (GetParcel(parcelId).Equals(default(Parcel)))
            {
                throw new KeyNotFoundException("Update parcel-DAL-There is no suitable parcel in data");
            }

            UpdateItem(parcelsPath, parcelId, nameof(Parcel.Scheduled), DateTime.Now);
            UpdateItem(parcelsPath, parcelId, nameof(Parcel.Droneld), droneId);
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

            UpdateItem(parcelsPath, id, nameof(Parcel.IsDeleted), true);
        }

        //------------------------------------------Private auxiliary functions--------------
        /// <summary>
        /// Auxiliary function that returns the running number and advances it.
        /// </summary>
        /// <returns></returns>
        private int RunNumberForParcel()
        {
            XDocument document = XDocument.Load(ConfigPath);

            XElement indexElement = document.Root.Element("Index");
            int index = int.Parse(indexElement.Value);
            indexElement.SetValue(++index);

            return index;
        }
    }
}
