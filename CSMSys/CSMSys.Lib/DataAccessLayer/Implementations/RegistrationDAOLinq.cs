using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class RegistrationDAOLinq : BaseDAORepository<SRVRegistration, CSMSysConfiguration>, IRegistrationDAO
    {
        protected override System.Linq.Expressions.Expression<Func<SRVRegistration, bool>> GetIDSelector(int id)
        {
            return (item) => item.RegistrationID == id;
        }

        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="partyCode"></param>
        /// <param name="partyName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public SRVRegistration SearchRegistrationByNo(string serialNo)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "SRVRegistration";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE SerialID = '" + serialNo + "' ";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<SRVRegistration>(@strSQL).ToList().First();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
        public SRVRegistration SearchSRVBySerialParty(string serialNo,int partyid)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "SRVRegistration";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE SerialNo = '" + serialNo + "' and partyID='"+partyid+"' ";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<SRVRegistration>(@strSQL).ToList().First();
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
        public IList<SRVRegistration> SearchRegistration(string serialNo, float bag)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "SRVRegistration";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE RegistrationNo = '" + serialNo + "' ";
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

                return dc.ExecuteQuery<SRVRegistration>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
        public IList<SRVRegistration> SearchRegForTotalLoan(int partyid,string requisitioned)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "SRVRegistration";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if ((partyid!=0))
                {
                    whereClause += " WHERE partyID = '" + partyid + "' and requisitioned='"+requisitioned+"' ;";
                }

                

                strSQL += whereClause;

                return dc.ExecuteQuery<SRVRegistration>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
