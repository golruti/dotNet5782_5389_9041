using BO;
using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel
    {
        ParcelViewModel parcelViewModel;

        /// <summary>
        /// Constructor for "Add Parcel tomer" page.
        /// </summary>
        public Parcel()
        {
            InitializeComponent();
            parcelViewModel = new ParcelViewModel();
            this.DataContext = parcelViewModel;
            Add_grid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Constructor for "Update Parcel" page.
        /// </summary>
        public Parcel(int parcelInListId, bool IsInCustomerMode = false)
        {
            InitializeComponent();
            parcelViewModel = new ParcelViewModel(parcelInListId);
            this.DataContext = parcelViewModel;
            Update_grid.Visibility = Visibility.Visible;

            if (IsInCustomerMode)
            {
                ChangeButtons.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Constructor for "Update Drone" page for customer login.
        /// </summary>
        public Parcel(BO.User user)
        {
            InitializeComponent();
            parcelViewModel = new ParcelViewModel(user);
            this.DataContext = parcelViewModel;
            Add_grid.Visibility = Visibility.Visible;
            ChangeButtons.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Deleting a parcel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteParcel(object sender, RoutedEventArgs e)
        {
            try
            {
                ListsModel.Bl.deleteBLParcel(parcelViewModel.ParcelInList.Id);
            }
            catch (TheParcelIsAssociatedAndCannotBeDeleted)
            {
                MessageBox.Show($"The package can not be deleted, it is connected to the drone.");
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show($"The parcel was not found and not deleted.");
            }
            catch (Exception)
            {
                MessageBox.Show($"The parcel was not deleted.");
            }
            if (MessageBox.Show("the customer was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }


        /// <summary>
        /// Adding a new parcel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer senderCustomer = ListsModel.Bl.GetBLCustomer(int.Parse(senderId.Text));
                BO.Customer recieveCustomer = ListsModel.Bl.GetBLCustomer(int.Parse(reciverId.Text));

                ListsModel.Bl.AddParcel(new BO.Parcel()
                {
                    Weight = (BO.Enums.WeightCategories)Weight.SelectedItem,
                    Priority = (BO.Enums.Priorities)Priority.SelectedItem,
                    CustomerReceives = new BO.CustomerDelivery() { Id = senderCustomer.Id, Name = senderCustomer.Name },
                    CustomerSender = new BO.CustomerDelivery() { Id = recieveCustomer.Id, Name = recieveCustomer.Name },
                    Requested = DateTime.Now,
                    Delivered = null,
                    PickedUp = null,
                    Scheduled = null,
                    DroneParcel = null
                });

                if (MessageBox.Show("the parcel was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The parcel was not add, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The parcel was not add, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The parcel was not add, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The parcel was not add, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The parcel was not add, {ex.Message}");
            }
        }

        /// <summary>
        /// Entrance to a sender customer page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerSender(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListsModel.Bl.GetCustomerForList().FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.CustomerSender.Id) == null)
                {
                    MessageBox.Show($"Sorry, customer deleted.");
                }
                else
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Content = new Customer(ListsModel.Bl.GetCustomerForList().FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.CustomerSender.Id));
                    tabItem.Header = "update Sender Customer";
                    tabItem.Visibility = Visibility.Visible;
                    Tabs.AddTab(tabItem);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The station could not be found in the database, {ex.Message}");
            }
            catch (Exception)
            {
                MessageBox.Show($"The station can not be displayed");
            }
        }

        /// <summary>
        /// Entrance to a receive customer page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerReceives(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListsModel.Bl.GetCustomerForList().FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.CustomerReceives.Id) == null)
                {
                    MessageBox.Show($"Sorry, customer deleted.");
                }
                else
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Content = new Customer(ListsModel.Bl.GetCustomerForList().FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.CustomerReceives.Id));
                    tabItem.Header = "update Receives Customer";
                    tabItem.Visibility = Visibility.Visible;
                    Tabs.AddTab(tabItem);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The customer could not be found in the database, {ex.Message}");
            }
            catch (Exception)
            {
                MessageBox.Show($"The customer can not be displayed");
            }
        }

        /// <summary>
        /// Entrance to a drone page linked to the parcel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drone(object sender, RoutedEventArgs e)
        {
            try
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Drone(ListsModel.Bl.GetDroneForList()
                    .FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.DroneParcel.Id));
                tabItem.Header = "update  drone";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone could not be found in the database, {ex.Message}");
            }
            catch (Exception)
            {
                MessageBox.Show($"The drone can not be displayed");
            }
        }

        /// <summary>
        /// Closes the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Page(object sender, RoutedEventArgs e)
        {
            Tabs.RemoveTab(sender, e);
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

    }
}
