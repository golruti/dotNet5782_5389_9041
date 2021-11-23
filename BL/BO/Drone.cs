﻿//using BL;
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

       

        public Drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery, double longitude, double latitude)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            Status = status;
            Battery = battery;
            this.longitude = longitude;
            this.latitude = latitude;
        }
        public Drone() { }
    }
}




