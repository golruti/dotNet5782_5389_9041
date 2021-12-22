﻿using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersList.xaml
    /// </summary>
    public partial class CustomersList
    {
        BlApi.IBL bl;
        Action<TabItem> addTab;
        ObservableCollection<CustomerForList> customerForLists;

        public CustomersList(BlApi.IBL bl, Action<TabItem> addTab, Action<TabItem> removeTab)
        {
            InitializeComponent();
            this.bl = bl;
            this.addTab = addTab;

            customerForLists = new ObservableCollection<CustomerForList>(bl.GetCustomerForList());
            CustomersListView.DataContext = customerForLists;
        }

        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCustomer = (sender as ContentControl).DataContext as BO.CustomerForList;

            TabItem tabItem = new TabItem();
            tabItem.Content = new Customer(selectedCustomer, this.bl, RefreshCustomerList);
            tabItem.Header = "Update customer";
            tabItem.Visibility = Visibility.Visible;
            this.addTab(tabItem);
        }


        private void RefreshCustomerList()
        {
            customerForLists = new ObservableCollection<CustomerForList>(bl.GetCustomerForList());
            CustomersListView.DataContext = customerForLists;
        }
    }
}