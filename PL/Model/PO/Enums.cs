using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class Enums
    {

        public enum WeightCategories
        {
            Light, Medium, Heavy
        }

        public enum Priorities
        {
            Regular, Fast, Emergency
        }

        public enum DroneStatuses
        {
            Available, Maintenance, Delivery
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
