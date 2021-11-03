using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Phone { get; set; }
            public Location DeliveryLocatn { get; set; }


            public List<Customer> DeliveryListFromTheCustomer { get; set; }
            public List<Customer> DeliveryListToTheCustomer { get; set; }



        /// <summary>
        /// String of details for drone
        /// </summary>
        /// <returns>String of details for drone</returns>
        public override string ToString()
            {
                return ("\nId: " + Id + "\nName: " + Name + "\n");
            }

        }
    
}
