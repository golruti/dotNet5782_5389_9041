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
            DronesListView.DataContext = bl.GetDroneForList();
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
            
        private void k_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
    }
}
