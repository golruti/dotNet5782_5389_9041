using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PO.Enums;

namespace PO
{
   public class DroneForList : INotifyPropertyChanged
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

        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        private WeightCategories maxWeight;
        public WeightCategories MaxWeight
        {
            get { return maxWeight; }
            set
            {
                maxWeight = value;
                OnPropertyChanged(nameof(MaxWeight));
            }
        }

        private DroneStatuses status;
        public DroneStatuses Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private double battery;
        public double Battery
        {
            get { return battery; }
            set
            {
                battery = value;
                OnPropertyChanged(nameof(Battery));
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

        private int parcelDeliveredId;
        public int ParcelDeliveredId
        {
            get { return parcelDeliveredId; }
            set
            {
                parcelDeliveredId = value;
                OnPropertyChanged(nameof(ParcelDeliveredId));
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

