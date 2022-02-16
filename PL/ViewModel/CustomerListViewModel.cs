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
    public class CustomerListViewModel : /*Singleton.Singleton<PL>,*/ INotifyPropertyChanged
    {
        public CustomerListViewModel()
        {
            CustomersForList = new ListCollectionView(PO.ListsModel.customers);
        }


        private ListCollectionView customersForList;


        public event PropertyChangedEventHandler PropertyChanged;
        public ListCollectionView CustomersForList
        {
            get { return customersForList; }
            set
            {
                customersForList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomersForList)));
            }
        }
    }
}
