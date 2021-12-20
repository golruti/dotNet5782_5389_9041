using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BL;

namespace BlApi
{
    public static class BlFactory
    {
        public static IBL GetBl()
        {
            try
            {
                return BL.BL.Instance;
            }
            catch (BO.BLConfigException e)
            {
                throw e;
            }
            catch (System.TypeInitializationException e)
            {
                throw e;
            }
        }
    }
}