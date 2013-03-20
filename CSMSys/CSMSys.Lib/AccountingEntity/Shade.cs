using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Shade : BaseObject
    {
        public Shade() : base() { }

        #region Fields
        private int intShadeID = 0;
        private string strShadeNo = "";
        #endregion

        #region Properties

        public int ShadeID
        {
            get { return intShadeID; }
            set { intShadeID = value; }
        }

        public string ShadeNo
        {
            get { return strShadeNo; }
            set { strShadeNo = value; }
        }

        #endregion
    }
}
