using IDAL.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public partial interface IDal
    {
        public bool uniqueIDTaxCheck<T>(IEnumerable<T> lst, int id);
        public double[] GetElectricityUse();
        public double[] RequestElectricity();
    }
}
