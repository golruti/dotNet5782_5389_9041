﻿using System;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerOfCompany.xaml
    /// </summary>
    public partial class CustomerOfCompany
    {
        Action<TabItem> addTab;

        public CustomerOfCompany(Action<TabItem> addTab)
        {
            InitializeComponent();
            this.addTab = addTab;
        }

        private void ShowSignInWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new SignIn(addTab);
            tabItem.Header = "Sign in";
            this.addTab(tabItem);
            Close_Page(sender, e);

        }

        private void ShowSignUpWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new SignUp(addTab);
            tabItem.Header = "Sign up";
            this.addTab(tabItem);
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
            //TabItem tabItem = new TabItem();
            //tabItem.Content = new MainWindow();
            //tabItem.Header = "MainWindow";
            //this.addTab(tabItem);
            Close_Page(sender, e);
        }

        private void Show_SignIn_buttons(object sender, RoutedEventArgs e)
        {
            Id_sign_in.Visibility = Visibility.Visible;
            pass_sign_in.Visibility = Visibility.Visible;
            ok_sign_in.Visibility = Visibility.Visible;
            center_sign_in.Visibility = Visibility.Collapsed;
            center_sign_up.Visibility = Visibility.Collapsed;
        }
        private void Show_SignUp_buttons(object sender, RoutedEventArgs e)
        {
            Id_sign_up.Visibility = Visibility.Visible;
            pass_sign_up.Visibility = Visibility.Visible;
            ok_sign_up.Visibility = Visibility.Visible;
            center_sign_in.Visibility = Visibility.Collapsed;
            center_sign_up.Visibility = Visibility.Collapsed;
        }
    }
}