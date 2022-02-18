using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PL
{
    public class Tabs
    {
        static Action<TabItem> addTab;
        static Action<object, RoutedEventArgs> removeTab;
        public static void SetAddTab(Action<TabItem> addTab1)
        {
            addTab = addTab1;
        }
        public static void AddTab(TabItem tabItem)
        {
            addTab(tabItem);
        }
        public static void RemoveTab(object sender, RoutedEventArgs e)
        {
            removeTab(sender, e);
        }
        public static void SetRemoveTab(Action<object, RoutedEventArgs> removeTab1)
        {
            removeTab = removeTab1;
        }
        
    }
}
