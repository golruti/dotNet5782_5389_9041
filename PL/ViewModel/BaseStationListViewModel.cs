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


        private ListCollectionView baseStationsList;
        public ListCollectionView BaseStationsList
        {
            get { return baseStationsList; }
            set
            {
                baseStationsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseStationsList)));
            }
        }


        public BaseStationListViewModel(BlApi.IBL bl, Action<TabItem> addTab)
        {
            Bl = bl;
            AddTab = addTab;
            baseStationsList = new ListCollectionView(ListsModel.stations);

            //baseStationsList = new ListCollectionView((System.Collections.IList)PO.ConvertFunctions.BOBaseStationForListToPO( bl.GetBaseStationForList()));
        }

        //public void RefreshStationsList(Predicate<BaseStationForList> predicate)
        //{
        //    BaseStationsList = (new ListCollectionView((System.Collections.IList)ConvertFunctions.BOBaseStationForListToPO(Bl.GetAvaBaseStationForList())));
        //}

        //public void RefreshStationsList()
        //{          
        //    BaseStationsList = new ListCollectionView((System.Collections.IList)ConvertFunctions.BOBaseStationForListToPO(Bl.GetBaseStationForList()));           
        //}
    }
}

