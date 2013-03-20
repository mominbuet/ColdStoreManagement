using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingEntity;
using System.Data;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class Dadepartment
    {
        public Dadepartment() { }
        public void SaveUpdateDept(Department obDept, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "spSaveUpdateDepartment";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DeptID", SqlDbType.Int).Value = obDept.DeptID;
                com.Parameters.Add("@DeptName", SqlDbType.VarChar, 100).Value = obDept.DeptName;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
        }
        public void deleteDept(SqlConnection con, int deptId)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "Delete From T_Department Where DeptID = @deptID";
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@deptID", SqlDbType.Int).Value = deptId;
                com.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
}
