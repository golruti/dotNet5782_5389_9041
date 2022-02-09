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
        public event PropertyChangedEventHandler PropertyChanged;

        ListCollectionView baseStationsList;

        public BaseStationListViewModel()
        {
            baseStationsList = new ListCollectionView(ListsModel.stations);
        }

        public ListCollectionView BaseStationsList
        {
            get { return baseStationsList; }
            set
            {
                baseStationsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BaseStationsList)));
            }
        }
    }
}

