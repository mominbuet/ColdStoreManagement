using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.AccountingEntity;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaPurchaseReturn
    {
       public DaPurchaseReturn() { }

       public int SaveUpdatePurchaseReturnMaster(PurchaseReturn obPurchaseReturn, SqlConnection con, SqlTransaction trans)
       {
           SqlCommand com = null;
           int LastID = 0;
           try
           {
               com = new SqlCommand("spSaveUpdatePurchaseReturnMaster", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = obPurchaseReturn.ReturnMID;
               com.Parameters.Add("@PurchaseInvoiceID", SqlDbType.VarChar, 50).Value = obPurchaseReturn.PurchaseInvoiceID;
               com.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 50).Value = obPurchaseReturn.InvoiceNo;

               com.Parameters.Add("@ReturnDate", SqlDbType.DateTime).Value = obPurchaseReturn.ReturnDate;
               com.Parameters.Add("@SupplierAccountID", SqlDbType.Int).Value = obPurchaseReturn.SupplierAccountID;
               com.Parameters.Add("@PurchaseAccountID", SqlDbType.Int).Value = obPurchaseReturn.PurchaseAccountID;



               com.Parameters.Add("@ReturnAmount", SqlDbType.Money).Value = obPurchaseReturn.ReturnAmount;
               if (obPurchaseReturn.TransRefID <= 0)
                   com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = obPurchaseReturn.TransRefID;

               if (obPurchaseReturn.StockRefID <= 0)
                   com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = obPurchaseReturn.StockRefID;
               com.Parameters.Add("@Remarks", SqlDbType.VarChar, 1000).Value = obPurchaseReturn.Remarks;
               if (obPurchaseReturn.CurrencyID <= 0)
                   com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obPurchaseReturn.CurrencyID;
               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               com.Parameters.Add("@userID", SqlDbType.Int).Value = LogInInfo.UserID;
               com.Parameters.Add("@Rate", SqlDbType.Money).Value = obPurchaseReturn.Rate;
               com.ExecuteNonQuery();
               //if (obSalesInvoice.InvoiceID == 0)
               //{
               //    LastID = ConnectionHelper.GetID(con, "InvoiceID", "T_Sales_Invoice");
               //}
               //else
               //    LastID = obSalesInvoice.InvoiceID;
               if (obPurchaseReturn.ReturnMID == 0)
               {
                   SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ReturnMID),0) FROM T_Purchase_Return", con, trans);
                   LastID = Convert.ToInt32(cmd.ExecuteScalar());
               }
               else
                   LastID = obPurchaseReturn.ReturnMID;

               return LastID;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public void saveUpdatePurchase_Return_DTL(PurchaseReturnDetails  objPurchaseReturnDTL, SqlConnection con, SqlTransaction trans)
        {
           SqlCommand com = null;
           try
           {
               com = new SqlCommand("spSaveUpdatePurchaseReturnDetails", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@ReturnDID", SqlDbType.Int).Value = objPurchaseReturnDTL.ReturnDID;
               com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = objPurchaseReturnDTL.ReturnMID;

               com.Parameters.Add("@ItemID", SqlDbType.Int).Value = objPurchaseReturnDTL.ItemID;
              
          
              
             

               com.Parameters.Add("@ReturnQty", SqlDbType.Money).Value = objPurchaseReturnDTL.ReturnQty;
               
               com.Parameters.Add("@UnitID", SqlDbType.Int).Value = objPurchaseReturnDTL.UnitID;
               com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = objPurchaseReturnDTL.UnitPrice;
               com.Parameters.Add("@ReturnAmount", SqlDbType.Money).Value = objPurchaseReturnDTL.ReturnAmount;
               com.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = objPurchaseReturnDTL.Remarks;
               com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 50).Value = objPurchaseReturnDTL.ColorCode;
               com.Parameters.Add("@Labdip", SqlDbType.VarChar, 50).Value = objPurchaseReturnDTL.Labdip;
               com.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public DataTable PurchaseReturnDetails(SqlConnection con, int ReturnMID)
       {
           DataTable dt = new DataTable();
           try
           {
               //SqlDataAdapter da = new SqlDataAdapter("SELECT * from View_Sales_Invoice Where InvoiceID = @InvoiceID", con);
               //SqlDataAdapter da = new SqlDataAdapter("spPurchaseReturnDTLInfo", con);
               SqlDataAdapter da = new SqlDataAdapter("spLoadOrFindPurchaseReturn", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = ReturnMID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public DataTable PurchaseReturnDetail(SqlConnection con, int InvoiceID)
       {
           DataTable dt = new DataTable();
           try
           {
               //SqlDataAdapter da = new SqlDataAdapter("SELECT * from View_Sales_Invoice Where InvoiceID = @InvoiceID", con);
               SqlDataAdapter da = new SqlDataAdapter("spLoadInvoiceForPurchaseReturn", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = InvoiceID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable searchSelectedPurchaseReturn(SqlConnection con, DateTime sDate, DateTime eDate, string InvNo)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("searchSelectedPurchaseReturn", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 100).Value = InvNo;
               da.SelectCommand.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sDate;
               da.SelectCommand.Parameters.Add("@eDate", SqlDbType.DateTime).Value = eDate;
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public PurchaseReturn getPurchaseReturn(SqlConnection con, int InvoiceID)
       {
           PurchaseReturn obPurchaseReturn = new PurchaseReturn();
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("Select * from T_Purchase_Return WHERE ReturnMID = @InvoiceID", con);
               da.SelectCommand.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = InvoiceID;
               da.Fill(dt);
               da.Dispose();
               if (dt.Rows.Count == 0)
                   return null;
               obPurchaseReturn.PurchaseInvoiceID = dt.Rows[0].Field<int>("PurchaseInvoiceID");
               obPurchaseReturn.ReturnMID = dt.Rows[0].Field<int>("ReturnMID");
               obPurchaseReturn.InvoiceNo = dt.Rows[0].Field<string>("InvoiceNo");
              
               obPurchaseReturn.SupplierAccountID = dt.Rows[0].Field<int>("SupplierAccountID");

               obPurchaseReturn.ReturnDate = dt.Rows[0].Field<DateTime>("ReturnDate");
               obPurchaseReturn.PurchaseAccountID = dt.Rows[0].Field<int>("PurchaseAccountID");

               obPurchaseReturn.ReturnAmount = Convert.ToDouble(dt.Rows[0].Field<object>("ReturnAmount"));

               if (Convert.ToInt32(dt.Rows[0].Field<object>("TransRefID")) == 0)
                   obPurchaseReturn.TransRefID = 0;
               else
                   obPurchaseReturn.TransRefID = Convert.ToInt32(dt.Rows[0].Field<object>("TransRefID"));
               obPurchaseReturn.StockRefID = Convert.ToInt32(dt.Rows[0].Field<object>("StockRefID"));
               obPurchaseReturn.CurrencyID = Convert.ToInt32(dt.Rows[0].Field<int>("CurrencyID"));
               obPurchaseReturn.Remarks = dt.Rows[0].Field<string>("Remarks");
               obPurchaseReturn.Rate = Convert.ToDouble(dt.Rows[0].Field<object>("Rate"));

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return obPurchaseReturn;
       }

       public void DeletePurchaseReturn(SqlConnection con, int ReturnMID)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "spDeletePurchaseReturn";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = ReturnMID;
               
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               trans.Rollback();
               throw new Exception(ex.Message);
           }
       }
       public void DeletePurchaseReturn(SqlConnection con,SqlTransaction trans, int ReturnMID)
       {
           SqlCommand com = null;
           
           try
           {
               com = new SqlCommand();
              
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "spDeletePurchaseReturn";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = ReturnMID;

               com.ExecuteNonQuery();
              
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
       }
    
       public void UpdateRefIDs(SqlConnection con, SqlTransaction trans, int returnMID, int transMID, int stockMID)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("UPDATE T_Purchase_Return SET TransRefID=@TransRefID,StockRefID=@StockRefID WHERE ReturnMID=@ReturnMID", con, trans);
               if (transMID <= 0)
                   cmd.Parameters.Add("@TransRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   cmd.Parameters.Add("@TransRefID", SqlDbType.Int).Value = transMID;

               if (stockMID <= 0)
                   cmd.Parameters.Add("@StockRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   cmd.Parameters.Add("@StockRefID", SqlDbType.Int).Value = stockMID;

               cmd.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = returnMID;
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }


       public DataTable LoadCurrency(SqlConnection con)
       {
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from Currency", con);
               DataTable dt = new DataTable();
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }




    }
}
