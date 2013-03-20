using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaSalesInvoice
    {
        public DaSalesInvoice() { }
        public int saveUpdateSales_Invoice(Sales_Invoice obSalesInvoice, SqlConnection con,SqlTransaction trans)
        {
            SqlCommand com = null;
            int LastID = 0;
            try
            {
                com = new SqlCommand("spSaveUpdateSalesInvoice", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = obSalesInvoice.InvoiceID;
                com.Parameters.Add("@InvoiceType", SqlDbType.VarChar, 50).Value = obSalesInvoice.InvoiceType;
                com.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 100).Value = obSalesInvoice.InvoiceNo;
                com.Parameters.Add("@ChalanNo", SqlDbType.VarChar, 100).Value = obSalesInvoice.ChalanNo;
                com.Parameters.Add("@InvoiceDate", SqlDbType.DateTime).Value = obSalesInvoice.InvoiceDate;
                com.Parameters.Add("@CustomerAccount", SqlDbType.Int).Value = obSalesInvoice.CustomerAccount;
                com.Parameters.Add("@SalesAccount", SqlDbType.Int).Value = obSalesInvoice.SalesAccount;
                com.Parameters.Add("@SalesAmount", SqlDbType.Money).Value = obSalesInvoice.SalesAmount;
                com.Parameters.Add("@DiscountRate", SqlDbType.Money).Value = obSalesInvoice.DiscountRate;
                com.Parameters.Add("@DiscountAmount", SqlDbType.Money).Value = obSalesInvoice.DiscountAmount;
                com.Parameters.Add("@TransAmount", SqlDbType.Money).Value = obSalesInvoice.TransAmount;
                if (obSalesInvoice.TransRefID <= 0)
                    com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = obSalesInvoice.TransRefID;
                if (obSalesInvoice.StockRefID <= 0)
                    com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = obSalesInvoice.StockRefID;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 1000).Value = obSalesInvoice.Remarks;
                if (obSalesInvoice.CurrencyID <= 0)
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obSalesInvoice.CurrencyID;

                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@Rate", SqlDbType.Money).Value = obSalesInvoice.Rate;
                com.ExecuteNonQuery();
                //if (obSalesInvoice.InvoiceID == 0)
                //{
                //    LastID = ConnectionHelper.GetID(con, "InvoiceID", "T_Sales_Invoice");
                //}
                //else
                //    LastID = obSalesInvoice.InvoiceID;
                if (obSalesInvoice.InvoiceID == 0)
                {
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(InvoiceID),0) FROM T_Sales_Invoice", con, trans);
                    LastID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                    LastID = obSalesInvoice.InvoiceID;

                return LastID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void saveUpdateSales_Invoice_Detail(Sales_Invoice_Detail obSales_Invoice_Detail, SqlConnection con,SqlTransaction trans)
        {
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateSalesInvoiceDetail", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@SLNo", SqlDbType.Int).Value = obSales_Invoice_Detail.SLNo;
                com.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = obSales_Invoice_Detail.InvoiceID;
                if (obSales_Invoice_Detail.OrderID <= 0)
                    com.Parameters.Add("@OrderID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@OrderID", SqlDbType.Int).Value = obSales_Invoice_Detail.OrderID;
                com.Parameters.Add("@ItemID", SqlDbType.Int).Value = obSales_Invoice_Detail.ItemID;

                com.Parameters.Add("@InvQty", SqlDbType.Money).Value = obSales_Invoice_Detail.InvQty;
                com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obSales_Invoice_Detail.UnitID;
                com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = obSales_Invoice_Detail.UnitPrice;
                com.Parameters.Add("@PriceAmount", SqlDbType.Money).Value = obSales_Invoice_Detail.PriceAmount;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obSales_Invoice_Detail.Remarks;
                com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 50).Value = obSales_Invoice_Detail.ColorCode;
                com.Parameters.Add("@Labdip", SqlDbType.VarChar, 50).Value = obSales_Invoice_Detail.Labdip;

                if (obSales_Invoice_Detail.CountID <= 0)
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = obSales_Invoice_Detail.CountID;
                if (obSales_Invoice_Detail.SizeID <= 0)
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = obSales_Invoice_Detail.SizeID;
                if (obSales_Invoice_Detail.ColorID <= 0)
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = obSales_Invoice_Detail.ColorID;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable loadInvoiceDetails(SqlConnection con, int InvoiceID)
        {
            DataTable dt = new DataTable();
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter("SELECT * from View_Sales_Invoice Where InvoiceID = @InvoiceID", con);
                SqlDataAdapter da = new SqlDataAdapter("sploadInvoiceDetail", con);
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
        /*
        public DataTable loadFindingInvoice(SqlConnection con, int InvoiceID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * from View_Sales_Invoice Where InvoiceID = - 4", con);
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
        public DataTable searchSelectedOrder(SqlConnection con, DateTime sDate, DateTime eDate, string OrderNo,int CustomerAccID)
        {
            try
            {
                DataTable dt = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter("spSearchSelectedOrder", con);
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
        public DataTable searchSelectedInvoice(SqlConnection con, DateTime sDate, DateTime eDate, string InvNo)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSearchInvoice", con);
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

        public Sales_Invoice getSalesInvoice(SqlConnection con, int InvoiceID)
        {
            Sales_Invoice obSalesInvoice = new Sales_Invoice();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from T_Sales_Invoice WHERE InvoiceID = @InvoiceID", con);
                da.SelectCommand.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = InvoiceID;
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0)
                    return null;
                obSalesInvoice.InvoiceID = dt.Rows[0].Field<int>("InvoiceID");
                obSalesInvoice.CurrencyID = Convert.ToInt32(dt.Rows[0].Field<int>("CurrencyID"));
                obSalesInvoice.InvoiceNo = dt.Rows[0].Field<string>("InvoiceNo");
                obSalesInvoice.InvoiceType = dt.Rows[0].Field<string>("InvoiceType");
                obSalesInvoice.ChalanNo = dt.Rows[0].Field<string>("ChalanNo");
                obSalesInvoice.CustomerAccount = dt.Rows[0].Field<int>("CustomerAccount");
                obSalesInvoice.InvoiceDate = dt.Rows[0].Field<DateTime>("InvoiceDate");
                obSalesInvoice.SalesAccount = dt.Rows[0].Field<int>("SalesAccount");
                obSalesInvoice.SalesAmount = Convert.ToDouble(dt.Rows[0].Field<object>("SalesAmount"));
                obSalesInvoice.DiscountRate = Convert.ToDouble(dt.Rows[0].Field<object>("DiscountRate"));
                obSalesInvoice.DiscountAmount = Convert.ToDouble(dt.Rows[0].Field<object>("DiscountAmount"));
                obSalesInvoice.TransAmount = Convert.ToDouble(dt.Rows[0].Field<object>("TransAmount"));
                if (Convert.ToInt32(dt.Rows[0].Field<object>("TransRefID")) == 0)
                    obSalesInvoice.TransRefID = 0;
                else
                    obSalesInvoice.TransRefID = Convert.ToInt32(dt.Rows[0].Field<object>("TransRefID"));
                if (Convert.ToInt32(dt.Rows[0].Field<object>("StockRefID")) == 0)
                    obSalesInvoice.StockRefID = 0;
                else
                    obSalesInvoice.StockRefID = dt.Rows[0].Field<int>("StockRefID");
                obSalesInvoice.Remarks = dt.Rows[0].Field<string>("Remarks");
                obSalesInvoice.CurrencyID = Convert.ToInt32(dt.Rows[0].Field<int>("CurrencyID"));
                obSalesInvoice.Rate = Convert.ToDouble(dt.Rows[0].Field<object>("Rate"));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return obSalesInvoice;
        }

        public void DeleteInvoice(SqlConnection con, int InvoiceID,int TMID,int InOutMID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteInvoice";
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

        public void DeleteInvoice(SqlConnection con,SqlTransaction trans, int InvoiceID, int TMID, int InOutMID)
        {
            SqlCommand com = null;
          
            try
            {
                com = new SqlCommand();
             
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteInvoice";
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
