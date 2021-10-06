using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace CsCat
{
    public class XMLUtil
    {
        #region static method

        public static string GetNodeValue(XmlNode node, string defaultValue)
        {
            if (node == null)
                return defaultValue;
            string value = null;
            if (node.NodeType == XmlNodeType.Text || node.NodeType == XmlNodeType.CDATA)
                value = node.Value;
            else
            {
                var child = node.FirstChild;
                while (child != null)
                {
                    if (child.NodeType == XmlNodeType.Text)
                    {
                        value = child.Value;
                        break;
                    }

                    child = node.NextSibling;
                }
            }

            return value ?? defaultValue;
        }

        public static bool SetNodeValue(XmlNode node, string value)
        {
            if (node == null)
                return false;
            if (node.NodeType == XmlNodeType.Text || node.NodeType == XmlNodeType.CDATA)
            {
                node.Value = value;
                return true;
            }

            XmlNode child = node.FirstChild;
            while (child != null)
            {
                if (child.NodeType == XmlNodeType.Text)
                {
                    child.Value = value;
                    return true;
                }

                child = node.NextSibling;
            }

            if (node.NodeType != XmlNodeType.Element) return false;
            child = node.OwnerDocument.CreateTextNode("");
            child.Value = value;
            node.AppendChild(child);
            return true;
        }

        public static string GetNodeCDataValue(XmlNode node, string defaultValue)
        {
            if (node == null)
                return defaultValue;
            string value = null;
            if (node.NodeType == XmlNodeType.CDATA)
                value = node.Value;
            else
            {
                var child = node.FirstChild;
                while (child != null)
                {
                    if (child.NodeType == XmlNodeType.CDATA)
                    {
                        value = child.Value;
                        break;
                    }

                    child = node.NextSibling;
                }
            }

            return value ?? defaultValue;
        }

        public static bool SetNodeCDataValue(XmlNode node, string value)
        {
            if (node == null)
                return false;
            if (node.NodeType == XmlNodeType.CDATA)
            {
                node.Value = value;
                return true;
            }

            var child = node.FirstChild;
            while (child != null)
            {
                if (child.NodeType == XmlNodeType.CDATA)
                {
                    child.Value = value;
                    return true;
                }

                child = node.NextSibling;
            }

            if (node.NodeType != XmlNodeType.Element) return false;
            child = node.OwnerDocument.CreateCDataSection(value);
            node.AppendChild(child);
            return true;
        }

        public static XmlAttribute GetNodeAttr(XmlNode node, string name)
        {
            return node?.NodeType == XmlNodeType.Element ? ((XmlElement) node).GetAttributeNode(name) : null;
        }

        public static string GetNodeAttrValue(XmlNode node, string name, string defaultValue)
        {
            string value = null;
            XmlAttribute attr = GetNodeAttr(node, name);
            if (attr != null)
                value = attr.Value;
            return value ?? defaultValue;
        }

        public static Dictionary<string, string> GetNodeAttrs(XmlNode node)
        {
            Dictionary<string, string> propertyDict = new Dictionary<string, string>();
            XmlAttributeCollection attrs = node.Attributes;

            if (attrs != null)
            {
                for (var i = 0; i < attrs.Count; i++)
                {
                    XmlAttribute attr = (XmlAttribute) attrs.Item(i);
                    propertyDict[Convert.ToString(attr.Name)] = Convert.ToString(attr.Value);
                }
            }

            return propertyDict;
        }

        public static bool SetNodeAttrValue(XmlNode node, string name, string value)
        {
            if (node == null)
                return false;
            if (node.NodeType != XmlNodeType.Element) return false;
            ((XmlElement) node).SetAttribute(name, value);
            return true;
        }

        public static XmlNode GetChildNode(XmlNode node, string name)
        {
            if (node == null)
                return null;
            XmlNodeList childNodes = node.ChildNodes;
            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode child = childNodes.Item(i);
                if (name.Equals(child.Name))
                    return child;
            }

            return null;
        }

        public static XmlNode GetChildNode(XmlNode node, int pos)
        {
            if (node == null)
                return null;
            XmlNodeList childNodeList = node.ChildNodes;
            if (pos >= 0 && pos < childNodeList.Count)
                return childNodeList.Item(pos);
            return null;
        }

        public static XmlNode AddChildNode(XmlNode node, string name, string value)
        {
            if (node == null)
                return null;
            XmlElement element;
            switch (node.NodeType)
            {
                case XmlNodeType.Document:
                    element = ((XmlDocument) node).CreateElement(name);
                    break;
                case XmlNodeType.Element:
                    element = node.OwnerDocument.CreateElement(name);
                    break;
                default:
                    return null;
            }

            if (value != null)
                SetNodeValue(element, value);
            node.AppendChild(element);
            return element;
        }

        public static void AddChildNode(XmlNode node, Hashtable hashtable)
        {
            foreach (DictionaryEntry dictionaryEntry in hashtable)
            {
                string name = dictionaryEntry.Key.ToStringOrToDefault(StringConst.StringEmpty);
                string value = dictionaryEntry.Value.ToStringOrToDefault(StringConst.StringEmpty);
                if (name.Length > 0)
                    AddChildNode(node, name, value);
            }
        }

        #endregion
    }
}