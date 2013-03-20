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
   public sealed class LocalConnection
    {
       public LocalConnection() 
       {

       }

        string connectionString = "";
         SqlConnection con = null;

        public  SqlConnection getConnection()
        {
            // connectionString = ConfigurationSettings.AppSettings["ConnectionString"];
            NameValueCollection configaration = ConfigurationSettings.AppSettings;
            foreach (string keys in configaration.AllKeys)
            {
                if (keys == "RegKey") continue;
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
        
        public  void closeConnection(SqlConnection connection)
        {
            if ((connection != null) || (connection.State == ConnectionState.Open))
            {
                connection.Close();
            }
        }
      
    }
}
