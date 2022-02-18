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
            Tabs.SetAddTab(AddTab);
            Tabs.SetRemoveTab(RemoveTab);

        }

        /// <summary>
        /// add tab
        /// </summary>
        /// <param name="tabItem">the tab to add</param>
        internal void AddTab(TabItem tabItem)
        {
            tub_control.Items.Add(tabItem);
            tub_control.SelectedItem = tabItem;
        }


        internal void RemoveTab(object sender, RoutedEventArgs e)
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
            ViewModel.ListsModel.RefreshDrones();
            ViewModel.ListsModel.RefreshStations();
            ViewModel.ListsModel.RefreshParcels();
            ViewModel.ListsModel.RefreshCustomers();
        }


        private void CloseTab(object sender, RoutedEventArgs e)
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
        }
        private void showCustomerOfCompanyWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomerEntrance();
            tabItem.Header = "Customer of company";
            tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }

        private void showEmployeeWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Employee();
            tabItem.Header = "Employee";
            tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }
    }
}
