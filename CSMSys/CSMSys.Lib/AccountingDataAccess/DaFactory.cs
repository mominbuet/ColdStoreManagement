using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CSMSys.Lib.AccountingEntity;
using CSMSys.Lib.AccountingUtility;

namespace CSMSys.Lib.AccountingDataAccess
{
    public class DaFactory
    {
        public DaFactory()
        {

        }

        public int SaveUpdateFactory(Factory obFactory, SqlConnection con)
        {
            int ID = 0;
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;

                com.CommandText = "spSaveUpdateFactory";
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = obFactory.FactoryID;
                com.Parameters.Add("@FactoryName", SqlDbType.VarChar, 100).Value = obFactory.FactoryName;
                com.Parameters.Add("@Address", SqlDbType.VarChar, 200).Value = obFactory.Address;
                com.Parameters.Add("@CustomerID", SqlDbType.Int).Value = obFactory.CustomerID;
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
            return ID;
        }

        public void DeleteFactory(SqlConnection con,int FactoryID)
        {
            SqlCommand com = null;
            SqlTransaction trans = null;
            try
            {
                com = new SqlCommand();
                trans = con.BeginTransaction();
                com.Transaction = trans;
                com.Connection = con;
                com.CommandText = "delete from Factory Where FactoryID = @FactoryID";
                com.CommandType = CommandType.Text;
                com.Parameters.Add("@FactoryID", SqlDbType.Int).Value = FactoryID;
                com.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public DataTable formLoadFactory(SqlConnection con, int CustomerID)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("select * from Factory where CustomerID = @CustomerID AND CompanyID =" + LogInInfo.CompanyID, con);
                da.SelectCommand.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;
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
