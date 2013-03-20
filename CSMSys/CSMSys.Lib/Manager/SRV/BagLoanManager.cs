using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;
using System.Data.SqlClient;
using System.Configuration;

namespace CSMSys.Lib.Manager.SRV
{
    public class BagLoanManager
    {
        
        #region Properties
        IBagLoanDAO _IBagLoanDAO;
        #endregion

        #region Constructor
        public BagLoanManager()
        {
            _IBagLoanDAO = new BagLoanDAOLinq();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get all District from District table 
        /// </summary>
        /// <returns></returns>
        public IList<SRVBagLoan> GetAllItemType()
        {
            try
            {
                return _IBagLoanDAO.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool SaveBagLoanDetails(SRVBagLoan bagLoan)
        {
            try
            {
                if (bagLoan.BagLoanID == 0)
                {
                    return new BagLoanDAOLinq().Add(bagLoan);
                }
                else
                {
                    return new BagLoanDAOLinq().Edit(bagLoan);
                }
                // return new RegistrationDAOLinq().Add(party);
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        #region getMaxBagLoanID
        public int getNextRegistrationID()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
            SqlCommand command = connection.CreateCommand();
            SqlDataReader Reader;
            command.CommandText = "SELECT IDENT_CURRENT('SRVBagLoan') as id";
            connection.Open();
            Reader = command.ExecuteReader();
            int log = 0;
            while (Reader.Read())
            {
                if (Reader["id"] != DBNull.Value)
                    log = Convert.ToInt16(Reader["id"]);
                break;

            }
            Reader.Close();
            connection.Close();

            //string strSQL = "";
            return log;
            ////IList<SRVLoanDisburse> allLoans = new loan();
            //return 0;
        }
        #endregion

        public SRVBagLoan GetBagLoanByID(int id)
        {
            return _IBagLoanDAO.PickByID(id);
        }

        public SRVBagLoan GetPartyByID(int id)
        {
            return _IBagLoanDAO.PickByID(id);
        }

        #region getTotalEmptyBagByparty
        public string getBagLoanByID(int partyID)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
          //  SqlCommand command = connection.CreateCommand();
            //SqlDataReader Reader;

            conn.Open();
            string whereClause = string.Empty;
            string queryTable = "SRVBagLoan";
            string strSQL = "SELECT SUM(BagNumber) FROM " + queryTable;

            if ((partyID != 0))
            {
                whereClause += " WHERE PartyID LIKE '%" + partyID + "%';";
            }

            strSQL += whereClause;
            SqlCommand cmd = new SqlCommand(strSQL, conn);

            string mySum = cmd.ExecuteScalar().ToString();

            conn.Dispose();
            cmd.Dispose();
            return mySum;
        }
        #endregion

        public int GetAllBagLoansByparty(int pid)
        {
            return _IBagLoanDAO.getAllBagLoansByparty(pid);
        }
    }
}
