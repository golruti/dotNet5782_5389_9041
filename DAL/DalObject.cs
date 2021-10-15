using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public class DalObject
    {
        public DalObject()
        {
            DataSource.initialize();
        }


        //stations
        public void Insert(Station station)
        {
            DataSource.stations[DataSource.Config.indStation++] = station;
        }

        public void Update(Station station,int idxChangeStation)
        {
            DataSource.stations[idxChangeStation] = station;
        }

    }
}
