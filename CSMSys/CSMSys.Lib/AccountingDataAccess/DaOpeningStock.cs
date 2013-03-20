using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingEntity;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaOpeningStock
    {
       public DaOpeningStock() { }


       public DataTable getFiscalYears(SqlConnection con)
       {
           DataTable dt = null;
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM FiscalYear WHERE CompanyID=" + LogInInfo.CompanyID.ToString() +" ORDER BY startDate Desc", con);
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
       public DataTable getOpeningStock(SqlConnection con, int CustomerID, string ItemIDs, string LoadSelect)
       {
           DataTable dt = null;
           try
           {
               dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("spSearchOpeningStock", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.SelectCommand.Parameters.Add("@Items", SqlDbType.NVarChar, 1000).Value = ItemIDs;
               da.SelectCommand.Parameters.Add("@LoadSelect", SqlDbType.VarChar, 50).Value = LoadSelect;
               da.Fill(dt);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return dt;
       }
       public int SaveUpdateOpenningStock(OpeningStock obOpStk, SqlConnection con, SqlTransaction trans)
       {
           int ID = 0;
           SqlCommand com = null;
           try
           {
               com = new SqlCommand("spSaveOpentingStock", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@OpeningStockID", SqlDbType.Int).Value = obOpStk.OpeningID;
               //com.Parameters.Add("@FiscalYearID", SqlDbType.Int).Value = DBNull.Value;
               com.Parameters.Add("@CustomerID", SqlDbType.Int).Value = obOpStk.CustomerID;
               com.Parameters.Add("@ItemID", SqlDbType.Int).Value = obOpStk.ItemID;
               com.Parameters.Add("@OpeningQty", SqlDbType.Money).Value = obOpStk.OpeningQuantity;
               if (obOpStk.UnitID == 0)
                   com.Parameters.Add("@UnitID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obOpStk.UnitID;
               com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = obOpStk.UnitPrice;
               com.Parameters.Add("@OpeningAmount", SqlDbType.Money).Value = obOpStk.OpeningAmount;
               com.Parameters.Add("@OpeningDate", SqlDbType.DateTime).Value = obOpStk.OpeningDate;
               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               com.Parameters.Add("@DollorRate", SqlDbType.Money).Value = obOpStk.DRate;
               com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 100).Value = obOpStk.ColorCode;
               com.Parameters.Add("@Labdip", SqlDbType.VarChar, 100).Value = obOpStk.Labdip;
               com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obOpStk.Remarks;
               if (obOpStk.CountID == 0)
                   com.Parameters.Add("@CountID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@CountID", SqlDbType.Int).Value = obOpStk.CountID;
               if (obOpStk.SizeID == 0)
                   com.Parameters.Add("@SizeID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@SizeID", SqlDbType.Int).Value = obOpStk.SizeID;
               if (obOpStk.ColorID == 0)
                   com.Parameters.Add("@ColorID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ColorID", SqlDbType.Int).Value = obOpStk.ColorID;
               com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obOpStk.CurrencyID;
               com.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return ID;
       }
       public void DeleteOpeningStock(SqlConnection con, int OpeningStockID)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "Delete FROM T_Opening_Stock WHERE OpeningStockID = @OpeningStockID";
               com.CommandType = CommandType.Text;
               com.Parameters.Add("@OpeningStockID", SqlDbType.Int).Value = OpeningStockID;
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               if (trans != null)
                   trans.Rollback();
               throw new Exception(ex.Message);
           }
       }
       public void DeleteOpeningStock(SqlConnection con, SqlTransaction trans,int OpeningStockID)
       {
           SqlCommand com = null;
           
           try
           {
               com = new SqlCommand();
              
               com.Connection = con;
               com.Transaction = trans;
               com.CommandText = "Delete FROM T_Opening_Stock WHERE OpeningStockID = @OpeningStockID";
               com.CommandType = CommandType.Text;
               com.Parameters.Add("@OpeningStockID", SqlDbType.Int).Value = OpeningStockID;
               com.ExecuteNonQuery();
              
           }
           catch (Exception ex)
           {
              
               throw new Exception(ex.Message);
           }
       }

       public DataTable loadItems(SqlConnection con)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("SELECT ItemID,ItemName FROM T_Item WHERE CompanyID = " + LogInInfo.CompanyID, con);
               da.Fill(dt);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return dt;
       }

       public string getItemCategory(SqlConnection con, int ItemID)
       {
           string Category = "";
           try
           {
               SqlCommand cmd = new SqlCommand("SELECT ItemCategory FROM T_Item WHERE ItemID = @ItemId", con);
               cmd.Parameters.Add("ItemId", SqlDbType.Int).Value = ItemID;
               
               Category = GlobalFunctions.isNull(cmd.ExecuteScalar(), "");
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return Category;
       }
    }
}
