using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IUpazilaPSDAO : IRepository<ADMUpazilaPS>
    {
        IList<ADMUpazilaPS> SearchUpazilaPS(int districtID, string upazillaCode, string upazillaName);
    }
}
