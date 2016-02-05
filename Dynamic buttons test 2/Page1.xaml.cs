using Dynamic_buttons_test_2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

        public ObservableCollection<ComboBoxIdentity> ComboExList = new ObservableCollection<ComboBoxIdentity>();
        public ObservableCollection<LabelIdentity> LabelsList = new ObservableCollection<LabelIdentity>();

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
            TextboxAndLabelSetup("ComputerName", "Some computername", "Computer Name", false);
            TextboxAndLabelSetup("NetworkName", "Some networkname", "Network Name", false);

            //Items for the combobox
            List<ComboBoxOption> bindingList = new List<ComboBoxOption>();
            BindingListAdding("", "", bindingList, "");
            BindingListAdding("Test1", "Test1", bindingList, "");
            BindingListAdding("Test2", "Test2", bindingList, "");
            BindingListAdding("Test3", "Test3", bindingList, "");
            //first parameter is name for combobox and label, second is content of the label, third is the combobox items
            //fourth is true if it is a child, fifth is unused ID
            ComboBoxAndLabelSetup("Oldtool", "Old tool", bindingList, false, "1111");

            //Items for the combobox
            List<ComboBoxOption> bindingList2 = new List<ComboBoxOption>();
            BindingListAdding("", "", bindingList2, "");
            BindingListAdding("Testafter1", "Testafter1", bindingList2, "Test1");
            BindingListAdding("Testafter2", "Testafter2", bindingList2, "");
            BindingListAdding("Testafter3", "Testafter3", bindingList2, "");
            //first parameter is name for combobox and label, second is content of the label, third is the combobox items
            //fourth is true if it is a child, fifth is unused ID
            ComboBoxAndLabelSetup("Newtool", "New tool", bindingList2, true, "2222");

            //foreach (var box in ComboExList)
            //{
            //    MessageBox.Show("Id: " + box.Id);
            //}
        }
        private TextBox AddTextBox(string name, string content)
        {
            TextBox standardName = new TextBox
            {
                Name = name,
                Text = content
            };

            return standardName;
        }
        private Label AddLabel(string name, string content, bool child)
        {
            Label labelName = new Label
            {
                Name = name,
                Content = content
            };

            LabelsList.Add(new LabelIdentity { Label = labelName });

            if (child)
            {
                labelName.Visibility = Visibility.Collapsed;
            }

            return labelName;
        }
        private ComboBox AddComboBox(string id, List<ComboBoxOption> bindingList, bool child, string name)
        {
            ComboBox test = new ComboBox
            {
                Name = name,
                ItemsSource = bindingList,
                DisplayMemberPath = "DisplayName",
                SelectedValuePath = "Value",
                SelectedIndex = 0
            };

            ComboExList.Add(new ComboBoxIdentity { Id = id, ComboBox = test });
            if (child)
            {
                test.Visibility = Visibility.Collapsed;
                test.SelectionChanged += comboChild_SelectionChanged;
            }
            else
            {
                test.SelectionChanged += combo_SelectionChanged;
            }

            return test;
        }
        private void BindingListAdding(string displayName, string value, List<ComboBoxOption> bindingList, string parentId)
        {
            bindingList.Add(new ComboBoxOption { DisplayName = displayName, Value = value, ParentId = parentId });
        }

        private void ComboBoxAndLabelSetup(string name, string labelContent, List<ComboBoxOption> items, bool childOrNOrmal, string id)
        {
            AddRow(AddLabel("lbl" + name, labelContent, childOrNOrmal));
            AddRow(AddComboBox(id, items, childOrNOrmal, "CB" + name));
            _gridRow++;
        }

        private void comboChild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox currentComboBox = (ComboBox)sender;
            string value = (string)currentComboBox.SelectedValue;

            ComboBoxIdentity ex = null;

            foreach (ComboBoxIdentity boxIdentity in ComboExList)
            {
                if (boxIdentity.ComboBox.ItemsSource.Cast<ComboBoxOption>().Any(x => x.ParentId == value))
                {
                    ex = boxIdentity;
                }
                if (ex != null)
                {
                    UIElement box = ex.ComboBox;
                    box.Visibility = Visibility.Visible;

                    string name = ex.ComboBox.Name;
                    var removed = name.Remove(0, 2);
                    var results = LabelsList.First(x => x.Label.Name == "lbl" + removed);

                    UIElement label = results.Label;
                    label.Visibility = Visibility.Visible;

                    break;
                }
            }         
        }

        private void TextboxAndLabelSetup(string name, string textboxContent, string labelContent, bool child)
        {
            AddRow(AddLabel("lbl" + name, labelContent, child));
            AddRow(AddTextBox("TxtBox" + name, textboxContent));
            _gridRow++;
        }
        private void AddRow(object gridControl)
        {
            RowDefinition newRow = new RowDefinition();
            //newRow.Height = new GridLength(40);
            newRow.Height = GridLength.Auto;
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
            //if a combobox has a parentId, and its parent's selected.value is null or empty, then it must be visible.collapsed
            //else it is visible.visible.
            //OR
            //if a combobox has no parentID and its selected.value is null or empty then all its children must be visible.collapsed
            //if they are all visible.visible.

        }
    }
}
