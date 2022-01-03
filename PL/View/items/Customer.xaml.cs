﻿using BO;
using System;
using System.Collections.Generic;
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
using PL.ViewModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer
    {
        CustomerViewModel customerViewModel;

        public Customer(BlApi.IBL bl, Action refreshCustomersList)
        {
            InitializeComponent();
            customerViewModel = new CustomerViewModel(bl, refreshCustomersList);
            this.DataContext = customerViewModel;
            Add_grid.Visibility = Visibility.Visible;
        }
         
        public Customer(CustomerForList customerInList, BlApi.IBL bl, Action refreshCustomersList)
        {
            InitializeComponent();
            customerViewModel = new CustomerViewModel(customerInList, bl, refreshCustomersList);
            this.DataContext = customerViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }

        private void Close_Page(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            TabItem tabItem = null;
            while (tmp.GetType() != typeof(TabControl))
            {
                if (tmp.GetType() == typeof(TabItem))
                    tabItem = (tmp as TabItem);
                tmp = ((FrameworkElement)tmp).Parent;
            }
            if (tmp is TabControl tabControl)
                tabControl.Items.Remove(tabItem);
            customerViewModel.RefreshCustomersList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            customerViewModel.Bl.DeleteBLCustomer(customerViewModel.CustomerInList.Id);
            if (MessageBox.Show("the customer was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        private void Add_Customer_finish_click(object sender, RoutedEventArgs e)
        {
            customerViewModel.Bl.AddCustomer(new BO.Customer(int.Parse(ID.Text), name.Text, phone.Text, double.Parse(longg.Text), double.Parse(longg.Text)));

            if (MessageBox.Show("the customer was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              customerViewModel.Bl.UpdateCustomer(customerViewModel.CustomerInList.Id, customerViewModel.CustomerInList.Name, customerViewModel.CustomerInList.Phone);
            }
            catch (Exception)
            {
                throw;
            }

            if (MessageBox.Show("the customer was seccessfully updated", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }


    }
}


