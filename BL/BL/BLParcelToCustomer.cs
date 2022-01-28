using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BL
{
    partial class BL
    {
        /// <summary>
        /// The function returns the list of parcels at a customer - from the customer
        /// </summary>
        /// <returns>list of Parcels at a customer</returns>
        private IEnumerable<ParcelToCustomer> findFromCustomer()
        {
            return GetAllParcels().Select(parcel => parcelToParcelAtCustomer(parcel, "sender")).ToList();
        }
        /// <summary>
        /// The function returns the list of packages at the customer - to the customer
        /// </summary>
        /// <returns>list of Parcels at a customer</returns>
        private IEnumerable<ParcelToCustomer> findToCustomer()
        {
            return GetAllParcels().Select(parcel => parcelToParcelAtCustomer(parcel, "Recive")).ToList();
        }
    }
}
