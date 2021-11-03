using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enum;

namespace BL
{
    class ParcelForList
    {
        public string Id { get; set; }
        public string CustomerSends { get; set; }
        public string SendCustomer { get; set; }
        public string ReceiveCustomer { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities priority { get; set; }



    }
}
