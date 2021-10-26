using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    interface IBL
    {
        public void InsertStation(Station station);
        public void InsertDrone(Drone drone);
        public void InsertCustomer(Customer customer);
        public void InsertParcel(Parcel parcel);
        public void UpdateParcelScheduled(int idxParcel);
        public void UpdateParcelPickedUp(int idxParcel);
        public void UpdateParcelDelivered(int idxParcel);
        public bool TryAddDroneCarge(int droneId);
        public bool TryRemoveDroneCarge(int droneId);
        public Station GetStation(int idxStation);
        public Drone GetDrone(int idxDrone);
        public Customer GetCustomer(int idxCustomer);
        public Parcel GetParcel(int idxParcel);
        public List<Station> GetStations();
        public List<Customer> GetCustomers();
        public List<Drone> GetDrones();
        public List<Parcel> GetParcels();
        public List<Parcel> UnassignedPackages();
        public List<Station> GetAvaStations();
    }
}
