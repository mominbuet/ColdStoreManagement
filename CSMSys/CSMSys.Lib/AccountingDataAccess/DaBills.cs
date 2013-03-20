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
   public class DaBills
    {
       public DaBills() { }


       public int SaveUpdateBills(SqlConnection con, SqlTransaction trans, Bills objBills)
       {
           int ID = 0;
           try
           {
               SqlCommand cmd = new SqlCommand("spSaveUpdateBills", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@BillsID", SqlDbType.Int).Value=objBills.BillsID;
               cmd.Parameters.Add("@BillType", SqlDbType.VarChar, 100).Value = objBills.BillsType;
               cmd.Parameters.Add("@CustSuppAccID", SqlDbType.Int).Value = objBills.CustomerSupplierAccountID;
               cmd.Parameters.Add("@InvoiceID", SqlDbType.Int).Value = objBills.RefInvoiceID;
               cmd.Parameters.Add("@InvoiceNo", SqlDbType.VarChar, 100).Value = objBills.RefInvoiceNo;
               cmd.Parameters.Add("@BillAmount", SqlDbType.Money).Value = objBills.BillAmount;
               cmd.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = objBills.BillDate;
               cmd.Parameters.Add("@DueAmount", SqlDbType.Money).Value = objBills.DueAmount;
               cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objBills.Remarks;
               cmd.Parameters.Add("@Module", SqlDbType.VarChar, 100).Value = objBills.Module;
               cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               cmd.Parameters.Add("@BillForAccID", SqlDbType.Int).Value = objBills.BillForAccountID;
               cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objBills.CurrencyID;
               cmd.Parameters.Add("@CurrencyRate", SqlDbType.Money).Value = objBills.CurrencyRate;
               cmd.Parameters.Add("@LCID", SqlDbType.Int).Value = objBills.LCID;
               cmd.Parameters.Add("@BillQty", SqlDbType.Money).Value = objBills.BillQuantity;
               cmd.Parameters.Add("@TransRefID", SqlDbType.Int).Value = objBills.TransRefID;
               cmd.Parameters.Add("@BillForAcc2ID", SqlDbType.Int).Value = objBills.BillForAccount2ID;
               cmd.ExecuteNonQuery();

               if (objBills.BillsID == 0)
               {
                   SqlCommand com = new SqlCommand("SELECT ISNULL(MAX(BillsID),0) FROM T_Bills", con, trans);
                   ID = Convert.ToInt32(com.ExecuteScalar());
               }
               else
                   ID = objBills.BillsID;
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return ID;
       }

       public int SaveUpdateBillsAdjust(SqlConnection con, SqlTransaction trans, BillAdjust objBillsA)
       {
           int ID = 0;
           try
           {
               SqlCommand cmd = new SqlCommand("spSaveUpdateBillAdjust", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@BillAdjustID", SqlDbType.Int).Value = objBillsA.BillAdjustID;
               cmd.Parameters.Add("@BillsID", SqlDbType.Int).Value = objBillsA.BillsID;
               cmd.Parameters.Add("@AdjustDate", SqlDbType.DateTime).Value = objBillsA.AdjustDate;
               cmd.Parameters.Add("@AdjustAmount", SqlDbType.Money).Value = objBillsA.AdjustAmount;
               cmd.Parameters.Add("@AdjustAccID", SqlDbType.Int).Value = objBillsA.AdjustAccountID;
               cmd.Parameters.Add("@AdjustMethodID", SqlDbType.Int).Value = objBillsA.AdjustMethodID;
               if (objBillsA.AdjustRefLCID <= 0)
                   cmd.Parameters.Add("@AdjustRefLCID", SqlDbType.Int).Value = DBNull.Value;
               else
                   cmd.Parameters.Add("@AdjustRefLCID", SqlDbType.Int).Value = objBillsA.AdjustRefLCID;
               cmd.Parameters.Add("@AdjustRefNo", SqlDbType.VarChar, 100).Value = objBillsA.AdjustRefNo;
               cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objBillsA.Remarks;
               if (objBillsA.TransRefID <= 0)
                   cmd.Parameters.Add("@TransRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   cmd.Parameters.Add("@TransRefID", SqlDbType.Int).Value = objBillsA.TransRefID;
               cmd.Parameters.Add("@TransVoucherNo", SqlDbType.VarChar, 100).Value = objBillsA.TransVoucherNo;

               if (objBillsA.AcceptDate == new DateTime(1900, 1, 1))
                   cmd.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   cmd.Parameters.Add("@AcceptDate", SqlDbType.DateTime).Value = objBillsA.AcceptDate;

               if (objBillsA.MaturityDate == new DateTime(1900, 1, 1))
                   cmd.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   cmd.Parameters.Add("@MaturityDate", SqlDbType.DateTime).Value = objBillsA.MaturityDate;

               cmd.Parameters.Add("@BillAccID", SqlDbType.Int).Value = objBillsA.BillAccountID;
               cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objBillsA.CurrencyID;
               cmd.Parameters.Add("@CurrencyRate", SqlDbType.Money).Value = objBillsA.CurrencyRate;
               cmd.ExecuteNonQuery();


               //if (objBillsA.BillAdjustID == 0)
               //{
               //    SqlCommand com = new SqlCommand("SELECT ISNULL(MAX(BillAdjustID),0) FROM T_Bills_Adjust", con, trans);
               //    ID = Convert.ToInt32(com.ExecuteScalar());
               //}
               //else
               //    ID = objBillsA.BillAdjustID;
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return ID;
       }
       public DataTable getBills(SqlConnection con, string BillType, int CustSuppAccID, bool all)
       {
           DataTable dt = null;
           try
           {
               string qstr = "SELECT BillsID, InvoiceID, InvoiceNo, BillAmount, BillDate, DueAmount, null as AdjustNow,LCNo,T_Bills.Remarks, Module FROM  T_Bills LEFT OUTER JOIN T_LC_Master ON T_Bills.LCID=T_LC_Master.LCID  WHERE  (BillType LIKE @BillType ) AND (CustSuppAccID = @CustSuppAccID) AND (T_Bills.CompanyID = @CompanyID)  AND (DueAmount <> 0 OR  @all = 1) ";
               

               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               da.SelectCommand.Parameters.Add("@BillType", SqlDbType.VarChar, 100).Value = BillType + "%";
               da.SelectCommand.Parameters.Add("@CustSuppAccID", SqlDbType.Int).Value = CustSuppAccID;
               da.SelectCommand.Parameters.Add("@all",SqlDbType.Int).Value = all ? 1 : 0;
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
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

       public DataTable getInvoice(SqlConnection con, int LciD)
       {
           DataTable dt = null;
           try
           {
               string qstr = "SELECT ComInvoiceID, ComInvoiceNo AS InvoiceNo, ComInvoiceDate, ExpNo, ExpDate, TotalQty AS InvoiceQty, TotalValue AS InvoiceValue, Remarks AS Comment FROM  T_Commercial_Documents WHERE (LCID = @LCID)";
               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               da.SelectCommand.Parameters.Add("@LCID", SqlDbType.Int).Value = LciD;
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

       public DataTable getBills(SqlConnection con, string BillType, int CustSuppAccID, bool LCbill,string Option)
       {
           DataTable dt = null;
           try
           {
               string qstr = "SELECT BillsID, InvoiceID, InvoiceNo,BillAmount AS BillAmountTK,BillAmount/(CASE WHEN CurrencyRate IS NULL OR CurrencyRate=0 THEN 1 ELSE CurrencyRate END) AS BillAmount, BillDate, DueAmount, null as AdjustNow,LCNo,T_Bills.Remarks, Module,T_Bills.CurrencyRate,T_Bills.CurrencyID FROM  T_Bills LEFT OUTER JOIN T_LC_Master ON T_Bills.LCID=T_LC_Master.LCID  WHERE  (BillType LIKE @BillType ) AND (CustSuppAccID = @CustSuppAccID) AND (T_Bills.CompanyID = @CompanyID) ";
               // AND (DueAmount <> 0 OR  @all = 1) ";
               if (LCbill == true) qstr += " AND T_Bills.LCID IS NOT NULL ";
               if (Option == "PAID") qstr += " AND T_Bills.DueAmount =0 ";
               else if (Option == "UNPAID") qstr += " AND T_Bills.DueAmount <> 0";
               qstr += " Order By BillDate";
               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               da.SelectCommand.Parameters.Add("@BillType",SqlDbType.VarChar,100).Value = BillType + "%";
               da.SelectCommand.Parameters.Add("@CustSuppAccID",SqlDbType.Int).Value = CustSuppAccID;
               //da.SelectCommand.Parameters.Add("@all",SqlDbType.Int).Value = all ? 1 : 0;
               da.SelectCommand.Parameters.Add("@CompanyID",SqlDbType.Int).Value = LogInInfo.CompanyID;
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

       public DataTable getBillAdjusts(SqlConnection con,int BillsID)
       {
           DataTable dt = null;
           try
           {
               string qstr = "SELECT BA.BillAdjustID,BA.AdjustDate, BA.AdjustAmount,TransRefID,TransVoucherNo,A.AccountTitle ";
               qstr += " FROM T_Bills_Adjust AS BA INNER JOIN  T_Account AS A ON BA.AdjustAccID = A.AccountID INNER JOIN T_TransactionMethod AS M ON BA.AdjustMethodID = M.TransMethodID WHERE (BA.BillsID = @BillsID)";

               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               da.SelectCommand.Parameters.Add("@BillsID", SqlDbType.Int).Value = BillsID;
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
       public BillAdjust getBillAdjust(SqlConnection con, int BillsAdjID)
       {
           BillAdjust objBAdj = null;
           try
           {

               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Bills_Adjust WHERE BillAdjustID = @BillAdjustID", con);
               da.SelectCommand.Parameters.Add("@BillAdjustID", SqlDbType.Int).Value = BillsAdjID;
              DataTable  dt = new DataTable();
               da.Fill(dt);
               da.Dispose();

               if (dt.Rows.Count == 0) return null;
               objBAdj = new BillAdjust();
               objBAdj.BillAdjustID = dt.Rows[0].Field<int>("BillAdjustID");
               objBAdj.BillsID = dt.Rows[0].Field<int>("BillsID");
               objBAdj.AdjustDate = dt.Rows[0].Field<DateTime>("AdjustDate");
               objBAdj.AdjustAmount = GlobalFunctions.isNull( dt.Rows[0].Field<object>("AdjustAmount"),(double)0.0);
               objBAdj.AdjustAccountID = GlobalFunctions.isNull( dt.Rows[0].Field<object>("AdjustAccID"),-1);
               objBAdj.AdjustMethodID = GlobalFunctions.isNull( dt.Rows[0].Field<object>("AdustMethodID"),-1);
               objBAdj.AdjustRefNo =GlobalFunctions.isNull( dt.Rows[0].Field<object>("AdjustRefNo"),string.Empty);
               objBAdj.Remarks = GlobalFunctions.isNull( dt.Rows[0].Field<object>("Remarks"),string.Empty);
               objBAdj.TransRefID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("TransRefID"), -1);
               objBAdj.TransVoucherNo = GlobalFunctions.isNull( dt.Rows[0].Field<object>("TransVoucherNo"),string.Empty);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return objBAdj;
       }

       public DataTable getBills(SqlConnection con, int CustSuppID,int LCID,DateTime sDate,DateTime eDate)
       {
           DataTable dt = new DataTable();
           try
           {
               string qstr = "SELECT BillsID,InvoiceNo,BillAmount,BillDate,T_Bills.Remarks,LCNo,AccountTitle AS Supplier,TransRefID " +
                            " FROM T_Bills INNER JOIN T_Account ON T_Bills.CustSuppAccID = T_Account.AccountID LEFT OUTER JOIN T_LC_Master ON T_Bills.LCID = T_LC_Master.LCID " +
                            " WHERE T_Bills.BillType LIKE 'Bills Payable' AND InvoiceID = 0 AND T_Bills.CompanyID =" + LogInInfo.CompanyID.ToString();
               if( CustSuppID >0)
             qstr +=" AND T_Bills.CustSuppAccID = "+CustSuppID.ToString();
             if(sDate !=new DateTime(1900,1,1) && eDate!=new DateTime(1900,1,1))  
             qstr +=" AND T_Bills.BillDate BETWEEN @startDate AND @endDate ";
             if(LCID >0)   
             qstr +="  AND T_Bills.LCID = " + LCID.ToString();

             SqlDataAdapter da = new SqlDataAdapter(qstr, con);
             if (sDate != new DateTime(1900, 1, 1) && eDate != new DateTime(1900, 1, 1))
             {
                 da.SelectCommand.Parameters.Add("@startDate", SqlDbType.DateTime).Value = sDate;
                 da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = eDate;
             }
             da.Fill(dt);
             da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return dt;
       }
       public DataTable getReceivableBills(SqlConnection con, int CustSuppID, int LCID, DateTime sDate, DateTime eDate)
       {
           DataTable dt = new DataTable();
           try
           {
               string qstr = "SELECT BillsID,InvoiceNo,BillAmount,BillDate,T_Bills.Remarks,LCNo,AccountTitle AS Customer,TransRefID " +
                            " FROM T_Bills INNER JOIN T_Account ON T_Bills.CustSuppAccID = T_Account.AccountID LEFT OUTER JOIN T_LC_Master ON T_Bills.LCID = T_LC_Master.LCID " +
                            " WHERE T_Bills.BillType LIKE 'Bills Receivable' AND InvoiceID = 0  AND T_Bills.CompanyID =" + LogInInfo.CompanyID.ToString();
               if (CustSuppID > 0)
                   qstr += " AND T_Bills.CustSuppAccID = " + CustSuppID.ToString();
               if (sDate != new DateTime(1900, 1, 1) && eDate != new DateTime(1900, 1, 1))
                   qstr += " AND T_Bills.BillDate BETWEEN @startDate AND @endDate ";
               if (LCID > 0)
                   qstr += "  AND T_Bills.LCID = " + LCID.ToString();

               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               if (sDate != new DateTime(1900, 1, 1) && eDate != new DateTime(1900, 1, 1))
               {
                   da.SelectCommand.Parameters.Add("@startDate", SqlDbType.DateTime).Value = sDate;
                   da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = eDate;
               }
               da.Fill(dt);
               da.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return dt;
       }
       public Bills getBill(SqlConnection con,int BillID)
       {
           Bills objBill = new Bills();
           try
           {
               DataTable dt=new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Bills WHERE BillsID=" + BillID.ToString(), con);
               da.Fill(dt);
               da.Dispose();
               if (dt.Rows.Count == 0) return null;

               objBill.BillsID = GlobalFunctions.isNull( dt.Rows[0].Field<object>("BillsID"),0);
               objBill.BillsType = GlobalFunctions.isNull(dt.Rows[0].Field<object>("BillType"),"");
               objBill.CustomerSupplierAccountID =GlobalFunctions.isNull( dt.Rows[0].Field<object>("CustSuppAccID"),0);
               objBill.RefInvoiceID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("InvoiceID"), 0);
               objBill.RefInvoiceNo = GlobalFunctions.isNull(dt.Rows[0].Field<object>("InvoiceNo"), "");
               objBill.BillAmount = GlobalFunctions.isNull(dt.Rows[0].Field<object>("BillAmount"), 0.0);
               objBill.BillDate = GlobalFunctions.isNull(dt.Rows[0].Field<object>("BillDate"), new DateTime(1900, 1, 1));
               objBill.DueAmount = GlobalFunctions.isNull(dt.Rows[0].Field<object>("DueAmount"), 0.0);
               objBill.Remarks = GlobalFunctions.isNull(dt.Rows[0].Field<object>("Remarks"), "");
               objBill.Module = GlobalFunctions.isNull(dt.Rows[0].Field<object>("Module"), "");
               objBill.BillForAccountID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("BillForAccID"), 0);
               objBill.CurrencyID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("CurrencyID"), 0);
               objBill.CurrencyRate = GlobalFunctions.isNull(dt.Rows[0].Field<object>("CurrencyRate"), 0.0);
               objBill.LCID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("LCID"), 0);
               objBill.BillQuantity = GlobalFunctions.isNull(dt.Rows[0].Field<object>("BillQty"), 0.0);
               objBill.TransRefID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("TransRefID"), 0);
               objBill.BillForAccount2ID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("BillForAcc2ID"), 0);

               return objBill;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void DeleteBill(SqlConnection con, SqlTransaction trans, int BillID)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("DELETE FROM T_Bills WHERE BillsID=" + BillID.ToString(), con, trans);
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteBillAdjust(SqlConnection con, SqlTransaction trans, int AdjustID)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("DELETE FROM T_Import_BillLCAdjust WHERE AdjustID=" + AdjustID.ToString(), con, trans);
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteBillAdjust(SqlConnection con, int AdjustID)
       {
           SqlTransaction trans = null;
           try
           {
               trans = con.BeginTransaction();
               SqlCommand cmd = new SqlCommand("spDeleteBillAdjust", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@BillAdjustID", SqlDbType.Int).Value = AdjustID;
               cmd.ExecuteNonQuery();
               trans.Commit();
           }
           catch (Exception ex)
           {
               if (trans != null) trans.Rollback();
               throw ex;
           }
       }

       public DataTable getBillPaysForAdjust(SqlConnection con, int billOfAccID, bool isLCs, bool isAll)
       {
           DataTable dt = null;
           try
           {
               //string qstr = "SELECT  BA.BillAdjustID, BA.BillsID, BA.AdjustDate AS PayDate, BA.AdjustAmount AS PayAmtTK, BA.AdjustAmount/BA.CurrencyRate AS PayAmtFC, A2.AccountTitle AS PaidFrom, " +
               //          " BA.AdjustRefLCID,LC.LCNo AS PayFromLC, A1.AccountTitle AS BillOf,BA.CurrencyRate,BA.CurrencyID FROM T_Bills_Adjust AS BA LEFT OUTER JOIN T_LC_Master AS LC ON BA.AdjustRefLCID = LC.LCID " +
               //          " LEFT OUTER JOIN T_Account AS A2 ON BA.AdjustAccID = A2.AccountID LEFT OUTER JOIN T_Account AS A1 ON BA.BillAccID = A1.AccountID WHERE 1=1 ";
               //if (billOfAccID > 0)
               //    qstr += " AND ISNULL(BA.BillAccID,0) =" + billOfAccID.ToString();
               //if (isLCs == true)
               //    qstr += " AND BA.AdjustRefLCID IS NOT NULL ";
               //qstr += " ORDER BY PayDate"; ;              

               SqlDataAdapter da = new SqlDataAdapter("spSelectBillPaymentsForLCAdjust", con);
               da.SelectCommand.CommandType = CommandType.StoredProcedure;
               da.SelectCommand.Parameters.Add("@SupplierAccID", SqlDbType.Int).Value = billOfAccID;
               da.SelectCommand.Parameters.Add("@isLCPay", SqlDbType.Int).Value = isLCs ? 1 : 0;
               da.SelectCommand.Parameters.Add("@isAll", SqlDbType.Int).Value = isAll ? 1 : 0;
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

       public int SaveUpdateImport_Bill(SqlConnection con, SqlTransaction trans, Import_Bill_LCAdjust objImportBill)
       {
           int ID = 0;
           try
           {
               SqlCommand cmd = new SqlCommand("spSaveUpdateImport_BillLCAdjust", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@AdjustID", SqlDbType.Int).Value = objImportBill.AdjustID;
               cmd.Parameters.Add("@BillPayID", SqlDbType.Int).Value = objImportBill.BillPayID;
               cmd.Parameters.Add("@AdjustDate", SqlDbType.DateTime).Value = objImportBill.AdjustDate;
               cmd.Parameters.Add("@AdjustAmount", SqlDbType.Money).Value = objImportBill.AdjustAmount;
               cmd.Parameters.Add("@CurrencyID", SqlDbType.Int).Value = objImportBill.CurrencyID;
               cmd.Parameters.Add("@CurrencyRate", SqlDbType.Money).Value = objImportBill.CurrencyRate;
               cmd.Parameters.Add("@PayFromAccID", SqlDbType.Int).Value = objImportBill.PayFromAccID;
               cmd.Parameters.Add("@AdjustFromAccID", SqlDbType.Int).Value = objImportBill.AdjustFromAccID;
               cmd.Parameters.Add("@TransRefID", SqlDbType.Int).Value = objImportBill.TransRefID;
               cmd.Parameters.Add("@LCID", SqlDbType.Int).Value = objImportBill.LCID;
               cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 500).Value = objImportBill.Remarks;
              
               cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;


               cmd.ExecuteNonQuery();

               //if (objImportBill.AdjustID == 0)
               //{
               //    SqlCommand com = new SqlCommand("SELECT ISNULL(MAX(AdjustID),0) FROM T_Import_BillLCAdjust", con, trans);
               //    ID = Convert.ToInt32(com.ExecuteScalar());
               //}
               //else
               //    ID = objImportBill.AdjustID;
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return ID;
       }

       public DataTable getLcAdjusts(SqlConnection con, int BillPayID)
       {
           DataTable dt = new DataTable();
           try
           {
               string qstr = "SELECT   Adj.AdjustID, Adj.BillPayID, Adj.AdjustDate, Adj.AdjustAmount, Acc.AccountTitle AS AdjustFromAccount, Adj.Remarks, Adj.TransRefID " +
                             " FROM  T_Import_BillLCAdjust AS Adj INNER JOIN T_Account AS Acc ON Adj.AdjustFromAccID = Acc.AccountID " +
                             " WHERE Adj.BillPayID = " + BillPayID.ToString() + " ORDER BY Adj.AdjustDate";
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

    }
}
