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
    public class DaExportBill
    {
        public DaExportBill() { }
        public int SaveUpdateExportBill(ExportBill obExBill, SqlConnection con, SqlTransaction trans)
        {
            int ID = 0;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateExportBill", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@BillID", SqlDbType.Int).Value = obExBill.BillID;
                com.Parameters.Add("@BillType", SqlDbType.VarChar, 50).Value = obExBill.BillType;
                com.Parameters.Add("@TypeNo", SqlDbType.VarChar, 100).Value = obExBill.BillNo;
                com.Parameters.Add("@LCID", SqlDbType.Int).Value = obExBill.LCID;
                com.Parameters.Add("@LCValue", SqlDbType.Money).Value = obExBill.LCValue;
                if (obExBill.CurrencyID <= 0)
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = obExBill.CurrencyID;
                com.Parameters.Add("@LCQty", SqlDbType.Money).Value = obExBill.LCQuantity;
                if (obExBill.UnitID <= 0)
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@UnitID", SqlDbType.Int).Value = obExBill.UnitID;
                com.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = obExBill.BillDate;
                com.Parameters.Add("@BillAmount", SqlDbType.Money).Value = obExBill.BillAmount;
                com.Parameters.Add("@LoanNo", SqlDbType.VarChar, 100).Value = obExBill.LoanNo;
                com.Parameters.Add("@RealisedAmt", SqlDbType.Money).Value = obExBill.RealisedAmount;
                com.Parameters.Add("@RealisedLoss", SqlDbType.Money).Value = obExBill.RealisedLoss;
                if (obExBill.RealisedDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@RealisedDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@RealisedDate", SqlDbType.DateTime).Value = obExBill.RealisedDate;
                com.Parameters.Add("@remarks", SqlDbType.VarChar, 500).Value = obExBill.remarks;
                if (obExBill.AcceptDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = obExBill.AcceptDate;
                if (obExBill.MaturityDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = obExBill.MaturityDate;

                if (obExBill.TransactionRefID <=0)
                    com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = obExBill.TransactionRefID;

                if (obExBill.PurchaseDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@PurchaseDate", SqlDbType.DateTime).Value =DBNull.Value;
                else
                    com.Parameters.Add("@PurchaseDate", SqlDbType.DateTime).Value = obExBill.PurchaseDate;
                
                com.Parameters.Add("@PurchaseAmount", SqlDbType.Money).Value = obExBill.PurchaseAmount;
                
                 com.Parameters.Add("@CompanyID", SqlDbType.Int).Value =LogInInfo.CompanyID;
                 com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                 com.Parameters.Add("@CurrencyRate", SqlDbType.Money).Value = obExBill.CurrencyRate;
                com.ExecuteNonQuery();
                if (obExBill.BillID == 0)
                {
                    SqlCommand cmd = new SqlCommand("Select ISNULL(MAX(BillID),0) FROM T_Export_Bill", con, trans);
                    ID = Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                    ID = obExBill.BillID;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            return ID;
        }
        public int RealizeExportBill(int BillID,DateTime rDate,double rAmt,double rLoss,int refID,double CurrencyRate, SqlConnection con, SqlTransaction trans)
        {
            int ID = 0;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spRealizationExportBill", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
                if (rDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@RealisedDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@RealisedDate", SqlDbType.DateTime).Value = rDate;
                com.Parameters.Add("@RealisedAmt", SqlDbType.Money).Value = rAmt;
                com.Parameters.Add("@RealisedLoss", SqlDbType.Money).Value = rLoss;
                
               
                if (refID <= 0)
                    com.Parameters.Add("@RealizedTransRefID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@RealizedTransRefID", SqlDbType.Int).Value = refID;

                com.Parameters.Add("@CurrencyRate", SqlDbType.Money).Value = CurrencyRate;
                com.ExecuteNonQuery();
                
                    ID = BillID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ID;
        }
        public int PurchaseExportBill(int BillID, string BillType, DateTime pDate, double pAmt, string loanNo, DateTime aDate,DateTime mDate,double CurrencyRate, SqlConnection con, SqlTransaction trans)
        {
            int ID = 0;
            SqlCommand com = null;
            try
            {
                string qstr = "UPDATE T_Export_Bill SET BillType=@BillType,LoanNo=@LoanNo, AcceptDate = @AcceptDate, MaturityDate = @MaturityDate, PurchaseDate = @PurchaseDate,PurchaseAmount = @PurchaseAmount,CurrencyRate=@CurrencyRate WHERE (BillID = @BillID)";
                com = new SqlCommand(qstr, con, trans);
                com.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
                com.Parameters.Add("@BillType", SqlDbType.VarChar, 500).Value = BillType;
                com.Parameters.Add("@LoanNo", SqlDbType.VarChar, 500).Value = loanNo;
                if (aDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = aDate;

                if (mDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = mDate;

                if (pDate == new DateTime(1900, 1, 1))
                    com.Parameters.Add("@PurchaseDate", SqlDbType.DateTime).Value = DBNull.Value;
                else
                    com.Parameters.Add("@PurchaseDate", SqlDbType.DateTime).Value = pDate;

                com.Parameters.Add("@PurchaseAmount", SqlDbType.Money).Value = pAmt;
                com.Parameters.Add("@CurrencyRate", SqlDbType.Money).Value = CurrencyRate;
                com.ExecuteNonQuery();

                ID = BillID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ID;
        }
        public int SaveUpdateBillAccount(BillAccount obBillAcc, SqlConnection con, SqlTransaction trans)
        {
            int ID = 0;
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("spSaveUpdateBillAccount", con, trans);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@SlNo", SqlDbType.Int).Value = obBillAcc.SlNo;
                com.Parameters.Add("@BillID", SqlDbType.Int).Value = obBillAcc.BillID;
                com.Parameters.Add("@AccountID", SqlDbType.Int).Value = obBillAcc.AccountID;
                com.Parameters.Add("@Particulars", SqlDbType.VarChar, 500).Value = obBillAcc.Particulars;
                com.Parameters.Add("@DebitAmount", SqlDbType.Money).Value = obBillAcc.DebitAmount;
                com.Parameters.Add("@CreditAmount", SqlDbType.Money).Value = obBillAcc.CreditAmount;
                com.Parameters.Add("@posted", SqlDbType.Int).Value = obBillAcc.Posted;
                com.Parameters.Add("@Ref", SqlDbType.VarChar, 50).Value = obBillAcc.Reference;
                com.Parameters.Add("@VoucherType", SqlDbType.VarChar, 100).Value = obBillAcc.VoucherType;
                com.Parameters.Add("@VoucherSlNo", SqlDbType.Int).Value = obBillAcc.VoucherSlNo;

                com.ExecuteNonQuery();
                //if (obBillAcc.SlNo == 0)
                //{
                //    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(SINo),0) FROM T_Bill_Accounts", con, trans);
                //    ID = Convert.ToInt32(cmd.ExecuteScalar());
                //}
                //else
                //    ID = obBillAcc.SlNo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ID;
        }

        public DataTable getCommercialInvoiceNos(SqlConnection con)
        {
            DataTable dt = null;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT ComInvoiceNo FROM T_Commercial_Documents WHERE CompanyID="+LogInInfo.CompanyID.ToString() +" Order by ComInvoiceNo", con);
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
        public ExportBill getExportBill(SqlConnection con, int ExportBillID)
        {
            ExportBill objExB=null;
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * FROM T_Export_Bill WHERE BillID=" + ExportBillID.ToString(), con);
                da.Fill(dt);
                da.Dispose();
                
                if (dt.Rows.Count == 0) return null;
                objExB = new ExportBill();
                objExB.BillID = dt.Rows[0].Field<int>("BillID");
               
                objExB.BillType = dt.Rows[0].Field<string>("BillType");
                objExB.BillNo = dt.Rows[0].Field<string>("TypeNo");
               
                objExB.LCID = dt.Rows[0].Field<object>("LCID") == DBNull.Value || dt.Rows[0].Field<object>("LCID") == null ? -1 : dt.Rows[0].Field<int>("LCID");
                objExB.LCValue = Convert.ToDouble( dt.Rows[0].Field<object>("LCValue"));
                objExB.CurrencyID = dt.Rows[0].Field<object>("CurrencyID") == DBNull.Value || dt.Rows[0].Field<object>("CurrencyID") == null ? -1 : dt.Rows[0].Field<int>("CurrencyID");
                objExB.LCQuantity = Convert.ToDouble(dt.Rows[0].Field<object>("LCQty"));
                objExB.UnitID = dt.Rows[0].Field<object>("UnitID") == DBNull.Value || dt.Rows[0].Field<object>("UnitID") == null ? -1 : dt.Rows[0].Field<int>("UnitID");
                objExB.BillDate = dt.Rows[0].Field<DateTime>("BillDate");
                objExB.BillAmount=Convert.ToDouble(dt.Rows[0].Field<object>("BillAmount"));
                
                objExB.LoanNo = dt.Rows[0].Field<string>("LoanNo");
                objExB.RealisedAmount = Convert.ToDouble( dt.Rows[0].Field<object>("RealisedAmt"));
                objExB.RealisedDate = dt.Rows[0].Field<object>("RealisedDate") == DBNull.Value || dt.Rows[0].Field<object>("RealisedDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("RealisedDate");
                objExB.RealisedLoss = Convert.ToDouble(dt.Rows[0].Field<object>("RealisedLoss"));
                objExB.AcceptDate = dt.Rows[0].Field<object>("AcceptDate") == DBNull.Value || dt.Rows[0].Field<object>("AcceptDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("AcceptDate");
                objExB.MaturityDate = dt.Rows[0].Field<object>("MaturityDate") == DBNull.Value || dt.Rows[0].Field<object>("MaturityDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("MaturityDate");
                objExB.remarks = dt.Rows[0].Field<string>("remarks");
                objExB.TransactionRefID = dt.Rows[0].Field<object>("TransRefID") == DBNull.Value || dt.Rows[0].Field<object>("TransRefID") == null ? -1 : dt.Rows[0].Field<int>("TransRefID");
                objExB.PurchaseDate = dt.Rows[0].Field<object>("PurchaseDate") == DBNull.Value || dt.Rows[0].Field<object>("PurchaseDate") == null ? new DateTime(1900, 1, 1) : dt.Rows[0].Field<DateTime>("PurchaseDate");
                objExB.PurchaseAmount = Convert.ToDouble(dt.Rows[0].Field<object>("PurchaseAmount"));
                objExB.CurrencyRate = Convert.ToDouble(dt.Rows[0].Field<object>("CurrencyRate"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objExB;
        }
        public DataTable getExportBills(SqlConnection con, string colNames, string BillNo, int realized,int purchase)
        {
            DataTable dt = new DataTable();
            try
            {
                string qstr = "Select " + colNames + " FROM T_Export_Bill WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " AND TypeNo Like '" + BillNo + "%' ";
                if (realized == 1) qstr += " AND RealisedDate IS NOT NULL ";
                else if (realized == 0) qstr += " AND RealisedDate IS NULL ";
                if (purchase == 1) qstr += " AND PurchaseDate IS NOT NULL ";
                else if (purchase == 0) qstr += " AND PurchaseDate IS  NULL ";
               qstr +=" ORDER BY TypeNo";
                SqlDataAdapter da = new SqlDataAdapter(qstr, con);
                da.Fill(dt);
                da.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable getExportBills(SqlConnection con,string colNames, string BillNo,DateTime startDate,DateTime endDate,int realized,int purchase)
        {
            DataTable dt = new DataTable();
            try
            {
                string qstr = "Select " + colNames + " FROM T_Export_Bill WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " AND TypeNo Like '" + BillNo + "%' AND BillDate BETWEEN @startDate AND @endDate ";
                if (realized==1) qstr += " AND RealisedDate IS NOT NULL ";
                else if (realized == 0) qstr += " AND RealisedDate IS  NULL ";
                if(purchase==1) qstr += " AND PurchaseDate IS NOT NULL ";
                else if (purchase == 0) qstr += " AND PurchaseDate IS  NULL ";
                qstr +=" ORDER BY TypeNo";
                SqlDataAdapter da = new SqlDataAdapter(qstr, con);
                da.SelectCommand.Parameters.Add("@startDate", SqlDbType.DateTime).Value = startDate;
                da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                da.Fill(dt);
                da.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable getExportBillAccount(SqlConnection con, int ExportBillID)
        {
            DataTable dt = null;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT  Posted, SlNo, BillID, B.AccountID, A.AccountTitle, DebitAmount AS Debit,CreditAmount AS Credit, Particulars,Ref FROM T_Bill_Accounts B INNER JOIN T_Account A ON B.AccountID=A.AccountID WHERE  ISNULL(ref,'')  LIKE 'First Voucher%' AND BillID=" + ExportBillID.ToString(), con);
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
        public DataTable getExportBillAccountAtRealization(SqlConnection con, int ExportBillID)
        {
            DataTable dt = null;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT VoucherType, Posted, SlNo, BillID, B.AccountID, A.AccountTitle, DebitAmount AS Debit,CreditAmount AS Credit, Particulars,Ref FROM T_Bill_Accounts B INNER JOIN T_Account A ON B.AccountID=A.AccountID WHERE  BillID=" + ExportBillID.ToString() + " AND ISNULL(ref,'') NOT LIKE 'First Voucher%' ORDER BY VoucherSlNo,SlNo", con);
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

        public void UpdateTransRefIDinExportBill(SqlConnection con, SqlTransaction trans, int BillID, int transMID)
        {
            try
            {
                SqlCommand com = new SqlCommand("SELECT TransRefID FROM T_Export_Bill WHERE BillID=@bID", con, trans);
                com.Parameters.Add("@bID", SqlDbType.Int).Value = BillID;
                int TRID = GlobalFunctions.isNull( com.ExecuteScalar(),0);
                string qstr = "UPDATE T_Export_Bill SET TransRefID=@refID WHERE BillID=@bID";
                SqlCommand cmd = new SqlCommand(qstr, con, trans);
                if (transMID <= 0)
                    cmd.Parameters.Add("@refID", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@refID", SqlDbType.Int).Value = transMID;
                cmd.Parameters.Add("@bID", SqlDbType.Int).Value = BillID;
                cmd.ExecuteNonQuery();

                new DaTransaction().DeleteTransaction(TRID, con);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRealizedTransRefIDinExportBill(SqlConnection con, SqlTransaction trans, int BillID, int transMID)
        {
            try
            {
                SqlCommand com = new SqlCommand("SELECT RealizedTransRefID FROM T_Export_Bill WHERE BillID=@bID", con, trans);
                com.Parameters.Add("@bID", SqlDbType.Int).Value = BillID;
                int TRID = GlobalFunctions.isNull(com.ExecuteScalar(), 0);
                SqlCommand cmd = new SqlCommand("UPDATE T_Export_Bill SET RealizedTransRefID=@refID WHERE BillID=@bID", con, trans);
                if (transMID <= 0)
                    cmd.Parameters.Add("@refID", SqlDbType.Int).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@refID", SqlDbType.Int).Value = transMID;
                cmd.Parameters.Add("@bID", SqlDbType.Int).Value = BillID;
                cmd.ExecuteNonQuery();
                new DaTransaction().DeleteTransaction(TRID, con);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateRealizedTransRefIDsinExportBill(SqlConnection con, SqlTransaction trans, int BillID, string  transMIDs)
        {
            try
            {
                SqlCommand com = new SqlCommand("SELECT RealizedTransRefIDs FROM T_Export_Bill WHERE BillID=@bID", con, trans);
                com.Parameters.Add("@bID", SqlDbType.Int).Value = BillID;
                string TRID = GlobalFunctions.isNull(com.ExecuteScalar(), "");
                SqlCommand cmd = new SqlCommand("UPDATE T_Export_Bill SET RealizedTransRefIDs=@refID WHERE BillID=@bID", con, trans);
                cmd.Parameters.Add("@refID", SqlDbType.VarChar,1000).Value = transMIDs;
                cmd.Parameters.Add("@bID", SqlDbType.Int).Value = BillID;
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlCommand cmnd=new SqlCommand("select distinct ID from dbo.fnSelectedIDList('" + TRID + "')",con,trans);
                SqlDataAdapter da = new SqlDataAdapter(cmnd);
                da.Fill(dt);
                da.Dispose();
              DaTransaction objDaT=  new DaTransaction();
              int ID;
              foreach (DataRow r in dt.Rows)
              {
                  ID = r.Field<int>("ID");
                  if(ID >0)
                  objDaT.DeleteTransaction(ID, con);
              }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteExportBillAccount(SqlConnection con, SqlTransaction trans, int SlNo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM T_Bill_Accounts WHERE SlNo=" + SlNo.ToString(), con, trans);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void InsertExportBillInvoices(SqlConnection con, SqlTransaction trans, ExportBillInvoices objInvoice)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spInsertExportBillInvoices", con, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BillID",SqlDbType.Int).Value=objInvoice.BillID;
                cmd.Parameters.Add("@InvoiceNo",SqlDbType.VarChar,500).Value=objInvoice.InvoiceNo;
                cmd.Parameters.Add("@InvoiceValue",SqlDbType.Money).Value=objInvoice.InvoiceValue;
                cmd.Parameters.Add("@InvoiceQty",SqlDbType.Money).Value=objInvoice.InvoiceQuantity;
                cmd.Parameters.Add("@ExpNo",SqlDbType.VarChar,500).Value=objInvoice.ExpNo;
                cmd.Parameters.Add("@Comment",SqlDbType.VarChar,500).Value=objInvoice.Comment;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteInvoices(SqlConnection con, SqlTransaction trans, int BillID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM T_ExportBill_Invoices WHERE BillID=@BillID", con, trans);
               
                cmd.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
                
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getExportBillInvoices(SqlConnection con, int BillID)
        {
            DataTable dt=new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT BillID, InvoiceNo, InvoiceQty, InvoiceValue, ExpNo, Comment FROM T_ExportBill_Invoices WHERE BillID=@BillID", con);
                da.SelectCommand.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return dt;
        }

        public double[] getAvaiableQtyValueForBill(SqlConnection con, int LCid)
        {
            double[] QtyValue;
            try
            {
                QtyValue = new double[2];
                DataTable dt=new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT  AvaiableQty, AvaiableValue FROM VW_LC_ExportBill_Info WHERE (LCID = @LCID) AND (CompanyID = @CompanyID)", con);
                da.SelectCommand.Parameters.Add("@LCID", SqlDbType.Int).Value = LCid;
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0)
                {
                    QtyValue[0] = 0;
                    QtyValue[1] = 0;
                }
                else
                {
                    QtyValue[0] = Convert.ToDouble(dt.Rows[0].Field<object>("AvaiableQty"));
                    QtyValue[1] = Convert.ToDouble(dt.Rows[0].Field<object>("AvaiableValue"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return QtyValue;
        }

        public void DeleteExportBill(SqlConnection con, SqlTransaction trans, int BillID, string Tab)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteExportBill", con, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Step", SqlDbType.VarChar, 100).Value = Tab;
                cmd.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public string getCurrency(SqlConnection con,
    }
}
