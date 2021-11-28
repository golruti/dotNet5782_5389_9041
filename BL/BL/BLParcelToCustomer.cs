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
        /// <summary>
        /// The function returns the list of parcels at a customer - from the customer
        /// </summary>
        /// <returns>list of Parcels at a customer</returns>
        private List<ParcelToCustomer> findFromCustomer()
        {
            return getAllParcels().Select(parcel => ParcelToParcelAtCustomer(parcel, "sender")).ToList();
        }
        /// <summary>
        /// The function returns the list of packages at the customer - to the customer
        /// </summary>
        /// <returns>list of Parcels at a customer</returns>
        private List<ParcelToCustomer> findToCustomer()
        {
            return getAllParcels().Select(parcel => ParcelToParcelAtCustomer(parcel, "Recive")).ToList();
        }
    }
}
