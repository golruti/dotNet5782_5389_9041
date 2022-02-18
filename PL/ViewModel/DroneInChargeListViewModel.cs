using PO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class DroneInChargeListViewModel : INotifyPropertyChanged
    {

        public ListCollectionView DronesInChargeList
        {
            get { return dronesInChargeList; }
            set
            {
                dronesInChargeList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DronesInChargeList)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private ListCollectionView dronesInChargeList;
    }
}
