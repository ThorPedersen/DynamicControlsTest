using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dynamic_buttons_test_2.Models
{
    public class ConfigurationIdentity
    {
        //public Label Label { get; set; }
        //public ComboBox ComboBox { get; set; }
        //public TextBox TextBox { get; set; }
        //public Button Button { get; set; }
        //public DockPanel DockPanel { get; set;}
        public string LabelId { get; set; }
        public string TextBoxOrComputerName { get; set; }
        public string TextBoxContent { get; set; }
        public string LabelContent { get; set; }
        public string TextboxId { get; set; }
        public string ParentId { get; set; }
        public string ParentOptionId { get; set; }

        public ObservableCollection<ComboBoxOption> Items { get; set; }
        public string ComboBoxId { get; set; }

        public ConfigurationIdentity()
        {
            
        }

        //string labelId, string name, string textboxContent, string labelContent, string textboxId, string parentId, string parentOptionId
        //    string labelId, string name, string labelContent, ObservableCollection<ComboBoxOption> items, string comboBoxId, string parentId, string parentOptionId
    }
}
