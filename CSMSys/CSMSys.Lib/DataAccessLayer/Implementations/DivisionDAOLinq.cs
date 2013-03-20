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
    public class DivisionDAOLinq : BaseDAORepository<ADMDivision, CSMSysConfiguration>, IDivisionDAO
    {
        protected override System.Linq.Expressions.Expression<Func<ADMDivision, bool>> GetIDSelector(int id)
        {
            return (item) => item.DivisionID == id;
        }

        public DivisionDAOLinq(bool isLoadWith)
        {
            if (isLoadWith)
            {
                base.DataContext.DeferredLoadingEnabled = false;

                DataLoadOptions options = new DataLoadOptions();

                options.LoadWith<ADMDivision>(d => d.ADMCountry);

                base.DataContext.LoadOptions = options;
            }
            else
            {
                base.DataContext.DeferredLoadingEnabled = true;
            }
        }

        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="divisionCode"></param>
        /// <param name="divisionName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<ADMDivision> SearchDivision(string divisionCode, string divisionName)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "t_ADMDivision";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(divisionCode))
                {
                    whereClause += " WHERE DivisionCode = '" + divisionCode + "'";
                }

                if (!string.IsNullOrEmpty(divisionName))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "DivisionName='" + divisionName + "'";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<ADMDivision>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
