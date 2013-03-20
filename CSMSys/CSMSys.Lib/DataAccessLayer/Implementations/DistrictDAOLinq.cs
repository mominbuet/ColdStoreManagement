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
    public class DistrictDAOLinq : BaseDAORepository<ADMDistrict, CSMSysConfiguration>, IDistrictDAO
    {
        protected override System.Linq.Expressions.Expression<Func<ADMDistrict, bool>> GetIDSelector(int id)
        {
            return (item) => item.DistrictID == id;
        }

        #region Constructor/s
        public DistrictDAOLinq(bool isLoadWith)
        {
            if (isLoadWith)
            {
                base.DataContext.DeferredLoadingEnabled = false;

                DataLoadOptions options = new DataLoadOptions();

                options.LoadWith<ADMDistrict>(d => d.ADMDivision);
                options.LoadWith<ADMDivision>(d => d.ADMCountry);

                base.DataContext.LoadOptions = options;
            }
            else
            {
                base.DataContext.DeferredLoadingEnabled = true;
            }

        }
        #endregion

        /// <summary>
        /// DA method to search object by params
        /// </summary>
        /// <param name="districtCode"></param>
        /// <param name="districtName"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IList<ADMDistrict> SearchDistrict(string districtCode, string districtName)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "t_ADMDistrict";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(districtCode))
                {
                    whereClause += " WHERE DistrictCode = '" + districtCode + "'";
                }

                if (!string.IsNullOrEmpty(districtName))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "DistrictName='" + districtName + "'";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<ADMDistrict>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
