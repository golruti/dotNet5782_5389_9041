using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enum;


namespace BL
{
    class DroneForList
    {
        public int Id { get; set; }
        public string Modal { get; set; }
        public WeightCategories Weight { get; set; }
        public DroneStatuses Status { get; set; }
        public int Battery { get; set; }
        public Location Location { get; set; }
        public int IdParcelDeliver { get; set; } = 0;

    }
}
