﻿using PO;
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
        public CustomerListViewModel(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            this.Bl = bl;
            this.AddTab = addTab;
            this.RemoveTab = removeTab;
            CustomersForList = new ListCollectionView(PO.ListsModel.customers);
        }

        //public void RefreshCustomerList()
        //{
        //    CustomersForList = new ListCollectionView((System.Collections.IList)ConvertFunctions.BOCustomerForListToPO(Bl.GetCustomerForList()));
        //    //CustomersForList.Refresh();
        //}


        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<object, RoutedEventArgs> RemoveTab { get; private set; }
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
