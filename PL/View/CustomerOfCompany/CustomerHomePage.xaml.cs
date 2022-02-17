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
using PL.ViewModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerHomePage.xaml
    /// </summary>
    public partial class CustomerHomePage : UserControl
    {
        
        int userId;
        string password;

        public CustomerHomePage(int customerId,string password)
        {
            this.userId = customerId;
            this.password = password;
            InitializeComponent();
        }

        private void ShowParcelList_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new ParcelList(this.userId);
            tabItem.Header = "parcel list";
            AddTab(tabItem);
        }

        private void AddParcel_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new Parcel(ListsModel.Bl.GetUser(userId, password,BO.Enums.Access.Client));
            tabItem.Header = "parcel list";
            AddTab(tabItem);
        }
    }
}
