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

        //------------------------------------------Add------------------------------------------
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


        //------------------------------------------Display------------------------------------------
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

        /// <summary>
        /// The function prepares a new array of all existing parcels
        /// </summary>
        /// <returns>array of parceles</returns>
        public IEnumerable<Parcel> GetParcels()
        {
            return DataSource.parcels.Select(parcel => parcel.Clone()).ToList();
        }


        /// <summary>
        /// Package assembly by drone
        ///   //אסיפת חבילה עי רחפן
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



        /// <summary>
        /// Displays a list of packages that have not yet been assigned to the glider
        /// </summary>
        /// <returns>array of parcels that have not yet been assigned to the glider</returns>
        public IEnumerable<Parcel> UnassignedParcels()
        {
            return new List<Parcel>(DataSource.parcels.Where(parcel => parcel.Droneld == 0).ToList());
        }

        static int Index = 0;
        int IncreastNumberIndea()
        {
            return ++Index;
        }

        /// <summary>
        /// The function returns the list of parcels provided to customers
        /// </summary>
        /// <returns>The list of parcels provided </returns>
        public IEnumerable<Parcel> GetParcelsProvided()
        {
            List<Parcel> parcelProvided = new List<Parcel>();
            foreach (var parcel in GetParcels())
            {
                if (!(parcel.Delivered.Equals(default(DateTime))))
                {
                    parcelProvided.Add(parcel);
                }
            }
            return parcelProvided;
        }

    }
}
