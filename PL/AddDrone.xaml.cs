using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            StationsId.DataContext = bl.GetBaseStationForListsId();
            DroneWeights.DataContext = Enum.GetValues(typeof(Enums.WeightCategories));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            try
            {
                
                BaseStation baseStation = bl.GetBLBaseStation(int.Parse(StationsId.Text));
            }
            catch
            {
                throw new ArgumentNullException("station dond exist");
                
            }

            try
            {
                bl.AddDrone(int.Parse(Id.Text), int.Parse(StationsId.Text), (IBL.BO.Enums.WeightCategories)DroneWeights.SelectedItem, Model.Text);

                if (MessageBox.Show("the drone was seccessfully added", "success", MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    Close_Page(sender, e);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"The skimmer was not add, {ex.Message}");
            }
            


        }

        private void Close_Page(object sender, RoutedEventArgs e)
        {
            object tmp = sender;
            while (tmp.GetType() != typeof(TabItem))
            {
                tmp = ((FrameworkElement)tmp).Parent;
            }
            TabItem tabItem = (TabItem)tmp;
            
            (tmp as MainWindow).tub_control.Items.Remove(tabItem);


        }

        private void textID_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textModel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }   

    }
}
