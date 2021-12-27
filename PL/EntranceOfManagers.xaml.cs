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
    public partial class EntranceOfManagers : UserControl
    {
        private BlApi.IBL bl;

        public EntranceOfManagers()
        {
            InitializeComponent();
            bl = BlApi.BlFactory.GetBl();
        }


        /// <summary>
        /// open the drone list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowDroneListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new DronesList(bl, AddTab, RemoveTab);
            button.Visibility = Visibility.Collapsed;
            tabItem.Header = "Drone List";
            //tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }

        private void ShowBaseStationListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStationList(bl, AddTab);
            button.Visibility = Visibility.Collapsed;
            tabItem.Header = "Base station List";
            //tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }

        /// <summary>
        /// add tab
        /// </summary>
        /// <param name="tabItem">the tab to add</param>
        internal void AddTab(TabItem tabItem)
        {
            //tub_control.Items.Add(tabItem);
        }

        /// <summary>
        /// remove tab
        /// </summary>
        /// <param name="tabItem">the tub to remove</param>
        internal void RemoveTab(TabItem tabItem)
        {
            //tub_control.Items.Remove(tabItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomerOfCompany(AddTab,bl);
            tabItem.Header = "CustomerOfCompany";
            //tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }

        private void showCustomersListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomersList(bl, AddTab, RemoveTab);
            button.Visibility = Visibility.Collapsed;
            tabItem.Header = "customers List";
            //tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }
    }
}
