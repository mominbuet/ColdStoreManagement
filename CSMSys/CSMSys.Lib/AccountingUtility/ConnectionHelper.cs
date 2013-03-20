using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;

namespace CSMSys.Lib.AccountingUtility
{
   public class ConnectionHelper
    {
        ConnectionHelper()
        {


        }

        static string connectionString = "";
        static SqlConnection con = null;
       
        public static SqlConnection getConnection()
        {
            // connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            NameValueCollection configaration = ConfigurationSettings.AppSettings;
            foreach (string keys in configaration.AllKeys)
            {
                if (keys == "RegKey" || keys == "VisitedCount") continue;
                connectionString = connectionString + keys + "=" + configaration.Get(keys) + ";";
            }

            try
            {

                if ((con == null) || (con.State == ConnectionState.Closed))
                {
                    con = new SqlConnection(connectionString);
                    con.Open();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred when Connection is going to be opened" + ex.Message);
            }
            return con;
        }
        //public static void closeConnection()
        //{
        //    if ((con != null) || (con.State == ConnectionState.Open))
        //    {
        //        con.Close();
        //    }     
        //}
        public static void closeConnection(SqlConnection connection)
        {
            if ((connection != null) || (connection.State == ConnectionState.Open))
            {
                connection.Close();
            }
        }
        public static int GetID(SqlConnection con,  string sFieldName, string sTableName)
        {

            SqlCommand com = new SqlCommand();
            com.Connection = con;
            int ID = 0;

            try
            {

                com.CommandText = "Select ISNULL(Max( " + sFieldName + " ),0) FROM " + sTableName;
                ID =(int) com.ExecuteScalar();
                //IDataReader oReader = com.ExecuteReader();

                //while (oReader.Read())
                //{
                //    ID = oReader.GetInt32(0);
                    
                //}
                //oReader.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Can not generate ID " + Ex.Message);
            }

            return ID;
        }

        public static int GetID(SqlConnection con, SqlTransaction trans,string sFieldName, string sTableName)
        {

            SqlCommand com = new SqlCommand();
            com.Connection = con;
            int ID = 0;

            try
            {

                com.CommandText = "Select ISNULL(Max( " + sFieldName + " ),0)+1 FROM " + sTableName;
                com.Transaction = trans;
                ID = (int)com.ExecuteScalar();
                //IDataReader oReader = com.ExecuteReader();

                //while (oReader.Read())
                //{
                //    ID = oReader.GetInt32(0);

                //}
                //oReader.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Can not generate ID " + Ex.Message);
            }

            return ID;
        }
        public static int GetIDForInsert(SqlConnection con, SqlTransaction trans, string sFieldName, string sTableName)
        {

            SqlCommand com = new SqlCommand();
            com.Connection = con;
            int ID = 0;

            try
            {

                com.CommandText = "Select ISNULL(Max( " + sFieldName + " ),0) FROM " + sTableName;
                com.Transaction = trans;
                ID = (int)com.ExecuteScalar();
                //IDataReader oReader = com.ExecuteReader();

                //while (oReader.Read())
                //{
                //    ID = oReader.GetInt32(0);

                //}
                //oReader.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Can not generate ID " + Ex.Message);
            }

            return ID;
        }
        public static int GenerateID(SqlConnection con, SqlCommand com, string sFieldName, string sTableName)
        {

            //SqlCommand com = con.CreateCommand();
            int ID = 0;

            try
            {

                com.CommandText = "Select ISNULL(Max( " + sFieldName + " ),0) FROM " + sTableName;
                IDataReader oReader = com.ExecuteReader();

                while (oReader.Read())
                {
                    ID = oReader.GetInt32(0);
                    ID = ID + 1;
                }
                oReader.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception("Can not generate ID " + Ex.Message);
            }

            return ID;
        }
       
    }
}
