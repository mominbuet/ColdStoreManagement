using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSMSys.Lib.Model;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Manager.Administration.Application;
using CSMSys.Lib.Manager.INV;
using CSMSys.Lib.Manager.SRV;
using Newtonsoft.Json;

namespace CSMSys.Web.Utility
{
    public class UIUtility
    {
        private static Page _Page;

        public static DropDownList FillDropDownList(DropDownList ddl, IList<ComboItem> objList, string Caption)
        {
            ddl.DataTextField = "Description";
            ddl.DataValueField = "Value";
            ddl.DataSource = objList;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("   <-- Select " + Caption + " -->", "-1"));
            ddl.SelectedIndex = -1;

            return ddl;
        }

        /// <summary>
        /// Method to bind Country drop down
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="defaultDisabled"></param>
        public static void BindCountryDDL(DropDownList ddl, bool defaultDisabled)
        {
            IList<ADMCountry> country = new CountryManager().GetAllCountry();

            if (country != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = country;
                ddl.DataTextField = "CountryName";
                ddl.DataValueField = "CountryID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select Country -->", "-1"));
            }
        }

        public static void BindDivisionDDL(DropDownList ddl, int countryID, bool defaultDisabled)
        {
            IList<ADMDivision> division;

            if (countryID > 0)
            {
                division = new DivisionManager(true).GetAllDivisionByCountry(countryID);
            }
            else
            {
                division = new DivisionManager(true).GetAllDivision();
                //division = new List<ADMDivision>();
            }

            if (division != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = division;
                ddl.DataTextField = "DivisionName";
                ddl.DataValueField = "DivisionID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select Division -->", "-1"));
            }
        }

        public static void BindDistrictDDL(DropDownList ddl, int divisionID, bool defaultDisabled)
        {
            IList<ADMDistrict> district;

            if (divisionID > 0)
            {
                district = new DistrictManager(true).GetAllDistrictByDivision(divisionID);
            }
            else
            {
                district = new DistrictManager(true).GetAllDistrict();
                //district = new List<ADMDistrict>();
            }

            if (district != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = district;
                ddl.DataTextField = "DistrictName";
                ddl.DataValueField = "DistrictID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select District -->", "-1"));
            }
        }

        #region DDL Type
        public static void BindTypeDDL(DropDownList ddl, int TypeID, bool defaultDisabled)
        {
            IList<INVItemType> typeName;

            
                typeName = new ItemTypeManager(true).GetAllItemType();
                //district = new List<ADMDistrict>();
           

            if (typeName != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = typeName;
                ddl.DataTextField = "TypeName";
                ddl.DataValueField = "TypeID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select Type -->", "-1"));
            }
        }
        #endregion

        #region DDL BagFair
        public static void BindBagDDL(DropDownList ddl, int FairID, bool defaultDisabled)
        {
            IList<INVBagFair> bagFair;

            if (FairID > 0)
            {
                bagFair = new BagManager(true).GetByFairID(FairID); 
            }
            else
            {
                bagFair = new BagManager(true).GetAllBagWeight();
                //district = new List<ADMDistrict>();
            }

           // typeName = new BagManager(true).GetAllBagWeight();
            //district = new List<ADMDistrict>();


            if (bagFair != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = bagFair;
                ddl.DataTextField = "BagWeight";
                ddl.DataValueField = "FairID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select Weight -->", "-1"));
            }
        }
        #endregion



        public static void BindUpazillaPSDDL(DropDownList ddl, int districtID, bool defaultDisabled)
        {
            IList<ADMUpazilaPS> upazilaPS;

            if (districtID > 0)
            {
                upazilaPS = new UpazilaPSManager(true).GetAllUpazilaPSByDistrict(districtID);
            }
            else
            {
                upazilaPS = new UpazilaPSManager(true).GetAllUpazilaPS();
                //upazilaPS = new List<ADMUpazilaPS>();
            }

            if (upazilaPS != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = upazilaPS;
                ddl.DataTextField = "UpazilaPSName";
                ddl.DataValueField = "UpazilaPSID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select Upazila/Police Station -->", "-1"));
            }
        }

        public static void BindAreaPODDL(DropDownList ddl, int upazilaPSID, bool defaultDisabled)
        {
            IList<ADMAreaPO> areaPO;

            if (upazilaPSID > 0)
            {
                areaPO = new AreaPOManager(true).GetAllAreaPOByUpazilaPS(upazilaPSID);
            }
            else
            {
                areaPO = new AreaPOManager(true).GetAllAreaPO();
                //AreaPO = new List<ADMAreaPO>();
            }

            if (areaPO != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = areaPO;
                ddl.DataTextField = "AreaPOName";
                ddl.DataValueField = "AreaPOID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select Area/Post Office -->", "-1"));
            }
        }

        public static void BindCustomerDDL(DropDownList ddl, bool defaultDisabled)
        {
            IList<INVParty> country = new PartyManager().GetAllParty();

            if (country != null)
            {
                ddl.Items.Clear();
                ddl.DataSource = country;
                ddl.DataTextField = "PartyCode";
                ddl.DataValueField = "PartyID";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("   <-- Select Customer Code -->", "-1"));
            }
        }

        public static bool MinimumRequiredValidation(IList<Object> objList)
        {
            bool hasValue = false;

            foreach (Object item in objList)
            {
                Type t = item.GetType();

                switch (t.Name)
                {
                    case "String":

                        string value = (string)item;

                        if (!string.IsNullOrEmpty(value))
                        {
                            return true;
                        }

                        break;

                    case "TextBox":

                        TextBox textBox = (TextBox)item;

                        if (!string.IsNullOrEmpty(textBox.Text))
                        {
                            return true;
                        }

                        break;

                    case "DropDownList":

                        DropDownList ddlBox = (DropDownList)item;

                        if (!string.IsNullOrEmpty(ddlBox.SelectedItem.Text))
                        {
                            return true;
                        }

                        break;

                    default:
                        break;
                }

            }


            return hasValue;
        }

        /// <summary>
        /// Method to bind Upazilla drop down
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="defaultDisabled"></param>
        public static void DropDownListBinding<T>(DropDownList ddl, IList<T> dataList)
        {
            //if (dataList != null)
            //{
            //    JsonReader jsonReader = new JsonReader();

            //    Store store = ddl.GetStore();

            //    ddl.ValueField = "OID";
            //    ddl.DisplayField = "DropDownDisplayField" + UtilityCommon.GetCultureCodeSuffixParam();

            //    jsonReader.IDProperty = "OID";
            //    jsonReader.Fields.Add(new RecordField("DropDownDisplayField" + UtilityCommon.GetCultureCodeSuffixParam()));
            //    jsonReader.Fields.Add(new RecordField("OID"));

            //    store.Reader.Clear();
            //    store.Reader.Add(jsonReader);

            //    store.DataSource = dataList;
            //    store.DataBind();
            //}
        }

        public static void BindMemberTypeDDL(DropDownList ddl, bool defaultDisabled)
        {
            //IList<MemberType> memberTypeList = new List<MemberType>();

            //memberTypeList.Add(new MemberType { TeamMemberType = "Team Leader", TeamMemberType = "Team Leader BN" });
            //memberTypeList.Add(new MemberType { TeamMemberType = "Team Member", TeamMemberType = "Team Member BN" });

            //_Page = HttpContext.Current.Handler as Page;

            //if (memberTypeList != null)
            //{
            //    ddl.Items.Clear();

            //    foreach (MemberType memberType in memberTypeList)
            //    {
            //        string textField = string.Empty;
            //        string valueField = string.Empty;

            //        if (UtilityCommon.GetCurrentCulture().Equals(CSMSysConstants.SITE_CULTURE))
            //        {
            //            textField = memberType.TeamMemberType;
            //            valueField = memberType.TeamMemberType;
            //        }
            //        else
            //        {
            //            textField = memberType.TeamMemberType;
            //            valueField = memberType.TeamMemberType;
            //        }



            //        ddl.Items.Add(new Ext.Net.ListItem(textField, valueField));
            //    }
            //    ddl.GetStore().DataBind();
            //}
        }
    }
}