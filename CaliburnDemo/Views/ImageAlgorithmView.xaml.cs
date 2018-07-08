using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ImageToolDemo.Views
{
    /// <summary>
    /// ImageAlgorithm.xaml 的交互逻辑
    /// </summary>
    public partial class ImageAlgorithmView : MetroWindow
    {
        public ImageAlgorithmView()
        {
            InitializeComponent();
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "All Pics (*.*)|*.*|BMP (*.bmp)|*.bmp|JPG (*.jpg)|*.jpg"
            };
            openFileDialog.Title = "选择需要的图片";
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var filePath = openFileDialog.FileName;
                ImageAlgorithmRes.Source = new BitmapImage(new Uri(filePath));               

                //TODO

            }
        }



    }
}
