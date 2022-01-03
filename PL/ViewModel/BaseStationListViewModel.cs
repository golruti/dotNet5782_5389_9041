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

        private ObservableCollection<BaseStationForList> baseStationsForList;
        public ObservableCollection<PO.BaseStationForList> BaseStationsForList
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
            baseStationsForList = new ObservableCollection<BaseStationForList>(PO.ConvertFunctions.BOBaseStationForListToPO( bl.GetBaseStationForList()));
        }

        public void RefreshStationsList(Predicate<BaseStationForList> predicate)
        {
            BaseStationsForList = (ObservableCollection<PO.BaseStationForList>)((new ObservableCollection<PO.BaseStationForList>(ConvertFunctions.BOBaseStationForListToPO(Bl.GetBaseStationForList()))).Where(s => predicate(s)));
            var t = ConvertFunctions.BOBaseStationForListToPO(Bl.GetBaseStationForList());
            var w = t.Filtering(s => predicate(s));
            var s = new ObservableCollection<BaseStationForList>(w);
        }

        public void RefreshStationsList()
        {
            
            BaseStationsForList = new ObservableCollection<BaseStationForList>(ConvertFunctions.BOBaseStationForListToPO(Bl.GetBaseStationForList()));
            
        }
    }
}

