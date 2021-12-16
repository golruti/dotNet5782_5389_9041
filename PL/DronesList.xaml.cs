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
using IBL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DronesList
    {
        IBL.IBL bl;
        public DronesList(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;
            DronesListView.DataContext = bl.GetDroneForList() as ObservableCollection<DroneForList>;
            DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
            DroneStatuses.DataContext = Enum.GetValues(typeof(Enums.DroneStatuses));
        }

        private void DroneWeights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneWeights.SelectedItem == null)
            {
                DronesListView.ItemsSource = bl.GetDroneForList();
            }
            else
            {
                Enums.WeightCategories weight = (Enums.WeightCategories)DroneWeights.SelectedItem;
                DronesListView.ItemsSource = bl.GetDroneForList(drone => drone.MaxWeight == weight);
            }
        }

        private void DroneDtatuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DroneStatuses.SelectedItem == null)
            {
                DronesListView.ItemsSource = bl.GetDroneForList();
            }
            else
            {
                Enums.DroneStatuses status = (Enums.DroneStatuses)DroneStatuses.SelectedItem;
                DronesListView.ItemsSource = bl.GetDroneForList(drone => drone.Status == status);
            }
        }

        private void ShowAddDroneWindow(object sender, RoutedEventArgs e)
        {
            var tmp = sender;
            while (tmp.GetType() != typeof(MainWindow))
            {
                tmp = (tmp as FrameworkElement).Parent;
            }

            TabItem tabItem = new TabItem();
            tabItem.Content = new AddDrone(bl);
            tabItem.Header = "Add drone";
            (tmp as MainWindow).tub_control.Items.Add(tabItem);
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var tmp = sender;
            while (tmp.GetType() != typeof(MainWindow))
            {
                tmp = (tmp as FrameworkElement).Parent;
            }

            TabItem tabItem = new TabItem();
            tabItem.Content = new Drone((e.OriginalSource as FrameworkElement).DataContext as IBL.BO.DroneForList, this.bl);
            tabItem.Header = "Update drone";
            tabItem.Visibility = Visibility.Visible;
            (tmp as MainWindow).tub_control.Items.Add(tabItem);
            //(tmp as MainWindow).AddTab(tabItem);

        }

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
