﻿using PO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace PL.ViewModel
{
   public class DroneListModel
    {
        BlApi.IBL bl;
        ObservableCollection<DroneForList> droneForLists;
        ListCollectionView listCollectionView;
        Action<TabItem> addTab;
        Action<TabItem> closeTab;
    }
}