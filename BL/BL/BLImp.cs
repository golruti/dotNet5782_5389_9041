using BO;
using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using DalApi;
using static BO.Enums;

namespace BL
{
    internal sealed partial class BLImp /*: IBL*/
    {
    //    static readonly Random rand = new();
   
    //    List<DroneForList> drones { get; set; }

    //    internal readonly double[] BatteryUsages;
    //    internal const int DRONE_FREE = 0;
    //    internal const int DRONE_CHARGE = 4;
    //    private static DalApi.IDal dal;

    //    BLImp()
    //    {
    //        dal = DalApi.DalFactory.GetDal();
    //        BatteryUsages = dal.BatteryUsages().Select(n => n / 100.0).ToArray();
    //        initializeDrones();
    //    }

    //    void initializeDrones()
    //    {
    //        drones = (from d in dal.GetDrones()
    //                  let drone = (DO.Drone)d
    //                  select new DroneForList
    //                  {
    //                      Id = drone.Id,
    //                      Model = drone.Model,
    //                      MaxWeight = (WeightCategories)drone.MaxWeight,
    //                      ParcelDeliveredId = null
    //                  }).ToList();

    //        foreach (var drone in drones)
    //        {
    //            try
    //            {
    //                if (rand.NextDouble() < 0.5)
    //                    throw new Exception(); // to go to catch
    //                drone.Location = dal.GetBaseStation(dal.GetDroneChargeBaseStationId(drone.Id)).Location();
    //                drone.Status = DroneStatuses.Maintenance;
    //                drone.Battery = 0.05 + 0.15 * rand.NextDouble();
    //            }
    //            catch (Exception)
    //            {
    //                int? parcelId = dal.GetParcels().FirstOrDefault(p => p.Droneld == drone.Id
    //                                                              && p.Scheduled != null
    //                                                              && p.Delivered == null).Id;
    //                if (parcelId != null)
    //                {
    //                    drone.ParcelDeliveredId = parcelId;
    //                    drone.Status = DroneStatuses.Delivery;
    //                    drone.Location = findDroneLocation(drone);
    //                    double minBattery = drone.RequiredBattery(this, (int)parcelId);
    //                    drone.Battery = minBattery + rand.NextDouble() * (1 - minBattery);
    //                }
    //                else
    //                {
    //                    drone.Status = DroneStatuses.Available;
    //                    drone.Location = findDroneLocation(drone);
    //                    double minBattery = BatteryUsages[(int)BatteryUsage.Available] * drone.Distance(FindClosestBaseStation(drone, charge: false));
    //                    drone.Battery = minBattery + rand.NextDouble() * (1 - minBattery);
    //                }
    //            }
    //        }
    //    }

    //    private Location findDroneLocation(DroneForList drone)
    //    {
    //        int parcelId = drone.ParcelDeliveredId ?? 0;

    //        switch (drone.Status)
    //        {
    //            case DroneStatuses.Maintenance:
    //                return dal.GetBaseStation(dal.GetDroneChargeBaseStationId(drone.Id)).Location();

    //            case DroneStatuses.Delivery:
    //                DO.Parcel parcel = dal.GetParcel(parcelId);
    //                if (parcel.PickedUp == null)
    //                {
    //                    Customer customer = GetCustomer(parcel.SenderId);
    //                    return FindClosestBaseStation(customer, charge: false).Location;
    //                }
    //                if (parcel.Delivered == null)
    //                {
    //                    return GetCustomer(parcel.SenderId).Location;
    //                }
    //                return dal.GetCustomer(parcel.TargetId).Location();

    //            case DroneStatuses.Available:
    //                if (parcelId != 0)
    //                    return GetCustomer(dal.GetParcel(parcelId).TargetId).Location;

    //                var targetsIds = dal.GetParcels(parcel => parcel?.DroneId == drone.Id)
    //                    .Select(parcel => parcel?.TargetId ?? 0);
    //                if (rand.NextDouble() < 0.5 && targetsIds.Any())
    //                {
    //                    var ids = targetsIds.ToArray();
    //                    return GetCustomer(ids[rand.Next(ids.Length)]).Location;
    //                }

    //                var stations = dal.GetBaseStations().ToArray();
    //                return stations[rand.Next(1, stations.Length)]?.Location();

    //            default:
    //                return new Location();
    //        }
    //    }

    //    internal BaseStation FindClosestBaseStation(Location fromLocatable, bool charge) =>
    //        (from s in dal.GetBaseStations(bs => !charge || bs.AvailableChargingPorts > 0)
    //         let l = (DO.BaseStation)s
    //         let bs = new BaseStation
    //         {
    //             Id = l.Id,
    //             Name = l.Name,
    //             AvailableChargingPorts = l.AvailableChargingPorts,
    //             DronesInCharging = from dc in dal.GetDronesCharges(dc => dc.StationId == l.Id)
    //                                let id = dc?.DroneId ?? 0
    //                                let drone = drones.Find(d => d.Id == id)
    //                                select new DroneInCharging { Id = id, Battery = drone.Battery },
    //             Location = new Location { Latitude = l.Latitude, Longitude = l.Longitude }
    //         }
    //         select new { Distance = fromLocatable.Distance(bs), Station = bs }
    //        ).Aggregate(new { Distance = double.PositiveInfinity, Station = (BaseStation)null },
    //                    (closest, next) => next.Distance < closest.Distance ? next : closest, best => best.Station);

    //    public void StartDroneSimulator(int id, Action update, Func<bool> checkStop) => new DroneSimulator(this, id, update, checkStop);
    }
}
