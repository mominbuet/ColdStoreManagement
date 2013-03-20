using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaVoucherType
    {
       public DaVoucherType() { }

       public DataTable getVoucherType(SqlConnection con)
       {
           DataTable dt = null;
           try
           {
               dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_VoucherType ORDER BY VoucherType", con);
               da.Fill(dt);
               da.Dispose();
               
           }
           catch (Exception ex)
           {
               
               throw ex;
           }

           return dt;
       }

    }
}
