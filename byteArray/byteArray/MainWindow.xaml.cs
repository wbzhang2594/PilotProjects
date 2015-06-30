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

namespace byteArray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            byte[] arr1 = new byte[10] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            byte[] arr2 = new byte[arr1.Length];
            int startFrom = 2;
            Array.Copy(arr1, startFrom, arr2, startFrom, arr1.Length - startFrom);

            byte mask = 1;
            byte value_source = 0x39;
            var value_target = value_source & (~mask);
            var value_target2 = value_target ^ mask;

            System.Console.WriteLine(arr2.ToString());
        }
    }
}
