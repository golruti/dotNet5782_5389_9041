using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IBL.BO.Enums;


namespace IBL.BO
{
    public class Customer : ILocatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public IEnumerable<CustomerDelivery> FromCustomer { get; set; }
        public IEnumerable<CustomerDelivery> ToCustomer { get; set; }

        public override string ToString()
        {
            return $"Customer #{Id}: {Name}, tel={Phone}, location={Location}";
        }

        public Customer(int id,string name,string phone, double longitude, double latitude)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Location = new Location(longitude, latitude);
            FromCustomer = new List<CustomerDelivery>();
            ToCustomer = new List<CustomerDelivery>();
        }
        public Customer()
        {

        }
    }
}


