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
    public class DaShade
    {
        public DaShade() { }

        public void saveUpdateShade(Shade obShade, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateShade";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ShadeID", SqlDbType.Int).Value = obShade.ShadeID == -1 ? 0 : obShade.ShadeID;
                com.Parameters.Add("@ShadeNo", SqlDbType.VarChar, 100).Value = obShade.ShadeNo;
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

        public void deleteShade(SqlConnection con, int ShadeID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteShade";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ShadeID", SqlDbType.Int).Value = ShadeID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public DataTable shadeLoad(SqlConnection con)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * from T_Shade WHERE CompanyID=" + LogInInfo.CompanyID.ToString() , con);
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
