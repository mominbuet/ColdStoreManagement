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
    public class DaSalesReturn
    {
        public DaSalesReturn() { }
        public DataTable loadSalesreturnDetail(SqlConnection con, int ReturnMID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("sploadSelesReturnDetail", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = ReturnMID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public int SaveUpdateSalesReturn(SalesReturn obS_Ret, SqlConnection con, SqlTransaction tran)
        {
            int ID = 0;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateSalesReturn", con, tran);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = obS_Ret.ReturnMID;
                com.Parameters.Add("@SalesInvoiceID", SqlDbType.Int).Value = obS_Ret.SalesInvoiceID;
                com.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 100).Value = obS_Ret.InvoiceNo;
                com.Parameters.Add("@ChalanNo", SqlDbType.VarChar, 100).Value = obS_Ret.ChalanNo;
                com.Parameters.Add("@ReturnDate", SqlDbType.DateTime).Value = obS_Ret.ReturnDate;
                com.Parameters.Add("@CustomerAccount", SqlDbType.Int).Value = obS_Ret.CustomerAccount;
                com.Parameters.Add("@SalesAccount", SqlDbType.Int).Value = obS_Ret.SalesAccount;
                com.Parameters.Add("@ReturnAmount", SqlDbType.Money).Value = obS_Ret.ReturnAmount;
                if (obS_Ret.TransRefID <= 0)
                    com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = DBNull.Value;
                else
                com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = obS_Ret.TransRefID;
                if (obS_Ret.StockRefID <= 0)
                    com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = DBNull.Value;
                else
                com.Parameters.Add("@StockRefID", SqlDbType.Int).Value = obS_Ret.StockRefID;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 1000).Value = obS_Ret.Remarks;
                if (obS_Ret.CurrencyID <= 0)
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obS_Ret.CurrencyID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@Rate", SqlDbType.Money).Value = obS_Ret.Rate;
                com.ExecuteNonQuery();
                if (obS_Ret.ReturnMID == 0)
                {
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ReturnMID),0) FROM T_Sales_Return", con, tran);
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                    ID = obS_Ret.ReturnMID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ID;
        }
        public void SaveUpdateSalesReturnDetail(SalesReturnDetail obS_R_Detail, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateSalesReturnDetail", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ReturnDID", SqlDbType.Int).Value = obS_R_Detail.ReturnDID;
                com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = obS_R_Detail.ReturnMID;
                com.Parameters.Add("@ItemID", SqlDbType.Int).Value = obS_R_Detail.ItemID;
                com.Parameters.Add("@ReturnQty", SqlDbType.Money).Value = obS_R_Detail.ReturnQty;
                com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obS_R_Detail.UnitID;
                com.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = obS_R_Detail.UnitPrice;
                com.Parameters.Add("@ReturnAmount", SqlDbType.Money).Value = obS_R_Detail.ReturnAmount;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = obS_R_Detail.Remarks;
                com.Parameters.Add("@ColorCode", SqlDbType.VarChar, 50).Value = obS_R_Detail.ColorCode;
                com.Parameters.Add("@Labdip", SqlDbType.VarChar, 50).Value = obS_R_Detail.Labdip;
                if (obS_R_Detail.CountID <= 0)
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CountID", SqlDbType.Int).Value = obS_R_Detail.CountID;

                if (obS_R_Detail.SizeID <= 0)
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@SizeID", SqlDbType.Int).Value = obS_R_Detail.SizeID;

                if (obS_R_Detail.ColorID <= 0)
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@ColorID", SqlDbType.Int).Value = obS_R_Detail.ColorID;
                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public SalesReturn getSalesreturn(SqlConnection con, int ReturnMID)
        {
            SalesReturn obSalesReturn = new SalesReturn();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Sales_Return WHERE ReturnMID = @ReturnMID", con);
                da.SelectCommand.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = ReturnMID;
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0) return null;
                obSalesReturn.ReturnMID = dt.Rows[0].Field<int>("ReturnMID");
                obSalesReturn.CurrencyID = Convert.ToInt32(dt.Rows[0].Field<int>("CurrencyID"));
                obSalesReturn.SalesInvoiceID = dt.Rows[0].Field<int>("SalesInvoiceID");
                obSalesReturn.InvoiceNo = dt.Rows[0].Field<string>("InvoiceNo");
                obSalesReturn.ChalanNo = dt.Rows[0].Field<string>("ChalanNo");
                obSalesReturn.ReturnDate = dt.Rows[0].Field<DateTime>("ReturnDate");
                obSalesReturn.CustomerAccount = dt.Rows[0].Field<int>("CustomerAccount");
                obSalesReturn.SalesAccount = dt.Rows[0].Field<int>("SalesAccount");
                obSalesReturn.ReturnAmount = Convert.ToDouble(dt.Rows[0].Field<object>("ReturnAmount"));
                if (dt.Rows[0].Field<object>("TransRefID") == null || dt.Rows[0].Field<object>("TransRefID") == DBNull.Value)
                    obSalesReturn.TransRefID = 0;
                else
                obSalesReturn.TransRefID = dt.Rows[0].Field<int>("TransRefID");
                if (dt.Rows[0].Field<object>("StockRefID") == null || dt.Rows[0].Field<object>("StockRefID") == DBNull.Value)
                    obSalesReturn.StockRefID = 0;
                else
                obSalesReturn.StockRefID = dt.Rows[0].Field<int>("StockRefID");
                obSalesReturn.Remarks = dt.Rows[0].Field<string>("Remarks");
                obSalesReturn.Rate = Convert.ToDouble(dt.Rows[0].Field<object>("Rate"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return obSalesReturn;
        }
        public DataTable searchSalesReturn(SqlConnection con, string InvoiceNo, DateTime sDate, DateTime eDate)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("searchSalesReturn", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 100).Value = InvoiceNo;
                da.SelectCommand.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sDate;
                da.SelectCommand.Parameters.Add("@eDate", SqlDbType.DateTime).Value = eDate;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public void DeleteSalesReturn(SqlConnection con, int ReturnMID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "DeleteSalesReturn";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = ReturnMID;
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
        public void DeleteSalesReturn(SqlConnection con,SqlTransaction trans, int ReturnMID)
        {
            SqlCommand com = null;
            
            try
            {
                com = new SqlCommand();
                
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "DeleteSalesReturn";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ReturnMID", SqlDbType.Int).Value = ReturnMID;
                com.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);
            }
        }
        public void UpdateRefIDs(SqlConnection con, SqlTransaction trans, int RetMID, int TransID, int StockRID)
        {
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("Update T_Sales_Return SET TransRefID = @TransID, StockRefID = @StockRID WHERE ReturnMID = @RetMID", con, trans);

                com.Parameters.Add("@RetMID", SqlDbType.Int).Value = RetMID;
                if (TransID == 0)
                    com.Parameters.Add("@TransID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@TransID", SqlDbType.Int).Value = TransID;
                if (StockRID == 0)
                    com.Parameters.Add("@StockRID", SqlDbType.Int).Value = DBNull.Value;
                else
                com.Parameters.Add("@StockRID", SqlDbType.Int).Value = StockRID;

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

        public DataTable loadReturnDetails(SqlConnection con, int InvoiceID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spLoadInvoiceForReturn", con);
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

    
    
    }
}
