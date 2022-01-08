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
        public BlApi.IBL Bl { get; private set; }
        public Action RefreshCustomersList { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public PO.Customer CustomerInList { get; set; }
        public PO.Parcel parcelsTemp { get; set; }


        public CustomerViewModel(BlApi.IBL bl, Action refreshCustomersList)
        {
            this.Bl = bl;
            this.RefreshCustomersList = refreshCustomersList;
            this.CustomerInList = new PO.Customer();
        }
        public CustomerViewModel( BO.CustomerForList customerInList, BlApi.IBL bl, Action refreshCustomersList)
        {
            this.Bl = bl;
            this.RefreshCustomersList = refreshCustomersList;
            this.CustomerInList = ConvertFunctions.BOCustomerToPO(bl.GetBLCustomer(customerInList.Id));
            //parcelsTemp = ConvertFunctions.BOParcelToPO(bl.GetBLParcel(pp.Id));
        }
    }
}
