using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dynamic_buttons_test_2.Models
{
    public class ComboEx
    {
        public string Id { get; set; }

        public ComboBox Combo { get; set; }

        public ComboEx(string id, ComboBox combo)
        {
            Id = id;
            Combo = combo;
        }

        public ComboEx()
        {
            
        }
    }

}
