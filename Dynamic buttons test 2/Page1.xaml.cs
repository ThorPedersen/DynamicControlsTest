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

namespace Dynamic_buttons_test_2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>

    public partial class Page1
    {
        public ObservableCollection<ComboBoxIdentity> ComboBoxIdentityList = new ObservableCollection<ComboBoxIdentity>();
        public ObservableCollection<LabelIdentity> LabelIdentityList = new ObservableCollection<LabelIdentity>();

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
            //the third is the content of the label, and the fourth is the panel row 
            TextboxAndLabelSetup("ComputerName", "Some computername", "Computer Name", false);
            TextboxAndLabelSetup("NetworkName", "Some networkname", "Network Name", false);

            //Items for the combobox
            List<ComboBoxOption> bindingList = new List<ComboBoxOption>();
            List<UIElementCollection> comboBoxChildren = new List<UIElementCollection>();
            BindingListAdding("0", "", "", bindingList, "");
            BindingListAdding("1", "Test1", "Test1", bindingList, "");
            BindingListAdding("2", "Test2", "Test2", bindingList, "");
            BindingListAdding("3", "Test3", "Test3", bindingList, "");
            //first parameter is name for combobox and label, second is content of the label, third is the combobox items
            //fourth is true if it is a child, fifth is unused ID
            ComboBoxAndLabelSetup("Oldtool", "Old tool", bindingList, false, "1111");

            //Items for the combobox
            List<ComboBoxOption> bindingList2 = new List<ComboBoxOption>();
            BindingListAdding("4", "", "", bindingList2, "");
            BindingListAdding("5", "Testafter1", "Testafter1", bindingList2, "1");
            BindingListAdding("6", "Testafter2", "Testafter2", bindingList2, "");
            BindingListAdding("7", "Testafter3", "Testafter3", bindingList2, "");
            //first parameter is name for combobox and label, second is content of the label, third is the combobox items
            //fourth is true if it is a child, fifth is unused ID
            ComboBoxAndLabelSetup("Newtool", "New tool", bindingList2, true, "2222");

            //foreach (var box in ComboBoxIdentityList)
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

            LabelIdentityList.Add(new LabelIdentity { Label = labelName });

            if (child)
            {
                labelName.Visibility = Visibility.Collapsed;
            }

            return labelName;
        }
        private ComboBox AddComboBox(string id, List<ComboBoxOption> bindingList, bool child, string name )
        {
            ComboBox comboBox = new ComboBox
            {
                Name = name,
                ItemsSource = bindingList,
                DisplayMemberPath = "DisplayName",
                SelectedValuePath = "Value",
                SelectedIndex = 0
            };
            ComboBoxIdentityList.Add(new ComboBoxIdentity { Id = id, ComboBox = comboBox, ComboBoxChildren = new List<UIElement>() });
            if (child)
            {
                
                comboBox.Visibility = Visibility.Collapsed;
                comboBox.SelectionChanged += comboChild_SelectionChanged;
            }
            else
            {
                comboBox.SelectionChanged += combo_SelectionChanged;
            }

            return comboBox;
        }
        private void BindingListAdding(string id, string displayName, string value, List<ComboBoxOption> bindingList, string parentId)
        {
            bindingList.Add(new ComboBoxOption { Id=id, DisplayName = displayName, Value = value, ParentId = parentId });
        }

        private void ComboBoxAndLabelSetup(string name, string labelContent, List<ComboBoxOption> items, bool childOrNOrmal, string id)
        {
            if (childOrNOrmal)
            {
                AddRow(AddLabel("lbl" + name, labelContent, childOrNOrmal));
                AddRow(AddComboBox(id, items, childOrNOrmal, "CB" + name));
            }
            else
            {
                AddRow(AddLabel("lbl" + name, labelContent, childOrNOrmal));
                AddRow(AddComboBox(id, items, childOrNOrmal, "CB" + name));
            }
            
            _gridRow++;
        }

        private void comboChild_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox currentComboBox = (ComboBox)sender;
            //string value = (string)currentComboBox.SelectedValue;
            ComboBoxOption comboBoxOption = (ComboBoxOption) currentComboBox.SelectedItem;
            ComboBoxIdentity claus = ComboBoxIdentityList.First(x => x.ComboBox == currentComboBox);

            foreach (UIElement thor in claus.ComboBoxChildren)
            {
                thor.Visibility = Visibility.Collapsed;
            }
            claus.ComboBoxChildren.Clear();

            ComboBoxIdentity ex = null;

            foreach (ComboBoxIdentity boxIdentity in ComboBoxIdentityList)
            {
                //if (boxIdentity.ParentOptionId == claus2.Id)
                //{
                //    ex = boxIdentity;
                //}
                if (boxIdentity.ComboBox.ItemsSource.Cast<ComboBoxOption>().Any(x => x.ParentId == comboBoxOption.Id))
                {
                    ex = boxIdentity;
                }
                if (ex != null)
                {
                    UIElement box = ex.ComboBox;
                    box.Visibility = Visibility.Visible;

                    string name = ex.ComboBox.Name;
                    var removed = name.Remove(0, 2);
                    var results = LabelIdentityList.First(x => x.Label.Name == "lbl" + removed);

                    UIElement label = results.Label;
                    label.Visibility = Visibility.Visible;

                    claus.ComboBoxChildren.Add(box);
                    claus.ComboBoxChildren.Add(label);


                    break;
                }
                //If combobox changes selected value then childboxes with parenId's that are equal to that value must become visible.collapsed
                
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
