//using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;


namespace IBL.BO
{
    public class Drone 
    {
        
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public DroneStatuses Status { get; set; }
        public double Battery { get; set; } 
        public Location Location { get; set; }
        public ParcelByTransfer Delivery { get; set; }

        public override string ToString()
        {
            return $"------\nDrone {Id} : {Model}\n{Status}\n{MaxWeight}\nlocation = {Location.Latitude},{Location.Latitude}/nbattery = {(int)(Battery)}%\n------\n ";
        }

       

        public Drone(int id, string model, WeightCategories maxWeight, DroneStatuses status, double battery, double longitude, double latitude)
        {
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
            Status = status;
            Battery = battery;
            Location = new Location(longitude, latitude);
        }

    
        public Drone() { }
    }
}




