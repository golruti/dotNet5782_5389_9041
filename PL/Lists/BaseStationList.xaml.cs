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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationList.xaml
    /// </summary>
    public partial class BaseStationList : UserControl
    {
        BlApi.IBL bl;
        Action<TabItem> addTab;
        ObservableCollection<BaseStationForList> BaseStations;
        public BaseStationList(BlApi.IBL bl, Action<TabItem> addTab)
        {
            InitializeComponent();
            this.bl = bl;
            this.addTab = addTab;
            BaseStations = new ObservableCollection<BaseStationForList>(bl.GetBaseStationForList());
            BaseStationListView.DataContext = BaseStations;
            
        }
        private void RefreshBaseStationList()
        {
            if (Available.IsChecked == true)
            {
                
                BaseStations = new ObservableCollection<BaseStationForList>(bl.GetBaseStationForList().Where(item=>item.AvailableChargingPorts>0));
                BaseStationListView.DataContext = BaseStations;
            }
            else 
            {
                BaseStations = new ObservableCollection<BaseStationForList>(bl.GetBaseStationForList());
                BaseStationListView.DataContext = BaseStations;
            }
            
        }

        private void AvailableBaseStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Available.IsChecked == false)
            {
                BaseStationListView.DataContext = bl.GetBaseStationForList();
            }
            else
            {
                BaseStationListView.DataContext = bl.GetBaseStationForList().Where(item => item.AvailableChargingPorts > 0);
                
            }
        }

      
        private void ShowAddBaseStationWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStation(bl, RefreshBaseStationList);
            tabItem.Header = "Add base station";
            this.addTab(tabItem);
        }

        /// <summary>
        /// window winder that allows you to update the details of the glider that you double-clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseStationListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedBaseStation = (sender as ContentControl).DataContext as BO.BaseStationForList;

            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStation(this.bl,addTab,selectedBaseStation, RefreshBaseStationList);
            tabItem.Header = "Update drone";
            tabItem.Visibility = Visibility.Visible;
            this.addTab(tabItem);

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
    

