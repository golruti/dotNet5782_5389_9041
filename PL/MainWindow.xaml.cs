﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// 
    public partial class MainWindow : Window/*, INotifyPropertyChanged*/
    {
        private IBL.IBL bl;
        private TabItem selectedTab;
        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            bl = Singleton<IBL.BL>.Instance;
            InitializeComponent();

        }

        private void ShowDroneListWindow(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = new TabItem();
            tabItem.Content = new DronesList(bl);
            button.Visibility = Visibility.Collapsed;
            tabItem.Header = "Drone List";
            tub_control.Visibility = Visibility.Visible;
            tub_control.Items.Add(tabItem);
            //SelectedTab = tabItem;
        }

        private void ShowDrone(object sender, RoutedEventArgs e)
        {
            //DroneView.ItemsSource = bl.GetBLDrone();
            //new DroneList(bl).Show();
        }

        //public TabItem SelectedTab
        //{
        //    get => selectedTab;
        //    set
        //    {
        //        selectedTab = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTab)));
        //    }
        //}

        //internal void AddTab(TabItem tabItem)
        //{
        //    tub_control.Items.Add(tabItem);
        //    SelectedTab = tabItem;
        //}
    }
}