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
using System.Xml;

namespace Dynamic_buttons_test_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            
            InitializeComponent();
            LoadXamlMethod();
        }

        ///


        //public static readonly DependencyProperty HandlerProperty = DependencyProperty.RegisterAttached("Handler", typeof(DynamicEvent), typeof(DynamicEvent), new UIPropertyMetadata(OnHandlerChanged));
        //public static DynamicEvent GetHandler(DependencyObject obj)
        //{
        //    return (DynamicEvent)obj.GetValue(HandlerProperty);
        //}
        //public static void SetHandler(DependencyObject obj, bool value)
        //{
        //    obj.SetValue(HandlerProperty, value);
        //}
        //private static void OnHandlerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    UIElement uie = o as UIElement;
        //    if (uie == null)
        //        throw new Exception("Attempt to set EventHandler on non-UIElement Type");

        //    DynamicEvent oldHandler = e.OldValue as DynamicEvent;
        //    DynamicEvent newHandler = e.NewValue as DynamicEvent;

        //    // unhook the old event
        //    if (oldHandler != null)
        //        uie.RemoveHandler(oldHandler.RoutedEvent, new RoutedEventHandler(oldHandler.OnEventExecuted));

        //    // hook up the new
        //    if (newHandler != null)
        //        uie.AddHandler(newHandler.RoutedEvent, new RoutedEventHandler(newHandler.OnEventExecuted));

        //}





        ///
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
    }
}
