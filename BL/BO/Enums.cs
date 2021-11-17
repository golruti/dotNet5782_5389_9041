using System;
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
                Light, Medium, Heavy
            }

            public enum Priorities
            {
                Regular, Emergency, Fast
        }

        public enum DroneStatuses
        {
            Available, Delivery, Maintenance,
        }

        public enum ParcelStatuses
        {
            Created,
            Associated,
            Collected,
            Provided
        }
    }
}
