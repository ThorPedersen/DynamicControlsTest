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

        public ObservableCollection<StackPanel> StackPanelList = new ObservableCollection<StackPanel>();

        //public ObservableCollection<ComboBox> ComboBoxList = new ObservableCollection<ComboBox>();
        public ObservableCollection<ComboEx> ComboboxesList = new ObservableCollection<ComboEx>();
        public ObservableCollection<LabelEx> LabelsList = new ObservableCollection<LabelEx>();

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
            //string[] comboboxItems = new ComboEx { "", "Tool 1", "Tool 2", "Tool 3", "Tool 4" };
            //first parameter is name for combobox and label, second is content of the label, third is the panel row
            //and the fourth is the items in the box, from the string array above
            //ComboBoxAndLabelSetup("Newtool", "New tool", comboboxItems);

            //Items for the combobox
            //string[] comboboxItems2 = { "", "Old Tool 1", "Old Tool 2", "Old Tool 3" };
            List<Options> bindingList = new List<Options>();
            BindingListAdding("Test1", "Test1", bindingList, "");
            BindingListAdding("Test2", "Test2", bindingList, "");
            BindingListAdding("Test3", "Test3", bindingList, "");
            //first parameter is name for combobox and label, second is content of the label, third is the combobox items, fourth is true if it is a child
            //and the fourth is the items in the box, from the string array above
            ComboBoxAndLabelSetup("Oldtool", "Old tool", bindingList, false, "1111");

            //Items for the combobox
            //string[] comboboxItems3 = { "", "Child for old tool 1", "Child for old tool 2", "Child for old tool 3" };
            List<Options> bindingList2 = new List<Options>();
            BindingListAdding("Testafter1", "Testafter1", bindingList2, "Test1");
            BindingListAdding("Testafter2", "Testafter2", bindingList2, "");
            BindingListAdding("Testafter3", "Testafter3", bindingList2, "");
            //first parameter is name for combobox and label, second is content of the label, third is the combobox items, fourth is true if it is a child
            //and the fourth is the items in the box, from the string array above
            //ChildComboBoxAndLabelSetup("Oldchildtool", "Old child tool", comboboxItems3, true);
            ComboBoxAndLabelSetup("Newtool", "New tool", bindingList2, true, "2222");

            foreach (var box in ComboboxesList)
            {
                MessageBox.Show("Id: " + box.Id);
            }
        }
        private TextBox TxtboxSetup(string name, string content)
        {
            TextBox standardName = new TextBox
            {
                Name = name,
                Text = content
            };

            return standardName;
        }
        private Label LabelSetup(string name, string content, bool child)
        {
            Label labelName = new Label
            {
                Name = name,
                Content = content
            };

            LabelsList.Add(new LabelEx { Label = labelName });

            if (child)
            {
                labelName.Visibility = Visibility.Collapsed;
            }

            return labelName;
        }
        //private ComboBox ComboBoxSetup(string name, string[] items)
        //{
        //    ComboBox standardName = new ComboBox
        //    {
        //        Name = name,
        //        ItemsSource = items
        //    };

        //    ComboEx combo = new ComboEx();
        //    string guid = Guid.NewGuid().ToString();
        //    combo.Combo = standardName;
        //    combo.Id = guid;

        //    ComboboxesList.Add(combo);
        //    ComboBoxList.Add(standardName);

        //    standardName.SelectionChanged += combo_SelectionChanged;

        //    return standardName;
        //}
        private ComboBox CBnormal(string id, List<Options> bindingList, bool child, string name)
        {
            ComboBox test = new ComboBox();
            test.Name = name;
            test.ItemsSource = bindingList;
            test.DisplayMemberPath = "DisplayName";
            test.SelectedValuePath = "Value";

            ComboboxesList.Add(new ComboEx { Id = id, Combo = test });
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
        private void BindingListAdding(string displayName, string value, List<Options> BindingList, string parentId)
        {
            BindingList.Add(new Options { DisplayName = displayName, Value = value, ParentId = parentId });
        }

        private void ComboBoxAndLabelSetup(string name, string labelContent, List<Options> items, bool childOrNOrmal, string Id)
        {
            AddRow(LabelSetup("lbl" + name, labelContent, childOrNOrmal));
            AddRow(CBnormal(Id, items, childOrNOrmal, "CB" + name));
            _gridRow++;
        }
        //private void ChildComboBoxAndLabelSetup(string name, string labelContent, List<Options> items, bool childOrNOrmal, string Id)
        //{
        //    //List<Options> bindingList = new List<Options>();
        //    //bindingList.Add(new Options { DisplayName = "lort1", Value = "Testa1", ParentId = "Test1" });

        //    //bindingList.Add(new Options { DisplayName = "lort2", Value = "Testa2", ParentId = "" });

        //    //bindingList.Add(new Options { DisplayName = "lort3", Value = "Testa3", ParentId = "" });

        //    AddRow(LabelSetup("lbl" + name, labelContent));
        //    AddRow(CBnormal(Id, items, childOrNOrmal));
        //    _gridRow++;
        //}
        //string name, string[] items, ComboBox comboParent, object parentItem
        //private ComboBox ComboBoxChildSetup()
        //{
        //    //ComboBox standardName = new ComboBox
        //    //{
        //    //    Name = name,
        //    //    ItemsSource = items,
        //    //};
        //    //ComboEx combo = new ComboEx();
        //    //Guid guid = Guid.NewGuid();
        //    //combo.Combo = standardName;
        //    //combo.Id = guid;

        //    //ComboboxesList.Add(combo);
        //    //ComboBoxList.Add(standardName);

        //    return standardName;
        //}
        void comboChild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBox curCombo = (ComboBox)sender;
            //Options curOptions = (Options)curCombo.SelectedItem;

            //ComboEx test = ComboboxesList.First(x => x.Id == curOptions.ParentId);

            //UIElement test2 = test.Combo;

            //test2.Visibility = Visibility.Visible;
        }
        void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ComboBox curCombo = (ComboBox)sender;
            //Options curOptions = (Options)curCombo.SelectedItem;


            ComboBox curCombo = (ComboBox)sender;
            string value = (string) curCombo.SelectedValue;

            ComboEx test = null;

            foreach (ComboEx claus in ComboboxesList)
            {
                if (claus.Combo.ItemsSource.Cast<Options>().Any(thor => thor.ParentId == value))
                {
                    test = claus;
                    //UIElement test2 = test.Combo;
                    //test2.Visibility = Visibility.Collapsed;
                }
                if (test != null)
                {
                    UIElement test2 = test.Combo;
                    test2.Visibility = Visibility.Visible;

                    string comboname = test.Combo.Name;
                    var removed = comboname.Remove(0, 2);
                    var results = LabelsList.First(x => x.Label.Name == "lbl" + removed);
                    UIElement test3 = results.Label;
                    test3.Visibility = Visibility.Visible;

                    break;
                }
            }

            //Get the sender ID
            //Search comboboxeslist for box with parentId == ID

            //ComboEx test = ComboboxesList.First(x => x.Id == curOptions.ParentId);

            //if (test?.Combo != null)
            //{
            //    UIElement test2 = test.Combo;
            //    test2.Visibility = Visibility.Visible;
            //}
        }
        //private void ComboBoxAndLabelSetup(string name, string labelContent, string[] items)
        //{
        //    AddRow(LabelSetup("lbl" + name, labelContent));
        //    AddRow(ComboBoxSetup("ComboBox" + name, items));
        //    _gridRow++;
        //}
        ////Needs fixing
        //private void ChildComboBoxAndLabelSetup(string name, string labelContent, string[] items)
        //{
        //    AddRow(LabelSetup("lbl" + name, labelContent));
        //    //"ComboBox" + name, items
        //    AddRow(ComboBoxChildSetup());
        //    _gridRow++;
        //}

        private void TxtboxAndLabelSetup(string name, string textboxContent, string labelContent, int row)
        {
            AddRow(LabelSetup("lbl" + name, labelContent, false));
            AddRow(TxtboxSetup("TxtBox" + name, textboxContent));
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
