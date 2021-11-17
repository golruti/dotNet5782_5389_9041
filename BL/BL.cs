using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using DalObject;
using IBL.BO;





namespace IBL
{
    private IDAL.IDal dal;
    private List<DroneForList> drones;
    private static Random rand = new Random();

    public BL():IBL
    {
        dal = new DalObject.DalObject();
        drones = new List<DroneForList>();

        private enum parcelState { DroneNotAssociated, associatedNotCollected, collectedNotDelivered }


        public BL()
        {
            dal = new DalObject.DalObject();
            drones = new List<DroneForList>();
            initializeDrones();
        }


        private void InitializeDrones()
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
                    newDrone.Status = (Enums.DroneStatuses)rand.Next(Enum.GetNames(typeof(Enums.DroneStatuses)).Length);
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



        


        










        public void AddBaseStation(int id,string name, double longitude, double latitude,int chargingStations)
        {
            BO.BaseStation tempBaseStation = new BO.BaseStation(id, name, longitude, latitude, chargingStations);

            IDAL.DO.BaseStation station = new IDAL.DO.BaseStation(tempBaseStation.Id, tempBaseStation.Name, tempBaseStation.Location.Longitude, tempBaseStation.Location.Latitude, chargingStations);
            dal.InsertStation(station);
            
        }






        public void UpdateDroneModel(int id,string model)
        {
            DroneForList tempDroneForList = drones.Find(item=> item.Id == id);
            drones.Remove(tempDroneForList);
            tempDroneForList.Model = model;
            drones.Add(tempDroneForList);
            dal.DeleteDrone(id);
            IDAL.DO.Drone drone = new IDAL.DO.Drone(tempDroneForList.Id, tempDroneForList.Model, (IDAL.DO.Enum.WeightCategories)tempDroneForList.MaxWeight);
            dal.InsertDrone(drone);


        }

        public void UpdateParcelScheduled(int idxParcel)
        {
            throw new NotImplementedException();
        }





        public IDAL.DO.BaseStation GetStation(int idxStation)
        {
            throw new NotImplementedException();
        }




        public List<IDAL.DO.BaseStation> GetStations()
        {
            throw new NotImplementedException();
        }





        public List<IDAL.DO.BaseStation> GetAvaStations()
        {
            throw new NotImplementedException();
        }

        //public Drone CreateLogicDrone(int idDrone)
        //{
        //    var dalDrones = _dal.GetDrones();
        //    foreach (var drone in dalDrones)
        //    {

        //    }
        //}

        public void AddCustomer(int idCustomer, string nameCustomer, string phoneCustomer, int longitudeCustomer, int latitudeCustomer)
        {
            throw new NotImplementedException();
        }



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
