﻿using IBL.BO;
using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Customer = IBL.BO.Customer;
using Drone = IBL.BO.Drone;
using Parcel = IBL.BO.Parcel;
using BaseStation = IBL.BO.BaseStation;

namespace IBL
{
    public partial interface IBL
    {
        public void AddParcel(Parcel tempParcel);
        public Parcel GetBLParcel(int parcelId);

        public IEnumerable<ParcelForList> GetParcelForList();
        public IEnumerable<ParcelForList> UnassignedParcelsForList();
    }
}
