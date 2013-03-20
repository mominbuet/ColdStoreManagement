using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class DeliveryDAOLinq  :  BaseDAORepository<ADMCountry, CSMSysConfiguration>, IDeliveryDAO
    {
        //change after srvdelivery is added.
        protected override System.Linq.Expressions.Expression<Func<ADMCountry, bool>> GetIDSelector(int id)
        {
            return (item) => item.CountryID == id;
        }
    }
}
