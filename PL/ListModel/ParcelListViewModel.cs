using PO;
using System.ComponentModel;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class ParcelListViewModel
    {
        public ParcelListViewModel(int? customerId = null)
        {
            ParcelsForList = new ListCollectionView(ListsModel.parcels);
            if (customerId.HasValue)
                this.Customer = ListsModel.Bl.GetBLCustomer(customerId.Value);
        }

        public ListCollectionView ParcelsForList
        {
            get { return parcelsForList; }
            set
            {
                parcelsForList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ParcelsForList)));
            }
        }
        private ListCollectionView parcelsForList;
        public BO.Customer Customer { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
