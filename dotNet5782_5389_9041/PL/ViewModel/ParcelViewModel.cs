using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class ParcelViewModel
    {
        public BlApi.IBL Bl { get; private set; }
        public Action RefreshParcelList { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public PO.Parcel ParcelInList { get; set; }
        

        public ParcelViewModel(BlApi.IBL bl, Action refreshParcelList)
        {
            this.Bl = bl;
            this.RefreshParcelList = refreshParcelList;
            this.ParcelInList = new PO.Parcel();
            
        }
        public ParcelViewModel(int parcelInListId, BlApi.IBL bl, Action refreshParcelList)
        {
            this.Bl = bl;
            this.RefreshParcelList = refreshParcelList;
            this.ParcelInList = ConvertFunctions.BOParcelToPO(bl.GetBLParcel(parcelInListId));
            
        }
    }
}
