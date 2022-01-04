using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class BaseStationListViewModel : INotifyPropertyChanged
    {
        public BlApi.IBL Bl { get; private set; }
        public Action<TabItem> AddTab { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private ListCollectionView baseStationsForList;
        public ListCollectionView BaseStationsForList
        {
            get { return baseStationsForList; }
            set
            {
                baseStationsForList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseStationsForList)));
            }
        }


        public BaseStationListViewModel(BlApi.IBL bl, Action<TabItem> addTab)
        {
            Bl = bl;
            AddTab = addTab;
            baseStationsForList = new ListCollectionView((System.Collections.IList)PO.ConvertFunctions.BOBaseStationForListToPO( bl.GetBaseStationForList()));
        }

        public void RefreshStationsList(Predicate<BaseStationForList> predicate)
        {
            BaseStationsForList = (new ListCollectionView((System.Collections.IList)ConvertFunctions.BOBaseStationForListToPO(Bl.GetAvaBaseStationForList())));
        }

        public void RefreshStationsList()
        {          
            BaseStationsForList = new ListCollectionView((System.Collections.IList)ConvertFunctions.BOBaseStationForListToPO(Bl.GetBaseStationForList()));           
        }
    }
}

