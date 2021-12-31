using PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL
{
    class CustomerModel
    {
        private BlApi.IBL bl;
        private PO.Customer customerInList;
        Action refreshCustomersList;
        Action<TabItem> addTab;
        Action<TabItem> closeTab;
    }
}
