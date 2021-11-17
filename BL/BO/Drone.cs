using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;


namespace IBL.BO
{
    public class Drone : ILocatable
    {
        private DroneStatuses maxWeight;
        private double longitude;
        private double latitude;

        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public DroneStatuses Status { get; set; }
        public double Battery { get; set; } 
        public Location Location { get; set; }
        public ParcelByTransfer Delivery { get; set; }

        public override string ToString()
        {
            return $"Drone #{Id}: model={Model}, {Status}, {MaxWeight}, location = {Location}, battery={(int)(Battery * 100)} ";

        }

        public Drone(int id, string model,WeightCategories maxWeight,DroneStatuses status,int battery,double longitude, double latitude)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            Status = DroneStatuses.Maintenance;
            Location = new Location(longitude, latitude);

        }

        public Drone(int id, string model, DroneStatuses maxWeight, DroneStatuses maintenance, double longitude, double latitude, double latitude1)
        {
            Id = id;
            Model = model;
            this.maxWeight = maxWeight;
            this.longitude = longitude;
            this.latitude = latitude;
        }
    }
}




