using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.Utility
{
    public class ComboItem
    {
        public string Description { get; set; }
        public string Value { get; set; }

        public string CombinedValue
        {
            get
            {
                return Value + " " + Description;
            }
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
