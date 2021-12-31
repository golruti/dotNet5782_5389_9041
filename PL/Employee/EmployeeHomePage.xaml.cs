using BlApi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntranceOfManagers.xaml
    /// </summary>
    public partial class EmployeeHomePage : UserControl
    {
        private BlApi.IBL bl;
        Action<TabItem> addTab;
        public EmployeeHomePage(BlApi.IBL bl, Action<TabItem> addTab)
        {
            InitializeComponent();
            this.bl = bl;
            this.addTab = addTab;
        }

        /// <summary>
        /// open the drone list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDroneListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new DronesList(bl, addTab);
            tabItem.Header = "Drone list";
            addTab(tabItem);
        }

        private void ShowParcelListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new ParcelList(bl, addTab);
            //button.Visibility = Visibility.Collapsed;
            tabItem.Header = "parcel list";
            addTab(tabItem);
        }

        private void ShowBaseStationListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStationList(bl, addTab);
            button.Visibility = Visibility.Collapsed;
            //tabItem.Header = "Base station list";
            addTab(tabItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomerEntrance(bl,addTab);
            tabItem.Header = "Customer entrance";
            addTab(tabItem);
        }

        private void showCustomersListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomersList(bl, addTab);
            button.Visibility = Visibility.Collapsed;
            tabItem.Header = "Customers list";
            addTab(tabItem);
        }
    }
}
