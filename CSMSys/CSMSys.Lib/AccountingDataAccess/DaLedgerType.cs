using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaLedgerType
    {
       public DaLedgerType() { }

       public DataTable getLedgerType(SqlConnection con)
       {
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from T_LedgerType", con);
               DataTable ds = new DataTable();
               da.Fill(ds);
               da.Dispose();
               return ds;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
