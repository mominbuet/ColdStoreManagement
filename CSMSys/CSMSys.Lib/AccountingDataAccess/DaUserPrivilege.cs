using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using CSMSys.Lib.AccountingUtility;
using CSMSys.Lib.AccountingEntity;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaUserPrivilege
    {
        public DaUserPrivilege() { }
        
        public ArrayList getUserPrivilege(int id)
        {
            ArrayList list = new ArrayList();
            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;


                com.CommandText = "Select * FROM UserPrivilege WHERE (UserID = @UserID OR @UserID=0)  Order by FriendlyName";
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = id;


                IDataReader oReader = com.ExecuteReader();
                while (oReader.Read())
                {
                    list.Add(CreateObject(oReader, false));

                }
                oReader.Close();

                trans.Commit();

                //ConnectionHelper.closeConnection();
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get UserPrivilege" + Ex.Message);

            }
            return list;
        }
        public ArrayList getUserPrivilege(int id, int CompanyID)
        {
            ArrayList list = new ArrayList();
            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;


                com.CommandText = "Select * FROM UserPrivilege WHERE (UserID = @UserID OR @UserID=0) and (CompanyID = @CompanyID OR @CompanyID=0)  Order by FriendlyName";
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;

                IDataReader oReader = com.ExecuteReader();
                while (oReader.Read())
                {
                    list.Add(CreateObject(oReader, false));

                }
                oReader.Close();

                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get UserPrivilege" + Ex.Message);

            }
            return list;
        }

        public int getUserPrivilege(string strModulesMenuName)
        {
            int Count = 0;
            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;


                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "spGetTotalPrivilege";
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@MenuName", SqlDbType.VarChar, 200).Value = strModulesMenuName;

                IDataReader oReader = com.ExecuteReader();
                if (oReader.Read())
                {
                    Count = int.Parse(oReader[0].ToString());

                }
                oReader.Close();

                trans.Commit();

                //ConnectionHelper.closeConnection();
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get UserPrivilege" + Ex.Message);

            }
            return Count;
        }
        public ArrayList getUserPrivileges(string strModulesMenuName)
        {
            ArrayList list = new ArrayList();
            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;

            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;


                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "spGetTotalPrivilege";
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@MenuName", SqlDbType.VarChar, 200).Value = strModulesMenuName;

                IDataReader oReader = com.ExecuteReader();
                while (oReader.Read())
                {
                    list.Add(CreateObject(oReader));

                }
                oReader.Close();

                trans.Commit();

                //ConnectionHelper.closeConnection();
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get UserPrivilege" + Ex.Message);

            }
            return list;
        }
        public DataTable getUserPrivilege(int id, string strParentMenuName, int CompanyID)
        {
            DataTable list = new DataTable();
            SqlConnection con = null;
            SqlCommand com = null;
            SqlTransaction trans = null;
            SqlDataAdapter da = null;
            try
            {
                con = ConnectionHelper.getConnection();
                trans = con.BeginTransaction();
                com = new SqlCommand();

                com.Connection = con;
                com.Transaction = trans;

                if (id != 0)
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "spGetPrivileges";
                    com.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
                    com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
                    com.Parameters.Add("@ModulesName", SqlDbType.VarChar).Value = "all";
                }
                else
                    com.CommandText = "Select DISTINCT FriendlyName  AS Modules FROM Modules WHERE (ParentMenuName = @ParentMenuName OR @ParentMenuName = 'all') AND MenuName IS NULL Order by FriendlyName";

                com.Parameters.Add("@ParentMenuName", SqlDbType.VarChar).Value = strParentMenuName;



                da = new SqlDataAdapter(com);
                da.Fill(list);
                da.Dispose();

                trans.Commit();

                //ConnectionHelper.closeConnection();
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get UserPrivilege" + Ex.Message);

            }
            return list;
        }
        public void deleteUserPrivilege(int id, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand com = null;
            try
            {
                com = new SqlCommand("DELETE FROM UserPrivilege WHERE UserID = @UserID", con, trans);
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
                com.ExecuteNonQuery();
                //trans.Commit();
                com = new SqlCommand("DELETE FROM UserReportPrivilege WHERE UserID = @UserID", con, trans);
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = id;
                com.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                throw new Exception("Can not get UserPrivilege" + Ex.Message);

            }
        }

        #region CreateObjects
        private UserPrivilege CreateObject(IDataReader oReader, bool IsDefault)
        {
            UserPrivilege objUserPrivilege = new UserPrivilege();

            NullManager reader = new NullManager(oReader);
            try
            {
                objUserPrivilege.UserID = reader.GetInt32("UserID");
                objUserPrivilege.FriendlyName = reader.GetString("FriendlyName");
                objUserPrivilege.CompanyID = reader.GetInt32("CompanyID");
                if (!IsDefault)
                {
                    objUserPrivilege.CanEdit = (int)reader.GetByte("CanEdit");
                    objUserPrivilege.CanDelete = (int)reader.GetByte("CanDelete");
                    objUserPrivilege.CanAdd = (int)reader.GetByte("CanAdd");
                    objUserPrivilege.CanView = (int)reader.GetByte("CanView");
                }



            }
            catch (Exception Ex)
            {
                throw new Exception("Error while creating object" + Ex.Message);
            }
            return objUserPrivilege;
        }
        private ModuleDictionary CreateObject(IDataReader oReader)
        {
            ModuleDictionary objModuleDictionary = new ModuleDictionary();

            NullManager reader = new NullManager(oReader);
            try
            {
                objModuleDictionary.TotalPrivilege = (int)reader.GetByte("TotalPrivilege");
                objModuleDictionary.MenuName = reader.GetString("MenuName");

            }
            catch (Exception Ex)
            {
                throw new Exception("Error while creating object" + Ex.Message);
            }
            return objModuleDictionary;
        }
        #endregion
    }
}
