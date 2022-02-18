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
using static PL.ViewModel.ListsModel;

namespace PL
{
    /// <summary>
    /// Behind code for login and customer.
    /// </summary>
    public partial class CustomerEntrance : UserControl
    {
        public CustomerEntrance()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Login of an existing customer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sign_in_show_CustomerPageHome(object sender, RoutedEventArgs e)
        {
            int parse;
            if (Id_sign_in.Text != null && pass_sign_in.Text != null && int.TryParse(Id_sign_in.Text, out parse))
            {

                if (Bl.IsExistClient(int.Parse(Id_sign_in.Text), pass_sign_in.Text) == true)
                {
                    TabItem tabItem = new TabItem();
                    tabItem.Content = new CustomerHomePage(int.Parse(Id_sign_in.Text), pass_sign_in.Text);
                    tabItem.Header = "Customer Home Page";
                    Tabs.AddTab(tabItem);
                    Close_Page(sender, e);
                }
                else
                {
                    MessageBox.Show($"Incorrect id or password, please try again.");
                }
            }
        }

        /// <summary>
        /// Add another user to the system.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sign_up_show_CustomerPageHome(object sender, RoutedEventArgs e)
        {
            bool succeeded = true;
            int parse;

            try
            {
                if (int.TryParse(Id_sign_up.Text, out parse))
                {
                    Bl.AddUser(new BO.User()
                    {
                        Access = BO.Enums.Access.Client,
                        IsDeleted = false,
                        UserId = int.Parse(Id_sign_up.Text),
                        Password = pass_sign_up.Text
                    });
                }
                else
                {
                    succeeded = false;
                    MessageBox.Show($"Invalid username.");
                }
            }
            catch (BO.ThereIsAnObjectWithTheSameKeyInTheListException)
            {
                succeeded = false;
                MessageBox.Show($"here is someone in the system with the same name.");
            }
            catch (KeyNotFoundException)
            {
                succeeded = false;
                MessageBox.Show($"No matching customer found for UserId.");
            }
            if (succeeded == true)
            {
                TabItem tabItem = new TabItem();
                tabItem.Content = new CustomerHomePage(int.Parse(Id_sign_up.Text), pass_sign_up.Text);
                tabItem.Header = "Customer Home Page";
                Tabs.AddTab(tabItem);
                Close_Page(sender, e);
            }
        }

        /// <summary>
        /// the function close the page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Page(object sender, RoutedEventArgs e)
        {
            Tabs.RemoveTab(sender, e);
        }

        /// <summary>
        /// Back button to previous page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Screen display - existing user login.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_SignIn_buttons(object sender, RoutedEventArgs e)
        {
            Id_sign_in.Visibility = Visibility.Visible;
            pass_sign_in.Visibility = Visibility.Visible;
            ok_sign_in.Visibility = Visibility.Visible;
            center_sign_in.Visibility = Visibility.Collapsed;
            center_sign_up.Visibility = Visibility.Collapsed;
            back.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Screen display - Add user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
