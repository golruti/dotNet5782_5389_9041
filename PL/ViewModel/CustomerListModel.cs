using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class CustomerListModel
    {
        BlApi.IBL bl;
        ObservableCollection<CustomerForList> customerForLists;
        ListCollectionView listCollectionView;
        Action<TabItem> addTab;
        Action<TabItem> closeTab;
    }
}
