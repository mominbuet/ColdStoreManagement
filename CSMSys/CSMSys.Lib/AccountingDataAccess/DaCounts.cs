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
    public class DaCounts
    {
        public DaCounts() { }

        public void saveUpdateCounts(Counts obCounts, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateCounts";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@CountID", SqlDbType.Int).Value = obCounts.CountID == -1 ? 0 : obCounts.CountID;
                com.Parameters.Add("@CountName", SqlDbType.VarChar, 100).Value = obCounts.CountName;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCounts(SqlConnection con, int CountID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteCounts";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@CountID", SqlDbType.Int).Value = CountID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public DataTable countsload(SqlConnection con)
        {
            try
            {
                DataTable dt = new DataTable();
                
                SqlDataAdapter da = new SqlDataAdapter("select CountID,CountName from T_Count  WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " Order By CountName", con);
                da.Fill(dt);
                da.Dispose();
                return dt;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable getCountsForGrid(SqlConnection con)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM (select CountID,CountName from T_Count  WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " UNION SELECT 0,' ') A  Order By (CASE WHEN CountName LIKE ' ' THEN 0 ELSE 1 END), CountName", con);
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
