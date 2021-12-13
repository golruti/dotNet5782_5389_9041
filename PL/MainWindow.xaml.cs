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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //// לשנות לפי סינגלטון
        private IBL.IBL bl = new IBL.BL();

        public MainWindow()
        {
            InitializeComponent();
            //StatusDrones.ItemsSource = Enum.GetValues(typeof(BL.Enum.DroneStatuses));
        }

        private void ShowDroneList(object sender, RoutedEventArgs e)
        {
            DronesList.ItemsSource = bl.GetDroneForList();
            //DroneList.
        }

        private void ShowDrone(object sender, RoutedEventArgs e)
        {
            //DroneView.ItemsSource = bl.GetBLDrone();
            //new DroneList(bl).Show();
        }
    }
}
