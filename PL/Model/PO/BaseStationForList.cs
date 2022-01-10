using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
   public class BaseStationForList : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int availableChargingPorts;
        public int AvailableChargingPorts
        {
            get { return availableChargingPorts; }
            set
            {
                availableChargingPorts = value;
                OnPropertyChanged(nameof(AvailableChargingPorts));
            }
        }

        private int usedChargingPorts;
        public int UsedChargingPorts
        {
            get { return usedChargingPorts; }
            set
            {
                usedChargingPorts = value;
                OnPropertyChanged(nameof(UsedChargingPorts));
            }
        }


        #region INotifyPropertyChanged Members  
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
