﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
        public class Enums
        {
            public enum WeightCategories
            {
                Light, Heavy, Medium
            }

            public enum Priorities
            {
                Regular, Fast, Emergency
            }

            public enum DroneStatuses
            {
                Available, Delivery, Maintenance,
            }
            public enum ParselStatus
            {
                Created, associated, collected, provided
            }

            public enum ParcelStatuses
            {
                Defined,
                Associated,
                Collected,
                Provided
            }
        }
    }
