using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Manager.SRV
{
    public class BookIssueManager
    {
        
        #region Properties
        IBookIssueDAO _IBookIssueDAO;
        #endregion

        #region Constructor
        public BookIssueManager()
        {
            _IBookIssueDAO = new BookIssueDAOLinq();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Get all District from District table 
        /// </summary>
        /// <returns></returns>
        public IList<SRVBookIssue> GetAllItemType()
        {
            try
            {
                return _IBookIssueDAO.All().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool SaveBookIssueDetails(SRVBookIssue bookIssue)
        {
            try
            {
                if (bookIssue.BookID == 0)
                {
                    return new BookIssueDAOLinq().Add(bookIssue);
                }
                else
                {
                    return new BookIssueDAOLinq().Edit(bookIssue);
                }
                // return new RegistrationDAOLinq().Add(party);
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}
