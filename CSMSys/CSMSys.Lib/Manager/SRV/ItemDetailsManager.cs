using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSMSys.Lib.Model;
using CSMSys.Lib.DataAccessLayer.Interfaces;
using CSMSys.Lib.DataAccessLayer.Implementations;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Manager.SRV
{
    public class ItemDetailsManager
    {
        #region Properties
        IItemDetailsDAO _IItemDetailsDAOLinq;
        #endregion            

        #region Constructor
        public ItemDetailsManager(bool isLoadWith)
        {
            _IItemDetailsDAOLinq = new ItemDetailsDAOLinq(isLoadWith);
        }
        #endregion

        #region Public Methods

        public bool SaveItemDetails(INVItemDetail item)
        {
            try
            {
                if (item.ItemDetailID == 0)
                {
                    return _IItemDetailsDAOLinq.Add(item);
                }
                else
                {
                    return _IItemDetailsDAOLinq.Edit(item, true);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
