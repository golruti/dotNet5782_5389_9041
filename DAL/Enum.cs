using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public struct Enum
        {

            public enum WeightCategories
            {
                Light,
                Heavy,
                Medium
            }

            public enum Priorities
            {

                Regular,
                Fast,
                Emergency
            }

            public enum DroneStatuses
            {
                Available,
                Maintenance,
                Delivery
            }

          
        }
    }
}
