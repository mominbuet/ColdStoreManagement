using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.BusinessObjects
{
    public class XMLSiteMapParserObject
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public bool IsSelected { get; set; }
    }
}
