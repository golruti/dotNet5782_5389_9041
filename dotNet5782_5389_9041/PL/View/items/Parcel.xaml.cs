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
        static int idParcel { get; set; } = 0;
        public Parcel(BlApi.IBL bl, Action refreshParcelsList)
        {
            InitializeComponent();
            parcelViewModel = new ParcelViewModel(bl, refreshParcelsList);
            this.DataContext = parcelViewModel;
            Add_grid.Visibility = Visibility.Visible;
            Weight.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
            Priority.DataContext = Enum.GetValues(typeof(Enums.Priorities));
        }

        
        public Parcel(int parcelInListId, BlApi.IBL bl, Action refreshParcelsList)
        {
            InitializeComponent();
            parcelViewModel = new ParcelViewModel(parcelInListId, bl, refreshParcelsList);
            this.DataContext = parcelViewModel;
            Update_grid.Visibility = Visibility.Visible;
           
        }

        private void DeleteParcel(object sender, RoutedEventArgs e)
        {
            parcelViewModel.Bl.deleteBLParcel(parcelViewModel.ParcelInList.Id);
            if (MessageBox.Show("the customer was seccessfully deleted", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                Close_Page(sender, e);
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                parcelViewModel.Bl.AddParcel(new BO.Parcel(idParcel++, int.Parse(senderId.Text), int.Parse(reciverId.Text), (BO.Enums.WeightCategories)Weight.SelectedItem, (BO.Enums.Priorities)Priority.SelectedItem, new BO.DroneInParcel()));

                if (MessageBox.Show("the parcel was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone was not add, {ex.Message}");
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
            this.parcelViewModel.RefreshParcelList();
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


        private void ShowDrone_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                parcelViewModel.Bl.UpdateCharge(parcelViewModel.ParcelInList.Id);
                (sender as Button).IsEnabled = false;
                if (MessageBox.Show("The drone was sent for loading", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }
            }
            catch (KeyNotFoundException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (BO.ThereIsNoNearbyBaseStationThatTheDroneCanReachException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The drone could not be sent for loading, {ex.Message}");
            }
        }

        private void CustomerView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedCustomer = (sender as ContentControl).DataContext as string;

            TabItem tabItem = new TabItem();
            tabItem.Content = new Customer(parcelViewModel.Bl.GetCustomerForList(selectedCustomer), this.parcelViewModel.Bl, parcelViewModel.RefreshParcelList,parcelViewModel.AddTab);
            tabItem.Header = "parcel";
            tabItem.Visibility = Visibility.Visible;
            this.parcelViewModel.AddTab(tabItem);

        }

    
       
    }
}
