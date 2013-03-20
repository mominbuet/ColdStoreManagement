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
    public class UpazilaPSDAOLinq : BaseDAORepository<ADMUpazilaPS, CSMSysConfiguration>, IUpazilaPSDAO
    {
        protected override System.Linq.Expressions.Expression<Func<ADMUpazilaPS, bool>> GetIDSelector(int id)
        {
            return (item) => item.UpazilaPSID == id;
        }

        public UpazilaPSDAOLinq(bool isLoadWith)
        {
            if (isLoadWith)
            {
                base.DataContext.DeferredLoadingEnabled = false;

                DataLoadOptions options = new DataLoadOptions();

                options.LoadWith<ADMUpazilaPS>(u => u.DistrictID);
                options.LoadWith<ADMDistrict>(d => d.DivisionID);
                options.LoadWith<ADMDivision>(d => d.CountryID);

                base.DataContext.LoadOptions = options;
            }
            else
            {
                base.DataContext.DeferredLoadingEnabled = true;
            }
        }

        public IList<ADMUpazilaPS> SearchUpazilaPS(int districtID, string upazilaPSCode, string upazilaPSName)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "t_ADMUpazilaPS";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (districtID > 0)
                {
                    whereClause += " WHERE DistrictID" + " = " + districtID;
                }

                if (!string.IsNullOrEmpty(upazilaPSCode))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "UpazilaPSCode='" + upazilaPSCode + "'";
                }

                if (!string.IsNullOrEmpty(upazilaPSName))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "UpazilaPSName='" + upazilaPSName + "'";
                }


                strSQL += whereClause;

                return dc.ExecuteQuery<ADMUpazilaPS>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
