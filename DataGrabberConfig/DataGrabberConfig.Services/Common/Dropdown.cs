using System;
using System.Collections.Generic;
using System.Text;

namespace DataGrabberConfig.Services.Common
{
    public class Dropdown
    {
        public Dropdown()
        {
            Text = string.Empty;
            Value = string.Empty;
        }

        public string Text { get; set; }

        public string Value { get; set; }

    }
}
