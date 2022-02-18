using BO;
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
using PL.ViewModel;


namespace PL
{
    /// <summary>
    /// Interaction logic for CustomersList.xaml
    /// </summary>
    public partial class CustomersList
    {
        CustomerListViewModel customerListViewModel;
        /// <summary>
        /// Constructor
        /// </summary>
        public CustomersList()
        {
            InitializeComponent();
            customerListViewModel = new CustomerListViewModel();
            this.DataContext = customerListViewModel;
        }

        /// <summary>
        /// Show "Add Customer" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_customer_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Customer();
            tabItem.Header = "Add customer";
            Tabs.AddTab(tabItem);
        }

        /// <summary>
        /// View a specific customer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomersListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            var selectedCustomer = CustomersListViewXaml.SelectedItem as PO.CustomerForList;
            if (selectedCustomer !=null)
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Customer(PO.ConvertFunctions.POCustomerForListToBO(selectedCustomer));
                tabItem.Header = "Update customer";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
            }
        }

        /// <summary>
        /// View customers by groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.IsChecked == true)
            {
                customerListViewModel.CustomersForList.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
            }
            else
            {
                customerListViewModel.CustomersForList.GroupDescriptions.Clear();
            }
        }
    }
}