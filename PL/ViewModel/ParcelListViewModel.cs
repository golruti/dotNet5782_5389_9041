using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
    public class ParcelListViewModel
    {
        private ListCollectionView parcelsForList;
        public event PropertyChangedEventHandler PropertyChanged;

        public ParcelListViewModel()
        {
            parcelsForList = new ListCollectionView(ListsModel.parcels);
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
    }
}
