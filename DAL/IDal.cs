using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDal
{
    public interface IDal
    {
        public void InsertStation(Station station);
        public void InsertDrone(Drone drone);
        public void InsertCustomer(Customer customer);
        public void InsertParcel(Parcel parcel);
        //public void UpdateParcelScheduled(int idxParcel);
        //public void UpdateParcelPickedUp(int idxParcel);
        //public void UpdateParcelDelivered(int idxParcel);
        //public bool TryAddDroneCarge(int droneId);
        //public bool TryRemoveDroneCarge(int droneId);
        public Station GetStation(int idxStation);
        public Drone GetDrone(int idxDrone);
        public Customer GetCustomer(int idxCustomer);
        public Parcel GetParcel(int idxParcel);
        public IEnumerable<Station> GetStations();
        public IEnumerable<Customer> GetCustomers();
        public IEnumerable<Drone> GetDrones();
        public IEnumerable<Parcel> GetParcels();
        public IEnumerable<Parcel> UnassignedPackages();
        public IEnumerable<Station> GetAvaStations();
    }
}
