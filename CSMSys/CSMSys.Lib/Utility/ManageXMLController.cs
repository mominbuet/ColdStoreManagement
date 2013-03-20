using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.Web;
using CSMSys.Lib.BusinessObjects;
using CSMSys.Lib.Model;
using CSMSys.Lib.Utility;

namespace CSMSys.Lib.Utility
{
    /// <summary>
    /// Class to manipulate XML site map data - reezvi
    /// </summary>
    public class ManageXMLController
    {
        #region Propeties
        //logger to be used for this class
        static log4net.ILog _Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static object _Lock = new object();
        #endregion

        #region XML data manipulation methods

        #region Site Map Related

        /// <summary>
        /// Utility method to read sitemap nodes.
        /// This method will only return a list of site map nodes that are having urls. - reezvi
        /// </summary>
        /// <returns></returns>
        public static List<XMLSiteMapParserObject> ReadSiteMapNodes()
        {
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(CSMSysConstants.SITE_MAP_SERVER_PATH);
            }
            catch (Exception ex)
            {
                _Logger.Error("Error while loading XML file.", ex);
                return null;
            }

            if (xmlDoc != null)
            {
                XmlNode rootNode = xmlDoc.DocumentElement;
                List<XMLSiteMapParserObject> dataList = new List<XMLSiteMapParserObject>();

                return GetXMLSiteMapNodeList(dataList, rootNode);
            }

            return null;
        }

        /// <summary>
        /// Method that uses recursion to drill down and return a set of active (those are having urls) site map nodes - reezvi
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="rootNode"></param>
        /// <returns></returns>
        private static List<XMLSiteMapParserObject> GetXMLSiteMapNodeList(List<XMLSiteMapParserObject> dataList, XmlNode rootNode)
        {
            if (rootNode != null)
            {
                XmlNodeList nodeList = rootNode.ChildNodes;

                foreach (XmlNode node in nodeList)
                {
                    if (node.Name.ToLower().Equals("sitemapnode") && node.Attributes["url"] != null && !string.IsNullOrEmpty(node.Attributes["url"].Value))
                    {
                        XMLSiteMapParserObject parserObject = new XMLSiteMapParserObject();

                        parserObject.Title = node.Attributes["title"].Value;
                        parserObject.URL = node.Attributes["url"].Value;
                        parserObject.Description = node.Attributes["description"].Value;
                        parserObject.Role = node.Attributes["roles"].Value;

                        dataList.Add(parserObject);
                    }
                    if (node.ChildNodes.Count > 0)
                    {
                        dataList = GetXMLSiteMapNodeList(dataList, node);
                    }
                }
            }

            return dataList;
        }

        /// <summary>
        /// Method to update site map in reference to selected nodes/pages for a specific role - reezvi
        /// </summary>
        /// <param name="objXMLSiteMapParserObjectList"></param>
        /// <param name="roleName"></param>
        public static void UpdateSiteMap(IList<XMLSiteMapParserObject> objXMLSiteMapParserObjectList, string roleName)
        {
            lock (_Lock)
            {
                XmlDocument xmlDocument = new XmlDocument();

                try
                {
                    xmlDocument.Load(CSMSysConstants.SITE_MAP_SERVER_PATH);
                }
                catch (Exception ex)
                {
                    _Logger.Error("Error while loading Sitemap XML file.", ex);
                }

                if (xmlDocument != null)
                {
                    XmlNode root = xmlDocument.DocumentElement;

                    UpdateSiteMapWithNewRole(objXMLSiteMapParserObjectList, roleName, root);

                    xmlDocument.Save(CSMSysConstants.SITE_MAP_SERVER_PATH);
                }
            }
        }

