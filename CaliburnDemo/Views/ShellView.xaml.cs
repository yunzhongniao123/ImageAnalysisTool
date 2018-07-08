using System;
using MahApps.Metro.Controls;
using System.Windows.Navigation;

namespace ImageToolDemo.Views
{
    public partial class ShellView:MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void OpenImageAlgorithm_Click(object sender, System.Windows.RoutedEventArgs e)
        {            
            ImageAlgorithmView imageAlgorithmView = new ImageAlgorithmView();
            imageAlgorithmView.Show();
        }
    }
}
