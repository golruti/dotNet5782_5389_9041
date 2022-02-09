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
    /// Interaction logic for AA.xaml
    /// </summary>
    public partial class CustomerEntrance : UserControl
    {
        public CustomerEntrance( )
        {
            InitializeComponent();
        }

        private void sign_in_show_CustomerPageHome(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomerHomePage();
            tabItem.Header = "Customer Home Page";
            Tabs.AddTab(tabItem);
            Close_Page(sender, e);
        }

        private void sign_up_show_CustomerPageHome(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new CustomerHomePage();
            tabItem.Header = "Customer Home Page";
            Tabs.AddTab(tabItem);
            Close_Page(sender, e);
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Id_sign_up.Visibility = Visibility.Collapsed;
            pass_sign_up.Visibility = Visibility.Collapsed;
            ok_sign_up.Visibility = Visibility.Collapsed;
            Id_sign_in.Visibility = Visibility.Collapsed;
            pass_sign_in.Visibility = Visibility.Collapsed;
            ok_sign_in.Visibility = Visibility.Collapsed;
            center_sign_in.Visibility = Visibility.Visible;
            center_sign_up.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Collapsed;
        }

        private void Show_SignIn_buttons(object sender, RoutedEventArgs e)
        {
            Id_sign_in.Visibility = Visibility.Visible;
            pass_sign_in.Visibility = Visibility.Visible;
            ok_sign_in.Visibility = Visibility.Visible;
            center_sign_in.Visibility = Visibility.Collapsed;
            center_sign_up.Visibility = Visibility.Collapsed;
            back.Visibility = Visibility.Visible;
        }
        private void Show_SignUp_buttons(object sender, RoutedEventArgs e)
        {
            Id_sign_up.Visibility = Visibility.Visible;
            pass_sign_up.Visibility = Visibility.Visible;
            ok_sign_up.Visibility = Visibility.Visible;
            center_sign_in.Visibility = Visibility.Collapsed;
            center_sign_up.Visibility = Visibility.Collapsed;
            back.Visibility = Visibility.Visible;

        }
    }
}
