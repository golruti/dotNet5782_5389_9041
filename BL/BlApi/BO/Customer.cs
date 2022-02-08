using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public IEnumerable<ParcelToCustomer> FromCustomer { get; set; }
        public IEnumerable<ParcelToCustomer> ToCustomer { get; set; }

        public override string ToString()
        {
            return $"------\nCustomer #{Id}: {Name}\ntel={Phone}\nlocation={Location.Latitude},{Location.Latitude}\n------\n";
        }
    }
}


