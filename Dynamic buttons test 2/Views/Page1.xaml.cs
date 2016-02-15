using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Dynamic_buttons_test_2.Models;

namespace Dynamic_buttons_test_2.Views
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
            //InitializeComponent();

            _gridRow = 2;
            _gridColumnLabel = 1;
            _gridColumnObject = 2;

            string parameter = string.Empty;
            //if (NavigationContext.QueryString.TryGetValue("parameter", out parameter))
            //{
            //    MessageBox.Show(parameter);
            //}

            // First parameter is ID for label, second is name for textbox and label, third is the content of the textbox
            //the fourth is the content of the label, the fifth is TextBoxId, sixth is its ParentID, seventh is its ParentOptionId
            TextboxAndLabelSetup("8", "ComputerName", "Some computername", "Computer Name", new Guid().ToString(), null, null);
            TextboxAndLabelSetup("9", "NetworkName", "Some networkname", "Network Name", new Guid().ToString(), null, null);

            //Items for the combobox
            List<ComboBoxOption> bindingList = new List<ComboBoxOption>();
            //First parameter is Id, second is Display name, third is value, fourth is list of items to add.
            BindingListAdding("0", "", "", bindingList);
            BindingListAdding("1", "Test1", "Test1", bindingList);
            BindingListAdding("2", "Test2", "Test2", bindingList);
            BindingListAdding("3", "Test3", "Test3", bindingList);
            //first parameter is label id, second is name for combobox and label, thirds is content of the label, fourth is the combobox items
            //fifth is its ComboboxId, sixth is parentId, seventh is ParentOptionId
            ComboBoxAndLabelSetup("1", "Oldtool", "Old tool", bindingList, "1111", null, null);

            //Items for the combobox
            List<ComboBoxOption> bindingList2 = new List<ComboBoxOption>();
            BindingListAdding("4", "", "", bindingList2);
            BindingListAdding("5", "Testafter1", "Testafter1", bindingList2);
            BindingListAdding("6", "Testafter2", "Testafter2", bindingList2);
            BindingListAdding("7", "Testafter3", "Testafter3", bindingList2);
            //first parameter is label id, second is name for combobox and label, thirds is content of the label, fourth is the combobox items
            //fifth is its ComboboxId, sixth is parentId, seventh is ParentOptionId
            ComboBoxAndLabelSetup("2", "Newtool", "New tool", bindingList2, "2222", "1111", "1");

            //Items for the combobox
            List<ComboBoxOption> bindingList3 = new List<ComboBoxOption>();
            BindingListAdding("8", "", "", bindingList3);
            BindingListAdding("9", "LastTest1", "Last Test 1", bindingList3);
            BindingListAdding("10", "LastTest2", "Last Test 2", bindingList3);
            //first parameter is label id, second is name for combobox and label, thirds is content of the label, fourth is the combobox items
            //fifth is its ComboboxId, sixth is parentId, seventh is ParentOptionId
            ComboBoxAndLabelSetup("3", "SidsteTest", "SidsteTest", bindingList3, "3333", "2222", "6");

            TextboxAndLabelSetup("55", "Tooltext", "Some Tooltext", "Some Tooltext", new Guid().ToString(), "3", "9");
            TextboxAndLabelSetup("99", "Fejlgrid", "Fejlgrid", "Fejlgrid", new Guid().ToString(), null, null);

        }
        protected void OnNavigatedTo(NavigationEventArgs e)
        {
            IDictionary<string, string> parameters = NavigationC
        }
        private void BindingListAdding(string id, string displayName, string value, List<ComboBoxOption> bindingList)
        {
            bindingList.Add(new ComboBoxOption { Id = id, DisplayName = displayName, Value = value });
            
        }
        private TextBox AddTextBox(string id, string name, string content, string parentId, string parentOptionId)
        {
            //var wb = new WebService();

            TextBox standardName = new TextBox
            {
                Name = name,
                Text = content
            };
            TextBoxIdentity textBoxIdentity = new TextBoxIdentity
            {
                Id = id,
                TextBox = standardName,
                TextBoxChildren = new List<UIElement>(),
                ParentOptionId = parentOptionId
            };

            if (parentId != null)
            {
                standardName.Visibility = Visibility.Collapsed;

                foreach (var boxIdentity in ComboBoxIdentityList)
                {
                    if (boxIdentity.Id == parentId)
                    {
                        boxIdentity.ComboBoxChildren.Add(standardName);
                    }
                }
            }
            TextBoxIdentityList.Add(textBoxIdentity);
            //objservice.AddTextBox(id, name, content, child, parentId);
            //wb.AddTextBox(id, name, content, child, parentId);

            return standardName;
        }
        private Label AddLabel(string id, string name, string content, string parentId, string parentOptionId)
        {
            //var wb = new WebService();
            //wb.AddLabel(id, name, content, child);

            Label labelName = new Label
            {
                Name = name,
                Content = content,
            };

            LabelIdentity labelIdentity = new LabelIdentity
            {
                Id = id,
                Label = labelName,
                ParentOptionId = parentOptionId
            };

            if (parentId != null)
            {
                labelName.Visibility = Visibility.Collapsed;

                foreach (var boxIdentity in ComboBoxIdentityList)
                {
                    if (boxIdentity.Id == parentId)
                    {
                        boxIdentity.ComboBoxChildren.Add(labelName);
                    }
                }
            }
            LabelIdentityList.Add(labelIdentity);

            return labelName;
        }
        private ComboBox AddComboBox(string id, List<ComboBoxOption> bindingList, string name, string parentId, string parentOptionId)
        {
            //var wb = new WebService();
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
                ComboBoxChildren = new List<UIElement>(),
                ParentId = parentId,
                ParentOptionId = parentOptionId
            };
            if (parentId != null)
            {
                comboBox.Visibility = Visibility.Collapsed;
             
                ComboBoxIdentity comboBoxIdentity = ComboBoxIdentityList.First(x => x.Id == parentId);
                comboBoxIdentity.ComboBoxChildren.Add(comboBox);
            }

            comboBox.SelectionChanged += combo_SelectionChanged;
            ComboBoxIdentityList.Add(combo);

            return comboBox;
        }

        private void ComboBoxAndLabelSetup(string labelId, string name, string labelContent, List<ComboBoxOption> items, string comboBoxId, string parentId, string parentOptionId)
        {
            AddRow(AddLabel(labelId, "lbl" + name, labelContent, parentId, parentOptionId));
            AddRow(AddComboBox(comboBoxId, items, "CB" + name, parentId, parentOptionId));

            _gridRow++;
        }
        private void TextboxAndLabelSetup(string labelId, string name, string textboxContent, string labelContent, string textboxId, string parentId, string parentOptionId)
        {
            AddRow(AddLabel(labelId, "lbl" + name, labelContent, parentId, parentOptionId));
            AddRow(AddTextBox(textboxId, "TxtBox" + name, textboxContent, parentId, parentOptionId));
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
        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox currentComboBox = (ComboBox)sender;
            ComboBoxOption comboBoxOption = (ComboBoxOption)currentComboBox.SelectedItem;

            ComboBoxIdentity comboBoxIdentity = ComboBoxIdentityList.First(x => Equals(x.ComboBox, currentComboBox));

            foreach (UIElement element in comboBoxIdentity.ComboBoxChildren)
            {
                element.Visibility = Visibility.Collapsed;
            }

            foreach (TextBoxIdentity textBoxIdentity in TextBoxIdentityList)
            {
                if (comboBoxOption.Id == textBoxIdentity.ParentOptionId)
                {
                    UIElement box = textBoxIdentity.TextBox;
                    box.Visibility = Visibility.Visible;

                    string name = textBoxIdentity.TextBox.Name;
                    var removed = name.Remove(0, 6);
                    var results = LabelIdentityList.First(x => x.Label.Name == "lbl" + removed);

                    UIElement label = results.Label;
                    label.Visibility = Visibility.Visible;

                    comboBoxIdentity.ComboBoxChildren.Add(box);
                    comboBoxIdentity.ComboBoxChildren.Add(label);

                    break;
                }
            }
            foreach (ComboBoxIdentity boxIdentity in ComboBoxIdentityList)
            {
                if (comboBoxOption.Id == boxIdentity.ParentOptionId)
                {
                    UIElement box = boxIdentity.ComboBox;
                    box.Visibility = Visibility.Visible;

                    boxIdentity.ComboBox.SelectedIndex = 0;

                    string name = boxIdentity.ComboBox.Name;
                    var removed = name.Remove(0, 2);
                    var results = LabelIdentityList.First(x => x.Label.Name == "lbl" + removed);

                    UIElement label = results.Label;
                    label.Visibility = Visibility.Visible;

                    comboBoxIdentity.ComboBoxChildren.Add(box);
                    comboBoxIdentity.ComboBoxChildren.Add(label);

                    break;
                }
                if (boxIdentity.ComboBox.IsVisible == false)
                {
                    foreach (var element in boxIdentity.ComboBoxChildren)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        //private void ButtonXmlSave_OnClick(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Xml was saved. Or that is, it would be had the code been written for it");
        //}
    }
}
