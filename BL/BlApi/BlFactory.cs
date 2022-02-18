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
        /// <summary>
        /// Loads the BL variable
        /// </summary>
        /// <returns>Variable IBL interface type</returns>
        public static IBL GetBl()
        {
            try
            {
                return BL.BL.Instance;
            }
            catch (BO.BLConfigException e)
            {
                throw new BO.BLConfigException(e.Message, e);
            }
            catch (System.TypeInitializationException e)
            {
                throw new System.TypeInitializationException(e.Message, e);
            }
        }
    }
}