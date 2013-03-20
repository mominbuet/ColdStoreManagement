using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class BusinessSubTypeDA 
    {

        public BusinessSubTypeDA() { }

        public int SaveOrUpdate(BusinessSubType objBusinessSubType)
        {

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

                if (objBusinessSubType.BusinessSubTypeID == 0)
                {

                    objBusinessSubType.BusinessSubTypeID = ConnectionHelper.GetID(con,trans, "BusinessSubTypeID", "BusinessSubType");

                    com.CommandText = "Insert Into BusinessSubType(CompanyID, UserID, ModifiedDate,  BusinessSubTypeID, Name, BusinessTypeID) "
                                      + " Values(@CompanyID, @UserID, @ModifiedDate, @BusinessSubTypeID, @Name, @BusinessTypeID)";

                }
                else
                {
                    com.CommandText = "Update BusinessSubType SET CompanyID = @CompanyID, UserID =@UserID, ModifiedDate = @ModifiedDate, Name = @Name, BusinessTypeID = @BusinessTypeID  WHERE BusinessSubTypeID = @BusinessSubTypeID";

                }
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = LogInInfo.ModifiedDate;
                com.Parameters.Add("@BusinessSubTypeID", SqlDbType.Int).Value = objBusinessSubType.BusinessSubTypeID;
                com.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = objBusinessSubType.Name;
                com.Parameters.Add("@BusinessTypeID", SqlDbType.Int).Value = objBusinessSubType.BusinessTypeID;
                com.ExecuteNonQuery();
                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not save or update" + Ex.Message);

            }


            return objBusinessSubType.BusinessSubTypeID;

        }

        public ArrayList getBusinessSubType(int numBusinessSubTypeID)
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

                if (numBusinessSubTypeID == 0)
                {
                    com.CommandText = "Select * FROM BusinessSubType WHERE CompanyID = @CompanyID";
                    com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                }
                else
                {
                    com.CommandText = "Select * FROM BusinessSubType WHERE BusinessSubTypeID = @BusinessSubTypeID";
                    com.Parameters.Add("@BusinessSubTypeID", SqlDbType.Int).Value = numBusinessSubTypeID;
                }


                IDataReader objReader = com.ExecuteReader();
                while (objReader.Read())
                {
                    list.Add(CreateObject(objReader));

                }
                objReader.Close();

                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get BusinessSubType" + Ex.Message);

            }
            return list;

        }
        public  int getBusinessTypeID(int businessSubtypeID)
        {
            //int businessTypeID;
            SqlConnection con = null;
            SqlCommand com = null;


            try
            {
                con = ConnectionHelper.getConnection();
                com = new SqlCommand();

                com.Connection = con;

                com.CommandText = "Select BusinessTypeID FROM BusinessSubType WHERE BusinessSubTypeID = @BusinessSubTypeID";
                com.Parameters.Add("@BusinessSubTypeID", SqlDbType.Int).Value = businessSubtypeID;
                businessSubtypeID =(int) com.ExecuteScalar();
                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                throw new Exception("Can not get BusinessTypeID " + Ex.Message);

            }
            return businessSubtypeID;
        }
        public ArrayList getBusiness_SubTypeByType(int BusinessTypeID)
        {
            ArrayList list = new ArrayList();
            SqlConnection con = null;
            SqlCommand com = null;
            

            try
            {
                con = ConnectionHelper.getConnection();
                com = new SqlCommand();

                com.Connection = con;

                com.CommandText = "Select * FROM BusinessSubType WHERE BusinessTypeID = @BusinessTypeID AND CompanyID = @CompanyID";
               com.Parameters.Add("@BusinessTypeID", SqlDbType.Int).Value = BusinessTypeID;
               com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;


                IDataReader objReader = com.ExecuteReader();
                while (objReader.Read())
                {
                    list.Add(CreateObject(objReader));

                }
                objReader.Close();

                
                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                throw new Exception("Can not get BusinessSubType " + Ex.Message);

            }
            return list;
        }
        public void Delete(int numBusinessSubTypeID)
        {
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
                com.CommandText = "DELETE FROM BusinessSubType WHERE BusinessSubTypeID = @BusinessSubTypeID";
                com.Parameters.Add("@BusinessSubTypeID", SqlDbType.Int).Value = numBusinessSubTypeID;
                com.ExecuteNonQuery();
                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw new Exception("Can not get BusinessSubType" + Ex.Message);

            }

        }


        #region CreateObjects
        private BusinessSubType CreateObject(IDataReader objReader)
        {
            BusinessSubType objBusinessSubType = new BusinessSubType();
            NullManager reader = new NullManager(objReader);

            try
            {
                objBusinessSubType.BusinessSubTypeID = reader.GetInt32("BusinessSubTypeID");
                objBusinessSubType.Name = reader.GetString("Name");
                objBusinessSubType.BusinessTypeID = reader.GetInt32("BusinessTypeID");
                objBusinessSubType.CompanyID = reader.GetInt32("CompanyID");
                objBusinessSubType.UserID = reader.GetInt32("UserID");
                objBusinessSubType.ModifiedDate = reader.GetDateTime("ModifiedDate");
            }
            catch (Exception Ex)
            {
                throw new Exception("Error while creating object" + Ex.Message);
            }
            return objBusinessSubType;
        }
        #endregion


       
    }

}
