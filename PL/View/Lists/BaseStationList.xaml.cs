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
using System.Collections.ObjectModel;
using PL.ViewModel;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationList.xaml
    /// </summary>
    public partial class BaseStationList : UserControl
    {
        BaseStationListViewModel baseStationListViewModel;

        public BaseStationList(BlApi.IBL bl, Action<TabItem> addTab, Action<object, RoutedEventArgs> removeTab)
        {
            InitializeComponent();
            baseStationListViewModel = new BaseStationListViewModel(bl, addTab);
            this.DataContext = baseStationListViewModel;
            baseStationListViewModel.BaseStationsList.GroupDescriptions.Add(new PropertyGroupDescription("AvailableChargingPorts"));
        }

        private void ShowAddBaseStationWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStation();
            tabItem.Header = "Add base station";
            Tabs.AddTab(tabItem);
        }

        /// <summary>
        /// window winder that allows you to update the details of the glider that you double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseStationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedBaseStation = BaseStationListView.SelectedItem as PO.BaseStationForList;
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStation(PO.ConvertFunctions.POStationToBO( selectedBaseStation));
            tabItem.Header = "Update base station";
            tabItem.Visibility = Visibility.Visible;
            Tabs.AddTab(tabItem);
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

        private void Available_Checked(object sender, RoutedEventArgs e)
        {
            //baseStationListViewModel.RefreshStationsList(station => station.AvailableChargingPorts > 0);
        }
        private void Available_Unchecked(object sender, RoutedEventArgs e)
        {
            //baseStationListViewModel.RefreshStationsList();
        }
    }
}


