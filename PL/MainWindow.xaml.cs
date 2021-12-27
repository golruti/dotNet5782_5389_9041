using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Singleton;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBL bl;

        public MainWindow()
        {
            bl = BlApi.BlFactory.GetBl();
            InitializeComponent();
        }

        /// <summary>
        /// add tab
        /// </summary>
        /// <param name="tabItem">the tab to add</param>
        internal void AddTab(TabItem tabItem)
        {
            tub_control.Items.Add(tabItem);
        }

        private void showCustomerOfCompanyWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomerEntrance(bl, AddTab);
            tabItem.Header = "Customer of company";
            tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }

        private void showEmployeeWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Employee(bl, AddTab);
            tabItem.Header = "Employee";
            tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }
    }
}
