using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dynamic_buttons_test_2.Models
{
    public class TextBoxIdentity
    {
        public string Id { get; set; }
        public TextBox TextBox { get; set; }
        public List<UIElement> TextBoxChildren { get; set; }
        public TextBoxIdentity(TextBox textBox)
        {
            TextBox = textBox;
        }

        public TextBoxIdentity()
        {

        }
    }
}
