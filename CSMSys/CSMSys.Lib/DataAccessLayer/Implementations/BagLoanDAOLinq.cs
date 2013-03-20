using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;
using System.Data.SqlClient;


namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class BagLoanDAOLinq : BaseDAORepository<SRVBagLoan, CSMSysConfiguration>, IBagLoanDAO
    {
       protected override System.Linq.Expressions.Expression<Func<SRVBagLoan, bool>> GetIDSelector(int id)
        {
            return (item) => item.BagLoanID == id;
        }

       public int getAllBagLoansByparty(int publicId)
       {
           //IList<SRVLoanDisburse> invsl = new SRVLoanDisburse();
           SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CSMSysConnection"].ConnectionString);
           SqlCommand command = connection.CreateCommand();
           SqlDataReader Reader;
           command.CommandText = "SELECT SUM(BagNumber) as sm FROM SRVBagLoan  WHERE PartyID ="+publicId;
           connection.Open();
           Reader = command.ExecuteReader();
           int log = 0;
           while (Reader.Read())
           {
               if (Reader["sm"] != DBNull.Value)
                   log = int.Parse((Reader["sm"]).ToString());
               break;

           }
           Reader.Close();
           connection.Close();

           //string strSQL = "";
           return log;
       }

     
    }
}
