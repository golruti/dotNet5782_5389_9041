﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class BaseStation : INotifyPropertyChanged
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

        private Location location;
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        private IEnumerable<DroneInCharging> dronesInCharging;
        public IEnumerable<DroneInCharging> DronesInCharging
        {
            get { return dronesInCharging; }
            set
            {
                dronesInCharging = value;
                OnPropertyChanged(nameof(DronesInCharging));
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