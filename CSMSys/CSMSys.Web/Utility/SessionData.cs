﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSMSys.Web.Utility
{
    public class SessionData
    {
        public static T Get<T>(string key)
        {
            object value = HttpContext.Current.Session[key];

            if (value == null)
                return default(T);

            try
            {
                return (T)value;
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        public static void Put(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}