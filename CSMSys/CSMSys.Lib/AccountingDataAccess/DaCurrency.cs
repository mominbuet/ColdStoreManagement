using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaCurrency
    {
       public DaCurrency() { }

       public void saveUpdate(Currency obCurrency, SqlConnection con)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "spSaveUpdateCurrency";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obCurrency.CurrencyID;
               com.Parameters.Add("@Code", SqlDbType.VarChar, 50).Value = obCurrency.Code;
               com.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = obCurrency.Name;
               com.Parameters.Add("@Symbol", SqlDbType.VarChar, 10).Value = obCurrency.Symbol;
               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               //com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = obCurrency.ModifiedDate;
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               trans.Rollback();
               throw ex;
           }
       }

       public void deleteCurrency(SqlConnection con, int CurrencyID)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "delete from Currency where CurrencyID=@CurrencyID";
               com.CommandType = CommandType.Text;
               com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = CurrencyID;
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               trans.Rollback();
               throw ex;
           }
       }

       public DataTable getCurrency(SqlConnection con)
       {
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from Currency WHERE CompanyID = " + LogInInfo.CompanyID, con);
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
       public string  getCurrency(SqlConnection con,int CurrencyID)
       {
           string c =string.Empty;
           try
           {
               SqlCommand cmd = new SqlCommand("select CODE from Currency WHERE CurrencyID =" + CurrencyID.ToString(), con);
               object obj= cmd.ExecuteScalar();

               if (obj == null)
                   c = string.Empty;
               else
                   c = obj.ToString();
              
               
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return c;
       }
    }
}
