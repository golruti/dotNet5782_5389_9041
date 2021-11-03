using IBL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    interface IBL
    {

        public void UpdateParcelScheduled(int idxParcel);
        public void UpdateParcelPickedUp(int idxParcel);
        public void UpdateParcelDelivered(int idxParcel);
        public bool TryAddDroneCarge(int droneId);
        public bool TryRemoveDroneCarge(int droneId);


    }
}
