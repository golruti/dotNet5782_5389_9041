using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Customer
    {
        private int id;
        public int Id { get; set; }
        private string name;
        public string Name { get; set; }
        private string phone;
        public string Phone { get; set; }
        private Location location;
        public Location Location { get; set; }
        private IEnumerable<ParcelToCustomer> fromCustomer;
        public IEnumerable<ParcelToCustomer> FromCustomer { get; set; }
        private IEnumerable<ParcelToCustomer> toComCustomer;
        public IEnumerable<ParcelToCustomer> ToCustomer { get; set; }
    }
}
