using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.BusinessObjects
{
    public class BOCountry
    {
        public int CountryID { get; set; }
        public string CountryCode_EN { get; set; }
        public string CountryCode_BN { get; set; }
        public string CountryName_EN { get; set; }
        public string CountryName_BN { get; set; }
        public string ISDCode { get; set; }
    }
}
