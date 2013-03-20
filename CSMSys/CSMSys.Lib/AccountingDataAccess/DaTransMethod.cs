using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaTransMethod
    {
       public DaTransMethod() { }
       public DataTable getTransactionMethods(SqlConnection con)
       {
           DataTable dt = null;
           try
           {
               dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_TransactionMethod ORDER BY TransMethod", con);
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
