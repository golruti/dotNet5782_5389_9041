using BO;
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
        public Customer(CustomerForList customerInList, BlApi.IBL bl, Action refreshCustomersList)
        {
            InitializeComponent();
            this.bl = bl;
            this.refreshCustomersList = refreshCustomersList;
            this.customerInList = bl.GetBLCustomer(customerInList.Id);
            this.DataContext = customerInList;
        }

        private void showParcelsFromCustomer(object sender, RoutedEventArgs e)
        {
            //TabItem tabItem = new TabItem();
            //tabItem.Content = new CustomersList(bl, AddTab, RemoveTab);
            //button.Visibility = Visibility.Collapsed;
            //tabItem.Header = "customers List";
            //tub_control.Visibility = Visibility.Visible;
            //AddTab(tabItem);
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
            this.refreshCustomersList();
        }
    }
}


