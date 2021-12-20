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
    /// 
    public partial class MainWindow : Window/*, INotifyPropertyChanged*/
    {
        private BlApi.IBL bl;

        public MainWindow()
        {
            bl = Singleton<BL.BL>.Instance;
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
            tabItem.Content = new DronesList(bl, AddTab, RemoveTab);
            button.Visibility = Visibility.Collapsed;
            tabItem.Header = "Drone List";
            tub_control.Visibility = Visibility.Visible;
            AddTab(tabItem);
        }

        /// <summary>
        /// add tab
        /// </summary>
        /// <param name="tabItem">the tab to add</param>
        internal void AddTab(TabItem tabItem)
        {
            tub_control.Items.Add(tabItem);
        }

        /// <summary>
        /// remove tab
        /// </summary>
        /// <param name="tabItem">the tub to remove</param>
        internal void RemoveTab(TabItem tabItem)
        {
            tub_control.Items.Remove(tabItem);
        }      
    }
}
