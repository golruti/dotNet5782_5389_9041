using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DalObject;
using IBL.BO;




namespace IBL
{
    class BL : IBL
    {
        private IDAl.IDal dal;
        private List<DroneForList> drones;
        private static Random rand = new Random();
        private enum parcelState { DroneNotAssociated, associatedNotCollected, collectedNotDelivered }


        public BL()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneForList>();
            initializeDrones();
        }


        private void initializeDrones()
        {
            foreach (var drone in dal.GetDrones())
            {
                DroneForList newDrone = new DroneForList();
                newDrone.Id = drone.Id;
                newDrone.Model = drone.Model;
                newDrone.MaxWeight = (Enums.WeightCategories)drone.MaxWeight;
                newDrone.DeliveryId = 0;
                newDrone.Battery = 1;

                if (IsDroneMakesDelivery(newDrone.Id))  //אם הרחפן מבצע משלוח
                {
                    newDrone.Status = BO.Enums.DroneStatuses.Delivery;
                    if (findParcelState(newDrone.Id) == parcelState.associatedNotCollected)
                    {
                        //newDrone.Battery =
                        //newDrone.Location =
                    }
                    else if (findParcelState(newDrone.Id) == parcelState.collectedNotDelivered)
                    {
                        // newDrone.Battery =
                        //newDrone.Location =
                    }
                }

                else //אם הרחפן לא מבצע משלוח
                {
                    newDrone.Status = (Enums.DroneStatuses)rand.Next(0, Enum.GetNames(typeof(Enums.DroneStatuses)).Length);
                    if (newDrone.Status == Enums.DroneStatuses.Maintenance)
                    {
                        //newDrone.Location =
                        newDrone.Battery = rand.Next(0, 20);
                    }
                    else if (newDrone.Status == Enums.DroneStatuses.Available)
                    {
                        // newDrone.Battery =
                        //newDrone.Location =
                    }
                }
                drones.Add(newDrone);
            }
        }



