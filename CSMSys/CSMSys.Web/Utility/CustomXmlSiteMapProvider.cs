using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSMSys.Web.Utility
{
    public class CustomXmlSiteMapProvider : XmlSiteMapProvider
    {
        #region Properties

        #endregion

        #region Constructor/s
        public CustomXmlSiteMapProvider()
        {

        }
        #endregion

        #region Public Methods
        /// <summary>
        /// XMLSiteMapProvider method override to authenticate nodes that are accessible to user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            bool isAccessable = false;
            if (node.Roles.Count == 0)
            {
                return isAccessable;
            }

            foreach (object role in node.Roles)
            {
                if (role.ToString().Contains('!'))
                {
                    string roleAsString = role.ToString().Substring(1, (role.ToString().Length - 1));
                    if (context.User.IsInRole(roleAsString))
                    {
                        isAccessable = false;
                        break;
                    }
                }
                else
                {
                    if (context.User.IsInRole(role.ToString()))
                    {
                        isAccessable = true;
                        break;
                    }
                    if (role.ToString().Equals("*"))
                    {
                        isAccessable = true;
                        break;
                    }
                }
            }
            return isAccessable;
        }

        #endregion

        #region Private Methods

        #endregion

        #region Event Handlers

        #endregion
    }
}