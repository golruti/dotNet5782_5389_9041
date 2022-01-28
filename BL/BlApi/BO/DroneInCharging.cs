﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class DroneInCharging
    {
        public int Id { get; set; }
        public double Battery { get; set; }
        public DateTime? Time { get; set; }

        public DroneInCharging(int id, double battery)
        {
            Id = id;
            Battery = battery;
            Time = DateTime.Now;

    }
        public DroneInCharging()
        {
        }
    }

}
