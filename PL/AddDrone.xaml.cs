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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddDrone.xaml
    /// </summary>
    public partial class AddDrone
    {
       
        IBL.IBL bl;
        public AddDrone(IBL.IBL bl)
        {
            InitializeComponent();
            this.bl = bl;          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bl.AddDrone(int.Parse(Id.Text), int.Parse(StationsId.Text), (IBL.BO.Enums.WeightCategories)DroneWeights.SelectedItem, Model.Text);
        }

    }
}
