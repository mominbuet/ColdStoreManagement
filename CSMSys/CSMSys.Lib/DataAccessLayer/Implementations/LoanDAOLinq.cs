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
    public class LoanDAOLinq  : BaseDAORepository<SRVLoanDisburse, CSMSysConfiguration>, ILoanDAO
    {
        protected override System.Linq.Expressions.Expression<Func<SRVLoanDisburse, bool>> GetIDSelector(int id)
        {
            return (item) => item.LoanID == id;
        }
        public IList<SRVLoanDisburse> getAllLoansLikeSerialID(int serialid)
        {
            //IList<SRVLoanDisburse> invsl = new SRVLoanDisburse();
            string whereClause = string.Empty;
            string queryTable = "SRVLoanDisburse";

            DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

            string strSQL = "SELECT * FROM " + queryTable;

            if ((serialid != 0))
            {
                whereClause += " WHERE serialIDs LIKE '%" + serialid + "%';";
            }

            strSQL += whereClause;

            //int cnt = dc.ExecuteQuery<INVStockLoading>(@strSQL).Count();
            //invsl = (cnt > 0) ? dc.ExecuteQuery<INVStockLoading>(@strSQL).First() : null;
            return dc.ExecuteQuery<SRVLoanDisburse>(@strSQL).ToList();
        }
        public int getNextCaseNo()
        {
            //SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            //SqlCommand command = connection.CreateCommand();
            //SqlDataReader Reader;
            //command.CommandText = "SELECT IDENT_CURRENT('srvloan') as id";
            //connection.Open();
            //Reader = command.ExecuteReader();
            //int log = 0;
            //while (Reader.Read())
            //{
            //    if(Reader["id"]!=DBNull.Value)
            //        log = Convert.ToInt16(Reader["id"]);
            //    break;

            //}
            //Reader.Close();
            //connection.Close();

            ////string strSQL = "";
            //return log;
            //IList<SRVLoanDisburse> allLoans = new loan();
            return 0;
        }

        public IList<SRVRegistration> getAllRequisitionByParty(int partyid, string req)
        {
            //IList<SRVLoanDisburse> invsl = new SRVLoanDisburse();
            string whereClause = string.Empty;
            string queryTable = "SRVRegistration";

            DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

            string strSQL = "SELECT * FROM " + queryTable;

            if ((partyid != 0))
            {
                whereClause += " WHERE partyID = '" + partyid + "' and requisitioned='"+req+"';";
            }

            strSQL += whereClause;

            //int cnt = dc.ExecuteQuery<INVStockLoading>(@strSQL).Count();
            //invsl = (cnt > 0) ? dc.ExecuteQuery<INVStockLoading>(@strSQL).First() : null;
            return dc.ExecuteQuery<SRVRegistration>(@strSQL).ToList();
        }
        public IList<SRVLoanDisburse> getAllLoansByParty(int partyid)
        {
            //IList<SRVLoanDisburse> invsl = new SRVLoanDisburse();
            string whereClause = string.Empty;
            string queryTable = "SRVLoanDisburse";

            DataContext dc = new DataContext(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);

            string strSQL = "SELECT * FROM " + queryTable;

            if ((partyid != 0))
            {
                whereClause += " WHERE partyID = '" + partyid + "';";
            }

            strSQL += whereClause;

            //int cnt = dc.ExecuteQuery<INVStockLoading>(@strSQL).Count();
            //invsl = (cnt > 0) ? dc.ExecuteQuery<INVStockLoading>(@strSQL).First() : null;
            return dc.ExecuteQuery<SRVLoanDisburse>(@strSQL).ToList();
        }


    }
}
