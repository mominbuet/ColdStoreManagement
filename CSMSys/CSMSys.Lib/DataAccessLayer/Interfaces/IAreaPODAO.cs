using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IAreaPODAO : IRepository<ADMAreaPO>
    {
        IList<ADMAreaPO> SearchAreaPO(int upazilaPSID, string areaPOCode, string areaPOName);
    }
}
