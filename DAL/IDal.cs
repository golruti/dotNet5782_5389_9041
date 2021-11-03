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
        void InsertStation(Station station);
        void InsertDrone(Drone drone);
        void InsertCustomer(Customer customer);
        void InsertParcel(Parcel parcel);
        // void UpdateParcelScheduled(int idxParcel);
        void UpdateParcelPickedUp(int idxParcel);
        void UpdateParcelDelivered(int idxParcel);
        bool TryAddDroneCarge(int droneId);
        bool TryRemoveDroneCarge(int droneId);
        Station GetStation(int idxStation);
        Drone GetDrone(int idxDrone);
        Customer GetCustomer(int idxCustomer);
        Parcel GetParcel(int idxParcel);
        IEnumerable<Station> GetStations();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Drone> GetDrones();
        IEnumerable<Parcel> GetParcels();
        IEnumerable<Parcel> UnassignedPackages();
        IEnumerable<Station> GetAvaStations();

        double[] DronePowerConsumptionRequest(); 
    }
}
