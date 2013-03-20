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
    public class BookIssueDAOLinq : BaseDAORepository<SRVBookIssue, CSMSysConfiguration>, IBookIssueDAO
    {
        protected override System.Linq.Expressions.Expression<Func<SRVBookIssue, bool>> GetIDSelector(int id)
        {
            return (item) => item.BookID == id;
        }

     
    }
}
