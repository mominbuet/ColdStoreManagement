using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.AccountingEntity;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.AccountingDataAccess
{
   public class DaTransaction : CSMSysConnection
    {
       public DaTransaction()
            : base()
        {

        }

       private int VoucherNoDigitLength = 6;

       public int SaveEditTransactionMaster(TransactionMaster objTM, SqlConnection con, SqlTransaction trans)
       {
           int ID = 0;
           try
           {

               SqlCommand com = new SqlCommand("spInsertUpdateTransMaster", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@TransMID", SqlDbType.Int).Value = objTM.TransactionMasterID;
               com.Parameters.Add("@TransDate", SqlDbType.DateTime).Value = objTM.TransactionDate;
               com.Parameters.Add("@VoucherNo", SqlDbType.VarChar, 100).Value = objTM.VoucherNo;
               com.Parameters.Add("@VoucherPayee", SqlDbType.VarChar, 500).Value = objTM.VoucherPayee;
               com.Parameters.Add("@VoucherType", SqlDbType.Int).Value = objTM.VoucherType;

               if (objTM.TransactionMethodID <= 0)
                   com.Parameters.Add("@TransMethodID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@TransMethodID", SqlDbType.Int).Value = objTM.TransactionMethodID;

               if (objTM.MethodRefID <= 0)
                   com.Parameters.Add("@MethodRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@MethodRefID", SqlDbType.Int).Value = objTM.MethodRefID;
               com.Parameters.Add("@MethodRefNo", SqlDbType.VarChar, 100).Value = objTM.MethodRefNo;
               com.Parameters.Add("@TransDescription", SqlDbType.VarChar, 1000).Value = objTM.TransactionDescription;
               com.Parameters.Add("@ApprovedBy", SqlDbType.VarChar, 100).Value = objTM.ApprovedBy;

               if (objTM.ApprovedDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@ApprovedDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ApprovedDate", SqlDbType.DateTime).Value = objTM.ApprovedDate;


               if (objTM.MethodRefID <= 0)
               {
                   com.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = objTM.CreatedBy;
                   com.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = objTM.CreatedDate;
                   com.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = objTM.ModifiedBy;
                   com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = objTM.ModifiedDate;
               }
               else
               {
                   com.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = objTM.CreatedBy;
                   com.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = objTM.CreatedDate;
                   com.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = objTM.ModifiedBy;
                   com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = objTM.ModifiedDate;
               }

               //com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               //com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               com.Parameters.Add("@Module", SqlDbType.VarChar, 500).Value = objTM.Module;
               //com.ExecuteNonQuery();

               if (objTM.TransactionMasterID <= 0)
               {
                   SqlParameter retValParam = com.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                   retValParam.Direction = System.Data.ParameterDirection.ReturnValue;

                   com.ExecuteNonQuery();

                   ID = Convert.ToInt32(retValParam.Value);
                   //SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TransMID),0) FROM T_Transaction_Master", con, trans);
                   //ID = Convert.ToInt32(cmd.ExecuteScalar());
               }
               else
               {
                   com.ExecuteNonQuery();
                   ID = objTM.TransactionMasterID;
                   // ID = Convert.ToInt32(com.Parameters["@TransMID"].Value);
               }
               com.Dispose();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           //finally
           //{
           //    con.Close();
           //}
           return ID;
       }

       public int SaveEditTransactionDetail(TransactionDetail objTD, SqlConnection con, SqlTransaction trans)
       {
           int ID = 0;
           try
           {
               SqlCommand com = new SqlCommand("spInsertUpdateTransDetail", con, trans);
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@TransDID", SqlDbType.Int).Value = objTD.TransactionDetailID;
               com.Parameters.Add("@TransMID", SqlDbType.Int).Value = objTD.TransactionMasterID;
               com.Parameters.Add("@AccountID", SqlDbType.Int).Value = objTD.TransactionAccountID;
               com.Parameters.Add("@CreditAmt", SqlDbType.Money).Value = objTD.CreditAmount;
               com.Parameters.Add("@DebitAmt", SqlDbType.Money).Value = objTD.DebitAmount;
               com.Parameters.Add("@Comments", SqlDbType.VarChar,500).Value = objTD.Comments;
               
               com.ExecuteNonQuery();

               //if (objTM.AccountID == 0)
               //    ID = ConnectionHelper.GetID(con, "AccountID", "T_Account");
               //else
               //    ID = objTM.AccountID;
               //ID = Convert.ToInt32(com.Parameters["@TransDID"].Value);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return ID;
       }


       public int InsertUpdateTransactionMaster(TransactionMaster objTM, SqlConnection con, TransactionDetail objTD)
       {
           //SqlTransaction transaction = null;
           int ID = 0;

           SqlCommand com = new SqlCommand("spInsertUpdateTransMaster", sqlConn);

           try
           {
               //transaction = sqlConn.BeginTransaction();

               com.CommandType = System.Data.CommandType.StoredProcedure;
               com.CommandType = CommandType.StoredProcedure;
               com.Parameters.Add("@TransMID", SqlDbType.Int).Value = objTM.TransactionMasterID;
               com.Parameters.Add("@TransDate", SqlDbType.DateTime).Value = objTM.TransactionDate;
               com.Parameters.Add("@VoucherNo", SqlDbType.VarChar, 100).Value = objTM.VoucherNo;
               com.Parameters.Add("@VoucherPayee", SqlDbType.VarChar, 500).Value = objTM.VoucherPayee;
               com.Parameters.Add("@VoucherType", SqlDbType.Int).Value = objTM.VoucherType;

               if (objTM.TransactionMethodID <= 0)
                   com.Parameters.Add("@TransMethodID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@TransMethodID", SqlDbType.Int).Value = objTM.TransactionMethodID;

               if (objTM.MethodRefID <= 0)
                   com.Parameters.Add("@MethodRefID", SqlDbType.Int).Value = DBNull.Value;
               else
                   com.Parameters.Add("@MethodRefID", SqlDbType.Int).Value = objTM.MethodRefID;
               com.Parameters.Add("@MethodRefNo", SqlDbType.VarChar, 100).Value = objTM.MethodRefNo;
               com.Parameters.Add("@TransDescription", SqlDbType.VarChar, 1000).Value = objTM.TransactionDescription;
               com.Parameters.Add("@ApprovedBy", SqlDbType.VarChar, 100).Value = objTM.ApprovedBy;

               if (objTM.ApprovedDate == new DateTime(1900, 1, 1))
                   com.Parameters.Add("@ApprovedDate", SqlDbType.DateTime).Value = DBNull.Value;
               else
                   com.Parameters.Add("@ApprovedDate", SqlDbType.DateTime).Value = objTM.ApprovedDate;


               if (objTM.TransactionMasterID <= 0)
               {
                   com.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = objTM.CreatedBy;
                   com.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = objTM.CreatedDate;
                   com.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = objTM.ModifiedBy;
                   com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = objTM.ModifiedDate;
               }
               else
               {
                   com.Parameters.Add("@CreatedBy", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                   com.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DBNull.Value;
                   com.Parameters.Add("@ModifiedBy", SqlDbType.UniqueIdentifier).Value = objTM.ModifiedBy;
                   com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = objTM.ModifiedDate;
               }

               //com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               //com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
               com.Parameters.Add("@Module", SqlDbType.VarChar, 500).Value = objTM.Module;
               //com.ExecuteNonQuery();

               if (objTM.TransactionMasterID <= 0)
               {
                   SqlParameter retValParam = com.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                   retValParam.Direction = System.Data.ParameterDirection.ReturnValue;

                   com.ExecuteNonQuery();

                   ID = Convert.ToInt32(retValParam.Value);
                   //SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TransMID),0) FROM T_Transaction_Master", con, trans);
                   //ID = Convert.ToInt32(cmd.ExecuteScalar());
               }
               else
               {
                   com.ExecuteNonQuery();
                   ID = objTM.TransactionMasterID;
                   // ID = Convert.ToInt32(com.Parameters["@TransMID"].Value);
               }

               if (objTD != null)
               {
                   SqlCommand comd = new SqlCommand("spInsertUpdateTransDetail", con);
                   comd.CommandType = CommandType.StoredProcedure;
                   comd.Parameters.Add("@TransDID", SqlDbType.Int).Value = objTD.TransactionDetailID;
                   comd.Parameters.Add("@TransMID", SqlDbType.Int).Value = ID;
                   comd.Parameters.Add("@AccountID", SqlDbType.Int).Value = objTD.TransactionAccountID;
                   comd.Parameters.Add("@CreditAmt", SqlDbType.Money).Value = objTD.CreditAmount;
                   comd.Parameters.Add("@DebitAmt", SqlDbType.Money).Value = objTD.DebitAmount;
                   comd.Parameters.Add("@Comments", SqlDbType.VarChar, 500).Value = objTD.Comments;

                   comd.ExecuteNonQuery();
                   comd.Dispose();
               }

               //transaction.Commit();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               com.Dispose();
               sqlConn.Close();
           }

           return ID;
       }

       public TransactionMaster getTransMaster(SqlConnection con, int numTMID)
       {
           TransactionMaster objTM = null;

           try
           {
               if (con == null)
               {
                   con = ConnectionHelper.getConnection();
               }
               if (con.State != ConnectionState.Open) con.Open();
               DataTable dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM T_Transaction_Master WHERE TransMID=" + numTMID.ToString(), con);
               da.Fill(dt);
               da.Dispose();

               if (dt.Rows.Count == 0) return null;
               objTM = new TransactionMaster();
               objTM.TransactionMasterID = dt.Rows[0].Field<int>("TransMID");
               objTM.TransactionDate = dt.Rows[0].Field<DateTime>("TransDate");
               objTM.VoucherNo = dt.Rows[0].Field<string>("VoucherNo");
               objTM.VoucherPayee = dt.Rows[0].Field<string>("VoucherPayee");
               objTM.VoucherType = dt.Rows[0].Field<int>("VoucherType");
               objTM.TransactionMethodID = dt.Rows[0].Field<object>("TransMethodID") == DBNull.Value || dt.Rows[0].Field<object>("TransMethodID") == null ? -1 : dt.Rows[0].Field<int>("TransMethodID");
               objTM.MethodRefID = dt.Rows[0].Field<object>("MethodRefID") == DBNull.Value || dt.Rows[0].Field<object>("MethodRefID") ==null ? -1 : dt.Rows[0].Field<int>("MethodRefID");
               objTM.MethodRefNo = dt.Rows[0].Field<string>("MethodRefNo");
               objTM.TransactionDescription = dt.Rows[0].Field<string>("TransDescription");

               objTM.ApprovedBy = dt.Rows[0].Field<string>("approvedBy");

               if (dt.Rows[0].Field<object>("approvedDate") == DBNull.Value || dt.Rows[0].Field<object>("approvedDate")==null)
                   objTM.ApprovedDate = new DateTime(1900, 1, 1);
               else
                   objTM.ApprovedDate = dt.Rows[0].Field<DateTime>("approvedDate");
               objTM.Module = dt.Rows[0].Field<object>("Module") == DBNull.Value || dt.Rows[0].Field<object>("Module") == null ? "" : dt.Rows[0].Field<string>("Module");
               //objTM.CompanyID = dt.Rows[0].Field<object>("CompanyID") == DBNull.Value || dt.Rows[0].Field<object>("CompanyID") == null ? -1 : dt.Rows[0].Field<int>("CompanyID");
           }
      
           catch (Exception ex)
           {
               throw ex;
           }

           return objTM;
       }
       public DataTable getTransAccounts(SqlConnection con, int numTMID, string DebitOrCredit)
       {
           DataTable dt = null;
           try
           {
               if (con == null)
               {
                   con = ConnectionHelper.getConnection();
               }
               if (con.State != ConnectionState.Open) con.Open();
               string qstr=string.Empty;
               if(DebitOrCredit.ToLower()=="debit")
                   qstr = "SELECT TransDID,T.AccountID,A.AccountNo,A.AccountTitle,DebitAmt AS Amount FROM T_Transaction_Detail AS T INNER JOIN VW_Account AS A ON T.AccountID=A.AccountID  WHERE DebitAmt <> 0 AND TransMID= @TransMID Order By AccountTitle";
               else if(DebitOrCredit.ToLower()=="credit")
                   qstr = "SELECT TransDID,T.AccountID,A.AccountNo,A.AccountTitle,CreditAmt AS Amount FROM T_Transaction_Detail AS T INNER JOIN VW_Account AS A ON T.AccountID=A.AccountID  WHERE CreditAmt <> 0 AND TransMID= @TransMID Order By AccountTitle";
               else
                   qstr = "SELECT TransDID,T.AccountID,A.AccountNo,A.AccountTitle,DebitAmt AS Debit,CreditAmt AS Credit FROM T_Transaction_Detail AS T INNER JOIN VW_Account AS A ON T.AccountID=A.AccountID WHERE  TransMID= @TransMID  Order By CreditAmt,AccountTitle";
               dt = new DataTable();
               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               da.SelectCommand.Parameters.Add("@TransMID", SqlDbType.Int).Value = numTMID;
               da.Fill(dt);
               da.Dispose();
               return dt;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public DataTable getVoucher(SqlConnection con, int VoucherType, DateTime stDate, DateTime edDate)
       {
           DataTable dt = null;
           try
           {
               string qstr = "SELECT M.TransMID,VoucherType,M.VoucherNo,M.TransDate,SUM(D.CreditAmt)AS Amount,M.Module FROM T_Transaction_Master AS M INNER JOIN "
                           + " T_Transaction_Detail AS D ON M.TransMID = D.TransMID WHERE(M.VoucherType = @VoucherType OR @VoucherType = 0) AND (M.CompanyID=@CompanyID) GROUP BY M.TransMID, M.VoucherNo, M.TransDate,VoucherType,Module HAVING (M.TransDate BETWEEN @startDate AND @endDate)"
                           + " Order by M.TransDate,VoucherNo";
               SqlDataAdapter da = new SqlDataAdapter(qstr, con);
               da.SelectCommand.Parameters.Add("@VoucherType", SqlDbType.Int).Value = VoucherType;
               da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               da.SelectCommand.Parameters.Add("@startDate", SqlDbType.DateTime).Value = stDate;
               da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = edDate;
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

       public string getVoucherNo(SqlConnection con, string preFix)
       {
           string Vno = string.Empty;
           try
           {
               SqlCommand cmd = new SqlCommand("SELECT ISNULL((SELECT SUBSTRING(MAX(VoucherNo), 2, 50) + 1 FROM T_Transaction_Master WHERE CompanyID=@CompanyID AND (VoucherNo LIKE @DCJ + '%')),1)", con);
               cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               cmd.Parameters.Add("@DCJ", SqlDbType.VarChar, 5).Value = preFix;
               Vno = cmd.ExecuteScalar().ToString();
               Vno = preFix.ToUpper() + Vno.PadLeft(VoucherNoDigitLength, '0');

           }
           catch (Exception ex)
           {
               //return string.Empty;
               throw ex;
           }
           return Vno;
       }
       public string getVoucherNo(SqlConnection con, SqlTransaction trans,string preFix)
       {
           string Vno = string.Empty;
           try
           {
               SqlCommand cmd = new SqlCommand("SELECT ISNULL((SELECT SUBSTRING(MAX(VoucherNo), 2, 50) + 1 FROM T_Transaction_Master WHERE CompanyID=@CompanyID AND (VoucherNo LIKE @DCJ + '%')),1)", con,trans);
               cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
               cmd.Parameters.Add("@DCJ", SqlDbType.VarChar, 5).Value = preFix;
               Vno = cmd.ExecuteScalar().ToString();
               Vno = preFix.ToUpper() + Vno.PadLeft(VoucherNoDigitLength, '0');

           }
           catch (Exception ex)
           {
               return string.Empty;
               throw ex;
           }
           return Vno;
       }

       public void DeleteTransDetail(SqlConnection con, int TDID)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("spDeleteTransAccount", sqlConn);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@TransDID", SqlDbType.Int).Value = TDID;
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void DeleteTransAccount(int TDID, SqlConnection con, SqlTransaction trans)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("spDeleteTransAccount", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@TransDID", SqlDbType.Int).Value = TDID;
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       public void DeleteTransaction(int TMID, SqlConnection con)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("spDeleteTransaction", con);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@TransMID", SqlDbType.Int).Value = TMID;
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void DeleteTransDetail(int TMID, SqlConnection con, SqlTransaction trans)
       {
           try
           {
               SqlCommand cmd = new SqlCommand("spDeleteTransDetail", con, trans);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.Add("@TransMID", SqlDbType.Int).Value = TMID;
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public int getTransDID(SqlConnection con, int TransMID, int AccountID)
       {
           int TransDID=-1;
           try
           {
               SqlCommand cmd = new SqlCommand("Select dbo.fnGetTransDID( @TransMID,@AccountID)", con);
               cmd.Parameters.Add("@TransMID", SqlDbType.Int).Value = TransMID;
               cmd.Parameters.Add("@AccountID", SqlDbType.Int).Value = AccountID;
              TransDID=(int) cmd.ExecuteScalar();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return TransDID;
       }
       public int getTransDID(SqlConnection con, SqlTransaction trans,int TransMID, int AccountID)
       {
           int TransDID = -1;
           try
           {
               SqlCommand cmd = new SqlCommand("Select dbo.fnGetTransDID( @TransMID,@AccountID)", con,trans);
               cmd.Parameters.Add("@TransMID", SqlDbType.Int).Value = TransMID;
               cmd.Parameters.Add("@AccountID", SqlDbType.Int).Value = AccountID;
               TransDID = (int)cmd.ExecuteScalar();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
           return TransDID;
       }
    }
}
