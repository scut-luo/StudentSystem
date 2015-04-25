using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Media.Imaging;
using System.Windows.Data;
using System.IO;

namespace ManagementSystem
{
    public class ConvertFunction
    {
        static public byte[] ConvertImageToBinary(object value)
        {
            if (value == null)
                return null;
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
