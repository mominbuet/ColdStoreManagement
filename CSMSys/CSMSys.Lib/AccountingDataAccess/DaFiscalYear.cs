using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingUtility;
using System.Collections;
using CSMSys.Lib.AccountingEntity;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaFiscalYear
    {

        public DataTable getFiscalYear(SqlConnection con, int id)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select * from FiscalYear Where (FiscalYearID=@FiscalYearID OR @FiscalYearID=0) AND (CompanyID=@cID) ORDER BY startdate DESC", con);
                //SqlDataAdapter da = new SqlDataAdapter("sploadFiscalYear", con);
                //da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@FiscalYearID", SqlDbType.Int).Value = id;
                da.SelectCommand.Parameters.Add("@cID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }

            return dt;
        }
        /*
        public DataTable getFiscalYear(SqlConnection con, int id)
        {
            DataTable dt = new DataTable();
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter("Select * from ERP.dbo.FiscalYear Where (FiscalYearID=@FiscalYearID OR @FiscalYearID=0) AND (CompanyID=@cID) ORDER BY startdate DESC", con);
                SqlDataAdapter da = new SqlDataAdapter("sploadFiscalYear", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@FiscalYearID", SqlDbType.Int).Value = id;
                da.SelectCommand.Parameters.Add("@cID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }

            return dt;
        }
        */
        public FiscalYear getAFiscalYear(SqlConnection con, int Fid)
        {
            FiscalYear fy = new FiscalYear();
            DataTable dt = new DataTable();
            try
            {

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM FiscalYear WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " AND FiscalYearID=" + Fid.ToString(), con);

                da.Fill(dt);
                da.Dispose();
                if (dt.Rows.Count == 0) return null;
                fy.FiscalYearID = GlobalFunctions.isNull(dt.Rows[0].Field<object>("FiscalYearID"), 0);
                fy.Titile = GlobalFunctions.isNull(dt.Rows[0].Field<object>("Title"), string.Empty);
                fy.StartDate = GlobalFunctions.isNull(dt.Rows[0].Field<object>("startdate"), new DateTime(1900, 1, 1));
                fy.EndDate = GlobalFunctions.isNull(dt.Rows[0].Field<object>("enddate"), new DateTime(1900, 1, 1));
                return fy;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public int getFiscalYear(SqlConnection con, DateTime date)
        {

            int id = 0;

            SqlCommand com = null;


            try
            {

                com = new SqlCommand();

                com.Connection = con;



                com.CommandText = "SELECT  FiscalYearID  FROM  ERP.dbo.FiscalYear WHERE     (companyID = @cID) AND (@dt BETWEEN startdate AND enddate)";
                com.Parameters.Add("@dt", SqlDbType.DateTime).Value = date;
                com.Parameters.Add("@cID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                object obj = com.ExecuteScalar();
                id = GlobalFunctions.isNull(obj, 0);

            }
            catch (Exception Ex)
            {

                throw new Exception("Can not get Fiscal Year " + Ex.Message);

            }
            return id;
        }

        public int SaveUpdateFiscalYear(SqlConnection con, FiscalYear obj)
        {

            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {

                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                if (obj.FiscalYearID == 0)
                {



                    com.CommandText = "INSERT INTO ERP.dbo.FiscalYear (Title, startdate, enddate, companyID, userID, modifiedDate)"
                                        + " VALUES (@Title,@startdate,@enddate,@companyID,@userID, GETDATE())";

                }
                else
                {
                    com.CommandText = "UPDATE ERP.dbo.FiscalYear SET Title = @Title, startdate = @startdate, enddate = @enddate, companyID = @companyID, userID = @userID, modifiedDate = GETDATE() "
                                        + " WHERE (FiscalYearID = @FiscalYear)";
                    com.Parameters.Add("@FiscalYear", SqlDbType.Int).Value = obj.FiscalYearID;
                }

                com.Parameters.Add("@Title", SqlDbType.VarChar, 200).Value = obj.Titile;
                com.Parameters.Add("@startdate", SqlDbType.DateTime).Value = obj.StartDate;
                com.Parameters.Add("@enddate", SqlDbType.DateTime).Value = obj.EndDate;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;

                com.ExecuteNonQuery();
                trans.Commit();


            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not save or update " + Ex.Message);

            }


            return obj.FiscalYearID;
        }

        public void DeleteFiscalYear(SqlConnection con, int ID)
        {

            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {

                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;
                com.CommandText = "DELETE FROM ERP.dbo.FiscalYear WHERE FiscalYearID = @FiscalYearID AND CompanyID=@CompanyID";
                com.Parameters.Add("@FiscalYearID", SqlDbType.Int).Value = ID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.ExecuteNonQuery();
                trans.Commit();


            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not delete FiscalYear " + Ex.Message);


            }
        }


        public DataTable getFiscalYearS(SqlConnection formConnection, int p)
        {
            DataTable dt = new DataTable();
            try
            {
                //SqlDataAdapter da = new SqlDataAdapter("Select * from ERP.dbo.FiscalYear Where (FiscalYearID=@FiscalYearID OR @FiscalYearID=0) AND (CompanyID=@cID) ORDER BY startdate DESC", con);
                SqlDataAdapter da = new SqlDataAdapter("sploadFiscalYear", formConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@FiscalYearID", SqlDbType.Int).Value = p;
                da.SelectCommand.Parameters.Add("@cID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception ex)
            {
                dt = null;
                throw ex;
            }

            return dt;
        }

        public int getCurrentFiscalYearID(SqlConnection con)
        {
            int FYid = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT FiscalYearID FROM FiscalYear WHERE (enddate IS NULL) AND (companyID = @companyID)", con);
                cmd.Parameters.Add("@companyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                FYid = GlobalFunctions.isNull(cmd.ExecuteScalar(),0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FYid;
        }
        public int getCurrentFiscalYearID(SqlConnection con,SqlTransaction trans)
        {
            int FYid = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT FiscalYearID FROM FiscalYear WHERE (enddate IS NULL) AND (companyID = @companyID)", con, trans);
                cmd.Parameters.Add("@companyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                FYid = GlobalFunctions.isNull(cmd.ExecuteScalar(), 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FYid;
        }
    }
}
