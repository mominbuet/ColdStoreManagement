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
    public class DaLoan
    {
        public DaLoan() { }
        public int SaveUpdate(Loan objloan, SqlConnection conn)
        {
            SqlCommand com = new SqlCommand();
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                com.Connection = conn;
                com.Transaction = trans;
                com.CommandText = "spSaveUpdateLoan";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@LoanID", SqlDbType.Int).Value = objloan.LoanID;
                com.Parameters.Add("@LoanNo", SqlDbType.VarChar, 50).Value = objloan.LoanNo;
                com.Parameters.Add("@RefAccountID", SqlDbType.Int).Value = objloan.RefAccID;
                com.Parameters.Add("@LoanAmount", SqlDbType.Money).Value = objloan.LoanAmount;
                com.Parameters.Add("@ApplyDate", SqlDbType.DateTime).Value = objloan.ApplyDate;
                com.Parameters.Add("@SanctionDate", SqlDbType.DateTime).Value = objloan.SanktionDate;
                com.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = objloan.ExpireDate;
                com.Parameters.Add("@InterestPeriod", SqlDbType.VarChar, 50).Value = objloan.InterestPeriod;
                com.Parameters.Add("@InterestRate", SqlDbType.Money).Value = objloan.InterestRate;
                com.Parameters.Add("@DueAmount", SqlDbType.Money).Value = objloan.DueAmount;
                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = objloan.Remarks;
                com.Parameters.Add("@LoanAccID", SqlDbType.Int).Value = objloan.LoanAccID;
                if (objloan.LCID == 0)
                    com.Parameters.Add("@LCID", SqlDbType.Int).Value = DBNull.Value;
                else
                    com.Parameters.Add("@LCID", SqlDbType.Int).Value = objloan.LCID;
                com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = objloan.TransRefID;
                com.Parameters.Add("@AcceptedPercent", SqlDbType.Money).Value = objloan.AcceptedPercent;
                com.Parameters.Add("@Rate", SqlDbType.Money).Value = objloan.Rate;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.ExecuteNonQuery();
                trans.Commit();
                return objloan.LoanID;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetLoanInfo(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_Loan", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetLoanInfo(SqlConnection con,string loanNo,string LcNo,DateTime sDate,DateTime eDate)
        {
            string qstr = "SELECT T_Loan.LoanID, T_Loan.LoanNo, T_Loan.LoanAmount, T_Loan.SanktionDate, T_Loan.RefAccID, T_Loan.ApplyDate, T_Loan.ExpireDate, " +
                     " T_Loan.InterestPeriod, T_Loan.InterestRate, T_Loan.DueAmount, T_Loan.Remarks, T_Loan.LoanAccID, T_Loan.LCID, T_LC_Master.LCNo, " +
                     " T_Loan.TransRefID, T_Loan.AcceptedPercent, T_Loan.Rate, T_Loan.CompanyID " +
                     " FROM    T_Loan INNER JOIN T_LC_Master ON T_Loan.LCID = T_LC_Master.LCID ";
            
            
         qstr += " WHERE (T_Loan.LoanNo LIKE @LoanNo) AND (@f =1 OR T_Loan.SanktionDate BETWEEN @startDate AND @endDate) AND (T_LC_Master.LCNo LIKE @LCNo) AND  (T_Loan.CompanyID = @CompanyID) ";
         qstr += " ORDER BY T_Loan.SanktionDate, T_Loan.LoanNo";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(qstr, con);
                da.SelectCommand.Parameters.Add("@LoanNo", SqlDbType.VarChar, 50).Value = loanNo + "%";

                if (sDate.Date == new DateTime(1900, 1, 1) || eDate.Date == new DateTime(1900, 1, 1))
                    da.SelectCommand.Parameters.Add("@f", SqlDbType.Int).Value = 1;
                else
                    da.SelectCommand.Parameters.Add("@f", SqlDbType.Int).Value = 0;
                da.SelectCommand.Parameters.Add("@startDate", SqlDbType.DateTime).Value = sDate;
                da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = eDate;
                da.SelectCommand.Parameters.Add("@LCNo", SqlDbType.VarChar,50).Value = LcNo +"%";
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteLoan(int loanID, SqlConnection con)
        {
            SqlCommand com = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                trans = con.BeginTransaction();
                com.Connection = con;
                com.Transaction = trans;
                com.CommandText = "spDeleteLoan";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanID;
                com.ExecuteNonQuery();
             
                trans.Commit();
            }
            catch (Exception ex)
            {
              if(trans !=null)  trans.Rollback();
                throw ex;
            }



        }

       




        /////////saveUpdate LoanAdjust///////////////////////////////////////

        public void SaveUpdateLoanAdjust(LoanAdjust objloanAdjust, SqlConnection conn)
        {
            SqlCommand com = new SqlCommand();
            SqlTransaction trans = null;

            try
            {
                trans = conn.BeginTransaction();
                com.Connection = conn;
                com.Transaction = trans;
                com.CommandText = "spSaveUpdateLoanAdjust";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@LoanID", SqlDbType.Int).Value = objloanAdjust.LoanID;
                com.Parameters.Add("@LoanAdjustID", SqlDbType.Int).Value = objloanAdjust.LoanAdjustID;
                com.Parameters.Add("@AdjustmentDate", SqlDbType.DateTime).Value = objloanAdjust.AdjustDate;
                com.Parameters.Add("@AdjustAmount", SqlDbType.Money).Value = objloanAdjust.AdjustAmount;
                com.Parameters.Add("@AdjustMethodID", SqlDbType.Int).Value = objloanAdjust.AdjustMethodID;
                com.Parameters.Add("@AdjustRefNo", SqlDbType.VarChar, 50).Value = objloanAdjust.AdjustRefNo;

                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = objloanAdjust.Remarks;
                com.Parameters.Add("@PayFrom", SqlDbType.Int).Value = objloanAdjust.PayFrom;
                com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = objloanAdjust.TransRefID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void SaveUpdateLoanAdjust(LoanAdjust objloanAdjust, SqlConnection conn, SqlTransaction trans)
        {
            SqlCommand com = new SqlCommand();
            //SqlTransaction trans = null;

            try
            {
                //trans = conn.BeginTransaction();
                com.Connection = conn;
                com.Transaction = trans;
                com.CommandText = "spSaveUpdateLoanAdjust";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@LoanID", SqlDbType.Int).Value = objloanAdjust.LoanID;
                com.Parameters.Add("@LoanAdjustID", SqlDbType.Int).Value = objloanAdjust.LoanAdjustID;
                com.Parameters.Add("@AdgustmentDate", SqlDbType.DateTime).Value = objloanAdjust.AdjustDate;
                com.Parameters.Add("@AdjustAmount", SqlDbType.Money).Value = objloanAdjust.AdjustAmount;
                com.Parameters.Add("@AdjustMethodID", SqlDbType.Int).Value = objloanAdjust.AdjustMethodID;
                com.Parameters.Add("@AdjustRefNo", SqlDbType.VarChar, 50).Value = objloanAdjust.AdjustRefNo;

                com.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = objloanAdjust.Remarks;
                com.Parameters.Add("@PayFrom", SqlDbType.Int).Value = objloanAdjust.PayFrom;
                com.Parameters.Add("@TransRefID", SqlDbType.Int).Value = objloanAdjust.TransRefID;
                com.ExecuteNonQuery();
                //trans.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetLCNO(int lcid, SqlConnection con)
        {
            SqlCommand com = new SqlCommand();

            try
            {

                com.Connection = con;

                com.CommandText = "select LCNo from T_LC_Master where LCID=" + lcid.ToString();
               
                object str = com.ExecuteScalar();

                return str == null || str ==DBNull.Value ? "" : str.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*
        public DataTable loadLoan(SqlConnection con, int loanId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_Loan where LoanID = @LoanID", con);
                da.SelectCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanId;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        */
        public DataTable loadLoan(SqlConnection con, int loanId,DateTime pDate)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spLoadLoanWithInterest", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanId;
                da.SelectCommand.Parameters.Add("@PayDate", SqlDbType.DateTime).Value = pDate;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public DataTable GetPayMethod(SqlConnection con)
        {

            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_TransactionMethod", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetLCNO(SqlConnection con)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from T_LC_Master WHERE CompanyID=" + LogInInfo.CompanyID.ToString(), con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        public DataTable loadLoanPayment(SqlConnection con, int loanid)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from T_LoanAdjust where LoanID = @LoanID", con);
                da.SelectCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanid;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }



        public DataTable SearchLoan(SqlConnection con,DateTime stDate,DateTime endDate,string LoanNo)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spSearchLoan", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@stDate", SqlDbType.DateTime).Value = stDate;
                da.SelectCommand.Parameters.Add("@endDate", SqlDbType.DateTime).Value = endDate;
                da.SelectCommand.Parameters.Add("@LoanNo", SqlDbType.VarChar,50).Value = LoanNo;
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


        public DataTable ReturnLoan(SqlConnection con,int LoanID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * From T_Loan Where LoanID=@LoanID", con);
                da.SelectCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = LoanID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public Loan GetLoan(SqlConnection con, int loanid)
        {
            Loan obLoan = new Loan();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * From T_Loan Where LoanID=@LoanID", con);
                da.SelectCommand.Parameters.Add("@LoanID", SqlDbType.Int).Value = loanid;
                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0)
                    return null;
                int i, nR = dt.Rows.Count;
                for (i = 0; i < nR; i++)
                {
                    obLoan.ApplyDate = dt.Rows[i].Field<DateTime>("ApplyDate");
                    obLoan.LoanID = dt.Rows[i].Field<int>("LoanID");
                    obLoan.DueAmount = Convert.ToDouble(dt.Rows[i].Field<object>("DueAmount"));
                    obLoan.ExpireDate = dt.Rows[i].Field<DateTime>("ExpireDate");
                    obLoan.InterestPeriod = GlobalFunctions.isNull(dt.Rows[i].Field<string>("InterestPeriod"), "");
                    obLoan.InterestRate = Convert.ToDouble(dt.Rows[i].Field<object>("InterestRate"));
                    obLoan.LCID = GlobalFunctions.isNull(dt.Rows[i].Field<object>("LCID"), 0);
                    obLoan.LoanAccID = GlobalFunctions.isNull(dt.Rows[i].Field<int>("LoanAccID"), 0);
                    obLoan.LoanAmount = Convert.ToDouble(dt.Rows[i].Field<object>("LoanAmount"));
                    obLoan.LoanNo = GlobalFunctions.isNull(dt.Rows[i].Field<string>("LoanNo"), "");
                    obLoan.RefAccID = GlobalFunctions.isNull(dt.Rows[i].Field<int>("RefAccID"), 0);
                    obLoan.Remarks = GlobalFunctions.isNull(dt.Rows[i].Field<object>("Remarks"), "");
                    obLoan.SanktionDate = dt.Rows[i].Field<DateTime>("SanktionDate");
                    obLoan.TransRefID = GlobalFunctions.isNull(dt.Rows[i].Field<int>("TransRefID"), 0);
                    obLoan.Rate = GlobalFunctions.isNull(dt.Rows[i].Field<object>("Rate"), 0.0);
                    obLoan.AcceptedPercent = GlobalFunctions.isNull(dt.Rows[i].Field<object>("AcceptedPercent"), 0.0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return obLoan;
        }
        public DataTable loadLoanAgainstLC(SqlConnection con, int LcId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spLoadLoanAgainstLC", con);
                da.SelectCommand.Parameters.Add("@LCID", SqlDbType.Int).Value = LcId;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }
        public DataTable loadCurrency(SqlConnection con, int CurrencyId)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from Currency where CurrencyID = @CurrencyId", con);
                da.SelectCommand.Parameters.Add("@CurrencyId", SqlDbType.Int).Value = CurrencyId;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable SearchLC(SqlConnection con, DateTime sDate, DateTime eDate, string LCNo)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("spLoadLcOfLoan", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@sDate", SqlDbType.DateTime).Value = sDate;
                da.SelectCommand.Parameters.Add("@eDate", SqlDbType.DateTime).Value = eDate;
                da.SelectCommand.Parameters.Add("@LCNo", SqlDbType.VarChar, 100).Value = LCNo;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }


        public void DeleteLoanAdjust(int LoanAdjID, SqlConnection con)
        {
            SqlCommand com = new SqlCommand();
            SqlTransaction trans = null;
            try
            {
                trans = con.BeginTransaction();
                com.Connection = con;
                com.Transaction = trans;
                com.CommandText = "spDeleteLoanAdjust";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@LoanAdjustID", SqlDbType.Int).Value = LoanAdjID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }



        }


    }
}
