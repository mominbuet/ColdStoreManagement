using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class ItemDetailShadDAOLinq    : BaseDAORepository<INVItemDetail, CSMSysConfiguration>, IItemDetailShadDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVItemDetail, bool>> GetIDSelector(int id)
        {
            return (item) => item.ItemDetailID == id;
        }
        public IList<INVItemDetail> GetItemDetailByRegID(string regid)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVItemDetail";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(regid))
                {
                    whereClause += " WHERE RegistrationID = '" + regid + "' ";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVItemDetail>(@strSQL).ToList();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
