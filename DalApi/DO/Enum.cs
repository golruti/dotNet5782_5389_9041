using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DO
{
    public struct Enum
    {
        //The weight of the parcel
        public enum WeightCategories
        {
            Light,
            Heavy,
            Medium
        }

        //Priority of parcel delivery
        public enum Priorities
        {
            Regular,
            Fast,
            Emergency
        }

        //Allowing the user to access data and operations
        public enum Access
        {
            Client,
            Employee
        }
    }
}

