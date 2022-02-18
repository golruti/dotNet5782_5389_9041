using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {
        public CustomerListViewModel()
        {
            CustomersForList = new ListCollectionView(ListsModel.customers);
        }


        public ListCollectionView CustomersForList
        {
            get { return customersForList; }
            set
            {
                customersForList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomersForList)));
            }
        }
        private ListCollectionView customersForList;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
