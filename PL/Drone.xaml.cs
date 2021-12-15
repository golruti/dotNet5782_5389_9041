using IBL.BO;
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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone 
    {
        private IBL.IBL bl;
        private IBL.BO.Drone droneForList;

        public Drone(DroneForList droneForList, IBL.IBL bl)
        {
            InitializeComponent();

            this.bl = bl;
            this.droneForList =bl.GetBLDrone( droneForList.Id);
            this.DataContext = droneForList;

            if (droneForList.Status == Enums.DroneStatuses.Available)
            {
                Button sendingDroneForChargingBtn = new Button();
                sendingDroneForChargingBtn.Content = "Sending a drone for charging";
                sendingDroneForChargingBtn.Click += SendingDroneForCharging_Click;
                ButtonsGroup.Children.Add(sendingDroneForChargingBtn);
            }
            else if (droneForList.Status == Enums.DroneStatuses.Maintenance)
            {
                Button ReleaseDroneFromCharging = new Button();
                ReleaseDroneFromCharging.Content = "Release drone from charging";
                ReleaseDroneFromCharging.Click += ReleaseDroneFromCharging_Click;
                ButtonsGroup.Children.Add(ReleaseDroneFromCharging);
            }

            if (bl.GetParcelStatusByDrone(droneForList.Id) == Enums.ParcelStatuses.Associated)
            {
                Button ParcelCollection = new Button();
                ParcelCollection.Content = "Parcel collection";
                ParcelCollection.Click += ParcelCollection_Click;
                ButtonsGroup.Children.Add(ParcelCollection);
            }

            else if (bl.GetParcelStatusByDrone(droneForList.Id) == Enums.ParcelStatuses.Collected)
            {
                Button ParcelDelivery = new Button();
                ParcelDelivery.Content = "Parcel delivery";
                ParcelDelivery.Click += ParcelDelivery_Click;
                ButtonsGroup.Children.Add(ParcelDelivery);
            }
        }


        private void ParcelDelivery_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ParcelCollection_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ReleaseDroneFromCharging_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SendingDroneForCharging_Click(object sender, RoutedEventArgs e)
        {

            bl.SendDroneToRecharge(droneForList.Id);
        }
    }
}
