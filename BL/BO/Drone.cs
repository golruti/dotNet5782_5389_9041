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
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        public DroneStatuses Status { get; set; }
        public int Battery { get; set; }
        public Location Location { get; set; }
        public ParcelByTransfer Delivery { get; set; }

        public override string ToString()
        {
            return $"Drone #{Id}: model={Model}, {Status}, {MaxWeight}, location = {Location}, battery={(int)(Battery * 100)} ";

        }
    }
}




