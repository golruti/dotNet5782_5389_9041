using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ConvertFunctions
    {
        internal static Enums.WeightCategories BOEnumWeightCategoriesToPO(BO.Enums.WeightCategories weight)
        {
            return (Enums.WeightCategories)weight;
        }
        internal static Enums.DroneStatuses BOEnumDroneStatusesToPO(BO.Enums.DroneStatuses status)
        {
            return (Enums.DroneStatuses)status;
        }

        internal static Enums.Priorities BOEnumPrioritiesToPO(BO.Enums.Priorities prioruty)
        {
            return (Enums.Priorities)prioruty;
        }

        internal static Enums.ParcelStatuses BOEnumParcelStatusToPO(BO.Enums.ParcelStatuses parcelStatuses)
        {
            return (Enums.ParcelStatuses)parcelStatuses;
        }

        internal static BO.Enums.WeightCategories POEnumWeightCategoriesToBO(Enums.WeightCategories weight)
        {
            return (BO.Enums.WeightCategories)weight;
        }

        internal static BO.Enums.DroneStatuses POEnumDroneStatusesToBO(Enums.DroneStatuses status)
        {
            return (BO.Enums.DroneStatuses)status;
        }


        internal static BO.Enums.Priorities POEnumPrioritiesToBO(Enums.Priorities prioruty)
        {
            return (BO.Enums.Priorities)prioruty;
        }
        internal static BO.Enums.ParcelStatuses POEnumParcelStatusToBO(Enums.ParcelStatuses parcelStatuses)
        {
            return (BO.Enums.ParcelStatuses)parcelStatuses;
        }

        internal static Location BOLocationToPO(BO.Location location)
        {
            return new Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

        internal static BO.Location POLocationToBO(Location location)
        {
            return new BO.Location
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
        }

        internal static DroneInParcel BODroneParceToPO(BO.DroneInParcel droneParcel)
        {
            return new DroneInParcel
            {
                Id = droneParcel.Id,
                Battery = droneParcel.Battery,
                Location = BOLocationToPO(droneParcel.Location)
            };
        }

        internal static BO.DroneInParcel PODroneParceToBO(DroneInParcel droneParcel)
        {
            return new BO.DroneInParcel
            {
                Id = droneParcel.Id,
                Battery = droneParcel.Battery,
                Location = POLocationToBO(droneParcel.Location)
            };
        }

        internal static CustomerDelivery BOCustomerDeliveryToPO(BO.CustomerDelivery customerDelivery)
        {
            if (customerDelivery == null)
                return null;
            return new CustomerDelivery
            {
                Id = customerDelivery.Id,
                Name = customerDelivery.Name,
            };
        }

        internal static BO.CustomerDelivery POCustomerDeliveryToBO(CustomerDelivery customerDelivery)
        {
            if (customerDelivery == null)
                return null;
            return new BO.CustomerDelivery
            {
                Id = customerDelivery.Id,
                Name = customerDelivery.Name,
            };
        }

        internal static Parcel BOParcelToPO(BO.Parcel parcel)
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


        internal static BO.Parcel POCustomerToBO(PO.Parcel parcel)
        {
            return new BO.Parcel
            {
                Id = parcel.Id,
                CustomerSender = POCustomerDeliveryToBO(parcel.CustomerSender),
                CustomerReceives = POCustomerDeliveryToBO(parcel.CustomerReceives),
                Weight = POEnumWeightCategoriesToBO(parcel.Weight),
                Priority = POEnumPrioritiesToBO(parcel.Priority),
                DroneParcel = PODroneParceToBO(parcel.DroneParcel),
                Requested = parcel.Requested,
                Scheduled = parcel.Scheduled,
                PickedUp = parcel.PickedUp,
                Delivered = parcel.Delivered
            };
        }


        

        internal static PO.ParcelForList BOParcelForListToPO(BO.ParcelForList parcel)
        {
            return new PO.ParcelForList()
            {
                Id = parcel.Id,
                SendCustomer = parcel.SendCustomer,
                ReceiveCustomer = parcel.ReceiveCustomer,
                Weight = (PO.Enums.WeightCategories)parcel.Weight,
                Priority = (PO.Enums.Priorities)parcel.Priority,
                Status = (PO.Enums.ParcelStatuses)parcel.Status                
            };
        }

        internal static BO.ParcelForList POParcelForListToBO(PO.ParcelForList parcel)
        {
            return new BO.ParcelForList()
            {
                Id = parcel.Id,
                SendCustomer = parcel.SendCustomer,
                ReceiveCustomer = parcel.ReceiveCustomer,
                Weight = (BO.Enums.WeightCategories)parcel.Weight,
                Priority = (BO.Enums.Priorities)parcel.Priority,
                Status = (BO.Enums.ParcelStatuses)parcel.Status
            };
        }





        internal static IEnumerable<ParcelForList> BOParcelForListToPO(IEnumerable<BO.ParcelForList> parcelForLists)
        {
            List<PO.ParcelForList> p = new List<ParcelForList>();
            foreach (var parcel in parcelForLists)
            {
                p.Add(new ParcelForList()
                {
                    Id = parcel.Id,
                    SendCustomer = parcel.SendCustomer,
                    ReceiveCustomer = parcel.ReceiveCustomer,
                    Weight = (Enums.WeightCategories)parcel.Weight,
                    Priority = (Enums.Priorities)parcel.Priority,
                    Status = (Enums.ParcelStatuses)parcel.Status,
                });
            }

            //if (customersForList == null)
            //    return ObservableCollection.Empty<CustomerForList>();
            return p;
        }

        internal static IEnumerable<ParcelToCustomer> BOParcelToCustomerToPO(IEnumerable<BO.ParcelToCustomer> parcelsToCustomer)
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
        internal static Customer BOCustomerToPO(BO.Customer customer)
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

        internal static IEnumerable<CustomerForList> BOCustomerForListToPO(IEnumerable<BO.CustomerForList> customersForList)
        {
            List<PO.CustomerForList> c = new List<CustomerForList>();
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

        internal static IEnumerable<Parcel> BOParcelToPO(IEnumerable<BO.Parcel> parcels)
        {

            List<PO.Parcel> p = new List<Parcel>();
            foreach (var parcel in parcels)
            {
                p.Add(new PO.Parcel()
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
                });
            }
            return p;
        }

        internal static BO.CustomerForList POCustomerForListToBO(CustomerForList customer)
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

        internal static ParcelByTransfer BOParcelByTransferToPO(BO.ParcelByTransfer parcel)
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
        internal static Drone BODroneToPO(BO.Drone drone)
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

        internal static ObservableCollection<DroneForList> BODroneForListToPO(IEnumerable<BO.DroneForList> droneForList)
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
        internal static BO.DroneForList PODroneForListToBO(DroneForList drone)
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
                DronesInCharging = BODroneInChargingTOPO(baseStation.DronesInCharging)
            };
        }

        internal static IEnumerable<PO.DroneInCharging> BODroneInChargingTOPO(IEnumerable<BO.DroneInCharging> dronesInCharging)
        {
            List<PO.DroneInCharging> dronesCharging = new List<DroneInCharging>();
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

        internal static IEnumerable<PO.BaseStationForList> BOBaseStationForListToPO(IEnumerable<BO.BaseStationForList> baseStationsForList)
        {
            List<PO.BaseStationForList> baseStations = new List<BaseStationForList>();
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
