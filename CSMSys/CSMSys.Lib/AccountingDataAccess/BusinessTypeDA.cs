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
  public  class BusinessTypeDA 
    {
      public BusinessTypeDA(){}

        public int SaveOrUpdate(BusinessType objBusinessType)
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

                if (objBusinessType.BusinessTypeID == 0)
                {

                    objBusinessType.BusinessTypeID = ConnectionHelper.GetID(con, trans, "BusinessTypeID", "BusinessType");

                    com.CommandText = "Insert Into BusinessType(CompanyID, UserID, ModifiedDate, BusinessTypeID, Name) "
                                      + " Values(@CompanyID, @UserID, @ModifiedDate,@BusinessTypeID, @Name)";

                }
                else
                {
                    com.CommandText = "Update BusinessType SET CompanyID = @CompanyID, UserID =@UserID, ModifiedDate = @ModifiedDate, Name = @Name WHERE BusinessTypeID = @BusinessTypeID";

                }
                com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                com.Parameters.Add("@UserID", SqlDbType.Int).Value = LogInInfo.UserID;
                com.Parameters.Add("@ModifiedDate", SqlDbType.DateTime).Value = LogInInfo.ModifiedDate;
                com.Parameters.Add("@BusinessTypeID", SqlDbType.Int).Value = objBusinessType.BusinessTypeID;
                com.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = objBusinessType.Name;

                com.ExecuteNonQuery();
                trans.Commit();

                ConnectionHelper.closeConnection(con);
            }
            catch (Exception Ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                    ConnectionHelper.closeConnection(con);
                }
                throw new Exception("Can not save or update" + Ex.Message);

            }


            return objBusinessType.BusinessTypeID;

        }

        public ArrayList getBusinessType(int numBusinessTypeID)
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

                if (numBusinessTypeID == 0)
                {
                    com.CommandText = "Select * FROM BusinessType WHERE CompanyID = @CompanyID";
                    com.Parameters.Add("@CompanyID", SqlDbType.Int).Value = LogInInfo.CompanyID;
                }
                else
                {
                    com.CommandText = "Select * FROM BusinessType WHERE BusinessTypeID = @BusinessTypeID";
                    com.Parameters.Add("@BusinessTypeID", SqlDbType.Int).Value = numBusinessTypeID;
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
                throw new Exception("Can not get BusinessType" + Ex.Message);

            }
            return list;

        }

        public void Delete(int numBusinessTypeID)
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
                com.CommandText = "DELETE FROM BusinessType WHERE BusinessTypeID = @BusinessTypeID";
                com.Parameters.Add("@BusinessTypeID", SqlDbType.Int).Value = numBusinessTypeID;
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
                throw new Exception("Can not get BusinessType" + Ex.Message);

            }

        }


        #region CreateObjects
        private BusinessType CreateObject(IDataReader objReader)
        {
            BusinessType objBusinessType = new BusinessType();
            NullManager reader = new  NullManager(objReader);

            try
            {
                objBusinessType.BusinessTypeID = reader.GetInt32("BusinessTypeID");
                objBusinessType.Name = reader.GetString("Name");

                objBusinessType.CompanyID = reader.GetInt32("CompanyID");
                objBusinessType.UserID = reader.GetInt32("UserID");
                objBusinessType.ModifiedDate = reader.GetDateTime("ModifiedDate");
            }
            catch (Exception Ex)
            {
                throw new Exception("Error while creating object" + Ex.Message);
            }
            return objBusinessType;
        }
        #endregion


       
    }
}
