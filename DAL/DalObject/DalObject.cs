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
    public sealed partial class DalObject : Singleton.Singleton<DalObject>, DalApi.IDal
    {
        /// <summary>
        /// constructor
        /// </summary>
        private DalObject()
        {
            DataSource.Initialize();
        }

        /// <summary>
        /// The function checks if the ID is unique
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst"></param>
        /// <param name="id"></param>
        /// <returns>if the ID is unique</returns>
        private bool uniqueIDTaxCheck<T>(IEnumerable<T> lst, int id)
        {
            if (lst.Count() <= 0)
                return true;

            T temp = lst.FirstOrDefault(item => (int)item.GetType().GetProperty("Id")?.GetValue(item) == id);
            return temp.Equals(default(T));
        }


        /// <summary>
        /// Takes from the DataSource the electricity use data of the drone
        /// </summary>
        /// <returns>A array of electricity use</returns>
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
