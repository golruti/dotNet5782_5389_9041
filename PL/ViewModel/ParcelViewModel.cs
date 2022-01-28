using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace PL.ViewModel
{
    public class ParcelViewModel : INotifyPropertyChanged
    {
        public BlApi.IBL Bl { get; private set; }
        public Action RefreshParcelList { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public Action<TabItem> CloseTab { get; private set; }
        public IEnumerable<int> CustomersIds { get; set; }
        public IEnumerable<Enums.WeightCategories> Weights { get; set; }
        public IEnumerable<Enums.Priorities> Prioritys { get; set; }
        private PO.Parcel parcelInList;
        public PO.Parcel ParcelInList
        {
            get { return parcelInList; }
            set
            {
                parcelInList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelInList)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;







        public ParcelViewModel(int parcelInListId, BlApi.IBL bl, Action refreshParcelList, Action<TabItem> addTab)
            : this(bl, refreshParcelList)
        {
            this.ParcelInList = ConvertFunctions.BOParcelToPO(bl.GetBLParcel(parcelInListId));
            this.AddTab = addTab;
        }
        public ParcelViewModel(BlApi.IBL bl, Action refreshParcelList)
        {
            this.Bl = bl;
            this.RefreshParcelList = refreshParcelList;
            this.ParcelInList = new PO.Parcel();
            this.CustomersIds = bl.GetCustomerForList().Select(c => c.Id);
            Weights = (IEnumerable<Enums.WeightCategories>)Enum.GetValues(typeof(Enums.WeightCategories));
            Prioritys = (IEnumerable<Enums.Priorities>)Enum.GetValues(typeof(Enums.Priorities));
        }

        public void RefreshParcelInList()
        {
            ParcelInList = ConvertFunctions.BOParcelToPO(Bl.GetBLParcel(parcelInList.Id));
            RefreshParcelList();
        }
    }
}
