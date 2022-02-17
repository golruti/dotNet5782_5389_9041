using PL.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PL
{
    /// <summary>
    /// Interaction logic for BaseStationList.xaml
    /// </summary>
    public partial class BaseStationList : UserControl
    {
        BaseStationListViewModel baseStationListViewModel;

        public BaseStationList()
        {
            InitializeComponent();
            baseStationListViewModel = new BaseStationListViewModel();
            this.DataContext = baseStationListViewModel;
            baseStationListViewModel.BaseStationsList.Filter = FilterBaseStation;
        }

        private bool FilterBaseStation(object obj)
        {
            if (obj is PO.BaseStationForList station)
            {
                return (this.Available.IsChecked ?? false) ? station.AvailableChargingPorts > 0 : true;
            }
            return false;
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
            if (BaseStationListView.SelectedItem != null)
            {
                var selectedBaseStation = BaseStationListView.SelectedItem as PO.BaseStationForList;
                TabItem tabItem = new TabItem();
                tabItem.Content = new BaseStation(PO.ConvertFunctions.POStationToBO(selectedBaseStation));
                tabItem.Header = "Update base station";
                tabItem.Visibility = Visibility.Visible;
                Tabs.AddTab(tabItem);
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

        private void Available_Checked(object sender, RoutedEventArgs e)
        {
            baseStationListViewModel.BaseStationsList.Refresh();
        }
        private void Available_Unchecked(object sender, RoutedEventArgs e)
        {
            baseStationListViewModel.BaseStationsList.Refresh();
        }
        
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.IsChecked == true)
            {
                baseStationListViewModel.BaseStationsList.GroupDescriptions.Add(new PropertyGroupDescription("AvailableChargingPorts"));
            }
            else
            {
                baseStationListViewModel.BaseStationsList.GroupDescriptions.Clear();
            }
        }
        

    }
}


