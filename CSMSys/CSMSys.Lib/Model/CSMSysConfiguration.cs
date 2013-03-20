using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CSMSys.Lib.Model
{
    public partial class CSMSysConfiguration : CSMSysDataContext
    {
        partial void OnCreated();
        /// <summary>
        /// Every Time A new dbml file (Let, CSMSys.dbml) is created or new components added to the existing dbml,
        /// we will have to delete/comment out the following constructor from the designer.cs file of the dbml (from CSMSys.designer.cs):
        ///public CSMSysDataContext() : 
        ///        base(global::CSMSys.Lib.Properties.Settings.Default.CSMSysConnectionString, mappingSource)
        ///{
        ///    OnCreated();
        ///}
        /// </summary>
        public CSMSysConfiguration()
            : base(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString)
        {
            OnCreated();
        }
    }
}
