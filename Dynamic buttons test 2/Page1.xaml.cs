using Dynamic_buttons_test_2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dynamic_buttons_test_2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1
    {
        public ObservableCollection<string> OperationSystemsList = new ObservableCollection<string>();
        public ObservableCollection<string> ToolsSystemsList = new ObservableCollection<string>();

        readonly OperatingSystemsModel _operatingSystemsModel = new OperatingSystemsModel();
        readonly OperatingSystemToolsModel _operatingSystemsModelTools = new OperatingSystemToolsModel();

        private readonly ComboBox _comboboxTools = new ComboBox();
        private readonly Label _lblTools = new Label();

        public Page1()
        {
            InitializeComponent();

            OperationSystemsList.Add("");
            OperationSystemsList.Add("Windows 7");
            OperationSystemsList.Add("Windows 8");
            OperationSystemsList.Add("Windows best");
            CbOperatingSystem.ItemsSource = OperationSystemsList;
            _comboboxTools.SelectionChanged += _comboboxTools_SelectionChanged;
        }

        private void ToolsContainer(string windowsVersion)
        {
            if (ToolsSystemsList != null)
            {
                ToolsSystemsList.Clear();
                ToolsSystemsList.Add("Windows " + windowsVersion + " stuff");
                ToolsSystemsList.Add("Windows " + windowsVersion + " stuff 2");
                ToolsSystemsList.Add("Windows " + windowsVersion + " stuff 3");
            }

            _comboboxTools.Name = "CbTools";
            _comboboxTools.ItemsSource = ToolsSystemsList;

            _comboboxTools.SelectedIndex = 0;

            _lblTools.Content = "Tools";

            if (!BoxGrid.Children.Contains(_comboboxTools))
            {
                this.BoxGrid.Children.Add(_comboboxTools);
                this.LabelGrid.Children.Add(_lblTools);
            }
        }
        private void _comboboxTools_SelectionChanged(object sender, EventArgs e)
        {

            if (_comboboxTools.SelectedIndex != -1)
            { 
            //ComboBox test = (ComboBox)sender;
            //_operatingSystemsModelTools.OperativeSystemTools = test.SelectedValue.ToString();
            _operatingSystemsModelTools.OperativeSystemTools = _comboboxTools.SelectedValue.ToString();

            MessageBox.Show("OperationsToolModel: " + _operatingSystemsModelTools.OperativeSystemTools);
            }


        }

        private void CbOperatingSystem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _operatingSystemsModel.OperativeSystem = CbOperatingSystem.SelectedValue.ToString();

            MessageBox.Show("OperationsModel: " + _operatingSystemsModel.OperativeSystem);

            if (_operatingSystemsModel.OperativeSystem == "Windows 7")
            {
                ToolsContainer("7");
            }
            if (_operatingSystemsModel.OperativeSystem == "Windows 8")
            {
                ToolsContainer("8");
            }
            if (_operatingSystemsModel.OperativeSystem == "Windows best")
            {
                ToolsContainer("best");
            }
            VisibilityForToolsContainer();
        }

        private void VisibilityForToolsContainer()
        {
            if (_operatingSystemsModel.OperativeSystem == "" || _operatingSystemsModel == null)
            {
                _lblTools.Visibility = Visibility.Collapsed;
                _comboboxTools.Visibility = Visibility.Collapsed;
            }
            else
            {
                _lblTools.Visibility = Visibility.Visible;
                _comboboxTools.Visibility = Visibility.Visible;
            }
        }
    }
}
