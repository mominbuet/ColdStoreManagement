using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CSMSys.Lib.Utility
{
    public class CSMSysConnection
    {
        protected string _connectionString = "";
        public SqlConnection sqlConn = null;

        public CSMSysConnection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString;
            sqlConn = new SqlConnection(_connectionString);

            try
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();
                }
            }
            catch (Exception)
            {
                if (sqlConn.State != ConnectionState.Closed)
                    sqlConn.Close();

                sqlConn.Open();
            }

        }
    }
}
