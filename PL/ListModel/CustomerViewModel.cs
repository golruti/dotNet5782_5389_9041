using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            this.CustomerInList = new PO.Customer();
        }
        public CustomerViewModel( BO.CustomerForList customerInList)
        {
            this.CustomerInList = ConvertFunctions.BOCustomerToPO(ListsModel.Bl.GetBLCustomer(customerInList.Id));
        }

        public PO.Customer CustomerInList { get; set; }

    }
}
