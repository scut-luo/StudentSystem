using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
using System.Windows.Data;

namespace ManagementSystem
{
    //将代表Sex的数值（0和1）转换成字符串（"男"和"女"）
    // 0 = "男"
    // 1 = "女"
    [System.Windows.Data.ValueConversion(typeof(int),typeof(string))]
    public class SexConverter : IValueConverter
    {
        //由int -> string 
        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sex = "NULL";

            switch ((int)value)
            {
                case 0:
                    sex = "男";
                    break;

                case 1:
                    sex = "女";
                    break;
            }
            return sex;
        }

        //由string -> int              
        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            int sex = 0;

            switch ((string)value)
            {
                case "男":
                    sex = 0;
                    break;

                case "女":
                    sex = 1;
                    break;
            }
            return sex;
        }
    }
}
