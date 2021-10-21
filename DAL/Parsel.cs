using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDAL.DO.Enum;

namespace IDAL
{
    namespace DO
    {
        public struct Parsel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int Droneld { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
        }
        
        public override void ToString()
        {
             return "id: "+ base.Id + "Sender id: "+ base.SenderId +"Target id: "+ base.TargetId + "Weight: "+ base.Weight +
                 "Priority: "+ base.Priority + "Requested: "+ base.Requested+ "Droneld: "+ base.Droneld + "Scheduled: "+ base.Scheduled  +
                 "PickedUp: "+ base.PickedUp + "Delivered: "+ base.Delivered ;
         } 
    }
}
