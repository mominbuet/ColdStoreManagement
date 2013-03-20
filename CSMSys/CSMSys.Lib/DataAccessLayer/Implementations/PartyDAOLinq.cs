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
    public class PartyDAOLinq : BaseDAORepository<INVParty, CSMSysConfiguration>, IPartyDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVParty, bool>> GetIDSelector(int id)
        {
            return (item) => item.PartyID == id;
        }
        public long getnextPartyID()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader Reader;
            command.CommandText = "SELECT IDENT_CURRENT('invparty') as id";
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
        public IList<INVParty> SearchPartyByCode(string partyCode)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVParty";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(partyCode))
                {
                    whereClause += " WHERE PartyCode = '" + partyCode + "'";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVParty>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }

        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="partyCode"></param>
        /// <param name="partyName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<INVParty> SearchParty(string partyCode, string partyName)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "t_INVParty";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(partyCode))
                {
                    whereClause += " WHERE PartyCode = '" + partyCode + "'";
                }

                if (!string.IsNullOrEmpty(partyName))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "PartyName='" + partyName + "'";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVParty>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
