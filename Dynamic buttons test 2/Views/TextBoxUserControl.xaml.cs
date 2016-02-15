using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dynamic_buttons_test_2.Models;

namespace Dynamic_buttons_test_2.Views
{
    /// <summary>
    /// Interaction logic for TextBoxUserControl.xaml
    /// </summary>
    public partial class TextBoxUserControl : UserControl
    {
        private readonly List<string> _listBoxes = new List<string>();
        private readonly ObservableCollection<ComboBoxOption> _listOptions;
        private readonly ObservableCollection<ComboBoxIdentity> _listComboBoxIdentity;

        public TextBoxUserControl(ObservableCollection<ComboBoxOption> options,
            ObservableCollection<ComboBoxIdentity> comboBoxes)
        {
            InitializeComponent();

            _listOptions = options;
            _listComboBoxIdentity = comboBoxes;

            _listBoxes.Add("");

            foreach (var boxes in comboBoxes)
            {
                var name = boxes.ComboBox.Name.Remove(0, 2);
                CBParents.Items.Add(name);
            }
        }
        public void btnAddTextBox_Click(object sender, RoutedEventArgs e)
        {
            //ObservableCollection<ComboBoxOption> newlistBoxOptions = new ObservableCollection<ComboBoxOption>();

            var parent = VisualTreeHelper.GetParent(this);
            while (!(parent is Page2))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            string parentId = null;
            string parentOptionId = null;

            if (CBParents.SelectedValue != null && (string)CBParents.SelectedValue != "")
            {
                string selectedvalue = "CB" + CBParents.SelectionBoxItem;

                ComboBoxIdentity result = _listComboBoxIdentity.First(q => Equals(q.ComboBox.Name, selectedvalue));

                parentId = result.Id;
            }
            if (CBParentOptions.SelectedValue != null && (string)CBParentOptions.SelectedValue != "")
            {
                string selectedvalue = CBParentOptions.SelectedValue.ToString();

                ComboBoxOption result = _listOptions.First(q => Equals(q.DisplayName, selectedvalue));

                parentOptionId = result.Id;
            }

            string lablename = TxtboxTextBoxName.Text.Replace(" ", "");

            (parent as Page2).TextboxAndLabelSetup(Guid.NewGuid().ToString(), lablename, TxtboxTextBoxContent.Text, TxtboxTextBoxName.Text, Guid.NewGuid().ToString(), parentId, parentOptionId);

            MessageBox.Show("TextBox added!");

            CBParents.ItemsSource = null;
            foreach (var boxes in _listComboBoxIdentity)
            {
                var name = boxes.ComboBox.Name.Remove(0, 2);
                if (!_listComboBoxIdentity.Contains(boxes))
                {
                    CBParents.Items.Add(name);
                }
            }

            TxtboxTextBoxName.Clear();
            TxtboxTextBoxContent.Clear();
        }

        private void CbParent_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = "CB" + CBParents.SelectedValue;

            if (!string.IsNullOrEmpty(item) && item != "CB")
            {
                ComboBoxIdentity bob1 = _listComboBoxIdentity.First(q => Equals(q.ComboBox.Name, item));

                CBParentOptions.ItemsSource = bob1.ComboBox.ItemsSource;
                CBParentOptions.DisplayMemberPath = "DisplayName";
                CBParentOptions.SelectedValuePath = "Value";
            }
            else
            {
                CBParentOptions.ItemsSource = null;
            }

        }
    }
}
