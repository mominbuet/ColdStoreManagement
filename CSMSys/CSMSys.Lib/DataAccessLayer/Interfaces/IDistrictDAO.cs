using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IDistrictDAO : IRepository<ADMDistrict>
    {
        IList<ADMDistrict> SearchDistrict(string districtCode, string districtName);
    }
}
