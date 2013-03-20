using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;
using System.Data.Linq;
using System.Configuration;
using System.Data.SqlClient;
namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class LoanCollectionDAOLinq:  BaseDAORepository<SRVLoanCollection, CSMSysConfiguration>, ILoanCollectionDAO
    {
        protected override System.Linq.Expressions.Expression<Func<SRVLoanCollection, bool>> GetIDSelector(int id)
        {
            return (item) => item.LCollectionID == id;
        }
        public IList<SRVLoanCollection> getAllLoansLikeSerialID(int serialid)
        {
            //IList<SRVLoanDisburse> invsl = new SRVLoanDisburse();
            string whereClause = string.Empty;
            string queryTable = "SRVLoanCollection";

            DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

            string strSQL = "SELECT * FROM " + queryTable;

            if ((serialid != 0))
            {
                whereClause += " WHERE serialIDs LIKE '%" + serialid + "%';";
            }

            strSQL += whereClause;

            //int cnt = dc.ExecuteQuery<INVStockLoading>(@strSQL).Count();
            //invsl = (cnt > 0) ? dc.ExecuteQuery<INVStockLoading>(@strSQL).First() : null;
            return dc.ExecuteQuery<SRVLoanCollection>(@strSQL).ToList();
        }
        public IList<SRVLoanCollection> getAllLoansByParty(int partyid)
        {
            //IList<SRVLoanDisburse> invsl = new SRVLoanDisburse();
            string whereClause = string.Empty;
            string queryTable = "SRVLoanCollection";

            DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

            string strSQL = "SELECT * FROM " + queryTable;

            if ((partyid != 0))
            {
                whereClause += " WHERE PartyID = '" + partyid + "';";
            }

            strSQL += whereClause;

            //int cnt = dc.ExecuteQuery<INVStockLoading>(@strSQL).Count();
            //invsl = (cnt > 0) ? dc.ExecuteQuery<INVStockLoading>(@strSQL).First() : null;
            return dc.ExecuteQuery<SRVLoanCollection>(@strSQL).ToList();
        }
    }
}
