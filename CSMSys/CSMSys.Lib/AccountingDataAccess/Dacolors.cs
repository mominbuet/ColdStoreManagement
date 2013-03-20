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
    public class Dacolors
    {
        public Dacolors() { }

        public void SaveUpdateColors(Colors obColors, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateColors";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@ColorsID", SqlDbType.Int).Value = obColors.ColorsID == -1 ? 0 : obColors.ColorsID;
                com.Parameters.Add("@ColorsName", SqlDbType.VarChar, 200).Value = obColors.ColorsName;
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

        public void deleteColors(SqlConnection con, int ColorsID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "Delete from  P_Colors Where ColorsID = @ColorsID";
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@ColorsID", SqlDbType.Int).Value = ColorsID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public DataTable colorsload(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from  P_Colors WHERE CompanyID = @CompanyID Order By ColorsName", con);
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
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
        public DataTable getColorsForGrid(SqlConnection con)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM (select ColorsID,ColorsName from P_Colors  WHERE CompanyID=" + LogInInfo.CompanyID.ToString() + " UNION SELECT 0,' ') A  Order By (CASE WHEN ColorsName LIKE ' ' THEN 0 ELSE 1 END), ColorsName", con);
                da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
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
