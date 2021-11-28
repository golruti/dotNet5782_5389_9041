using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer = IBL.BO.Customer;
using Drone = IBL.BO.Drone;
using Parcel = IBL.BO.Parcel;
using BaseStation = IBL.BO.BaseStation;

namespace IBL
{
    public partial interface IBL
    {
        void UpdateCustomer(int customerId, string name, string phone);
        Customer GetCustomer(int requestedId);
        void AssignPackageToSkimmer(object d);
        void ReleaseDroneFromRecharge(int droneId, int time);
        void SendDroneToRecharge(int droneId);
        void UpdateBaseStation(int stationlId, string name, int chargeSlote);
        void UpdateDrone(int droneId, string model);
        void GetStation(int stationId);
        void AddDrone(Drone drone);
        //void SendDroneToRecharge(int droneId);
    }
}





