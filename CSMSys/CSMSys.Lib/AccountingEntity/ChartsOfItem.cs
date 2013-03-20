using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSMSys.Lib.AccountingEntity
{
    public class ChartsOfItem : BaseObject
    {
        public ChartsOfItem() : base() { }
        #region Fields
        private int numCountID;
        private string strCountName;
        private string strCountType;
        private int numItemID;
        private string strItemCode;
        private string strHSCode;
        private string strItemName;
        private string strItemDescription;
        private string strItemPurpose;
        private int numItemGroupID;
        private string strItemCatagory;
        private int numSizeID;
        private int numColorID;
        private int numShadeID;

        private int numUnitID;
        private int numOpeningQty;
        private double dbOpeningValue;
        private string strItemStatus;
        private int numMinQty;
        private double dbUnitPrice;
        private Byte[] bImage;
        private int numCurrentItem;

        #endregion
        #region Properties
        public int CountID
        {
            get { return numCountID; }
            set { numCountID = value; }
        }
        public string CountName
        {
            get { return strCountName; }
            set { strCountName = value; }
        }

        public string CountType
        {
            get { return strCountType; }
            set { strCountType = value; }
        }
        public int ItemID
        {
            get { return numItemID; }
            set { numItemID = value; }
        }
        public string ItemCode
        {
            get { return strItemCode; }
            set { strItemCode = value; }

        }
        public string HSCode
        {
            get { return strHSCode; }
            set { strHSCode = value; }

        }
        public string ItemName
        {
            get { return strItemName; }
            set { strItemName = value; }
        }
        public string ItemDescription
        {
            get { return strItemDescription; }
            set { strItemDescription = value; }
        }
        public string ItemPurpose
        {
            get { return strItemPurpose; }
            set { strItemPurpose = value; }
        }
        public int GroupID
        {
            get { return numItemGroupID; }
            set { numItemGroupID = value; }
        }
        public string ItemCatagory
        {
            get { return strItemCatagory; }
            set { strItemCatagory = value; }
        }
        public int SizeID
        {
            get { return numSizeID; }
            set { numSizeID = value; }

        }
        public int ColorID
        {
            get { return numColorID; }
            set { numColorID = value; }
        }
        public int ShadeID
        {
            get { return numShadeID; }
            set { numShadeID = value; }
        }

        public int UnitID
        {
            get { return numUnitID; }
            set { numUnitID = value; }
        }
        public double OpeningValue
        {
            get { return dbOpeningValue; }
            set { dbOpeningValue = value; }
        }
        public int OpeningQty
        {
            get { return numOpeningQty; }
            set { numOpeningQty = value; }
        }
        public string ItemStatus
        {
            get { return strItemStatus; }
            set { strItemStatus = value; }
        }
        public int MinQty
        {
            get { return numMinQty; }
            set { numMinQty = value; }
        }
        public double UnitPrice
        {
            get { return dbUnitPrice; }
            set { dbUnitPrice = value; }
        }
        public Byte[] Picture
        {
            get { return bImage; }
            set { bImage = value; }
        }
        public int CurrentItem
        {
            get { return numCurrentItem; }
            set { numCurrentItem = value; }
        }

        #endregion

    }
}
