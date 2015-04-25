using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.Globalization;
using System.IO;

namespace ManagementSystem
{
    [System.Windows.Data.ValueConversion(typeof(byte[]),typeof(BitmapImage))]
    class BinaryImageConverter : IValueConverter
    {
        public object Convert(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is byte[])		//Value是源对象，本例是Sphoto字段，Bytes[]类型
            {
                byte[] bytes = value as byte[];
                MemoryStream stream = new MemoryStream(bytes);
                BitmapImage image = new BitmapImage();
                image.CacheOption = BitmapCacheOption.OnLoad;	//Image设置完成后关闭流
                image.BeginInit();
                image.StreamSource = stream;					//从流得到BitmapImage类对象
                image.EndInit();
                return image;
            }
            return null;
        }

        public object ConvertBack(
            object value, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage image = (BitmapImage)value;	  //Value是目标对象，本例是BitmapImage类
            byte[] bytes = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                if (image is BitmapSource)
                {
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image));
                }
                encoder.QualityLevel = 100;	// JPEG 图像质量等级，从1(最低质量)到 100(最高质量)
                encoder.Save(memoryStream);
                bytes = memoryStream.ToArray();
            }
            return bytes;
        }
    }
}
