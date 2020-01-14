using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace wpf_treeviews_and_converters
{
    /// <summary>
    /// Converts a full path to a specific image type of a drive, folder or file
    /// </summary>
    /// 
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Full path
            var path = (string)value;

            if (path == null)
                return null;


            var name = MainWindow.GetFileFolderName(path);

            var image = "images/file.png";

            if (string.IsNullOrEmpty(name))
            {
                image = "images/drive.png";
            } else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
            {
                image = "images/folder-closed.png";
            }
           

            return new BitmapImage(new Uri($"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
