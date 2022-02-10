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

        static int idParcel { get; set; } = 10;
        public Parcel()
        {
            InitializeComponent();
            parcelViewModel = new ParcelViewModel();
            this.DataContext = parcelViewModel;
            Add_grid.Visibility = Visibility.Visible;

        }


        public Parcel(int parcelInListId)
        {
            InitializeComponent();
            parcelViewModel = new ParcelViewModel(parcelInListId);
            this.DataContext = parcelViewModel;
            Update_grid.Visibility = Visibility.Visible;
        }

        private void DeleteParcel(object sender, RoutedEventArgs e)
        {
            PO.ListsModel.Bl.deleteBLParcel(parcelViewModel.ParcelInList.Id);
            if (MessageBox.Show("the customer was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Customer senderCustomer = PO.ListsModel.Bl.GetBLCustomer(int.Parse(senderId.Text));
                BO.Customer recieveCustomer = PO.ListsModel.Bl.GetBLCustomer(int.Parse(reciverId.Text));

                PO.ListsModel.Bl.AddParcel(new BO.Parcel()
                {
                    Id = idParcel++,
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
        /// Closes the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            parcelViewModel.RefreshParcelInList();
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


        private void CustomerSender(object sender, RoutedEventArgs e)
        {
            try
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Customer(PO.ListsModel.Bl.GetCustomerForList().FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.CustomerSender.Id));
                tabItem.Header = "update Sender Customer";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
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

        private void CustomerReceives(object sender, RoutedEventArgs e)
        {
            try
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Customer(PO.ListsModel.Bl.GetCustomerForList().FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.CustomerReceives.Id));
                tabItem.Header = "update Receives Customer";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
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

        private void Drone(object sender, RoutedEventArgs e)
        {
            try
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new Drone(PO.ListsModel.Bl.GetDroneForList()
                    .FirstOrDefault(c => c.Id == parcelViewModel.ParcelInList.DroneParcel.Id));
                tabItem.Header = "update  drone";
                tabItem.Visibility = Visibility.Visible;
               Tabs.AddTab(tabItem);
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
    }
}
