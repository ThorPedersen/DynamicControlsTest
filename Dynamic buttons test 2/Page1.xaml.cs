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

        public ObservableCollection<StackPanel> StackPanelList = new ObservableCollection<StackPanel>();

        readonly OperatingSystemsModel _operatingSystemsModel = new OperatingSystemsModel();
        readonly OperatingSystemToolsModel _operatingSystemsModelTools = new OperatingSystemToolsModel();

        private int _gridRow;
        private readonly int _gridColumnLabel;
        private readonly int _gridColumnObject;

        private readonly ComboBox _comboboxTools = new ComboBox();
        private readonly Label _lblTools = new Label();

            
        public Page1()
        {
            InitializeComponent();

            OperationSystemSetup();
            OperationSystemToolsSetup();
                
            _gridRow = 5;
            _gridColumnLabel = 1;
            _gridColumnObject = 2;

            // First parameter is name for textbox and label, second is the content of the textbox
            //the third is the content of the label, and the fourth is the panel row 
            TxtboxAndLabelSetup("ComputerName", "Some computername", "Computer Name", _gridRow);
            TxtboxAndLabelSetup("NetworkName", "Some networkname", "Network Name", _gridRow);

            //Items for the combobox
            string[] comboboxItems = { "Tool 1", "Tool 2", "Tool 3", "Tool 4" };
            //first parameter is name for combobox and label, second is content of the label, third is the panel row
            //and the fourth is the items in the box, from the string array above
            ComboBoxAndLabelSetup("Newtool", "New tool", comboboxItems);
        }

        private static TextBox TxtboxSetup(string name, string content)
        {
            TextBox standardName = new TextBox
            {
                Name = name,
                Text = content
            };

            return standardName;
        }
        private static Label LabelSetup(string name, string content)
        {
            Label labelName = new Label
            {
                Name = name,
                Content = content
            };
            return labelName;
        }

        private ComboBox ComboBoxSetup(string name, string[] items)
        {
            ComboBox standardName = new ComboBox
            {
                Name = name,
                ItemsSource = items
            };
            return standardName;
        }

        private void ComboBoxAndLabelSetup(string name, string labelContent, string[] items)
        {
            AddRow(LabelSetup("lbl" + name, labelContent));
            AddRow(ComboBoxSetup("ComboBox" + name, items));
            _gridRow++;
        }
        //private void ChildComboBoxAndLabelSetup(string name, string labelContent, string[] items, Object objectName, Object labelName)
        //{
        //    AddRow(LabelSetup("lbl" + name, labelContent));
        //    AddRow(ComboBoxSetup("ComboBox" + name, items));
        //    _gridRow++;
        //}

        private void TxtboxAndLabelSetup(string name, string textboxContent, string labelContent, int row)
        {
            AddRow(LabelSetup("lbl" + name, labelContent));
            AddRow(TxtboxSetup("TxtBox" + name, textboxContent));
            _gridRow++;
        }
        private void AddRow(object gridControl)
        {
            RowDefinition newRow = new RowDefinition(); 
            newRow.Height = new GridLength(40);
            GridToSave.RowDefinitions.Add(newRow);

            Grid.SetRow((UIElement)gridControl, _gridRow);
            if (gridControl is Label)
            {
                Grid.SetColumn((UIElement)gridControl, _gridColumnLabel);
            }
            else
            {
                Grid.SetColumn((UIElement)gridControl, _gridColumnObject);
            }
            GridToSave.Children.Add((UIElement)gridControl);
        }
        private void OperationSystemSetup()
        {
            OperationSystemsList.Add("");
            OperationSystemsList.Add("Windows 7");
            OperationSystemsList.Add("Windows 8");
            OperationSystemsList.Add("Windows best");
            CbOperatingSystem.ItemsSource = OperationSystemsList;
        }
        private void OperationSystemToolsSetup()
        {
            _comboboxTools.SelectionChanged += _comboboxTools_SelectionChanged;
        }
        private void ToolsContainer(string windowsVersion)
        {
            if (ToolsSystemsList != null)
            {
                ToolsSystemsList.Clear();
                if (windowsVersion != "")
                {
                    ToolsSystemsList.Add("Windows " + windowsVersion + " stuff");
                    ToolsSystemsList.Add("Windows " + windowsVersion + " stuff 2");
                    ToolsSystemsList.Add("Windows " + windowsVersion + " stuff 3");
                }
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

            if (_operatingSystemsModel.OperativeSystem != "")
            {
                MessageBox.Show("OperationsModel: " + _operatingSystemsModel.OperativeSystem);
            }
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
