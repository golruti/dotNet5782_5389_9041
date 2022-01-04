
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.Threading.Tasks;  
using System.Windows.Data;  
  
namespace Converters
{
    public class AtLleastOneContentConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Count() >= 2)
            {
                if (!string.IsNullOrEmpty(values[0].ToString())&& values[0].ToString()!=values[2].ToString() || !string.IsNullOrEmpty(values[1].ToString())&&values[1].ToString() != values[3].ToString())
                    return true;
                else return false;
            }
            else
                return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}