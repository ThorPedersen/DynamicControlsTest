﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dynamic_buttons_test_2.Models
{
    public class LabelIdentity
    {
        public string Id { get; set; }
        public string ParentOptionId { get; set; }
        public string ParentPanelId { get; set; }
        public Label Label { get; set; }

        public LabelIdentity(Label label)
        {
            Label = label;
        }

        public LabelIdentity()
        {

        }
    }
}
