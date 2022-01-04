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

            //baseStationListViewModel.BaseStationsList= (ListCollectionView)CollectionViewSource.GetDefaultView(BaseStationListView.DataContext);
            //PropertyGroupDescription groupDescription = new PropertyGroupDescription("AvailableChargingPorts");
            //baseStationListViewModel.BaseStationsList.GroupDescriptions.Add(groupDescription);
        }


        private void Available_Checked_1(object sender, RoutedEventArgs e)
        {
            if (Available.IsChecked == false)
            {
                baseStationListViewModel.RefreshStationsList();
            }
            else
            {
                baseStationListViewModel.RefreshStationsList(station => station.AvailableChargingPorts > 0);
            }
        }


        private void ShowAddBaseStationWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStation(baseStationListViewModel.Bl, baseStationListViewModel.RefreshStationsList);
            tabItem.Header = "Add base station";
            baseStationListViewModel.AddTab(tabItem);
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
            tabItem.Content = new BaseStation(baseStationListViewModel.Bl, baseStationListViewModel.AddTab,PO.ConvertFunctions.POStationToBO( selectedBaseStation), baseStationListViewModel.RefreshStationsList);
            tabItem.Header = "Update base station";
            tabItem.Visibility = Visibility.Visible;
            baseStationListViewModel.AddTab(tabItem);
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


