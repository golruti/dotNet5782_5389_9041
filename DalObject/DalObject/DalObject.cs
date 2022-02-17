using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Singleton;

namespace DAL
{
    public sealed partial class DalObject : Singleton.Singleton<DalObject>, IDal
    {
        /// <summary>
        /// constructor
        /// </summary>
        private DalObject()
        {
            DataSource.Initialize();
        }

        public double[] GetElectricityUse()
        {
            return (new double[5]{
                DataSource.Config.free,
                 DataSource.Config.CarriesLightWeigh,
                  DataSource.Config.CarriesMediumWeigh,
                   DataSource.Config.CarriesHeavyWeight,
                    DataSource.Config.ChargingRate
                  });
        }
    }
}
