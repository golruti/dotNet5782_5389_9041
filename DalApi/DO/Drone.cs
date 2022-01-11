using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DO.Enum;


namespace DO
{
    /// <summary>
    /// This is a type that represents a drone in the shipping company -
    /// Including the details of the drone.
    /// </summary>
    public struct Drone
    {
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }

        /// <summary>
        /// String of details for drone
        /// </summary>
        /// <returns>String of details for drone</returns>
        public override string ToString()
        {
            return ("\nId: " + Id + "\nModel: " + Model + "\nMaxWeight: " + MaxWeight +
                "\n\nStatus: " + "%\n");
        }

        public Drone(int id, string model, WeightCategories maxWeight)
        {
            IsDeleted = false;
            Id = id;
            Model = model;
            MaxWeight = maxWeight;
        }


    }
}

