﻿using LibraryManagementSystem.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace LibraryManagementSystem.Utility.Converter
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    class TextReasonVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int source = (int)value;
            if (source == Constant.REJECTED_VALUE || source == Constant.PENDING_VALUE)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
