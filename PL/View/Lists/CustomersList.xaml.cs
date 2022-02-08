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

        public CustomersList(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            InitializeComponent();
            customerListViewModel = new CustomerListViewModel(bl, addTab,removeTab);
            this.DataContext = customerListViewModel;
            customerListViewModel.CustomersForList.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(CustomersListViewXaml.DataContext);
            //PropertyGroupDescription groupDescription = new PropertyGroupDescription("Name");
            ////view.GroupDescriptions.Add(groupDescription);
        }

        private void CustomersListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCustomer = CustomersListViewXaml.SelectedItem as PO.CustomerForList;
            
            TabItem tabItem = new TabItem();
            tabItem.Content = new Customer(PO.ConvertFunctions.POCustomerForListToBO( selectedCustomer), customerListViewModel.Bl/*, customerListViewModel.RefreshCustomerList*/, customerListViewModel.AddTab);
            tabItem.Header = "Update customer";
            tabItem.Visibility = Visibility.Visible;
            customerListViewModel.AddTab(tabItem);
        }

        private void add_customer_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Customer(customerListViewModel.Bl/*, customerListViewModel.RefreshCustomerList*/);
            tabItem.Header = "Add customer";
            customerListViewModel.AddTab(tabItem);
        }
    }
}