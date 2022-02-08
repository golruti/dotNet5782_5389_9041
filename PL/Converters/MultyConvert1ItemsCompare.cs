using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Converters
{
    public class MultyConvert1ItemsCompare : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //if (values != null)
            //{
            //    if (values[0].ToString() != values[1].ToString() && string.IsNullOrEmpty(values[0].ToString()))
            //    {
            //        return true;
            //    }
            //}
            //return false;
            return true;
        }

        
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
