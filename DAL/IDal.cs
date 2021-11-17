using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDal
    {
        void InsertStation(BaseStation station);
        void InsertDrone(Drone drone);
        void InsertCustomer(Customer customer);
        void InsertParcel(Parcel parcel);
        void UpdateParcelScheduled(int idxParcel);
        void UpdateParcelPickedUp(int idxParcel);
        public void UpdateDroneStatus(int IdParcel);
        void UpdateParcelDelivered(int idxParcel);
        bool TryAddDroneCarge(int droneId);
        bool TryRemoveDroneCarge(int droneId);
        BaseStation GetStation(int idxStation);
        Drone GetDrone(int idxDrone);
        Customer GetCustomer(int idxCustomer);
        Parcel GetParcel(int idxParcel);
        IEnumerable<BaseStation> GetAvaBaseStations();
        IEnumerable<BaseStation> GetBaseStations();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Drone> GetDrones();
        IEnumerable<Parcel> GetParcels();
        IEnumerable<Parcel> UnassignedParcels();
        double[] DronePowerConsumptionRequest();
        void DeleteDrone(int id);
        
        int IncreastNumberIndea();
        void DeleteBaseStation(int id);
    }
}
