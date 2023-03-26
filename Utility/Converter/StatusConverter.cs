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
    [ValueConversion(typeof(int?),typeof(string))]
    class StatusConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? status = (int?)value;
            if (status == Constant.BORROWING_VALUE)
                return Constant.BORROWING_STATUS;
            else if (status == Constant.RETURNED_VALUE)
                return Constant.RETURNED_STATUS;
            else if (status == Constant.OVERDUE_VALUE)
                return Constant.OVERDUE_STATUS;
            else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
