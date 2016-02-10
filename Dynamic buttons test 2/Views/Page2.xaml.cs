using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Dynamic_buttons_test_2.Models;
using Dynamic_buttons_test_2.Views;

namespace Dynamic_buttons_test_2.Views
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2
    {
        public ObservableCollection<ComboBox> ComboboxList = new ObservableCollection<ComboBox>();
        public ObservableCollection<ComboBoxIdentity> ComboBoxIdentityList = new ObservableCollection<ComboBoxIdentity>();
        public ObservableCollection<ComboBoxOption> ComboBoxOptionsList = new ObservableCollection<ComboBoxOption>();
        public ObservableCollection<LabelIdentity> LabelIdentityList = new ObservableCollection<LabelIdentity>();
        public ObservableCollection<TextBoxIdentity> TextBoxIdentityList = new ObservableCollection<TextBoxIdentity>();

        TestUserControl _testUserControl;

        private int _gridRow;
        private readonly int _gridColumnLabel;
        private readonly int _gridColumnObject;

        private readonly int _editorRow;
        private readonly int _editorColumm;

        public bool Editor;

        public Page2()
        {
            Editor = false;
            _testUserControl = new TestUserControl(ComboBoxOptionsList, ComboBoxIdentityList);

            InitializeComponent();

            _editorRow = 4;
            _editorColumm = 0;

            _gridRow = 2;
            _gridColumnLabel = 1;
            _gridColumnObject = 2;

            //ObservableCollection<ComboBoxOption> bindingList2 = new ObservableCollection<ComboBoxOption>();
            //BindingListAdding("4", "", "", bindingList2);
            //BindingListAdding("5", "Testafter1", "Testafter1", bindingList2);
            //BindingListAdding("6", "Testafter2", "Testafter2", bindingList2);
            //BindingListAdding("7", "Testafter3", "Testafter3", bindingList2);
            ////first parameter is label id, second is name for combobox and label, thirds is content of the label, fourth is the combobox items
            ////fifth is its ComboboxId, sixth is parentId, seventh is ParentOptionId
            //ComboBoxAndLabelSetup("2", "Newtool", "New tool", bindingList2, "2222", null, null);
        }

        private void Needbindingsomething()
        {
            _testUserControl = new TestUserControl(ComboBoxOptionsList, ComboBoxIdentityList);

            //if (_testUserControl.BtnAddCombobox.IsPressed)
            //{

            foreach (var var in _testUserControl.ListBox.ItemsSource)
            {
                ComboBoxOption newComboOption = new ComboBoxOption();
                newComboOption.Id = new Guid().ToString();
                newComboOption.DisplayName = var.ToString();
                newComboOption.Value = new Guid().ToString();

                ComboBoxOptionsList.Add(newComboOption);
            }
            ComboBoxAndLabelSetup(new Guid().ToString(), "lbl" + _testUserControl.LblComboBoxName.Name, _testUserControl.LblComboBoxName.Name, ComboBoxOptionsList, new Guid().ToString(), _testUserControl.CbParents.SelectedValue.ToString(), _testUserControl.CbParentOptions.SelectedValue.ToString());
            //}
        }
        private void BindingListAdding(string id, string displayName, string value, ObservableCollection<ComboBoxOption> bindingList)
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
        private ComboBox AddComboBox(string id, ObservableCollection<ComboBoxOption> bindingList, string name, string parentId, string parentOptionId)
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
            ComboboxList.Add(comboBox);
            return comboBox;
        }

        public void ComboBoxAndLabelSetup(string labelId, string name, string labelContent, ObservableCollection<ComboBoxOption> items, string comboBoxId, string parentId, string parentOptionId)
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
                    //identity = boxIdentity;
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
                    //identity = boxIdentity;
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

        private void ComboBoxUserControl_OnClick(object sender, RoutedEventArgs e)
        {        
            if (GridToSave.Children.Contains(_testUserControl))
            {
                GridToSave.Children.Remove(_testUserControl);
            }
            else
            {
                TestUserControl testUserControl = new TestUserControl(ComboBoxOptionsList, ComboBoxIdentityList);
                _testUserControl = testUserControl;

                Grid.SetRow(_testUserControl, _editorRow);
                Grid.SetColumnSpan(_testUserControl, 2);
                Grid.SetColumn(_testUserControl, _editorColumm);

                _testUserControl.Margin = new Thickness(10, 20, 0, 0);

                GridToSave.Children.Add(_testUserControl);
            }
        }
        //private void TextBoxUserControl_OnClick(object sender, RoutedEventArgs e)
        //{
        //    if (GridToSave.Children.Contains(_test2UserControl))
        //    {
        //        GridToSave.Children.Remove(_test2UserControl);
        //    }
        //    else
        //    {
        //        Grid.SetRow(_test2UserControl, _editorRow);
        //        Grid.SetColumnSpan(_test2UserControl, 2);
        //        Grid.SetColumn(_test2UserControl, _editorColumm);

        //        _test2UserControl.Margin = new Thickness(10, 20, 0, 0);

        //        GridToSave.Children.Add(_test2UserControl);
        //    }
        //}
    }
}
