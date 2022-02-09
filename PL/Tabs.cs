using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL
{
    public class Tabs
    {
        //static event EventHandler Tab
        static Action<TabItem> addTab;
        public static void SetAddTab(Action<TabItem> addTab1)
        {
            addTab = addTab1;
        }
        public static void AddTab(TabItem tabItem)
        {
            addTab(tabItem);
            //TabItem tabItem = new TabItem();
            //tabItem.Content = new Drone(ConvertFunctions.PODroneForListToBO(selectedDrone), droneListViewModel.Bl/*, RefreshDroneList*/);
            //tabItem.Header = "Update drone";
            //tabItem.Visibility = Visibility.Visible;
            //this.droneListViewModel.AddTab(tabItem);
        }
    }
}
