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
    /// Interaction logic for CustomerHomePage.xaml
    /// </summary>
    public partial class CustomerHomePage : UserControl
    {
        int customerId;
        public CustomerHomePage(int customerId)
        {
            this.customerId = customerId;
            InitializeComponent();
        }

        private void ShowParcelList_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new ParcelList(this.customerId);
            tabItem.Header = "parcel list";
            AddTab(tabItem);
        }
    }
}
