using LibraryManagementSystem.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LibraryManagementSystem.Utility
{
    [ValueConversion(typeof(int),typeof(string))]
    class AcceptedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = (int)value;
            if (status == Constant.PENDING_VALUE)
                return Constant.PENDING_REQUEST;
            else if (status == Constant.APPROVED_VALUE)
                return Constant.APPROVED_REQUEST;
            else
                return Constant.REJECTED_REQUEST;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
