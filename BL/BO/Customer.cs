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
        public IEnumerable<CustomerDelivery> ReceivedParcels { get; set; }
        public IEnumerable<CustomerDelivery> ShippedParcels { get; set; }

        public override string ToString()
        {
            return $"Customer #{Id}: {Name}, tel={Phone}, location={Location}";
        }
    }
}


