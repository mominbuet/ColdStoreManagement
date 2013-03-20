using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingEntity;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaLogIn
    {
       public DaLogIn() { }
       public DataTable GetCompant(SqlConnection con)
       {
           SqlDataAdapter da = new SqlDataAdapter("Select * From Company", con);
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.Dispose();
           return dt;

       }

       public DataTable GetUser(int numCompanyID,SqlConnection con)
       {
           SqlDataAdapter da = new SqlDataAdapter("Select * From Users where CompanyID=@CompanyID", con);
           da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = numCompanyID;
           DataTable dt = new DataTable();
           da.Fill(dt);
           da.Dispose();
           return dt;

       }
       public string SetUserInfo(User objUser,SqlConnection con)
       {
           string strpassword = "";
           try
           {
               DataTable dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("StoredProcedure10", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = objUser.UserID;
               //da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = objUser.CompanyID;
               da.Fill(dt);
               da.Dispose();
              


               strpassword = dt.Rows[0].Field<string>("Password");
               
                  
              }
           
           catch (Exception ex)
           {
               throw ex;
           }

           return strpassword;

       }

    }
}
