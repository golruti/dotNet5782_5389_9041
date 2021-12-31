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
        public CustomerViewModel(BlApi.IBL bl, Action refreshCustomersList)
        {
            this.Bl = bl;
            this.RefreshCustomersList = refreshCustomersList;
            this.CustomerInList = new PO.Customer();
            //newName = customerInList.Name;
            //newPhone = customerInList.Phone;
        }
        public CustomerViewModel(BO.CustomerForList customerInList, BlApi.IBL bl, Action refreshCustomersList)
        {
            this.Bl = bl;
            this.RefreshCustomersList = refreshCustomersList;
            this.CustomerInList = ConvertFunctions.BOCustomerToPO(bl.GetBLCustomer(customerInList.Id));
            NewName = customerInList.Name;
            //NewPhone = customerInList.Phone;
        }
        public BlApi.IBL Bl { get; private set; }
        public Action RefreshCustomersList { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public PO.Customer CustomerInList { get; set; }
        public string NewName { get; set; }
        //public string NewPhone { get; set; }
    }
}
