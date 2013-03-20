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
    public class ItemTypeDAOLinq : BaseDAORepository<INVItemType, CSMSysConfiguration>, IItemTypeDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVItemType, bool>> GetIDSelector(int id)
        {
            return (item) => item.TypeID == id;
        }

        #region Constructor/s
        public ItemTypeDAOLinq(bool isLoadWith)
        {
            if (isLoadWith)
            {
                base.DataContext.DeferredLoadingEnabled = false;

                DataLoadOptions options = new DataLoadOptions();

                options.LoadWith<INVItemType>(d => d.TypeID);
                //options.LoadWith<ADMDivision>(d => d.ADMCountry);

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
        public IList<INVItemType> SearchItem(string typeID, string typeName)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "t_ADMDistrict";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(typeID))
                {
                    whereClause += " WHERE DistrictCode = '" + typeID + "'";
                }

                if (!string.IsNullOrEmpty(typeName))
                {
                    if (string.IsNullOrEmpty(whereClause))
                    {
                        whereClause += " WHERE ";
                    }
                    else
                    {
                        whereClause += " AND ";
                    }

                    whereClause += "DistrictName='" + typeName + "'";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVItemType>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
