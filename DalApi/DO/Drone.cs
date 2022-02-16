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
        #region properties
        public bool IsDeleted { get; set; }
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        #endregion

        #region ToString
        public override string ToString()
        {
            return ("\nId: " + Id + "\nModel: " + Model + "\nMaxWeight: " + MaxWeight +
                "\n\nStatus: " + "%\n");
        }
        #endregion
    }
}

