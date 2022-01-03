﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ConvertFunctions
    {


        public static Enums.WeightCategories BOEnumWeightCategoriesToPO(BO.Enums.WeightCategories weight)
        {
            return (Enums.WeightCategories)weight;
        }
        public static Enums.DroneStatuses BOEnumDroneStatusesToPO(BO.Enums.DroneStatuses status)
        {
            return (Enums.DroneStatuses)status;
        }

        public static Enums.Priorities BOEnumPrioritiesToPO(BO.Enums.Priorities prioruty)
        {
            return (Enums.Priorities)prioruty;
        }

        public static Enums.ParcelStatuses BOEnumParcelStatusToPO(BO.Enums.ParcelStatuses parcelStatuses)
        {
            return (Enums.ParcelStatuses)parcelStatuses;
        }

        public static BO.Enums.WeightCategories POEnumWeightCategoriesToBO(Enums.WeightCategories weight)
        {
            return (BO.Enums.WeightCategories)weight;
        }



        public static BO.Enums.DroneStatuses POEnumDroneStatusesToBO(Enums.DroneStatuses status)
        {
            return (BO.Enums.DroneStatuses)status;
        }

        

        public static BO.Enums.Priorities POEnumPrioritiesToBO(Enums.Priorities prioruty)
        {
            return (BO.Enums.Priorities)prioruty;
        }

        public static BO.Enums.ParcelStatuses POEnumParcelStatusToBO(Enums.ParcelStatuses parcelStatuses)
        {
            return (BO.Enums.ParcelStatuses)parcelStatuses;
        }

        public static Location BOLocationToPO(BO.Location location)
        {
            return new Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }


        public static BO.Location POLocationToBO(Location location)
        {
            return new BO.Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }


        public static DroneInParcel BODroneParceToPO(BO.DroneInParcel droneParcel)
        {
            return new DroneInParcel
            {
                Id = droneParcel.Id,
                Battery = droneParcel.Battery,
                Location = BOLocationToPO(droneParcel.Location)
            };
        }
        public static CustomerDelivery BOCustomerDeliveryToPO(BO.CustomerDelivery customerDelivery)
        {
            if (customerDelivery == null)
                return null;
            return new CustomerDelivery
            {
                Id = customerDelivery.Id,
                Name = customerDelivery.Name,
            };
        }


        public static Parcel BOParcelToPO(BO.Parcel parcel)
        {
            return new Parcel
            {
                Id = parcel.Id,
                CustomerSender = BOCustomerDeliveryToPO(parcel.CustomerSender),
                CustomerReceives = BOCustomerDeliveryToPO(parcel.CustomerReceives),
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                DroneParcel = BODroneParceToPO(parcel.DroneParcel),
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Delivered = parcel.Delivered
            };
        }

        public static IEnumerable<ParcelToCustomer> BOParcelToCustomerToPO(IEnumerable<BO.ParcelToCustomer> parcelsToCustomer)
        {
            if (parcelsToCustomer == null)
                return Enumerable.Empty<ParcelToCustomer>();
            return parcelsToCustomer.Select(parcel => new ParcelToCustomer()
            {
                Id = parcel.Id,
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                Status = BOEnumParcelStatusToPO(parcel.Status),
                Customer = BOCustomerDeliveryToPO(parcel.Customer)
            });
        }
        public static Customer BOCustomerToPO(BO.Customer customer)
        {
            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                Location = BOLocationToPO(customer.Location),
                FromCustomer = BOParcelToCustomerToPO(customer.FromCustomer),
                ToCustomer = BOParcelToCustomerToPO(customer.ToCustomer)
            };
        }


        public static ObservableCollection<CustomerForList> BOCustomerForListToPO(IEnumerable<BO.CustomerForList> customersForList)
        {
            ObservableCollection<PO.CustomerForList> c = new ObservableCollection<CustomerForList>();

            foreach (var customer in customersForList)
            {
                c.Add(new CustomerForList()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    NumParcelSentDelivered = customer.NumParcelSentDelivered,
                    NumParcelSentNotDelivered = customer.NumParcelSentNotDelivered,
                    NumParcelReceived = customer.NumParcelReceived,
                    NumParcelWayToCustomer = customer.NumParcelWayToCustomer
                });
            }

            //if (customersForList == null)
            //    return ObservableCollection.Empty<CustomerForList>();
            return c;
        }

        public static BO.CustomerForList POCustomerForListToBO(CustomerForList customer)
        {
            return new BO.CustomerForList()
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                NumParcelSentDelivered = customer.NumParcelSentDelivered,
                NumParcelSentNotDelivered = customer.NumParcelSentNotDelivered,
                NumParcelReceived = customer.NumParcelReceived,
                NumParcelWayToCustomer = customer.NumParcelWayToCustomer
            };
        }

        public static ParcelByTransfer BOParcelByTransferToPO(BO.ParcelByTransfer parcel)
        {
            return new ParcelByTransfer
            {
                Id = parcel.Id,
                Priority = BOEnumPrioritiesToPO(parcel.Priority),
                Weight = BOEnumWeightCategoriesToPO(parcel.Weight),
                Sender = BOCustomerDeliveryToPO(parcel.Sender),
                Target = BOCustomerDeliveryToPO(parcel.Target),
                SenderLocation = BOLocationToPO(parcel.SenderLocation),
                TargetLocation = BOLocationToPO(parcel.TargetLocation),
                IsDestinationParcel = parcel.IsDestinationParcel,
                Distance = parcel.Distance
            };
        }

        public static Drone BODroneToPO(BO.Drone drone)
        {
            return new Drone()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = BOEnumWeightCategoriesToPO(drone.MaxWeight),
                Status = BOEnumDroneStatusesToPO(drone.Status),
                Battery = drone.Battery,
                Location = BOLocationToPO(drone.Location),
                Delivery = BOParcelByTransferToPO(drone.Delivery)
            };
        }

        public static ObservableCollection<DroneForList> BODroneForListToPO(IEnumerable<BO.DroneForList> droneForList)
        {
            ObservableCollection<PO.DroneForList> drones = new ObservableCollection<DroneForList>();

            foreach (var drone in droneForList)
            {
                drones.Add(new DroneForList()
                {
                    Id = drone.Id,
                    Model = drone.Model,
                    MaxWeight = BOEnumWeightCategoriesToPO(drone.MaxWeight),
                    Battery = drone.Battery,
                    Status = BOEnumDroneStatusesToPO(drone.Status),
                    Location = BOLocationToPO(drone.Location),
                    ParcelDeliveredId = drone.ParcelDeliveredId
                });
            }
            return drones;
        }

        public static BO.DroneForList PODroneForListToBO(DroneForList drone)
        {
            return new BO.DroneForList()
            {
                Id = drone.Id,
                Model = drone.Model,
                MaxWeight = POEnumWeightCategoriesToBO(drone.MaxWeight),
                Battery = drone.Battery,
                Status = POEnumDroneStatusesToBO(drone.Status),
                Location = POLocationToBO(drone.Location),
                ParcelDeliveredId = drone.ParcelDeliveredId
            };
        }

        internal static BaseStation BOBaseStationToPO(BO.BaseStation baseStation)
        {
            return new BaseStation()
            {
                Id = baseStation.Id,
                Name = baseStation.Name,
                AvailableChargingPorts = baseStation.AvailableChargingPorts,
                Location = BOLocationToPO(baseStation.Location),
                DronesInCharging = BODroneInCharging(baseStation.DronesInCharging)
            };
        }

        internal static IEnumerable<DroneInCharging> BODroneInCharging(IEnumerable<BO.DroneInCharging> dronesInCharging)
        {
            ObservableCollection<PO.DroneInCharging> dronesCharging = new ObservableCollection<DroneInCharging>();
            foreach (var droneCharging in dronesInCharging)
            {
                dronesCharging.Add(new DroneInCharging()
                {
                    Id = droneCharging.Id,
                    Battery = droneCharging.Battery,
                    Time = droneCharging.Time
                });
            }
            return dronesCharging;
        }

        internal static ObservableCollection<PO.BaseStationForList> BOBaseStationForListToPO(IEnumerable<BO.BaseStationForList> baseStationsForList)
        {
            ObservableCollection<PO.BaseStationForList> baseStations = new ObservableCollection<BaseStationForList>();
            foreach (var station in baseStationsForList)
            {
                baseStations.Add(new BaseStationForList()
                {
                    Id = station.Id,
                    Name = station.Name,
                    AvailableChargingPorts=station.AvailableChargingPorts,
                    UsedChargingPorts=station.UsedChargingPorts
                });
            }
            return baseStations;
        }
        internal static BO.BaseStationForList POStationToBO(BaseStationForList station)
        {
            return new BO.BaseStationForList()
            {
                Id = station.Id,
                Name = station.Name,
                AvailableChargingPorts = station.AvailableChargingPorts,
                UsedChargingPorts = station.UsedChargingPorts
            };
        }

    }
}
