using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingUtility;
using System.Collections;
using CSMSys.Lib.AccountingEntity;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaReportPrivilege
    {
        public DaReportPrivilege() { }
        public DataTable getMenuName(string strRbName,SqlConnection con)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select DISTINCT FriendlyName FROM Reports WHERE (RbName = @RbName OR @RbName='all') AND Software = 'ACC_INV' ORDER BY FriendlyName", con);
                da.SelectCommand.Parameters.Add("@RbName", SqlDbType.NVarChar, 200).Value = strRbName;
                da.Fill(dt);
                da.Dispose();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return dt;
        }
        public DataTable getRoleReportPrivilege(string role, string strParentMenuName, SqlConnection con)
        {
            DataTable list = new DataTable();
            SqlDataAdapter da = null;
            try
            {
                if (role != "")
                {
                    da = new SqlDataAdapter("SpGetAccReportPrivileges", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@Role", SqlDbType.VarChar, 100).Value = role;
                    da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                    da.SelectCommand.Parameters.Add("@FriendlyName", SqlDbType.NVarChar, 100).Value = strParentMenuName;
                }
                else
                {
                    da = new SqlDataAdapter("Select DISTINCT ReportName  AS Reports FROM Reports WHERE (FriendlyName = @FriendlyName OR @FriendlyName = 'all') AND Software = 'ACC_INV' AND MenuName IS NULL Order by FriendlyName", con);

                    da.SelectCommand.Parameters.Add("@FriendlyName", SqlDbType.NVarChar, 100).Value = strParentMenuName;
                }

                da.Fill(list);
                da.Dispose();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            return list;
        }
        public DataTable getUserReportPrivilege(int id, string strParentMenuName,SqlConnection con)
        {
            DataTable list = new DataTable();
            SqlDataAdapter da = null;
            try
            {
                if (id != 0)
                {
                    da = new SqlDataAdapter("SpGetRptUserPrivileges", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
                    da.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                    //com.Parameters.Add("@ReportName", SqlDbType.NVarChar,200).Value = "all";
                }
                else
                    da = new SqlDataAdapter("Select DISTINCT ReportName  AS Reports FROM Reports WHERE (FriendlyName = @FriendlyName OR @FriendlyName = 'all') AND MenuName AND Software = 'ACC_INV' IS NULL Order by FriendlyName", con);

                da.SelectCommand.Parameters.Add("@FriendlyName", SqlDbType.NVarChar, 100).Value = strParentMenuName;
                da.Fill(list);
                da.Dispose();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);

            }
            return list;
        }
        public void SaveUpdateUserReportPrivilege(UserReportPrivilege obUserReportPrivilege, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                com.CommandType = CommandType.StoredProcedure;

                com.CommandText = "SpSaveOrUpdateRptUPrivilege";

                com.Parameters.Add("@UserID", SqlDbType.Int).Value = obUserReportPrivilege.UserID;
                com.Parameters.Add("@RbName", SqlDbType.VarChar, 100).Value = obUserReportPrivilege.RbName;
                com.Parameters.Add("@CanView", SqlDbType.TinyInt).Value = obUserReportPrivilege.CanView;
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
                throw new Exception("Can not save or update" + Ex.Message);

            }

        }

        public void SaveUpdateRole(RoleReportPrivilege obRoleReportPrivilege, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            DataTable dtUser = new DataTable();
            DaUser obDaUser = new DaUser();
            try
            {
                dtUser = obDaUser.getUser(obRoleReportPrivilege.Role, con);
                int rowNo = dtUser.Rows.Count;
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                com.CommandType = CommandType.StoredProcedure;

                com.CommandText = "SpSaveOrUpdateRptRPrivilege";
                com.Parameters.Add("@Role", SqlDbType.VarChar, 100).Value = obRoleReportPrivilege.Role;
                com.Parameters.Add("@RbName", SqlDbType.VarChar, 100).Value = obRoleReportPrivilege.RbName;
                com.Parameters.Add("@CanView", SqlDbType.TinyInt).Value = obRoleReportPrivilege.CanView;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.ExecuteNonQuery();

                com.CommandText = "SpSaveOrUpdateRptUPrivilege";
                int UserId = 0;
                for (int i = 0; i < rowNo; i++)
                {
                    UserId = dtUser.Rows[i].Field<int>("UserID");
                    com.Parameters.Clear();
                    com.Parameters.Add("@UserID", SqlDbType.Int).Value = UserId;
                    com.Parameters.Add("@RbName", SqlDbType.VarChar, 100).Value = obRoleReportPrivilege.RbName;
                    com.Parameters.Add("@CanView", SqlDbType.TinyInt).Value = obRoleReportPrivilege.CanView;
                    com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                    com.ExecuteNonQuery();
                }

                trans.Commit();

            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception(Ex.Message);

            }


            //return objUserPrivilege.UserID;
        }
        public void SaveUpdateUser(UserReportPrivilege obUserReportPrivilege, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "SpSaveOrUpdateRptUPrivilege";

                com.Parameters.Add("@UserID", SqlDbType.Int).Value = obUserReportPrivilege.UserID;
                com.Parameters.Add("@RbName", SqlDbType.VarChar, 100).Value = obUserReportPrivilege.RbName;
                com.Parameters.Add("@CanView", SqlDbType.TinyInt).Value = obUserReportPrivilege.CanView;
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
                throw new Exception(Ex.Message);

            }
        }

        /*
        public void SaveUpdateRole(RoleReportPrivilege obRoleReportPrivilege, SqlConnection con)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                com.CommandType = CommandType.StoredProcedure;



                foreach (RoleReportPrivilege objRolePrivilege in list)
                {
                    com.Parameters.Clear();
                    com.CommandText = "SpSaveOrUpdateRptRPrivilege";
                    com.Parameters.Add("@Role", SqlDbType.VarChar, 100).Value = objRolePrivilege.Role;
                    com.Parameters.Add("@RbName", SqlDbType.VarChar, 100).Value = objRolePrivilege.RbName;
                    com.Parameters.Add("@CanView", SqlDbType.TinyInt).Value = objRolePrivilege.CanView;
                    com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                    com.ExecuteNonQuery();

                    com.CommandText = "SpSaveOrUpdateRptUPrivilege";

                    foreach (int numUserID in ulist)
                    {
                        com.Parameters.Clear();
                        com.Parameters.Add("@UserID", SqlDbType.Int).Value = numUserID;
                        com.Parameters.Add("@RbName", SqlDbType.VarChar, 100).Value = objRolePrivilege.RbName;
                        com.Parameters.Add("@CanView", SqlDbType.TinyInt).Value = objRolePrivilege.CanView;
                        com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                        com.ExecuteNonQuery();
                    }

                }


                trans.Commit();

                ConnectionHelper.closeConnection();
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    ConnectionHelper.closeConnection();
                }
                throw new Exception("Can not save or update" + Ex.Message);

            }
        }
        */
    }
}
