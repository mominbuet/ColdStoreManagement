using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using System.Collections.Specialized;
using System.Configuration;
namespace CSMSys.Lib.AccountingUtility
{
   public class CrystalReportHelper
    {
        public CrystalReportHelper() { }

        //private Table crTable;
        //private TableLogOnInfo crTableLogOnInfo;
        //private ConnectionInfo crConnectionInfo = new ConnectionInfo();

        //public void setDBConnectionForReport(Tables crTables)
        //{

        //    NameValueCollection configaration = ConfigurationSettings.AppSettings;
        //    crConnectionInfo.ServerName = configaration.Get("Data Source");
        //    crConnectionInfo.DatabaseName = configaration.Get("Database");

        //    if (configaration.Get("Integrated Security") == null)
        //    {
        //        crConnectionInfo.UserID = configaration.Get("User ID");
        //        crConnectionInfo.Password = configaration.Get("Password"); ;
        //    }
        //    else
        //        crConnectionInfo.IntegratedSecurity = true;

        //    for (int i = 0; i < crTables.Count; i++)
        //    {
        //        crTable = crTables[i];
        //        crTableLogOnInfo = crTable.LogOnInfo;
        //        crTableLogOnInfo.ConnectionInfo = crConnectionInfo;
        //        crTable.ApplyLogOnInfo(crTableLogOnInfo);
        //        //int r = crTable.Location.LastIndexOf(".");
        //        string s = crConnectionInfo.DatabaseName + ".dbo." + crTable.Location.Substring(crTable.Location.LastIndexOf(".") + 1);
        //        crTable.Location = s;

        //    }
        //}
    }
}
