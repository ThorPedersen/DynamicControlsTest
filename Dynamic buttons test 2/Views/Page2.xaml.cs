using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
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
        public ObservableCollection<DockPanelIdentity> DockPanelIdentityList = new ObservableCollection<DockPanelIdentity>();

        private ComboBoxUserControl _comboBoxUserControl;
        private TextBoxUserControl _textBoxUserControl;


        private readonly int _dockpanelRow;
        private readonly int _dockPanelColumn;

        private int _gridRow;
        private readonly int _gridColumnLabel;
        private readonly int _gridColumnObject;

        private readonly int _editorRow;
        private readonly int _editorColumm;

        public bool Editor;

        public Page2()
        {
            _dockpanelRow = 3;
            _dockPanelColumn = 1;

            Editor = false;
            _comboBoxUserControl = new ComboBoxUserControl(ComboBoxOptionsList, ComboBoxIdentityList);
            _textBoxUserControl = new TextBoxUserControl(ComboBoxOptionsList, ComboBoxIdentityList, TextBoxIdentityList);

            InitializeComponent();

            _editorRow = 3;
            _editorColumm = 0;

            //_gridRow = 2;
            //_gridColumnLabel = 2;
            //_gridColumnObject = 3;
        }
        private TextBox AddTextBox(string id, string name, string content, string parentId, string parentOptionId, DockPanel dockPanel)
        {
            //var wb = new WebService();

            TextBox standardName = new TextBox
            {
                Name = name,
                Text = content,
                MinWidth = 200,
                Margin = new Thickness(50, 0, 50, 0)
            };
            TextBoxIdentity textBoxIdentity = new TextBoxIdentity
            {
                Id = id,
                TextBox = standardName,
                TextBoxChildren = new List<UIElement>(),
                ParentOptionId = parentOptionId,
                ParentPanelId = dockPanel.Uid
            };
            DockPanelIdentity panel = new DockPanelIdentity
            {
                Id = dockPanel.Uid,
                Name = "Dp" + name,
                Panel = dockPanel
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
            DockPanelIdentityList.Add(panel);
            TextBoxIdentityList.Add(textBoxIdentity);
            //objservice.AddTextBox(id, name, content, child, parentId);
            //wb.AddTextBox(id, name, content, child, parentId);

            return standardName;
        }
        private Label AddLabel(string id, string name, string content, string parentId, string parentOptionId, DockPanel panel)
        {
            //var wb = new WebService();
            //wb.AddLabel(id, name, content, child);

            Label labelName = new Label
            {
                Name = name,
                Content = content,
                MinWidth = 200,
                Margin = new Thickness(50, 0, 0, 0)
            };

            LabelIdentity labelIdentity = new LabelIdentity
            {
                Id = id,
                Label = labelName,
                ParentOptionId = parentOptionId,
                ParentPanelId = panel.Uid
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
        private ComboBox AddComboBox(string id, ObservableCollection<ComboBoxOption> bindingList, string name, string parentId, string parentOptionId, Panel panel)
        {
            //var wb = new WebService();
            //wb.AddComboBox(id, bindingList, child, name, parentId);

            ComboBox comboBox = new ComboBox
            {
                Name = name,
                ItemsSource = bindingList,
                DisplayMemberPath = "DisplayName",
                SelectedValuePath = "Value",
                SelectedIndex = 0,
                MinWidth = 200,
                Margin = new Thickness(50, 0, 50, 0)
            };
            ComboBoxIdentity combo = new ComboBoxIdentity
            {
                Id = id,
                ComboBox = comboBox,
                ComboBoxChildren = new List<UIElement>(),
                ParentId = parentId,
                ParentOptionId = parentOptionId,
                ParentPanelId = panel.Uid
            };
            //DockPanelIdentity Panel = new DockPanelIdentity
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Dp" + name,
            //    Panel = dockPanel
            //};
            if (parentId != null)
            {
                comboBox.Visibility = Visibility.Collapsed;

                ComboBoxIdentity comboBoxIdentity = ComboBoxIdentityList.First(x => x.Id == parentId);
                comboBoxIdentity.ComboBoxChildren.Add(comboBox);
                //dockPanel.Visibility = Visibility.Collapsed;
            }

            comboBox.SelectionChanged += combo_SelectionChanged;
            ComboBoxIdentityList.Add(combo);
            //DockPanelIdentityList.Add(Panel);
            ComboboxList.Add(comboBox);
            return comboBox;
        }

        public void ComboBoxAndLabelSetup(string labelId, string name, string labelContent, ObservableCollection<ComboBoxOption> items, string comboBoxId, string parentId, string parentOptionId)
        {
            DockPanel newDockPanel = new DockPanel
            {
                Margin = new Thickness(0, 20, 0, 0),
                Name = "Dp" + name,
                Uid = Guid.NewGuid().ToString()
            };
            
            AddRow(AddLabel(labelId, "lbl" + name, labelContent, parentId, parentOptionId, newDockPanel), newDockPanel);
            AddRow(AddComboBox(comboBoxId, items, "CB" + name, parentId, parentOptionId, newDockPanel), newDockPanel);
            _gridRow++;
        }
        public void TextboxAndLabelSetup(string labelId, string name, string textboxContent, string labelContent, string textboxId, string parentId, string parentOptionId)
        {
            DockPanel newDockPanel = new DockPanel
            {
                Margin = new Thickness(0, 20, 0, 0),
                Name = "Dp" + name,
                Uid = Guid.NewGuid().ToString()
            };
            Button newButton = new Button
            {
                Name = "btn" + name,
                Content = "Delete",
                Margin = new Thickness(0,0,20,0),
                Uid = newDockPanel.Uid
            };
            newButton.Click += btnDelete_Click;

            AddRow(AddLabel(labelId, "lbl" + name, labelContent, parentId, parentOptionId, newDockPanel), newDockPanel);
            AddRow(AddTextBox(textboxId, "TxtBox" + name, textboxContent, parentId, parentOptionId, newDockPanel), newDockPanel);
            AddRow(newButton, newDockPanel);
            _gridRow++;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;

            var panelIdentity = DockPanelIdentityList.First(x => Equals(x.Id, button.Uid));
            Panel panel = panelIdentity.Panel;

            ComboBoxIdentity comboBoxIdentity = null;
            if (ComboBoxIdentityList.Any(x => x.ParentPanelId == button.Uid))
            {
                comboBoxIdentity = ComboBoxIdentityList.First(x => Equals(x.ParentPanelId, button.Uid));
            }
            LabelIdentity labelIdentity = null;
            if (LabelIdentityList.Any(x => x.ParentPanelId == button.Uid))
            {
                labelIdentity = LabelIdentityList.First(x => Equals(x.ParentPanelId, button.Uid));
            }
            TextBoxIdentity textboxIdentity = null;
            if (TextBoxIdentityList.Any(x => x.ParentPanelId == button.Uid))
            {
                textboxIdentity = TextBoxIdentityList.First(x => Equals(x.ParentPanelId, button.Uid));
            }

            if (comboBoxIdentity != null)
            {
                ComboBoxIdentityList.Remove(comboBoxIdentity);
            }
            if (labelIdentity != null)
            {
                LabelIdentityList.Remove(labelIdentity);
            }
            if (textboxIdentity != null)
            {
                TextBoxIdentityList.Remove(textboxIdentity);
            }

            panel.Children.Clear();
            
            StackPanelName.Children.Remove(panel);
        }
        private void AddRow(UIElement gridControl, DockPanel newDockPanel)
        {
            if (!StackPanelName.Children.Contains(newDockPanel))
            {
                Grid.SetRow(newDockPanel, _dockpanelRow);
                Grid.SetColumn(newDockPanel, _dockPanelColumn);
                StackPanelName.Children.Add(newDockPanel);
            }
            //var label = gridControl as Label;

            if (gridControl.GetType() == typeof (Label))
            {
                DockPanel.SetDock(gridControl, Dock.Left);              
            }
            newDockPanel.Children.Add(gridControl);


            //RowDefinition newRow = new RowDefinition
            //{
            //    Height = GridLength.Auto,
            //    MinHeight = 80
            //};
            //GridToSave.RowDefinitions.Add(newRow);

            //Grid.SetRow((UIElement)gridControl, _gridRow);
            //var label = gridControl as Label;
            //if (label != null)
            //{
            //    Grid.SetColumn(label, _gridColumnLabel);
            //}
            //else
            //{
            //    Grid.SetColumn((UIElement)gridControl, _gridColumnObject);
            //}
            //GridToSave.Children.Add((UIElement)gridControl);
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
                    var panelIdentity = DockPanelIdentityList.First(x => Equals(x.Id, textBoxIdentity.ParentPanelId));
                    Panel panel = panelIdentity.Panel;
                    
                    panel.Visibility = Visibility.Visible;

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

                    //break;
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

                    //break;
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
            if (GridToSave.Children.Contains(_comboBoxUserControl))
            {
                GridToSave.Children.Remove(_comboBoxUserControl);
            }

            else
            {
                ComboBoxUserControl comboBoxUserControl = new ComboBoxUserControl(ComboBoxOptionsList, ComboBoxIdentityList);
                _comboBoxUserControl = comboBoxUserControl;

                Grid.SetRow(_comboBoxUserControl, _editorRow);
                //Grid.SetColumnSpan(_comboBoxUserControl, 2);
                Grid.SetColumn(_comboBoxUserControl, _editorColumm);

                _comboBoxUserControl.Margin = new Thickness(0, 20, 0, 0);

                GridToSave.Children.Add(_comboBoxUserControl);
            }
            if (GridToSave.Children.Contains(_textBoxUserControl))
            {
                GridToSave.Children.Remove(_textBoxUserControl);
            }
        }
        private void ButtonTextBoxUserControl_Click(object sender, RoutedEventArgs e)
        {
            if (GridToSave.Children.Contains(_textBoxUserControl))
            {
                GridToSave.Children.Remove(_textBoxUserControl);
            }
            else
            {
                TextBoxUserControl textBoxUserControl = new TextBoxUserControl(ComboBoxOptionsList, ComboBoxIdentityList, TextBoxIdentityList);
                _textBoxUserControl = textBoxUserControl;

                Grid.SetRow(_textBoxUserControl, _editorRow);
                //Grid.SetColumnSpan(_textBoxUserControl, 2);
                Grid.SetColumn(_textBoxUserControl, _editorColumm);

                _textBoxUserControl.Margin = new Thickness(0, 20, 0, 0);

                GridToSave.Children.Add(_textBoxUserControl);
            }
            if (GridToSave.Children.Contains(_comboBoxUserControl))
            {
                GridToSave.Children.Remove(_comboBoxUserControl);
            }
        }
    }
}
