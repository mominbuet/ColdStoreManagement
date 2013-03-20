using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CSMSys.Lib.Utility
{
    public class CSMSysConstants
    {
        /// <summary>
        /// Constant for CSMSys sitemap location
        /// </summary>
        public static string SITE_MAP_SERVER_PATH = HttpContext.Current.Server.MapPath("~/Web_EN.sitemap");

        /// <summary>
        /// CSMSys Membership user object
        /// </summary>
        public static string CSMSys_USER = "CSMSys_User";
        public static string ASPNET_USER = "ASPNET_User";

        public static string AUDIT_EXECUTION_YEAR = "Audit_Execution_Year";
        public static string CSMSys_USER_DIRECTORATE = "CSMSys_User_Directorate";

        /// <summary>
        /// Audit Types
        /// </summary>
        public static string AUDIT_TYPE_COMPLIANCE = "Compliance Audit";
        public static string AUDIT_TYPE_ISSUE_BASED = "Issue Based Audit";
        public static string AUDIT_TYPE_SPECIAL = "Special Audit";
        public static string AUDIT_TYPE_PERFORMANCE = "Performance Audit";

        public static string AUDIT_TYPE_LIST = "AuditTypeList";

        /// <summary>
        /// Constants associated with audit program statuses
        /// </summary>
        public static string PROGRAM_STATUS_PENDING = "Pending For Approval";
        public static string PROGRAM_STATUS_APPROVED = "Approved Program";
        public static string PROGRAM_STATUS_CLOSED = "Closed Program";

        /// <summary>
        /// CSMSys Team Member Types
        /// </summary>
        public static string AUDIT_TEAM_LEAD = "Team Lead";
        public static string AUDIT_TEAM_MEMBER = "Team Member";

        /// <summary>
        /// Constant for Edit Obejct ID
        /// </summary>
        public static string EDIT_OBJECT_OID = "Edit_Object_OID";

        /// <summary>
        /// Event Target Constant
        /// </summary>
        public static string POST_BACK_EVENT_TARGET = "__EVENTTARGET";

        /// <summary>
        /// Culture specific constants
        /// </summary>
        public static string SITE_CULTURE = "Culture";
        public static string SITE_CULTURE_BN = "bn-IN";
        public static string SITE_CULTURE_EN = "en-US";

        /// <summary>
        /// Site Language constant
        /// </summary>
        public static string SITE_LANGUAGE_EN = "English";
        public static string SITE_LANGUAGE_BN = "Bangla";
        public static string SITE_LANGUAGE_BN_BANGLA = "বাংলা";

        /// <summary>
        /// Parent Type
        /// </summary>
        public static string PARENT_TYPE_COUNTRY = "Country";
        public static string PARENT_TYPE_DIVISION = "Division";
        public static string PARENT_TYPE_DISTRICT = "District";

        /// <summary>
        /// CSMSys SiteMap Provider
        /// </summary>
        public static string SITE_MAP_EN = "CustomXmlSiteMapProvider";
        public static string SITE_MAP_BN = "CustomXmlSiteMapProviderBN";

        /// <summary>
        /// Constant for site language change track
        /// </summary>
        public static string BOOL_SITE_LANGUAGE_CHANGED = "Bool_Site_Language_Changed";

        /// <summary>
        /// ID Prefix Constants
        /// </summary>
        public static string ID_PREFIX_TRAINIG_TYPE = "tt";

        public static string ID_PREFIX_TRAINIG_AUDITABLE_UNIT = "au";
        public static string ID_PREFIX_TRAINIG_AUDITABLE_UNIT_DETAIL = "auDE";

        public static string ID_PREFIX_COUNTRY = "cntry";
        public static string ID_PREFIX_DIVISION = "dvson";
        public static string ID_PREFIX_DISTRICT = "DST";
        public static string ID_PREFIX_DESIGNATION = "DGN";
        public static string ID_PREFIX_UPAZILLA = "UPZLA";
        public static string ID_PREFIX_PARA_CATEGORY = "PC";
        public static string ID_PREFIX_PARA_TYPE = "PT";
        public static string ID_PREFIX_DGOffice = "DGO";
        public static string ID_PREFIX_SECTOR = "SECTR";
        public static string ID_PREFIX_ORGANIZATION = "ORG";
        public static string ID_PREFIX_SECTION = "SCTON";
        public static string ID_PREFIX_LEVEL = "LVL";
        public static string ID_PREFIX_GRADE = "GRD";
        public static string ID_ROLE_PAGE_PERMISSION_MAP = "ROLP";
        public static string ID_ROLE_PARA_TYPE_MAP = "RPM";
        public static string ID_PREFIX_AUDIT_PROGRAM = "audp";
        public static string ID_PREFIX_AUDIT_PROGRAM_DETAILS = "apd";
        public static string ID_PREFIX_AUDIT_TYPE = "audtp";
        public static string ID_PREFIX_BRANCH_OFFICE = "boffice";
        public static string ID_PREFIX_ORGANIZATION_TYPE = "otype";
        public static string ID_PREFIX_TYPE_DATA = "tdata";
        public static string ID_PREFIX_VISIT_TYPE = "vtype";
        public static string ID_PREFIX_AUDIT_PERIOD = "audtp";
        public static string ID_PREFIX_AUDIT_FINANCIAL_YEAR = "aufy";
        public static string ID_PREFIX_AUDIT_FINANCIAL_YEAR_DETAIL = "aufyd";
        public static string ID_PREFIX_AUDIT_EXECUTION_YEAR = "auey";
        public static string ID_PREFIX_AUDIT_EXECUTION_YEAR_DETAIL = "aueyd";
        public static string ID_PREFIX_AUDITABLEUNIT_FOR_AUDITED = "aufa";
        public static string ID_PREFIX_AUDITABLEUNIT_SELECTION_DETAILS = "ausd";

        public static string ID_PREFIX_AUDIT_TEAM = "audt";
        public static string ID_PREFIX_AUDIT_TEAM_MEMBERS = "audtm";


        /// <summary>
        /// Button Text
        /// </summary>
        public static string BUTTON_TEXT_SAVE = "Save";
        public static string BUTTON_TEXT_UPDATE = "Update";

        /// <summary>
        /// Query string params
        /// </summary>
        //public static string QUERY_STRING_PARAM_COUNTRY_ID = "CountryID";
        //public static string QUERY_STRING_PARAM_DIVISION_ID = "DivisionID";
        //public static string QUERY_STRING_PARAM_DISTRICT_ID = "DistrictID";
        //public static string QUERY_STRING_PARAM_UPAZILLA_ID = "UpazillaID";

        //public static string QUERY_STRING_PARAM_DIRECTORATE_ID = "DirectorateID";
        //public static string QUERY_STRING_PARAM_SECTOR_ID = "SectorID";
        //public static string QUERY_STRING_PARAM_SECTION_ID = "SectionID";

        //public static string QUERY_STRING_PARAM_MINISTRY_ID = "MinistryID";
        //public static string QUERY_STRING_PARAM_AuditableUnitCategory_ID = "AuditableUnitCategoryID";

        //public static string QUERY_STRING_PARAM_EMPLOYEE_ID = "EmployeeID";
        //public static string QUERY_STRING_PARAM_LEVEL_ID = "LevelID";
        //public static string QUERY_STRING_PARAM_GRADE_ID = "GradeID";
        //public static string QUERY_STRING_PARAM_AUDIT_TEAM_ID = "AuditTeamID";
        //public static string QUERY_STRING_PARAM_AUDITABLE_UNIT_ID = "AuditableUnitID";
        //public static string QUERY_STRING_AUDIT_TYPE = "AuditType";
        //public static string QUERY_STRING_AUDIT_PROGRAM_DETAIL_ID = "AuditProgramDetail";


        /// <summary>
        /// Blank value
        /// </summary>
        public static string BLANK_VALUE = "-";

        //For ViewState
        public static string CURRENT_AUDITABLE_UNIT = "CurrentAuditableUnit";
    }
}
