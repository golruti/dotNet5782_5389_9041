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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer
    {
        private BlApi.IBL bl;
        private BO.Customer customerInList;
        Action refreshCustomersList;
        private string newPhone;
        public Customer(BlApi.IBL bl, Action refreshCustomersList)
        {
            InitializeComponent();
            this.bl = bl;
            this.refreshCustomersList = refreshCustomersList;
            Add_grid.Visibility = Visibility.Visible;
        }

        public Customer(CustomerForList customerInList, BlApi.IBL bl, Action refreshCustomersList)
        {
            InitializeComponent();
            this.bl = bl;
            this.refreshCustomersList = refreshCustomersList;
            this.customerInList = bl.GetBLCustomer(customerInList.Id);
            Update_grid.Visibility = Visibility.Visible;
            this.DataContext = customerInList;
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
            refreshCustomersList();
        }

        private void update_phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            newPhone = (sender as TextBox).Text;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            bl.DeleteBLCustomer(customerInList.Id);
            if (MessageBox.Show("the customer was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }


        private void Add_Customer_finish_click(object sender, RoutedEventArgs e)
        {
            bl.AddCustomer(new BO.Customer(int.Parse(ID.Text), name.Text, phone.Text, double.Parse(longg.Text), double.Parse(longg.Text)));

            if (MessageBox.Show("the customer was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

    }
}


