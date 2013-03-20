using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class Company : BaseObject
    {
        public Company() : base() { }


        #region Fields
        private int numCompanyID;
        private string strCompanyName;
        private string strAddressLine1;
        private string strAddressLine2;
        private string strPhone;
        private string strFax;
        private string strWebSite;
        private string strEmail;
        private int numBusinessSubTypeID;
        private string strTradeLicense;
        private string strTINno;
        private string strIRCNo;
        private string strERCNo;
        private string strMembershipNo1;
        private string strMembershipNo2;
        private string strContactPerson;
        private string strContactPersonPhone;
        private Byte[] objCompanyLogo;
        private int numCurrencyID;
        #endregion


        #region Properties
        public new int CompanyID
        {
            get { return numCompanyID; }
            set { numCompanyID = value; }
        }
        public string CompanyName
        {
            get { return strCompanyName; }
            set { strCompanyName = value; }
        }
        public string AddressLine1
        {
            get { return strAddressLine1; }
            set { strAddressLine1 = value; }
        }
        public string AddressLine2
        {
            get { return strAddressLine2; }
            set { strAddressLine2 = value; }
        }
        public string Phone
        {
            get { return strPhone; }
            set { strPhone = value; }
        }
        public string Fax
        {
            get { return strFax; }
            set { strFax = value; }
        }

        public string WebSite
        {
            get { return strWebSite; }
            set { strWebSite = value; }
        }
        public string Email
        {
            get { return strEmail; }
            set { strEmail = value; }
        }
        public int BusinessSubTypeID
        {
            get { return numBusinessSubTypeID; }
            set { numBusinessSubTypeID = value; }
        }
        public string TradeLicense
        {
            get { return strTradeLicense; }
            set { strTradeLicense = value; }
        }
        public string TINno
        {
            get { return strTINno; }
            set { strTINno = value; }
        }
        public string IRCNo
        {
            get { return strIRCNo; }
            set { strIRCNo = value; }
        }
        public string ERCNo
        {
            get { return strERCNo; }
            set { strERCNo = value; }
        }
        public string MembershipNo1
        {
            get { return strMembershipNo1; }
            set { strMembershipNo1 = value; }
        }
        public string MembershipNo2
        {
            get { return strMembershipNo2; }
            set { strMembershipNo2 = value; }
        }
        public string ContactPerson
        {
            get { return strContactPerson; }
            set { strContactPerson = value; }
        }
        public string ContactPersonPhone
        {
            get { return strContactPersonPhone; }
            set { strContactPersonPhone = value; }
        }
        public Byte[] CompanyLogo
        {
            get { return objCompanyLogo; }
            set { objCompanyLogo = value; }
        }
        public int CurrencyID
        {
            get { return numCurrencyID; }
            set { numCurrencyID = value; }
        }
        #endregion
    }
}
