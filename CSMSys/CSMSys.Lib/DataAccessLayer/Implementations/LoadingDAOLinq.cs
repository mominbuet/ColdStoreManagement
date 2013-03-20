using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;
using System.Data.Linq;
namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class LoadingDAOLinq : BaseDAORepository<INVStockLoading, CSMSysConfiguration>, ILoadingDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVStockLoading, bool>> GetIDSelector(int id)
        {
            return (item) => item.LoadingID == id;
        }
        public INVStockLoading checklocation(int chamber, int floor, int pocket)
        {
            //try
            //{
            INVStockLoading invsl = new INVStockLoading();
            string whereClause = string.Empty;
            string queryTable = "INVStockLoading";

            DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

            string strSQL = "SELECT * FROM " + queryTable;

            if ((chamber != 0))
            {
                whereClause += " WHERE ChamberNo = '" + chamber + "' and Floor='" + floor + "' and Pocket='" + pocket + "';";
            }

            strSQL += whereClause;

            int cnt = dc.ExecuteQuery<INVStockLoading>(@strSQL).Count();
            invsl = (cnt > 0) ? dc.ExecuteQuery<INVStockLoading>(@strSQL).First() : null;
            return invsl;
            //}
            //catch (Exception ex)
            //{
            //    //_Logger.Error(ex);
            //    return null;
            //}
        }
        public INVStockSerial GetPartyBySerial(string serialNo)
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

                return dc.ExecuteQuery<INVStockSerial>(@strSQL).First();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
        public INVStockLoading GetLoadBySerial(string serialNo)
        {
            try
            {
                string whereClause = string.Empty;
                string queryTable = "INVStockLoading";

                DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

                string strSQL = "SELECT * FROM " + queryTable;

                if (!string.IsNullOrEmpty(serialNo))
                {
                    whereClause += " WHERE SerialID = '" + serialNo + "' ";
                }

                strSQL += whereClause;

                return dc.ExecuteQuery<INVStockLoading>(@strSQL).First();
            }
            catch (Exception ex)
            {
                _Logger.Error(ex);
                return null;
            }
        }
    }
}
