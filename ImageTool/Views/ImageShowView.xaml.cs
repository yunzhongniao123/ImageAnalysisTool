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
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

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

        #region Privates
        private Mat _srcImage;
        private int _srcWidth;
        private int _srcHeight;
        private byte[] _srcArrayR;
        private byte[] _srcArrayG;
        private byte[] _srcArrayB;
        


        #endregion

        #region Publics

        #endregion

        #region 用于鼠标拖动图片
        System.Windows.Point _mousePosition;
        double _horizontalOffset = 1;
        double _verticalOffset = 1;

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

        //private void ImageScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    var scrollViewer = sender as ScrollViewer;
        //    scrollViewer.ScrollToHorizontalOffset(_horizontalOffset - e.Delta);
        //    scrollViewer.ScrollToVerticalOffset(_verticalOffset - e.Delta);
        //}
        #endregion

        #region 图片操作
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
                ImageShow.Source = new BitmapImage(new Uri(filePath));
                _srcImage = CvInvoke.Imread(filePath, ImreadModes.Color);                
                _srcWidth = _srcImage.Width;
                _srcHeight = _srcImage.Height;

                //注意下面两句很有用，没有无法缩放
                ImageShow.Width = _srcWidth;
                ImageShow.Height = _srcHeight;
                using (Mat channelR = new Mat(_srcImage.Rows, _srcImage.Cols, DepthType.Cv8U, 1))
                {
                    CvInvoke.ExtractChannel(_srcImage, channelR, 2);
                    _srcArrayR = channelR.GetByteArray();
                }
                using (Mat channelG = new Mat(_srcImage.Rows, _srcImage.Cols, DepthType.Cv8U, 1))
                {
                    CvInvoke.ExtractChannel(_srcImage, channelG, 1);
                    _srcArrayG = channelG.GetByteArray();
                }
                using (Mat channelB = new Mat(_srcImage.Rows, _srcImage.Cols, DepthType.Cv8U, 1))
                {
                    CvInvoke.ExtractChannel(_srcImage, channelB, 0);
                    _srcArrayB = channelB.GetByteArray();
                }

                //TODO

            }
        }

        private void ImageShow_MouseMove(object sender, MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(ImageShow);
            var imageActualWidth = ImageShow.ActualWidth;
            var imageActualHeight = ImageShow.ActualHeight;
            GetMousePositionValue(mousePosition, imageActualWidth, imageActualHeight);

        }

        private void GetMousePositionValue(Point mousePosition, double imageActualWidth, double imageActualHeight)
        {
            var realX = _srcWidth * Convert.ToInt32(mousePosition.X) / Convert.ToInt32(imageActualWidth);
            var realY = _srcHeight * Convert.ToInt32(mousePosition.Y) / Convert.ToInt32(imageActualHeight);
            var s1 = _srcArrayB.Count();
            if ((realY * _srcWidth + realX) >= _srcWidth * _srcHeight)
                return;
            var r = _srcArrayR[realY * _srcWidth + realX];
            var g = _srcArrayG[realY * _srcWidth + realX];
            var b = _srcArrayB[realY * _srcWidth + realX];
            RGBLabel.Content = $"R：{Convert.ToString(r)}  G：{Convert.ToString(g)}  B： {Convert.ToString(b)} ";
            XYLabel.Content = $"X：{realX}  Y： {realY} ";
        }

        private void ImageShow_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var horizontalOffset = ImageScrollViewer.HorizontalOffset;
            var verticalOffset = ImageScrollViewer.VerticalOffset;
            if (e.Delta > 0)
            {
                double delta = 1.1;
                ImageShow.Width *= delta;
                ImageShow.Height *= delta;
                ImageScrollViewer.ScrollToHorizontalOffset(horizontalOffset * delta);
                ImageScrollViewer.ScrollToVerticalOffset(verticalOffset * delta);
            }
            else
            {
                if (e.Delta < 0)
                {
                    double delta = 0.9;
                    ImageShow.Width *= delta;
                    ImageShow.Height *= delta;
                    ImageScrollViewer.ScrollToHorizontalOffset(horizontalOffset * delta);
                    ImageScrollViewer.ScrollToVerticalOffset(verticalOffset * delta);
                }

            }
        }

        private void ImageShow_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ImageShow_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        #endregion
    }
}
