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
        Button Btt = new Button();
        Grid OsSP = new Grid();

        public void LoadXamlMethod()
        {
            string fileName = "testfile.xaml";

            try
            {
                UIElement rootElement;
                FileStream s = new FileStream(fileName, FileMode.Open);
                rootElement = (UIElement)XamlReader.Load(s);
                s.Close();

                GridToSave.Children.Add(rootElement);

            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }

            InitializeComponent();
        }

        private void LoadClickEvents()
        {
            //BtnOs.Click += BtnOs_Click;
            //BtnComputerName.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnComputerName_Click));
            //BtnVersions.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnVersions_Click));
            //BtnSave.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnSaveXaml_Click));

            // Button button  = (Button)this.TryFindResource("BtnWindows99");

            Btt = (Button)this.FindName("BtnOs");
            OsSP = (Grid)this.FindName("OsStackPanel");

            MessageBox.Show(Btt.Name);
            MessageBox.Show(OsSP.Name);
            //var ButtonnOs = TryFindResource("BtnWindows7") as Button;
            //if (ButtonnOs == null) { throw new Exception("Button not found"); }
            //ButtonnOs.Click += BtnOs_Click;

            //Button buttonnOs = LayoutRoot.FindName("BtnOs") as Button;
            //MessageBox.Show(buttonnOs.Name);
            //if (buttonnOs == null) { throw new Exception("Button not found"); }
            //buttonnOs.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(BtnOs_Click));

            //Btt.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnOs3_click));
            Btt.Click += BtnOs_Click;
            //BtnWindows8.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnOs2_click));
            //BtnWindows10.AddHandler(Button.ClickEvent, new RoutedEventHandler(btnOs1_click));

            //BtnVersion1.AddHandler(Button.ClickEvent, new RoutedEventHandler(Version1_click));
            //BtnVersion2.AddHandler(Button.ClickEvent, new RoutedEventHandler(Version2_click));
            //BtnVersion3.AddHandler(Button.ClickEvent, new RoutedEventHandler(Version3_click));


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
            name.Background = Equals(name.Background, Brushes.Gray) ? Brushes.Gainsboro : Brushes.Gray;
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
            BackgroundChange(Btt);
        }
        private void BtnSaveXaml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string mystrXaml = XamlWriter.Save(GridToSave);
                FileStream fs = File.Create("testfile.xaml");
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(mystrXaml);
                sw.Close();
                fs.Close();
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.ToString());
            }
            finally
            {
                MessageBox.Show("Xaml saved.");
            }
        }

        private void Load_events_click(object sender, RoutedEventArgs e)
        {
            LoadClickEvents();
        }
    }
}
