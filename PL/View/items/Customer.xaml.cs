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

        public Customer()
        {
            InitializeComponent();
            customerViewModel = new CustomerViewModel();
            this.DataContext = customerViewModel;
            Add_grid.Visibility = Visibility.Visible;
        }

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
            PO.ListsModel.RefreshCustomers();
            //customerViewModel.RefreshCustomersList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PO.ListsModel.Bl.DeleteBLCustomer(customerViewModel.CustomerInList.Id);

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
                PO.ListsModel.Bl.AddCustomer(new BO.Customer()
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
                PO.ListsModel.Bl.UpdateCustomer(customerViewModel.CustomerInList.Id, customerViewModel.CustomerInList.Name, customerViewModel.CustomerInList.Phone);
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
    }
}


