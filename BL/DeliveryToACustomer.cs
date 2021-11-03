﻿using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enum;

namespace BL
{
    class DeliveryToACustomer
    {
        public int Id { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParselStatus  status { get; set; }
        public CustomerInDelivery SourceOrDestination { get; set; }
    }
}
