using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Dynamic_buttons_test_2.Views;

namespace Dynamic_buttons_test_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Page2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page2());
            btnPage2.Background = Brushes.SkyBlue;
            btnPage1.Background = Brushes.Gainsboro;
        }

        private void Page1_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page1());
            btnPage2.Background = Brushes.Gainsboro;
            btnPage1.Background = Brushes.SkyBlue;
        }
    }
}
