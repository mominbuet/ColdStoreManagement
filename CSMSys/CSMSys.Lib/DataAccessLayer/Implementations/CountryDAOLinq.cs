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
    public class CountryDAOLinq : BaseDAORepository<ADMCountry, CSMSysConfiguration>, ICountryDAO
    {
        protected override System.Linq.Expressions.Expression<Func<ADMCountry, bool>> GetIDSelector(int id)
        {
            return (item) => item.CountryID == id;
        }


        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="countryName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<ADMCountry> SearchCountry(string countryCode, string countryName)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "t_ADMCountry";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(countryCode))
                {
                    whereClause += " WHERE CountryCode = '" + countryCode + "'";
                }

                if (!string.IsNullOrEmpty(countryName))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "CountryName='" + countryName + "'";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<ADMCountry>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
