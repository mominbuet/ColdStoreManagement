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
   public class DaPurchaseInvoice
    {
       public DaPurchaseInvoice() { }
       public DataTable PurchaseInvoiceDetails(SqlConnection con, int InvoiceID)
       {
           DataTable dt = new DataTable();
           try
           {
               //SqlDataAdapter da = new SqlDataAdapter("SELECT * from View_Sales_Invoice Where InvoiceID = @InvoiceID", con);
               SqlDataAdapter da = new SqlDataAdapter("spPurchasesInvoiceDTLInfo", con);
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
       public DataTable searchSelectedOrder(SqlConnection con, DateTime sDate, DateTime eDate, string OrderNo, int CustomerAccID)
       {
           try
           {
               DataTable dt = new DataTable();

               SqlDataAdapter da = new SqlDataAdapter("spSearchSelectedOrder1", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@AccountID", SqlDbType.Int).Value = CustomerAccID;
               da.SelectCommand.Parameters.Add("@OrderNo", SqlDbType.VarChar, 100).Value = OrderNo;
               da.SelectCommand.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sDate;
               da.SelectCommand.Parameters.Add("@eDate", SqlDbType.DateTime).Value = eDate;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }
       public DataTable loadOrder(SqlConnection con, int OrderMID)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("spLoadOrderForSalesInvoice", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@OrderMID", SqlDbType.Int).Value = OrderMID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }
       public int SaveUpdatePurchase_Invoice(Purchases_Invoice obPurchaseInvoice, SqlConnection con, SqlTransaction trans)
       {
           SqlCommand com = null;
           int LastID = 0;
           try
           {
               com = new SqlCommand("spSaveUpdatePurchaseInvoice", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = obPurchaseInvoice.InvoiceID;
               com.Parameters.Add("@InvoiceType", SqlDbType.VarChar, 50).Value = obPurchaseInvoice.InvoiceType;
               com.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 100).Value = obPurchaseInvoice.InvoiceNo;

               com.Parameters.Add("@InvoiceDate", SqlDbType.DateTime).Value = obPurchaseInvoice.InvoiceDate;
               com.Parameters.Add("@SupplierAccountID", SqlDbType.Int).Value = obPurchaseInvoice.SupplierAccountID;
               com.Parameters.Add("@PurchaseAccountID", SqlDbType.Int).Value = obPurchaseInvoice.PurchasesAccountID;

               if (obPurchaseInvoice.CurrencyID <= 0)
                   com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obPurchaseInvoice.CurrencyID;

               com.Parameters.Add("@TransAmount", SqlDbType.Money).Value = obPurchaseInvoice.TransAmmount;
               if (obPurchaseInvoice.TransRefID<= 0)
                   com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = obPurchaseInvoice.TransRefID;

               if (obPurchaseInvoice.StockRefID <= 0)
                   com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = obPurchaseInvoice.StockRefID;
               com.Parameters.Add("@Remarks", SqlDbType.VarChar, 1000).Value = obPurchaseInvoice.Remarks;

               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               com.Parameters.Add("@Rate", SqlDbType.Money).Value = obPurchaseInvoice.Rate;
               com.Parameters.Add("@PurchaseAccount2ID", SqlDbType.Int).Value = obPurchaseInvoice.PurchasesAccount2ID;

               com.ExecuteNonQuery();
               //if (obSalesInvoice.InvoiceID == 0)
               //{
               //    LastID = ConnectionHelper.GetID(con, "InvoiceID", "T_Sales_Invoice");
               //}
               //else
               //    LastID = obSalesInvoice.InvoiceID;
               if (obPurchaseInvoice.InvoiceID == 0)
               {
                   SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(InvoiceID),0) FROM T_Purchase_Invoice", con, trans);
                   LastID = Convert.ToInt32(cmd.ExecuteScalar());
               }
               else
                   LastID = obPurchaseInvoice.InvoiceID;

               return LastID;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }
       public void saveUpdatePurchase_Invoice_DTL(Purchases_Invoice_DTL obPurchase_Invoice_DTL, SqlConnection con, SqlTransaction trans)
       {
           SqlCommand com = null;
           try
           {
               com = new SqlCommand("spSaveUpdatePurchaseInvoiceDTL", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@SLNo", SqlDbType.Int).Value = obPurchase_Invoice_DTL.SLNO;
               com.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = obPurchase_Invoice_DTL.InvoiceID;
               if (obPurchase_Invoice_DTL.OrderID <= 0)
                   com.Parameters.Add("@OrderID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@OrderID", SqlDbType.Int).Value = obPurchase_Invoice_DTL.OrderID;
               com.Parameters.Add("@ItemID", SqlDbType.Int).Value = obPurchase_Invoice_DTL.ItemID;

               com.Parameters.Add("@InvQty", SqlDbType.Money).Value = obPurchase_Invoice_DTL.InvQty;
               com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obPurchase_Invoice_DTL.UnitID;
               com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = obPurchase_Invoice_DTL.UnitPrice;
               com.Parameters.Add("@PriceAmount", SqlDbType.Money).Value = obPurchase_Invoice_DTL.PriceAmmount;
               com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obPurchase_Invoice_DTL.Remarks;
               com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 50).Value = obPurchase_Invoice_DTL.ColorCode;
               com.Parameters.Add("@Labdip", SqlDbType.VarChar, 50).Value = obPurchase_Invoice_DTL.Labdip;
               if (obPurchase_Invoice_DTL.CountID <= 0)
                   com.Parameters.Add("@CountID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@CountID", SqlDbType.Int).Value = obPurchase_Invoice_DTL.CountID;
               if (obPurchase_Invoice_DTL.SizeID <= 0)
                   com.Parameters.Add("@SizeID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@SizeID", SqlDbType.Int).Value = obPurchase_Invoice_DTL.SizeID;
               if (obPurchase_Invoice_DTL.ColorID <= 0)
                   com.Parameters.Add("@ColorID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ColorID", SqlDbType.Int).Value = obPurchase_Invoice_DTL.ColorID;
               com.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }
       public DataTable loadPurchasesInvoiceDetails(SqlConnection con, int InvoiceID)
       {
           DataTable dt = new DataTable();
           try
           {
               //SqlDataAdapter da = new SqlDataAdapter("SELECT * from View_Sales_Invoice Where InvoiceID = @InvoiceID", con);
               SqlDataAdapter da = new SqlDataAdapter("spPurchasesInvoiceDTLInfo", con);
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
       public DataTable searchSelectedPurchaseInvoice(SqlConnection con, DateTime sDate, DateTime eDate, string InvNo)
       {
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("spSearchPurchInvoice", con);
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
       public Purchases_Invoice getPurchaseInvoice(SqlConnection con, int InvoiceID)
       {
           Purchases_Invoice obPurchaseInvoice = new Purchases_Invoice();
           DataTable dt = new DataTable();
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("Select * from T_Purchase_Invoice WHERE InvoiceID = @InvoiceID", con);
               da.SelectCommand.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = InvoiceID;
               da.Fill(dt);
               da.Dispose();
               if (dt.Rows.Count == 0)
                   return null;
               obPurchaseInvoice.InvoiceID = dt.Rows[0].Field<int>("InvoiceID");
               obPurchaseInvoice.InvoiceNo = dt.Rows[0].Field<string>("InvoiceNo");
               obPurchaseInvoice.InvoiceType = dt.Rows[0].Field<string>("InvoiceType");
               obPurchaseInvoice.SupplierAccountID = dt.Rows[0].Field<int>("SupplierAccount");

               obPurchaseInvoice.InvoiceDate = dt.Rows[0].Field<DateTime>("InvoiceDate");
               obPurchaseInvoice.PurchasesAccountID = dt.Rows[0].Field<int>("PurchaseAccount");
               obPurchaseInvoice.PurchasesAccount2ID =GlobalFunctions.isNull( dt.Rows[0].Field<object>("PurchaseAccount2"),0);
               
               obPurchaseInvoice.TransAmmount = Convert.ToDouble(dt.Rows[0].Field<object>("TransAmount"));
     
               if (Convert.ToInt32(dt.Rows[0].Field<object>("TransRefID")) == 0)
                   obPurchaseInvoice.TransRefID = 0;
               else
                   obPurchaseInvoice.TransRefID = Convert.ToInt32(dt.Rows[0].Field<object>("TransRefID"));
               obPurchaseInvoice.StockRefID = Convert.ToInt32(dt.Rows[0].Field<object>("StockRefID"));
               obPurchaseInvoice.CurrencyID = Convert.ToInt32(dt.Rows[0].Field<int>("CurrencyID"));
               obPurchaseInvoice.Remarks = dt.Rows[0].Field<string>("Remarks");
               obPurchaseInvoice.Rate = Convert.ToDouble(dt.Rows[0].Field<object>("Rate"));

           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return obPurchaseInvoice;
       }

       public DataTable LoadCurrency(SqlConnection con)
       {
           try
           {
               SqlDataAdapter da = new SqlDataAdapter("select * from Currency WHERE CompanyID="+LogInInfo.CompanyID.ToString(), con);
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

       public void DeletePurchaseInvoice(SqlConnection con, int InvoiceID, int TMID, int InOutMID)
       {
           SqlCommand com = null;
           SqlTransaction trans = null;
           try
           {
               com = new SqlCommand();
               trans = con.BeginTransaction();
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "spDeletePurchase";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = InvoiceID;
               com.Parameters.Add("@TransMID", SqlDbType.Int).Value = TMID;
               com.Parameters.Add("@InOutMID", SqlDbType.Int).Value = InOutMID;
               com.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               trans.Rollback();
               throw new Exception(ex.Message);
           }
       }

       public void DeletePurchaseInvoice(SqlConnection con, SqlTransaction trans,int InvoiceID, int TMID, int InOutMID)
       {
           SqlCommand com = null;
          
           try
           {
               com = new SqlCommand();
              
               com.Transaction = trans;
               com.Connection = con;
               com.CommandText = "spDeletePurchase";
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = InvoiceID;
               com.Parameters.Add("@TransMID", SqlDbType.Int).Value = TMID;
               com.Parameters.Add("@InOutMID", SqlDbType.Int).Value = InOutMID;
               com.ExecuteNonQuery();
              
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
       }
    }
}
