using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace VidSwitch.Converter
{
    class AddZeroNoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string[] array = value as string[];
            string[] ret = new string[array.Length + 1];

            ret[0] = "(none)";
            for( int i=0; i < array.Length; i++ )
            {
                ret[i + 1] = array[i];
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
