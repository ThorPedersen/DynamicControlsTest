using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dynamic_buttons_test_2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void stackVisibility(Grid name)
        {
            name.Visibility = name.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void BtnOs_Click(object sender, RoutedEventArgs e)
        {
            stackVisibility(OsStackPanel);
        }
        private void BtnComputerName_Click(object sender, RoutedEventArgs e)
        {
            stackVisibility(ComputerNameStackPanel);
        }
        private void BtnVersions_Click(object sender, RoutedEventArgs e)
        {
            stackVisibility(VersionStackPanel);
        }

        private void BackgroundChange(Button name)
        {
            name.Background = name.Background == Brushes.Gray ? Brushes.Gainsboro : Brushes.Gray;
        }

        private void Version1_click(object sender, RoutedEventArgs e)
        {
            BackgroundChange(BtnVersion1);
        }
        private void Version2_click(object sender, RoutedEventArgs e)
        {
            BackgroundChange(BtnVersion2);
        }
        private void Version3_click(object sender, RoutedEventArgs e)
        {
            BackgroundChange(BtnVersion3);
        }

        private void btnOs1_click(object sender, RoutedEventArgs e)
        {
            BackgroundChange(BtnWindows10);
        }

        private void btnOs2_click(object sender, RoutedEventArgs e)
        {
            BackgroundChange(BtnWindows8);
        }

        private void btnOs3_click(object sender, RoutedEventArgs e)
        {
            BackgroundChange(BtnWindows7);
        }

        private void BtnSaveXaml_Click(object sender, RoutedEventArgs e)
        {


        }
    }
}
