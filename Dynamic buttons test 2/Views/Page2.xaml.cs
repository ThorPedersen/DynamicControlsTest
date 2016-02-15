using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Dynamic_buttons_test_2.Models;

namespace Dynamic_buttons_test_2.Views
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2
    {
        public ObservableCollection<ComboBox> ComboboxList = new ObservableCollection<ComboBox>();
        public ObservableCollection<Button> ButtonList = new ObservableCollection<Button>();
        public ObservableCollection<ComboBoxIdentity> ComboBoxIdentityList = new ObservableCollection<ComboBoxIdentity>();
        public ObservableCollection<ComboBoxOption> ComboBoxOptionsList = new ObservableCollection<ComboBoxOption>();
        public ObservableCollection<LabelIdentity> LabelIdentityList = new ObservableCollection<LabelIdentity>();
        public ObservableCollection<TextBoxIdentity> TextBoxIdentityList = new ObservableCollection<TextBoxIdentity>();
        public ObservableCollection<DockPanelIdentity> DockPanelIdentityList = new ObservableCollection<DockPanelIdentity>();
        //public ObservableCollection<ButtonIdentity> ButtonIdentityList = new ObservableCollection<ButtonIdentity>();

        public ObservableCollection<ConfigurationIdentity> ConfigurationIdentityList = new ObservableCollection<ConfigurationIdentity>();

        private ComboBoxUserControl _comboBoxUserControl;
        private TextBoxUserControl _textBoxUserControl;


        private readonly int _dockpanelRow;
        private readonly int _dockPanelColumn;

        private readonly int _editorRow;
        private readonly int _editorColumm;

        public Page2()
        {
            _dockpanelRow = 4;
            _dockPanelColumn = 1;

            _comboBoxUserControl = new ComboBoxUserControl(ComboBoxOptionsList, ComboBoxIdentityList);
            _textBoxUserControl = new TextBoxUserControl(ComboBoxOptionsList, ComboBoxIdentityList);

            InitializeComponent();

            _editorRow = 4;
            _editorColumm = 0;

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
                dockPanel.Visibility = Visibility.Collapsed;

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
        private ComboBox AddComboBox(string id, ObservableCollection<ComboBoxOption> bindingList, string name, string parentId, string parentOptionId, Panel dockPanel)
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
                comboBox.Visibility = Visibility.Collapsed;

                ComboBoxIdentity comboBoxIdentity = ComboBoxIdentityList.First(x => x.Id == parentId);
                comboBoxIdentity.ComboBoxChildren.Add(comboBox);
                dockPanel.Visibility = Visibility.Collapsed;
            }

            comboBox.SelectionChanged += combo_SelectionChanged;
            ComboBoxIdentityList.Add(combo);
            DockPanelIdentityList.Add(panel);
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
            Button newButton = new Button
            {
                Name = "btn" + name,
                Content = "Delete",
                Margin = new Thickness(0, 0, 20, 0),
                Uid = newDockPanel.Uid
            };
            newButton.Click += btnDelete_Click;

            AddDockPanel(AddLabel(labelId, "lbl" + name, labelContent, parentId, parentOptionId, newDockPanel), newDockPanel);
            AddDockPanel(AddComboBox(comboBoxId, items, "CB" + name, parentId, parentOptionId, newDockPanel), newDockPanel);
            AddDockPanel(newButton, newDockPanel);

            ConfigurationIdentity configurationIdentity = new ConfigurationIdentity
            {
                LabelId = labelId,
                TextBoxOrComputerName = name,
                LabelContent = labelContent,
                Items = items,
                ComboBoxId = comboBoxId,
                ParentId = parentId,
                ParentOptionId = parentOptionId
            };
            ConfigurationIdentityList.Add(configurationIdentity);
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
                Margin = new Thickness(0, 0, 20, 0),
                Uid = newDockPanel.Uid
            };
            newButton.Click += btnDelete_Click;

            AddDockPanel(AddLabel(labelId, "lbl" + name, labelContent, parentId, parentOptionId, newDockPanel), newDockPanel);
            AddDockPanel(AddTextBox(textboxId, "TxtBox" + name, textboxContent, parentId, parentOptionId, newDockPanel), newDockPanel);
            AddDockPanel(newButton, newDockPanel);

            ConfigurationIdentity configurationIdentity = new ConfigurationIdentity
            {
                LabelId = labelId,
                TextBoxOrComputerName = name,
                TextBoxContent = textboxContent,
                LabelContent = labelContent,
                TextboxId = textboxId,
                ParentId = parentId,
                ParentOptionId = parentOptionId
            };
            ConfigurationIdentityList.Add(configurationIdentity);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            //ButtonIdentity buttonIdentity = new ButtonIdentity
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Button = button
            //};
            //ButtonIdentityList.Add(buttonIdentity);

            ButtonList.Add(button);

            var panelIdentity = DockPanelIdentityList.First(x => Equals(x.Id, button.Uid));
            Panel panel = panelIdentity.Panel;

            ComboBoxIdentity comboBoxIdentity = null;
            if (ComboBoxIdentityList.Any(x => x.ParentPanelId == button.Uid))
            {
                comboBoxIdentity = ComboBoxIdentityList.First(x => Equals(x.ParentPanelId, button.Uid));

                if (comboBoxIdentity.ComboBoxChildren.Count > 0)
                {
                    //smart algorythm to delete all children within children within children ad infinitum until all children are removed.
                    //List<UIElement> list = comboBoxIdentity.ComboBoxChildren;
                    //foreach (var l in list)
                    //{

                    //}
                }
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

            SpContent.Children.Remove(panel);
        }
        private void AddDockPanel(UIElement gridControl, DockPanel newDockPanel)
        {
            if (!SpContent.Children.Contains(newDockPanel))
            {
                Grid.SetRow(newDockPanel, _dockpanelRow);
                Grid.SetColumn(newDockPanel, _dockPanelColumn);
                SpContent.Children.Add(newDockPanel);
            }
            if (gridControl.GetType() == typeof(Label))
            {
                DockPanel.SetDock(gridControl, Dock.Left);
            }
            newDockPanel.Children.Add(gridControl);
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
            //foreach (var dockPanelIdentity in DockPanelIdentityList)
            //{
            //    if (dockPanelIdentity.Panel.IsVisible == false)
            //    {
            //        dockPanelIdentity.Panel.Visibility = Visibility.Collapsed;
            //    }
            //    else
            //    {
            //        dockPanelIdentity.Panel.Visibility = Visibility.Visible;
            //    }
            //}
            foreach (TextBoxIdentity textBoxIdentity in TextBoxIdentityList)
            {
                var variableForPanel = DockPanelIdentityList.First(x => x.Id == textBoxIdentity.ParentPanelId);
                Panel panel = variableForPanel.Panel;
                panel.Visibility = Visibility.Visible;

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
                }
                if (textBoxIdentity.TextBox.IsVisible == false)
                {
                    panel.Visibility = Visibility.Collapsed;
                }
            }
            foreach (ComboBoxIdentity boxIdentity in ComboBoxIdentityList)
            {
                var variableForPanel = DockPanelIdentityList.First(x => x.Id == boxIdentity.ParentPanelId);
                Panel panel = variableForPanel.Panel;
                panel.Visibility = Visibility.Visible;

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
                }
                if (boxIdentity.ComboBox.IsVisible == false)
                {
                    foreach (var element in boxIdentity.ComboBoxChildren)
                    {
                        element.Visibility = Visibility.Collapsed;
                    }
                    panel.Visibility = Visibility.Collapsed;
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
                TextBoxUserControl textBoxUserControl = new TextBoxUserControl(ComboBoxOptionsList, ComboBoxIdentityList);
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

        private void BtnConfiguration_OnClick(object sender, RoutedEventArgs e)
        {
            //this.NavigationService?.Navigate(new Uri("Views/Page1.xaml?parameter=test", UriKind.Relative));
            Debug.Assert(NavigationService != null, "NavigationService != null");
            NavigationService.Navigate(new Page1(ConfigurationIdentityList));
        }
    }
}
