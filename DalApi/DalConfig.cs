﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DalApi
{
    class DalConfig
    {
        internal static string DalName;
        internal static Dictionary<string, string> DalPackages;
        static DalConfig()
        {
            XElement dalConfig = XElement.Load($@"{Directory.GetCurrentDirectory()}\..\..\..\..\dal-config.xml");
            DalName = dalConfig.Element("dal").Value;
            DalPackages = (from pkg in dalConfig.Element("dal-packages").Elements()
                           select pkg
                          ).ToDictionary(p => "" + p.Name, p => p.Value);
        }
    }
}
