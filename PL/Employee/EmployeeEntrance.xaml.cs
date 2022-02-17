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
using static PO.ListsModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        public Employee()
        {
            InitializeComponent();
        }

        private void show_EmployeeHomePage(object sender, RoutedEventArgs e)
        {
            int parse;
            if (userName.Text != null && password.Text != null && int.TryParse(userName.Text,out parse))
            {
                if (Bl.IsExistEmployee(int.Parse(userName.Text), password.Text))
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Content = new EmployeeHomePage();
                    tabItem.Header = "Employee Home Page";
                    AddTab(tabItem);
                    Close_Page(sender, e);
                }
                else
                {
                    MessageBox.Show($"Incorrect username or password, please try again.");
                }
            }
            else
            {
                MessageBox.Show($"Please fill in a username and password.");
            }
        }

        /// <summary>
        /// the function close the page
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
        }
    }
}
