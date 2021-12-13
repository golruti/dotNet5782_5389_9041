﻿using System;
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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private IBL.IBL bl = new IBL.BL();
        public Main()
        {
            InitializeComponent();
        }
        private void ShowDroneList(object sender, RoutedEventArgs e)
        {
            DroneList droneList = new DroneList();
            droneList.NavigationService.Navigate(droneList);
        }

        private void ShowDrone(object sender, RoutedEventArgs e)
        {
           
            Drone drone = new Drone();
            drone.Show();
        }
    }
}
