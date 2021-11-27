using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;


namespace IBL.BO
{
    public class Customer 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public IEnumerable<CustomerDelivery> ReceivedParcels { get; set; }
        public IEnumerable<CustomerDelivery> ShippedParcels { get; set; }

        public override string ToString()
        {
            return $"------\nCustomer #{Id}: {Name}\ntel={Phone}/nlocation={Location}/n------/n";
        }

        public Customer(int id, string name, string phone, double longitude, double latitude)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Location = new Location(longitude, latitude);
            ReceivedParcels = new List<CustomerDelivery>();
            ShippedParcels = new List<CustomerDelivery>();
        }
        public Customer()
        {
        }
       
    }
}


