using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic_buttons_test_2.Models
{
    public class ComboBoxOption
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public string OptionParentId { get; set; }
        public ComboBoxOption()
        {
            
        }
    }
}
