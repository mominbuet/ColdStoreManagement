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
    public class DaBranch
    {
        public DaBranch() { }
        public int SaveUpdateBranch(Branch obBranch, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateBranch";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@BranchID", SqlDbType.Int).Value = obBranch.BranchID;
                com.Parameters.Add("@BranchName", SqlDbType.VarChar, 100).Value = obBranch.BranchName;
                if (obBranch.Address == "")
                    com.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = DBNull.Value;
                else
                    com.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = obBranch.Address;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
            return 0;
        }
        public void deleteBranch(SqlConnection con, int BranchID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "Delete From T_Branch Where BranchID = @BranchID";
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if(trans!=null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
}
