using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dynamic_buttons_test_2.Models
{
    public class LabelEx
    {
        public Label Label { get; set; }

        public LabelEx(Label label)
        {
            Label = label;
        }

        public LabelEx()
        {

        }
    }
}
