using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public partial interface IDal
    {
        public void InsertParcel(Parcel parcel);
        public Parcel GetParcel(int idParcel);
        public IEnumerable<Parcel> GetParcels();
        public void UpdateParcelPickedUp(int idParcel);
        public void UpdateParcelDelivered(int idParcel);
        public IEnumerable<Parcel> UnassignedParcels();
        public IEnumerable<Parcel> GetParcelsProvided();
        public void DeleteParcel(int id);
        public void UpdatePickedUp(Parcel parcel);
        public void UpdateSupply(Parcel parcel);
    }
}
