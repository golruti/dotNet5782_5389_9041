using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];
            if (dalPkg == null) throw new DalConfigException($"Package {dalType} is not found in packages list in dal-config.xml");
            try
            {
                Assembly.LoadFrom($@"{Directory.GetCurrentDirectory()}\..\..\..\..\{dalPkg}\bin\Debug\net5.0\{dalPkg}.dll");
            }
            catch (Exception) { throw new DalConfigException("Failed to load the dal-config.xml file"); }

            Type type = Type.GetType($"DAL.{dalPkg}, {dalPkg}");
            if (type == null) throw new DalConfigException($"Class {dalPkg} was not found in the {dalPkg}.dll");

            IDal dal = (IDal)type.GetProperty("Instance",
                      BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy).GetValue(null);
            if (dal == null) throw new DalConfigException($"Class {dalPkg} is not a singleton or wrong propertry name for Instance");

            return dal;
        }
    }
}
