using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;

namespace CSMSys.Lib.DataAccessLayer.Interfaces
{
    public interface IDivisionDAO : IRepository<ADMDivision>
    {
        IList<ADMDivision> SearchDivision(string divisionCode, string divisionName);
    }
}
