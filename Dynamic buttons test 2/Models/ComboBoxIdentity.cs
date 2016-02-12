using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dynamic_buttons_test_2.Models
{
    public class ComboBoxIdentity
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string ParentOptionId { get; set; }
        public string ParentPanelId { get; set; }
        public ComboBox ComboBox { get; set; }

        public List<UIElement> ComboBoxChildren { get; set; } 

        public ComboBoxIdentity(string id, ComboBox comboBox)
        {
            Id = id;
            ComboBox = comboBox;
        }
        public ComboBoxIdentity(string id, ComboBox comboBox, List<UIElement> comboboxchildren)
        {
            Id = id;
            ComboBox = comboBox;
            ComboBoxChildren = comboboxchildren;
        }
        public ComboBoxIdentity()
        {
            
        }
    }

}
