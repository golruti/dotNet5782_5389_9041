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

        /// <summary>
        /// Constructor
        /// </summary>
        public BaseStationList()
        {
            InitializeComponent();
            baseStationListViewModel = new BaseStationListViewModel();
            this.DataContext = baseStationListViewModel;
            baseStationListViewModel.BaseStationsList.Filter = FilterBaseStation;
        }

        /// <summary>
        /// Show "Add Station" window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowAddBaseStationWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new BaseStation();
            tabItem.Header = "Add base station";
            Tabs.AddTab(tabItem);
        }

        /// <summary>
        /// View a specific station.
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
        /// View stations that have free charging slotes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Available_Checked(object sender, RoutedEventArgs e)
        {
            baseStationListViewModel.BaseStationsList.Refresh();
        }
        /// <summary>
        /// View all stations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Available_Unchecked(object sender, RoutedEventArgs e)
        {
            baseStationListViewModel.BaseStationsList.Refresh();
        }

        /// <summary>
        /// Filter for filtering the stations list.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterBaseStation(object obj)
        {
            if (obj is PO.BaseStationForList station)
            {
                return (this.Available.IsChecked ?? false) ? station.AvailableChargingPorts > 0 : true;
            }
            return false;
        }


        /// <summary>
        /// View stations by groups.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Close the page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Page(object sender, RoutedEventArgs e)
        {
            Tabs.RemoveTab(sender, e);
        }
    }
}


