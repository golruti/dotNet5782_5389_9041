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
        public IEnumerable<int> SenderCustomersIds { get; set; }
        public IEnumerable<int> TargetCustomersIds { get; set; }

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



        public ParcelViewModel(int parcelInListId)
        {
            this.ParcelInList = ConvertFunctions.BOParcelToPO(ListsModel.Bl.GetBLParcel(parcelInListId));
        }
        public ParcelViewModel()
        {
            this.ParcelInList = new PO.Parcel();
            this.SenderCustomersIds = ListsModel.Bl.GetCustomerForList().Select(c => c.Id);
            this.TargetCustomersIds = ListsModel.Bl.GetCustomerForList().Select(c => c.Id); 
            Weights = (IEnumerable<Enums.WeightCategories>)Enum.GetValues(typeof(Enums.WeightCategories));
            Prioritys = (IEnumerable<Enums.Priorities>)Enum.GetValues(typeof(Enums.Priorities));
        }

        public ParcelViewModel(BO.User user)
        {
            this.ParcelInList = new PO.Parcel();
            this.SenderCustomersIds = ListsModel.Bl.GetCustomerForList().Where(c=>c.Id == user.UserId).Select(c => c.Id);
            this.TargetCustomersIds = ListsModel.Bl.GetCustomerForList().Where(c=>!c.Equals(SenderCustomersIds)).Select(c => c.Id) ;
            Weights = (IEnumerable<Enums.WeightCategories>)Enum.GetValues(typeof(Enums.WeightCategories));
            Prioritys = (IEnumerable<Enums.Priorities>)Enum.GetValues(typeof(Enums.Priorities));
        }
        public void RefreshParcelInList()
        {
            ParcelInList = ConvertFunctions.BOParcelToPO(ListsModel.Bl.GetBLParcel(parcelInList.Id));
            PO.ListsModel.RefreshParcels();
        }
    }
}
