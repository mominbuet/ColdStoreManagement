using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class CompanySettings : BaseObject
    {
        public CompanySettings() : base() { }

        private int _slno;
        private string _SettingCode;
        private string _SettingTitle;
        private string _SettingValue;

        public int SlNo
        {
            get { return _slno; }
            set { _slno = value; }
        }
        public string SettingCode
        {
            get { return _SettingCode; }
            set { _SettingCode = value; }
        }
        public string SettingTitle
        {
            get { return _SettingTitle; }
            set { _SettingTitle = value; }
        }
        public string SettingValue
        {
            get { return _SettingValue; }
            set { _SettingValue = value; }
        }
        
    }
}
