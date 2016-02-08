using Dynamic_buttons_test_2.Models;
using System;
using System.CodeDom;
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
using Dynamic_buttons_test_2.localhost;

namespace Dynamic_buttons_test_2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class Page1
    {

        public ObservableCollection<ComboBoxIdentity> ComboBoxIdentityList = new ObservableCollection<ComboBoxIdentity>();
        public ObservableCollection<LabelIdentity> LabelIdentityList = new ObservableCollection<LabelIdentity>();
        public ObservableCollection<TextBoxIdentity> TextBoxIdentityList = new ObservableCollection<TextBoxIdentity>();

        private int _gridRow;
        private readonly int _gridColumnLabel;
        private readonly int _gridColumnObject;

        public Page1()
        {
            InitializeComponent();

            _gridRow = 2;
            _gridColumnLabel = 1;
            _gridColumnObject = 2;

            // First parameter is name for textbox and label, second is the content of the textbox
            //the third is the content of the label, and the fourth is a child if it is true , fifth is its ID
            TextboxAndLabelSetup("8", "ComputerName", "Some computername", "Computer Name", false, new Guid().ToString(), "");
            TextboxAndLabelSetup("9", "NetworkName", "Some networkname", "Network Name", false, new Guid().ToString(), "");

            //Items for the combobox
            List<ComboBoxOption> bindingList = new List<ComboBoxOption>();
            //First parameter is Id, second is Display name, third is value, fourth is list of items to add and fifth is parentId for an option.
            BindingListAdding("0", "", "", bindingList, "");
            BindingListAdding("1", "Test1", "Test1", bindingList, "");
            BindingListAdding("2", "Test2", "Test2", bindingList, "");
            BindingListAdding("3", "Test3", "Test3", bindingList, "");
            //first parameter is label id, second is name for combobox and label, thirds is content of the label, fourth is the combobox items
            //fifth is true if it is a child, sixth is comboBox ID, seventh is box parentId
            ComboBoxAndLabelSetup("1", "Oldtool", "Old tool", bindingList, false, "1111", "");

            //Items for the combobox
            List<ComboBoxOption> bindingList2 = new List<ComboBoxOption>();
            BindingListAdding("4", "", "", bindingList2, "");
            BindingListAdding("5", "Testafter1", "Testafter1", bindingList2, "1");
            BindingListAdding("6", "Testafter2", "Testafter2", bindingList2, "");
            BindingListAdding("7", "Testafter3", "Testafter3", bindingList2, "");
            //first parameter is label id, second is name for combobox and label, thirds is content of the label, fourth is the combobox items
            //fifth is true if it is a child, sixth is comboBox ID, seventh is box parentId
            ComboBoxAndLabelSetup("2", "Newtool", "New tool", bindingList2, true, "2222", "1111");

            //Items for the combobox
            List<ComboBoxOption> bindingList3 = new List<ComboBoxOption>();
            BindingListAdding("8", "", "", bindingList3, "");
            BindingListAdding("9", "LastTest1", "Last Test 1", bindingList3, "6");
            BindingListAdding("10", "LastTest2", "Last Test 2", bindingList3, "");
            //first parameter is label id, second is name for combobox and label, thirds is content of the label, fourth is the combobox items
            //fifth is true if it is a child, sixth is comboBox ID, seventh is box parentId
            ComboBoxAndLabelSetup("3", "SidsteTest", "SidsteTest", bindingList3, true, "3333", "2222");

            TextboxAndLabelSetup("55", "Tooltext", "Some Tooltext", "Some Tooltext", true, "3", "3333");

            //foreach (var box in ComboBoxIdentityList)
            //{
            //    MessageBox.Show("Id: " + box.Id);
            //}
        }
        private TextBox AddTextBox(string id, string name, string content, bool child, string parentId)
        {
            var wb = new WebService();


            TextBox standardName = new TextBox
            {
                Name = name,
                Text = content
            };
            TextBoxIdentity textBoxIdentity = new TextBoxIdentity
            {
                Id = id,
                TextBox = standardName,
                TextBoxChildren = new List<UIElement>()
            };
            if (child)
            {
                standardName.Visibility = Visibility.Collapsed;

                foreach (var boxIdentity in ComboBoxIdentityList)
                {
                    if (boxIdentity.Id == parentId)
                    {
                        boxIdentity.ComboBoxChildren.Add(standardName);
                        textBoxIdentity.ParentId = parentId;
                    }
                }
            }
            //objservice.AddTextBox(id, name, content, child, parentId);
            wb.AddTextBox(id, name, content, child, parentId);

            return standardName;
        }
        private Label AddLabel(string id, string name, string content, bool child)
        {
            var wb = new WebService();

            wb.AddLabel(id, name, content, child);

            Label labelName = new Label
            {
                Name = name,
                Content = content
            };

            LabelIdentityList.Add(new LabelIdentity { Id = id, Label = labelName });

            if (child)
            {
                labelName.Visibility = Visibility.Collapsed;
            }
            return labelName;
        }
        private ComboBox AddComboBox(string id, List<ComboBoxOption> bindingList, bool child, string name, string parentId)
        {
            var wb = new WebService();

            //wb.AddComboBox(id, bindingList, child, name, parentId);

            ComboBox comboBox = new ComboBox
            {
                Name = name,
                ItemsSource = bindingList,
                DisplayMemberPath = "DisplayName",
                SelectedValuePath = "Value",
                SelectedIndex = 0
            };
            ComboBoxIdentity combo = new ComboBoxIdentity
            {
                Id = id,
                ComboBox = comboBox,
                ComboBoxChildren = new List<UIElement>()
            };
            //ComboBoxIdentityList.Add(Combo);
            //ComboBoxIdentityList.Add(new ComboBoxIdentity { Id = id, ComboBox = comboBox, ComboBoxChildren = new List<UIElement>() });
            if (child)
            {
                comboBox.Visibility = Visibility.Collapsed;
                comboBox.SelectionChanged += combo_SelectionChanged;

                foreach (var comboBoxIdentity in ComboBoxIdentityList)
                {
                    if (comboBoxIdentity.Id == parentId)
                    {
                        comboBoxIdentity.ComboBoxChildren.Add(comboBox);
                        combo.ParentId = parentId;
                    }
                }
            }
            else
            {
                //ComboBoxIdentityList.Add(new ComboBoxIdentity { Id = id, ComboBox = comboBox, ComboBoxChildren = new List<UIElement>(), ParentId = ""});
                comboBox.SelectionChanged += combo_SelectionChanged;
            }

            ComboBoxIdentityList.Add(combo);

            return comboBox;
        }
        private void BindingListAdding(string id, string displayName, string value, List<ComboBoxOption> bindingList, string parentId)
        {
            bindingList.Add(new ComboBoxOption { Id = id, DisplayName = displayName, Value = value, OptionParentId = parentId });
        }

        private void ComboBoxAndLabelSetup(string labelId, string name, string labelContent, List<ComboBoxOption> items, bool childOrNOrmal, string comboBoxId, string parentId)
        {
            AddRow(AddLabel(labelId, "lbl" + name, labelContent, childOrNOrmal));
            AddRow(AddComboBox(comboBoxId, items, childOrNOrmal, "CB" + name, parentId));

            _gridRow++;
        }

        private void comboChild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox currentComboBox = (ComboBox)sender;
            //string value = (string)currentComboBox.SelectedValue;
            ComboBoxOption comboBoxOption = (ComboBoxOption)currentComboBox.SelectedItem;
            ComboBoxIdentity comboBoxIdentity = ComboBoxIdentityList.First(x => Equals(x.ComboBox, currentComboBox));

            foreach (UIElement element in comboBoxIdentity.ComboBoxChildren)
            {
                element.Visibility = Visibility.Collapsed;
            }

            comboBoxIdentity.ComboBoxChildren.Clear();

            ComboBoxIdentity identity = null;

            foreach (ComboBoxIdentity boxIdentity in ComboBoxIdentityList)
            {
                //if (boxIdentity.ComboBoxChildren == claus2.Id)
                //{
                //    identity = boxIdentity;
                //}
                //if (comboBoxIdentity.ComboBoxChildren.Contains(boxIdentity.ComboBox) && comboBoxIdentity.ParentId == boxIdentity.Id)
                //{
                //    identity = boxIdentity;
                //}
                // && boxIdentity.ComboBox.ItemsSource.Cast<ComboBoxOption>().Any(x => x.OptionParentId == comboBoxOption.Id
                if (comboBoxIdentity.Id == boxIdentity.ParentId && (boxIdentity.ComboBox.ItemsSource.Cast<ComboBoxOption>().Any(x => x.OptionParentId == comboBoxOption.Id)))
                {
                    identity = boxIdentity;
                }

                if (boxIdentity.ComboBox.IsVisible == false)
                {
                    foreach (var element in boxIdentity.ComboBoxChildren)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }
                }
                if (identity != null)
                {
                    UIElement box = identity.ComboBox;
                    box.Visibility = Visibility.Visible;

                    identity.ComboBox.SelectedIndex = 0;

                    string name = identity.ComboBox.Name;
                    var removed = name.Remove(0, 2);
                    var results = LabelIdentityList.First(x => x.Label.Name == "lbl" + removed);

                    UIElement label = results.Label;
                    label.Visibility = Visibility.Visible;

                    comboBoxIdentity.ComboBoxChildren.Add(box);
                    comboBoxIdentity.ComboBoxChildren.Add(label);

                    break;
                }
            }
        }

        private void TextboxAndLabelSetup(string labelId, string name, string textboxContent, string labelContent, bool child, string textboxId, string parentId)
        {
            AddRow(AddLabel(labelId, "lbl" + name, labelContent, child));
            AddRow(AddTextBox(textboxId, "TxtBox" + name, textboxContent, child, parentId));
            _gridRow++;
        }
        private void AddRow(object gridControl)
        {
            RowDefinition newRow = new RowDefinition
            {
                Height = GridLength.Auto,
                MinHeight = 40
            };
            GridToSave.RowDefinitions.Add(newRow);

            Grid.SetRow((UIElement)gridControl, _gridRow);
            var label = gridControl as Label;
            if (label != null)
            {
                Grid.SetColumn(label, _gridColumnLabel);
            }
            else
            {
                Grid.SetColumn((UIElement)gridControl, _gridColumnObject);
            }
            GridToSave.Children.Add((UIElement)gridControl);
        }
    }
}
