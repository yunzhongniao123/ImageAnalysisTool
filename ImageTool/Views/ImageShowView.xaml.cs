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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageTool.Views
{
    /// <summary>
    /// ImageShowView.xaml 的交互逻辑
    /// </summary>
    public partial class ImageShowView : UserControl
    {
        public ImageShowView()
        {
            InitializeComponent();
        }

        #region 用于鼠标拖动图片
        System.Windows.Point _mousePosition;
        double _horizontalOffset;
        double _verticalOffset;

        private void ImageScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            _mousePosition = e.GetPosition(scrollViewer);
            _horizontalOffset = scrollViewer.HorizontalOffset;
            _verticalOffset = scrollViewer.VerticalOffset;
            scrollViewer.CaptureMouse();
        }

        private void ImageScrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            scrollViewer.ReleaseMouseCapture();
        }

        private void ImageScrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var mousePositionNow = e.GetPosition(scrollViewer);
                scrollViewer.ScrollToHorizontalOffset(_horizontalOffset + (_mousePosition.X - mousePositionNow.X));
                scrollViewer.ScrollToVerticalOffset(_verticalOffset + (_mousePosition.Y - mousePositionNow.Y));
            }
        }

        private void ImageScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }
        #endregion

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "All Pics (*.*)|*.*|BMP (*.bmp)|*.bmp|JPG (*.jpg)|*.jpg"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                var filePath = openFileDialog.FileName;
                ImageShow.Source = new BitmapImage(new Uri(filePath));
                //TODO

            }
        }

        private void ImageShow_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void ImageShow_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }
    }
}
