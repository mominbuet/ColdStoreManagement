using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Manager.INV
{
    public class RelocateManager
    {
        IRelocateDAO _relocateDAO;
        #region Constructor
        public RelocateManager()
        {
            _relocateDAO = new RelocateDAOLinq();
        }
        #endregion
        public bool SaveRelocate(INVRelocate reloc)
        {
            try
            {
                if (reloc.RelocateID == 0)
                {
                    return _relocateDAO.Add(reloc);
                }
                else
                {
                    return _relocateDAO.Edit(reloc);      //bool deleted
                }

               
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
