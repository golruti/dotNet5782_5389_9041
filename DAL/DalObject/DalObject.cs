using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDal
    {
        public DalObject()
        {
            DataSource.Initialize();
        }


        bool uniqueIDTaxCheck<T>(List<T> lst, int id)
        {
            foreach (var item in lst)
            {
                if ((int)item.GetType().GetProperty("id").GetValue(item, null) == id) 
                    return false;
            }
            return true;
        }


        public T GetById<T>(List<T> lst, int id) 
        {
            return lst.Find(item => (int)item.GetType().GetProperty("id").GetValue(item, null) == id);
        }



       



       



        
      




        public double[] DronePowerConsumptionRequest()
        {
            return (new double[5]{
                DataSource.Config.vacant,
                 DataSource.Config.CarriesLightWeigh,
                  DataSource.Config.CarriesMediumWeigh,
                   DataSource.Config.CarriesHeavyWeight,
                    DataSource.Config.ChargingRatel
                  });
        }
    }
}
