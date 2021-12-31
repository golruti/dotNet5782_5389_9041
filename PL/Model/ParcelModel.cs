using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL
{
    class ParcelModel
    {
        private BlApi.IBL bl;
        private BO.Parcel parcelInList;
        private Action refreshParcelList;
        Action<TabItem> addTab;
        Action<TabItem> closeTab;
    }
}
