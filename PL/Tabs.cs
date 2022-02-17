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
        static Action<TabItem> addTab;
        public static void SetAddTab(Action<TabItem> addTab1)
        {
            addTab = addTab1;
        }
        public static void AddTab(TabItem tabItem)
        {
            addTab(tabItem);
        }
    }
}
