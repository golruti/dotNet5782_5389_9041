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
using PL.ViewModel;
using System.Text.RegularExpressions;

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
            try
            {
                customerViewModel.Bl.DeleteBLCustomer(customerViewModel.CustomerInList.Id);

            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"Customer not exists");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"Customer not exists");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The customer was not deleted, {ex.Message}");
            }
            if (MessageBox.Show("the customer was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        private void Add_Customer_finish_click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(longitude.Text) < -90 || double.Parse(longitude.Text) > 90 || double.Parse(latitude.Text) < -90 || double.Parse(latitude.Text) > 90)
            {
                MessageBox.Show("Location not in the middle");
                return;
            }

            try
            {
                customerViewModel.Bl.AddCustomer(new BO.Customer(int.Parse(ID.Text), name.Text, phone.Text, double.Parse(longitude.Text), double.Parse(latitude.Text)));
                if (MessageBox.Show("the customer was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The customer was not add, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"A key already exists");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The customer was not add, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The customer was not add, {ex.Message}");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              customerViewModel.Bl.UpdateCustomer(customerViewModel.CustomerInList.Id, customerViewModel.CustomerInList.Name, customerViewModel.CustomerInList.Phone);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The customer not updated, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"A key already exists");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The customer not updated, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The customer not updated, {ex.Message}");
            }
            if (MessageBox.Show("the customer was seccessfully updated", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        /// <summary>
        /// Input filter for ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void ToCustomerView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //var selectedCustomer = ToCustomerView.SelectedItem as PO.CustomerForList;

            //TabItem tabItem = new TabItem();
            //tabItem.Content = new Parcel(customerViewModel.Bl, customerListViewModel.RefreshCustomerList);
            //tabItem.Header = "Update parcel";
            //tabItem.Visibility = Visibility.Visible;
            //customerViewModel.AddTab(tabItem);
        }
    }
}


