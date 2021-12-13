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
using Singleton;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private IBL.IBL bl = Singleton<IBL.BL>.Instance;
        private IBL.IBL bl = new IBL.BL();

        public MainWindow()
        {
            InitializeComponent();
            //StatusDrones.ItemsSource = Enum.GetValues(typeof(BL.Enum.DroneStatuses));
        }

        private void ShowDroneListWindow(object sender, RoutedEventArgs e)
        {
            new DronesList(bl).Show();
        }

        private void ShowDrone(object sender, RoutedEventArgs e)
        {
            //DroneView.ItemsSource = bl.GetBLDrone();
            //new DroneList(bl).Show();
        }
    }
}
