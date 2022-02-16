using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BO
{
    public class ParcelForList
    {
        #region properties
        public int Id { get; set; }
        public string SendCustomer { get; set; }
        public string ReceiveCustomer { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public ParcelStatuses Status { get; set; }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"Parcel #{Id}:, Send Customer={SendCustomer},Receive Customer={ReceiveCustomer}, Weight{Weight}, " +
                $"Priority{Priority}, Status{Status}";
        }
        #endregion
    }
}

