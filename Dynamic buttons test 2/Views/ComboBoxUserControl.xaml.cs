using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Dynamic_buttons_test_2.Models;
using Dynamic_buttons_test_2.Views;

namespace Dynamic_buttons_test_2
{
    /// <summary>
    /// Interaction logic for ComboBoxUserControl.xaml
    /// </summary>

    public partial class ComboBoxUserControl
    {
        private readonly List<string> _listBoxes = new List<string>();
        private readonly ObservableCollection<ComboBoxOption> _listOptions;
        private readonly ObservableCollection<ComboBoxIdentity> _listComboBoxIdentities;

        private readonly ObservableCollection<ComboBoxOption> _parentOptions = new ObservableCollection<ComboBoxOption>();

        public ComboBoxUserControl(ObservableCollection<ComboBoxOption> options,
            ObservableCollection<ComboBoxIdentity> parents)
        {
            InitializeComponent();

            _listOptions = options;
            _listComboBoxIdentities = parents;

            _listBoxes.Add("");

            foreach (var boxes in _listComboBoxIdentities)
            {
                var name = boxes.ComboBox.Name.Remove(0, 2);
                CBParents.Items.Add(name);
            }
        }
        private void TextBox_Click(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Add(TxtBoxTextBoxItems.Text);
            TxtBoxTextBoxItems.Clear();
        }

        public void btnAddCombobox_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<ComboBoxOption> newlistBoxOptions = new ObservableCollection<ComboBoxOption>();

            foreach (var item in ListBox.Items)
            {
                ComboBoxOption newBoxOption = new ComboBoxOption
                {
                    Id = Guid.NewGuid().ToString(),
                    DisplayName = item.ToString(),
                    Value = item.ToString()
                };

                newlistBoxOptions.Add(newBoxOption);
                _listOptions.Add(newBoxOption);
            }


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

                ComboBoxIdentity result = _listComboBoxIdentities.First(q => Equals(q.ComboBox.Name, selectedvalue));

                parentId = result.Id;
            }
            if (CBParentOptions.SelectedValue != null && (string)CBParentOptions.SelectedValue != "")
            {
                string selectedvalue = CBParentOptions.SelectedValue.ToString();

                ComboBoxOption result = _listOptions.First(q => Equals(q.DisplayName, selectedvalue));

                parentOptionId = result.Id;
            }
            _listBoxes.Add(TxtboxComboBoxName.Text);

            string lablename = TxtboxComboBoxName.Text.Replace(" ", "");

            (parent as Page2).ComboBoxAndLabelSetup(Guid.NewGuid().ToString(), lablename, TxtboxComboBoxName.Text, newlistBoxOptions, Guid.NewGuid().ToString(), parentId, parentOptionId);

            MessageBox.Show("ComboBox Added!");

            CBParents.Items.Clear();
            foreach (var boxes in _listComboBoxIdentities)
            {
                var name = boxes.ComboBox.Name.Remove(0, 2);
                CBParents.Items.Add(name);
            }

            ListBox.Items.Clear();
            TxtBoxTextBoxItems.Clear();
            TxtboxComboBoxName.Clear();
        }

        private void CbParent_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = "CB" + CBParents.SelectedValue;

            if (!string.IsNullOrEmpty(item) && item != "CB")
            {
                ComboBoxIdentity bob1 = _listComboBoxIdentities.First(q => Equals(q.ComboBox.Name, item));

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
