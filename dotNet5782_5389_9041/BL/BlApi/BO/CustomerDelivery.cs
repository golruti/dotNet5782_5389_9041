﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO

{
    public class CustomerDelivery
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CustomerDelivery(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public CustomerDelivery()
        {

        }
    }


}

