﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;

namespace IBL.BO
{
    public class ParcelForList
    {
        public int Id { get; set; }
        public string SendCustomer { get; set; }
        public string ReceiveCustomer { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }


        public override string ToString()
        {
            return $"Parcel #{Id}:, Send Customer={SendCustomer},Receive Customer={ReceiveCustomer}, Weight{Weight}, " +
                $"Priority{Priority}, Status{Status}";
        }

    }
}

