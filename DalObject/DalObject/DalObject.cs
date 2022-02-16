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

        //User IDal.GetCUser(Predicate<User> predicate, DO.Enum.Access access)
        //{
        //    throw new NotImplementedException();
        //}

        //User IDal.GetUser(int userId, string password, DO.Enum.Access access)
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<User> IDal.GetUsers()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerable<User> IDal.GetUsers(Predicate<User> predicate)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
