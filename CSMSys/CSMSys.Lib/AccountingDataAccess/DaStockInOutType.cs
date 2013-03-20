using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaStockInOutType
    {
       public DaStockInOutType() { }

       public DataTable getStockInOutType(SqlConnection con)
       {
           DataTable dt = null;

           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_StockInOutType", con);
               dt = new DataTable();
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
