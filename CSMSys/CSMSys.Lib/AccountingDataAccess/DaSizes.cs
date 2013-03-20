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
    public class DaSizes
    {
        public DaSizes() { }

        public void saveUpdateSizes(Sizes obSizes, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateSizes";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@SizesID", SqlDbType.Int).Value = obSizes.SizesID == -1 ? 0 : obSizes.SizesID;
                com.Parameters.Add("@SizesName", SqlDbType.VarChar, 200).Value = obSizes.SizesName;
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

        public void deleteSizes(SqlConnection con, int SizesID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteSizes";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@SizesID", SqlDbType.Int).Value = SizesID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public DataTable SizesLoad(SqlConnection con)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * from P_Sizes WHERE CompanyID = @CompanyID Order By SizesName", con);
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
        public DataTable getSizesForGrid(SqlConnection con)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM (select SizesID,SizesName from P_Sizes  WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " UNION SELECT 0,' ') A  Order By (CASE WHEN SizesName LIKE ' ' THEN 0 ELSE 1 END), SizesName", con);
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
        
    }
}
