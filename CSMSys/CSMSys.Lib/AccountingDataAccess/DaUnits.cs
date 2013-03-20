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
    public class DaUnits
    {
        public DaUnits() { }

        public void saveUpdateUnits(Units obUnits, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateUnits";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@UnitsID", SqlDbType.Int).Value = obUnits.UnitsID == -1 ? 0 : obUnits.UnitsID;
                com.Parameters.Add("@UnitsName", SqlDbType.VarChar, 100).Value = obUnits.UnitsName;
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

        public void deleteUnits(SqlConnection con, int UnitsID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spDeleteUnits";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@UnitsID", SqlDbType.Int).Value = UnitsID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public DataTable unitsLoad(SqlConnection con)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select * from P_Units WHERE CompanyID=" + LogInInfo.CompanyID.ToString(), con);
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
