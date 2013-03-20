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
    public class ItemDetailsDAOLinq : BaseDAORepository<INVItemDetail, CSMSysConfiguration>, IItemDetailsDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVItemDetail, bool>> GetIDSelector(int id)
        {
            return (item) => item.ItemDetailID == id;
        }

         #region Constructor/s
        public ItemDetailsDAOLinq(bool isLoadWith)
        {
            if (isLoadWith)
            {
                base.DataContext.DeferredLoadingEnabled = false;

                DataLoadOptions options = new DataLoadOptions();

                options.LoadWith<INVItemDetail>(d => d.RegistrationID);
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
