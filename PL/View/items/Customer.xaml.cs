using BO;
using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for Customer.xaml
    /// </summary>
    public partial class Customer
    {
        CustomerViewModel customerViewModel;

        /// <summary>
        /// Constructor for "Add Customer" page.
        /// </summary>
        public Customer()
        {
            InitializeComponent();
            customerViewModel = new CustomerViewModel();
            this.DataContext = customerViewModel;
            Add_grid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Constructor for "Update Customer" page.
        /// </summary>
        public Customer(CustomerForList customerInList)
        {
            if (customerInList == null)
            {
                MessageBox.Show(" The Customer deleted");
            }
            else
            {
                InitializeComponent();
                customerViewModel = new CustomerViewModel(customerInList);
                this.DataContext = customerViewModel;
                Update_grid.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Deleting a customer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.DeleteBLCustomer(customerViewModel.CustomerInList.Id);

            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show($"Customer not exists");
            }
            catch (ArgumentNullException)
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

        /// <summary>
        /// Adding a new customer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Customer_finish_click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(longitude.Text) < -90 || double.Parse(longitude.Text) > 90 || double.Parse(latitude.Text) < -90 || double.Parse(latitude.Text) > 90)
            {
                MessageBox.Show("Location not in the middle");
                return;
            }

            if (int.Parse(phone.Text) < 500000000 || int.Parse(phone.Text) > 599999999)
            {
                MessageBox.Show("Phone not in the middle");
                return;
            }

            try
            {
                ListsModel.Bl.AddCustomer(new BO.Customer()
                {
                    Id = int.Parse(ID.Text),
                    Name = name.Text,
                    Phone = phone.Text,
                    Location = new Location() { Longitude = double.Parse(longitude.Text), Latitude = double.Parse(latitude.Text) },
                    FromCustomer = new List<ParcelToCustomer>(),
                    ToCustomer = new List<ParcelToCustomer>()
                });
                if (MessageBox.Show("the customer was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The customer was not add, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
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

        /// <summary>
        /// Update customer information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.UpdateCustomer(customerViewModel.CustomerInList.Id, customerViewModel.CustomerInList.Name, customerViewModel.CustomerInList.Phone);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The customer not updated, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
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
        /// View a specific customer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToCustomerView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ToCustomerView.SelectedItem != null)
            {
                var selectedCustomer = ToCustomerView.SelectedItem as PO.ParcelToCustomer;
                TabItem tabItem = new TabItem();
                tabItem.Content = new Parcel(selectedCustomer.Id);
                tabItem.Header = "Update parcel";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
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


        /// <summary>
        /// Close the tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Page(object sender, RoutedEventArgs e)
        {
            ListsModel.RefreshCustomers();
            ListsModel.RefreshParcels();
            ListsModel.RefreshDrones();
            ListsModel.RefreshStations();
            Tabs.RemoveTab(sender, e);
        }
    }
}


