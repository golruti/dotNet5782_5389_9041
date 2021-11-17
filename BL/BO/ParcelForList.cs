using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    class ParcelForList
    {
        public int Id { get; set; }
        public string SendCustomer { get; set; }
        public string ReceiveCustomer { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }

    }
}

