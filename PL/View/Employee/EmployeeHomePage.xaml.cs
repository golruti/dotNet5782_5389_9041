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
using static PL.Tabs;

namespace PL
{
    /// <summary>
    /// Interaction logic for EntranceOfManagers.xaml
    /// </summary>
    public partial class EmployeeHomePage : UserControl
    {

        public EmployeeHomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// open the drone list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDroneListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new DronesList();
            tabItem.Header = "Drone list";
            AddTab(tabItem);
        }

        private void ShowParcelListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new ParcelList();           
            tabItem.Header = "parcel list";
            AddTab(tabItem);
        }

        private void ShowBaseStationListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStationList();           
            tabItem.Header = "Base station list";
            AddTab(tabItem);
        }

        private void showCustomersListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomersList();
            tabItem.Header = "Customers list";
            AddTab(tabItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomerEntrance();
            tabItem.Header = "Customer entrance";
            AddTab(tabItem);
        }     
    }
}
