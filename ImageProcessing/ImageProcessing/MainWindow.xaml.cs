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
using ImageProcessing.ImageProcessingLib;
using ImageProcessing.ImageResources;

namespace ImageProcessing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PilotDraw(imgCanvas0, imgCanvas1);

        }

        public void PilotDraw(System.Windows.Controls.Image imgCanvas0, System.Windows.Controls.Image imgCanvas1)
        {
            string jpg1 = System.IO.Path.Combine(MyPaths.Folder_Resource_FullPath, ImageResourcesLib.Penguins_1);

            BitmapImage bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            bi.UriSource = new Uri(jpg1, UriKind.RelativeOrAbsolute);
            bi.EndInit();

            imgCanvas0.Source = bi;

            byte[,] GrayArray = (bi as BitmapSource).ToGrayArray();

            var NewImageSource = BasicTransform.GrayArrayToGrayBitmap(GrayArray);
            imgCanvas1.Source = NewImageSource;

            int threshold;
            var NewImageSource2 = Binarize.ToBinaryBitmap(bi, BinarizationMethods.Iterative, out threshold);
            imgCanvas1.Source = NewImageSource2;


        }
    }
}
