using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.DataAccessLayer.Implementations
{
    public class RelocateDAOLinq : BaseDAORepository<INVRelocate, CSMSysConfiguration> ,IRelocateDAO
    {
        protected override System.Linq.Expressions.Expression<Func<INVRelocate, bool>> GetIDSelector(int id)
        {
            return (item) => item.RelocateID == id;
        }
    }
}
