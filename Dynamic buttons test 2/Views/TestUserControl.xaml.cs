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
    /// Interaction logic for TestUserControl.xaml
    /// </summary>

    public partial class TestUserControl
    {
        private readonly List<string> _listString = new List<string>();
        private readonly List<string> _listBoxes = new List<string>();
        private readonly ObservableCollection<ComboBoxOption> _listOptions;
        private readonly ObservableCollection<ComboBoxIdentity> _listComboBoxIdentities;

        private ObservableCollection<ComboBoxOption> _parentOptions = new ObservableCollection<ComboBoxOption>();
        private ObservableCollection<ComboBoxOption> _newlistBoxOptions = new ObservableCollection<ComboBoxOption>();



        public TestUserControl(ObservableCollection<ComboBoxOption> options,
            ObservableCollection<ComboBoxIdentity> parents)
        {
            InitializeComponent();

            _listOptions = options;
            _listComboBoxIdentities = parents;

            _listBoxes.Add("");

            foreach (var p in parents)
            {
                string name = p.ComboBox.Name;
                var removed = name.Remove(0, 2);

                _listBoxes.Add(removed);

                ComboBoxOption combo = new ComboBoxOption();
                combo.Id = Guid.NewGuid().ToString();
                combo.DisplayName = removed;
                combo.Value = removed;

                _parentOptions.Add(combo);

            }

            CbParents.ItemsSource = _listBoxes;
            CbParentOptions.ItemsSource = _parentOptions;

        }
        private void TextBox_Click(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Add(TxtBoxTextBoxItems.Text);
        }

        public void btnAddCombobox_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxOption newBoxOption = new ComboBoxOption();
            foreach (var item in ListBox.Items)
            {
                _listString.Add(item.ToString());
                newBoxOption.Id = new Guid().ToString();
                newBoxOption.DisplayName = item.ToString();
                newBoxOption.Value = item.ToString();

                _newlistBoxOptions.Add(newBoxOption);
            }

            var parent = VisualTreeHelper.GetParent(this);
            while (!(parent is Page2))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            string parentId = null;
            string parentOptionId = null;

            if (CbParents.SelectedValue != null)
            {
                ComboBoxIdentity result = _listComboBoxIdentities.First(q => Equals(q.ComboBox, CbParents));
                parentId = result.ParentId;

                ComboBoxOption result2 = _listOptions.First(q => Equals(q.Id, result.ParentOptionId));
                parentOptionId = result2.Id;

            }


            //if (_listComboBoxIdentities.Count > 0)
            //{
            //    ComboBoxIdentity result = _listComboBoxIdentities.First(q => Equals(q.ComboBox, CbParents));
            //    parentId = result.ParentId;

            //    result2 = _listOptions.First(q => Equals(q.Id, result.ParentId)).ToString();
            //}

            //(parent as Page2).ComboBoxAndLabelSetup(new Guid().ToString(), TxtboxComboBoxName.Text, LblComboBoxName.Content.ToString(), _newlistBoxOptions, new Guid().ToString(), string.IsNullOrEmpty(parentId).ToString(), string.IsNullOrEmpty(result2).ToString());
                        (parent as Page2).ComboBoxAndLabelSetup(new Guid().ToString(), TxtboxComboBoxName.Text, LblComboBoxName.Content.ToString(), _newlistBoxOptions, new Guid().ToString(), parentId, parentOptionId);

            MessageBox.Show("ComboBox Added. Well. Almost..");
            //CbParents.ItemsSource.
            //CbParentOptions.Items.Clear();
            //ListBox.ItemsSource = null;
            //TxtBoxTextBoxItems.Clear();
            //TxtboxComboBoxName.Clear();

        }

        private void CbParentOptions_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox currentComboBox = (ComboBox)sender;

            ComboBoxIdentity result;
            string bla = null;


            if (_listComboBoxIdentities.First(q => Equals(q.ComboBox, currentComboBox)) != null)
            {
                result = _listComboBoxIdentities.First(q => Equals(q.ComboBox, currentComboBox));

                bla = result.ParentOptionId;

                var result2 = _listOptions.Where(q => (Equals(q.Id, bla)));

                CbParentOptions.ItemsSource = result2;
            }
            //ComboBoxIdentity result = _listComboBoxIdentities.First(q => Equals(q.ComboBox, currentComboBox));



            //result.Current.ParentOptionId



            //x => x.ComboBoxIdentity
            //where combobox = currentComboBox;


            //var bobo = from _listComboBoxIdentities where parentOptionsId == boxidentity.parentId)
            //{

            //}

            //foreach (var listBox in _listBoxes)
            //{
            //    _listOptions.Add(listBox);
            //}
            //CbParentOptions.ItemsSource = _listOptions;

        }
    }
}
