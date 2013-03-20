using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class SerialDAOLinq : BaseDAORepository<INVStockSerial, CSMSysConfiguration>, ISerialDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVStockSerial, bool>> GetIDSelector(int id)
        {
            return (item) => item.SerialID == id;
        }
        

        public long getNextSerialNo()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader Reader;
            command.CommandText = "SELECT IDENT_CURRENT('invstockserial') as id";
            connection.Open();
            Reader = command.ExecuteReader();
            long log = 0;
            while (Reader.Read())
            {
                if (Reader["id"] != DBNull.Value)
                    log = long.Parse((Reader["id"]).ToString());
                break;

            }
            Reader.Close();
            connection.Close();

            //string strSQL = "";
            return log;
        }

        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="partyCode"></param>
        /// <param name="partyName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<INVStockSerial> SearchSerialByNo(string serialNo)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVStockSerial";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE SerialNo = '" + serialNo + "' ";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVStockSerial>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
        public IList<INVStockSerial> SearchSerialByParty(int partyid)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVStockSerial";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (partyid!=0)
                {
                    whereClause += " WHERE partyid = '" + partyid + "' ";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVStockSerial>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
        public long GetSumByParty(string sql)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
                SqlCommand command = connection.CreateCommand();
                SqlDataReader Reader;
                command.CommandText = sql;
                connection.Open();
                Reader = command.ExecuteReader();
                long log = 0;
                while (Reader.Read())
                {
                    if (Reader["smbags"] != DBNull.Value)
                        log = long.Parse((Reader["smbags"]).ToString());
                    break;

                }
                Reader.Close();
                connection.Close();

                //string strSQL = "";
                return log;
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return 0;
            }
        }
        
        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="partyCode"></param>
        /// <param name="partyName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<INVStockSerial> SearchSerial(string serialNo, float bag)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVStockSerial";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE SerialNo = '" + serialNo + "' ";
                }

                if (bag > 0)
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "Bags = " + bag;
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVStockSerial>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