        /// <summary>
        /// Recursive method to drill down and update sitemap nodes/pages permission for the role
        /// This also updates permission to parent node/s - reezvi
        /// </summary>
        /// <param name="objXMLSiteMapParserObjectList"></param>
        /// <param name="roleName"></param>
        /// <param name="root"></param>
        private static void UpdateSiteMapWithNewRole(IList<XMLSiteMapParserObject> objXMLSiteMapParserObjectList, string roleName, XmlNode root)
        {
            if (root != null)
            {
                try
                {
                    XmlNodeList nodeList = root.ChildNodes;
                    XmlNode parentNode;

                    foreach (XmlNode node in nodeList)
                    {
                        foreach (XMLSiteMapParserObject item in objXMLSiteMapParserObjectList)
                        {
                            if (node.Attributes == null || node.Attributes["title"] == null)
                            {
                                break;
                            }
                            else
                            {
                                if (item.Title.Equals(node.Attributes["title"].Value))
                                {
                                    ///setting role to node
                                    if (item.IsSelected)
                                    {
                                        if (!node.Attributes["roles"].Value.Contains(roleName))
                                        {
                                            if (!string.IsNullOrEmpty(node.Attributes["roles"].Value))
                                            {
                                                node.Attributes["roles"].Value += ",";
                                            }

                                            node.Attributes["roles"].Value += roleName;
                                        }

                                        ///setting role in parent node
                                        parentNode = node.ParentNode;

                                        while (parentNode != null && parentNode.Name.ToLower().Equals("sitemapnode"))
                                        {
                                            if (parentNode.Attributes["title"] != null && !string.IsNullOrEmpty(parentNode.Attributes["title"].Value))
                                            {
                                                if (!parentNode.Attributes["roles"].Value.Contains(roleName))
                                                {
                                                    if (!string.IsNullOrEmpty(parentNode.Attributes["roles"].Value))
                                                    {
                                                        parentNode.Attributes["roles"].Value += ",";
                                                    }

                                                    parentNode.Attributes["roles"].Value += roleName;
                                                }
                                            }

                                            parentNode = parentNode.ParentNode;
                                        }
                                    }
                                    else
                                    {
                                        ///eliminating role from node
                                        if (node.Attributes["roles"].Value.Contains(roleName))
                                        {
                                            string[] roleList = node.Attributes["roles"].Value.Split(',');
                                            string updatedRoles = string.Empty;
                                            int counter = 0;

                                            foreach (string role in roleList)
                                            {
                                                if (!role.Equals(roleName))
                                                {
                                                    if (counter != 0)
                                                    {
                                                        updatedRoles += ",";
                                                    }

                                                    updatedRoles += role;

                                                    counter++;
                                                }
                                            }

                                            node.Attributes["roles"].Value = updatedRoles;

                                            ///eliminating role from parent node
                                            parentNode = node.ParentNode;

                                            while (parentNode != null && parentNode.Name.ToLower().Equals("sitemapnode"))
                                            {
                                                if (parentNode.Attributes != null && parentNode.Attributes["title"] != null && !string.IsNullOrEmpty(parentNode.Attributes["title"].Value))
                                                {
                                                    if (parentNode.Attributes["roles"].Value.Contains(roleName))
                                                    {
                                                        if (!RoleIsInUse(parentNode, roleName))
                                                        {
                                                            roleList = parentNode.Attributes["roles"].Value.Split(',');
                                                            updatedRoles = string.Empty;
                                                            counter = 0;

                                                            foreach (string role in roleList)
                                                            {
                                                                if (!role.Equals(roleName))
                                                                {
                                                                    if (counter != 0)
                                                                    {
                                                                        updatedRoles += ",";
                                                                    }

                                                                    updatedRoles += role;

                                                                    counter++;
                                                                }
                                                            }

                                                            parentNode.Attributes["roles"].Value = updatedRoles;
                                                        }
                                                    }
                                                }

                                                parentNode = parentNode.ParentNode;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (node.ChildNodes.Count > 0)
                        {
                            UpdateSiteMapWithNewRole(objXMLSiteMapParserObjectList, roleName, node);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private static bool RoleIsInUse(XmlNode rootNode, string roleName)
        {
            XmlNodeList childNodes = rootNode.ChildNodes;

            foreach (XmlNode node in childNodes)
            {
                if (node.Attributes != null && node.Attributes["title"] != null && !string.IsNullOrEmpty(node.Attributes["title"].Value))
                {
                    if (node.Attributes["roles"].Value.Contains(roleName))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion


        #endregion
    }
}
