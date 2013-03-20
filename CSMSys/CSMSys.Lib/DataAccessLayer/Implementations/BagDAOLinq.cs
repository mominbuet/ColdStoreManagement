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
    public class BagDAOLinq : BaseDAORepository<INVBagFair, CSMSysConfiguration>, IBagDAO
    {
       protected override System.Linq.Expressions.Expression<Func<INVBagFair, bool>> GetIDSelector(int id)
        {
            return (item) => item.FairID == id;
        }

        #region Constructor/s
       public BagDAOLinq(bool isLoadWith)
        {
            if (isLoadWith)
            {
                base.DataContext.DeferredLoadingEnabled = false;

                DataLoadOptions options = new DataLoadOptions();

                options.LoadWith<INVBagFair>(d => d.FairID);
                //options.LoadWith<ADMDivision>(d => d.ADMCountry);

                base.DataContext.LoadOptions = options;
            }
            else
            {
                base.DataContext.DeferredLoadingEnabled = true;
            }

        }
        #endregion
    }
}
