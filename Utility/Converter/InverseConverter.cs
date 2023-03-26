﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LibraryManagementSystem.Utility
{
    [ValueConversion(typeof(bool?),typeof(bool?))]
    class InverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool source = (bool)value;
            if (source)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool source = (bool)value;
            if(source) 
                return false;
            return true;
        }
    }
}
