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
    public class AreaPODAOLinq : BaseDAORepository<ADMAreaPO, CSMSysConfiguration>, IAreaPODAO
    {
        protected override System.Linq.Expressions.Expression<Func<ADMAreaPO, bool>> GetIDSelector(int id)
        {
            return (item) => item.AreaPOID == id;
        }

        public AreaPODAOLinq(bool isLoadWith)
        {
            if (isLoadWith)
            {
                base.DataContext.DeferredLoadingEnabled = false;

                DataLoadOptions options = new DataLoadOptions();

                options.LoadWith<ADMAreaPO>(u => u.UpazilaPSID);
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

        public IList<ADMAreaPO> SearchAreaPO(int areaPOID, string areaPOCode, string areaPOName)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "t_ADMAreaPO";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (areaPOID > 0)
                {
                    whereClause += " WHERE DistrictID" + " = " + areaPOID;
                }

                if (!string.IsNullOrEmpty(areaPOCode))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "AreaPOCode='" + areaPOCode + "'";
                }

                if (!string.IsNullOrEmpty(areaPOName))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "AreaPOName='" + areaPOName + "'";
                }


                strSQL += whereClause;

                return dc.ExecuteQuery<ADMAreaPO>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