        private parcelState findParcelState(int DroneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                //החבילה שויכה ולא נאספה
                if (!(parcel.Scheduled.Equals(default(DateTime))) && parcel.PickedUp.Equals(default(DateTime)))
                {
                    return parcelState.associatedNotCollected;
                }
                //החבילה נאספה אך לא סופקה 
                if (!parcel.PickedUp.Equals(default(DateTime)) && parcel.Delivered.Equals(default(DateTime)))
                {
                    return parcelState.collectedNotDelivered;

                }
            }
            return parcelState.DroneNotAssociated;
        }



        private bool IsDroneMakesDelivery(int droneId)
        {
            foreach (var parcel in dal.GetParcels())
            {
                ParcelForList newParcel = new ParcelForList();
                //חבילה שלא סופקה והרחפן שויך
                if (parcel.Delivered.Equals(default(DateTime)) && parcel.Droneld == droneId)
                {
                    return true;
                }
            }
            return false;
        }





        //===================================================================================================================
        //===============================================הצגת רשימות========================================================
        //===================================================================================================================




        //---------------------------------------------תחנות בסיס----------------------------------------------
        public IEnumerable<BaseStationForList> GetBaseStationForList()
        {
            List<BaseStationForList> BaseStationsForList = new List<BaseStationForList>();
            foreach (var baseStation in dal.GetBaseStations())
            {
                BaseStationsForList.Add(new BaseStationForList()
                {
                    Id = baseStation.Id,
                    Name = baseStation.Name,
                    AvailableChargingPorts = numOfUsedChargingPorts(baseStation.Id),
                    UsedChargingPorts = (baseStation.ChargeSlote) - numOfUsedChargingPorts(baseStation.Id)
                });
            }
            return BaseStationsForList;
        }


        private int numOfUsedChargingPorts(int idBaseStation)
        {
            int countUsedChargingPorts = 0;
            foreach (var BaseStation in dal.GetAvaStations())
            {
                ++countUsedChargingPorts;
            }
            return countUsedChargingPorts;
        }




        //--------------------------------------------רחפנים----------------------------------------------
        public IEnumerable<DroneForList> GetDroneForList()
        {
            return drones;
        }




        //--------------------------------------------לקוחות----------------------------------------------
        public IEnumerable<CustomerForList> GetCustomerForList()
        {
            List<CustomerForList> CustomerForList = new List<CustomerForList>();
            List<IDAL.DO.Parcel> parcels = (List<IDAL.DO.Parcel>)dal.GetParcels();
            foreach (var customer in dal.GetCustomers())
            {
                CustomerForList.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime))),
                    NumParcelSentNotDelivered = dal.GetParcels().Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime))),
                    NumParcelReceived = dal.GetParcels().Count(parcel => parcel.TargetId == customer.Id && !parcel.Delivered.Equals(default(DateTime))),
                    NumParcelWayToCustomer = dal.GetParcels()
                                        .Count(parcel => parcel.SenderId == customer.Id && parcel.Delivered.Equals(default(DateTime)) && !parcel.PickedUp.Equals(default(DateTime)))
                });
            }
            return CustomerForList;
        }





        //--------------------------------------------חבילות----------------------------------------------

        public IEnumerable<ParcelForList> ParcelForList()
        {
            List<ParcelForList> ParcelsForList = new List<ParcelForList>();
            foreach (var parcel in dal.GetParcels())
            {
                ParcelsForList.Add(new ParcelForList()
                {
                    Id = parcel.Id,
                    Weight=(Enums.WeightCategories)parcel.Weight,
                    Priority = (Enums.Priorities)parcel.Priority,
                    SendCustomer = getCustom(),

                });
            }
            return ParcelsForList;
        }



    private string getCustom(IDAL.DO.Parcel parcel)
    {
        if (!(dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.SenderId)).Equals(default(IDAL.DO.Customer)))
            return (dal.GetCustomers().FirstOrDefault(customer => customer.Id == parcel.SenderId)).Equals(default(IDAL.DO.Customer)).Name;

    }s





        ----BL------

        public string SendCustomer { get; set; }
        public string ReceiveCustomer { get; set; }

        public ParcelStatuses Status { get; set; }


        -----DAL------
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }

        //זמן יצירת חבילה
        public DateTime Requested { get; set; }
        public int Droneld { get; set; }
        //זמן שיוך
        public DateTime Scheduled { get; set; }
        //זמן איסוף
        public DateTime PickedUp { get; set; }
        //זמן הגעה
        public DateTime Delivered { get; set; }

        public double TargetLongitude { get; set; }
        public double TargetLattitude { get; set; }
        public double SenderLongitude { get; set; }
        public double SenderLattitude { get; set; }




























        //public Drone CreateLogicDrone(int idDrone)
        //{
        //    var dalDrones = _dal.GetDrones();
        //    foreach (var drone in dalDrones)
        //    {

        //    }
        //}



        ///// <summary>
        ///// Assigning a parcel to a drone
        ///// </summary>
        ///// <param name="idxParcel">Id of the parcel</param>
        //public void UpdateParcelScheduled(int idxParcel)
        //{
        //    for (int i = 0; i < DataSource.drones.Count; ++i)
        //    {
        //        if (DataSource.drones[i].Status == IDAL.DO.Enum.DroneStatuses.Available)
        //        {
        //            Parcel p = DataSource.parcels[idxParcel];
        //            p.Scheduled = new DateTime();
        //            p.Droneld = DataSource.drones[i].Id;
        //            DataSource.parcels[idxParcel] = p;

        //            Drone d = DataSource.drones[i];
        //            d.Status = IDAL.DO.Enum.DroneStatuses.Maintenance;

        //            d.MaxWeight = DataSource.parcels[idxParcel].Weight;

        //            DataSource.drones[DataSource.drones.Count] = d;
        //            break;
        //        }
        //    }
        //}
    }
}
