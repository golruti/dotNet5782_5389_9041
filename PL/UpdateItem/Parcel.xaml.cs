using BO;
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

namespace PL.UpdateItem
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    public partial class Parcel : UserControl
    {
        Action<TabItem> addTab;
        private BlApi.IBL bl;
        private BO.Parcel parcelInList;
        private Action refreshParcelList;
        
        static int idParcel = 0;
        public Parcel(BlApi.IBL bl, Action refreshParcelList)
        {
            InitializeComponent();
            this.bl = bl;

            Priority.DataContext = Enum.GetValues(typeof(Enums.Priorities));
            this.refreshParcelList = refreshParcelList;
            Weight.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
        }

        public Parcel(ParcelForList parcelForList, BlApi.IBL bl, Action refreshParcelList, Action<TabItem> addTab)
        {
            InitializeComponent();

            this.bl = bl;
            this.refreshParcelList = refreshParcelList;
            this.addTab = addTab;
            this.parcelInList = bl.GetBLParcel(parcelForList.Id);
            this.DataContext = parcelForList;

            if (parcelForList.Status != Enums.ParcelStatuses.Provided )
            {
                Button ShowDrone = new Button();
                ShowDrone.Content = "Details of the drone";
                ShowDrone.Click += ShowDrone_Click;
                ShowDrone.IsEnabled = true;
                ShowDrone.Background = Brushes.DarkCyan;
                ShowDrone.Height = 40;
                ShowDrone.Width = 130;
                ShowDrone.HorizontalAlignment = HorizontalAlignment.Center;
                ButtonsGroup.Children.Add(ShowDrone);

                
            }
            
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bl.AddParcel(new BO.Parcel(idParcel++, int.Parse(senderId.Text), int.Parse(reciverId.Text), (BO.Enums.WeightCategories)Weight.SelectedItem, (BO.Enums.Priorities)Priority.SelectedItem, new BO.DroneInParcel()));

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
            this.refreshParcelList();
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
                bl.UpdateCharge(parcelInList.Id);
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
            tabItem.Content = new Customer(bl.GetCustomerForList(selectedCustomer), this.bl, refreshParcelList);
            tabItem.Header = "parcel";
            tabItem.Visibility = Visibility.Visible;
            this.addTab(tabItem);

        }

    
       
    }
}
