using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using CSMSys.Lib.Utility;
using CSMSys.Lib.Model;

namespace CSMSys.Web.Utility
{
    public class WebCommonUtility
    {
        private static Page _Page = HttpContext.Current.Handler as Page;

        public static string GetCurrentCulture()
        {
            if (_Page.Session[CSMSysConstants.SITE_CULTURE] != null)
            {
                return _Page.Session[CSMSysConstants.SITE_CULTURE].ToString();
            }

            return null;
        }

        public static Guid GetCSMSysUserKey()
        {
            if (_Page.Session[CSMSysConstants.ASPNET_USER] != null)
            {
                return (Guid)Membership.GetUser(_Page.Session[CSMSysConstants.ASPNET_USER].ToString()).ProviderUserKey;
            }

            ///This will be removed
            return new Guid();
        }

        /// <summary>
        /// Method to get value of editing object that is stored in session for a particular page
        /// </summary>
        /// <returns></returns>
        public static string GetEditObjectOID()
        {
            if (_Page.Session[CSMSysConstants.EDIT_OBJECT_OID] != null)
            {
                return _Page.Session[CSMSysConstants.EDIT_OBJECT_OID].ToString();
            }

            return null;
        }

        /// <summary>
        /// Method to get value of editing object that is stored in session for a particular page
        /// </summary>
        /// <param name="oid"></param>
        public static void SetEditObjectOID(string oid)
        {
            RemoveEditObjectOIDSession();
            _Page.Session.Add(CSMSysConstants.EDIT_OBJECT_OID, oid);
        }

        /// <summary>
        /// Method to remove existing editing object oid that is stored in session
        /// </summary>
        public static void RemoveEditObjectOIDSession()
        {
            _Page.Session.Remove(CSMSysConstants.EDIT_OBJECT_OID);
        }

        public static bool HasLanguageSwitched()
        {
            if (_Page.Session[CSMSysConstants.BOOL_SITE_LANGUAGE_CHANGED] != null
                && Convert.ToBoolean(_Page.Session[CSMSysConstants.BOOL_SITE_LANGUAGE_CHANGED]) == true)
            {
                return true;
            }

            return false;
        }

        public static void ResetLanguageSwitchSession()
        {
            UpdateLanguageSwitchState(false);
        }

        public static void UpdateLanguageSwitchState(bool b)
        {
            _Page.Session.Add(CSMSysConstants.BOOL_SITE_LANGUAGE_CHANGED, b);
        }

        /// <summary>
        /// Method to get current language signature for the site.
        /// The signature return is either - "" or ''
        /// This is useful for search param to append it with param value
        /// </summary>
        /// <returns></returns>
        public static string GetLanguageCulture()
        {
            string culture = CSMSysConstants.SITE_CULTURE;

            if (_Page.Session[CSMSysConstants.SITE_CULTURE] != null)
            {
                culture = _Page.Session[CSMSysConstants.SITE_CULTURE].ToString();
            }

            return culture.Trim();
        }

        /// <summary>
        /// Method to get current short culture code
        /// i.e. values - EN or BN
        /// </summary>
        /// <returns></returns>
        public static string GetCultureCodeSuffixParam()
        {
            string cultureSuffix = "";

            if (GetCurrentCulture() != null)
            {
                if (GetCurrentCulture().Equals(CSMSysConstants.SITE_CULTURE))
                {
                    cultureSuffix = "";
                }
            }

            return cultureSuffix;
        }

        public static string GetValueFromHttpQueryString(string param)
        {
            try
            {
                _Page = HttpContext.Current.Handler as Page;

                if (_Page.Request.QueryString[param] != null)
                {
                    return _Page.Request.QueryString[param].Trim().ToString();
                }

                return null;
            }
            catch (Exception ex)
            {
                //_Logger.Error("Error during data " + param + " reading from QueryString..", ex);
                return "";
            }
        }

        /// <summary>
        /// Get a boolean value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetBoolean(object obj)
        {
            bool b = false;
            if (obj != null)
            {
                bool.TryParse(obj.ToString(), out b);
            }

            return b;
        }

        /// <summary>
        ///  Get a DateTime value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            DateTime dt = new DateTime();
            if (obj != null)
            {
                DateTime.TryParse(obj.ToString().Trim(), out dt);
            }

            return dt;
        }

        /// <summary>
        ///  Get a DateTime value examin the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Nullable<DateTime> GetNullableDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }
            else
            {
                DateTime dt = new DateTime();

                if (date != null)
                {
                    bool res = DateTime.TryParse(date.Trim(), out dt);

                    if (!res)
                    {
                        return null;
                    }
                }

                return dt;
            }
        }

        /// <summary>
        /// Convert Minimum date to empty string
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string HandleInvalidDate(DateTime dt)
        {
            if (dt == DateTime.MinValue || dt.Year.ToString() == "1899")
            {
                return string.Empty;
            }

            return dt.ToShortDateString();
        }

        public static string FormatStringForDBNull(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return "null";
            }

            return strValue;
        }

        public static int GetFormInteger(string strValue)
        {
            int res = 0;

            Int32.TryParse(strValue, out res);

            return res;
        }

        public static double GetFormDouble(string strValue)
        {
            double res = 0;

            double.TryParse(strValue, out res);

            return res;
        }

    }
}