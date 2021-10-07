using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace CsCat
{
    public static class XmlNodeExtension
    {
        public static string GetNodeValue(this XmlNode self, string defaultValue = null)
        {
            if (self == null)
                return defaultValue;
            string value = null;
            if (self.NodeType == XmlNodeType.Text || self.NodeType == XmlNodeType.CDATA)
                value = self.Value;
            else
            {
                var child = self.FirstChild;
                while (child != null)
                {
                    if (child.NodeType == XmlNodeType.Text)
                    {
                        value = child.Value;
                        break;
                    }

                    child = self.NextSibling;
                }
            }

            return value ?? defaultValue;
        }

        public static string GetNodeCDataValue(this XmlNode self, string defaultValue = null)
        {
            if (self == null)
                return defaultValue;
            string value = null;
            if (self.NodeType == XmlNodeType.CDATA)
                value = self.Value;
            else
            {
                var child = self.FirstChild;
                while (child != null)
                {
                    if (child.NodeType == XmlNodeType.CDATA)
                    {
                        value = child.Value;
                        break;
                    }

                    child = self.NextSibling;
                }
            }

            return value ?? defaultValue;
        }


        public static bool SetNodeCDataValue(this XmlNode self, string value)
        {
            if (self == null)
                return false;
            if (self.NodeType == XmlNodeType.CDATA)
            {
                self.Value = value;
                return true;
            }

            var child = self.FirstChild;
            while (child != null)
            {
                if (child.NodeType == XmlNodeType.CDATA)
                {
                    child.Value = value;
                    return true;
                }

                child = self.NextSibling;
            }

            if (self.NodeType != XmlNodeType.Element) return false;
            child = self.OwnerDocument.CreateCDataSection(value);
            self.AppendChild(child);
            return true;
        }

        public static XmlAttribute GetNodeAttr(this XmlNode self, string attributeName)
        {
            return self?.NodeType == XmlNodeType.Element ? ((XmlElement) self).GetAttributeNode(attributeName) : null;
        }

        public static string GetNodeAttrValue(this XmlNode self, string name, string defaultValue = null)
        {
            string str = null;
            var attr = self.GetNodeAttr(name);
            if (attr != null)
                str = attr.Value;
            return str ?? defaultValue;
        }

        public static Dictionary<string, string> GetNodeAttrs(this XmlNode self)
        {
            var propertyDict = new Dictionary<string, string>();
            var attrs = self.Attributes;

            if (attrs == null) return propertyDict;
            for (var i = 0; i < attrs.Count; i++)
            {
                var attr = (XmlAttribute) attrs.Item(i);
                propertyDict[Convert.ToString(attr.Name)] = Convert.ToString(attr.Value);
            }

            return propertyDict;
        }

        public static bool SetNodeAttrValue(this XmlNode self, string name, string value)
        {
            if (self == null)
                return false;
            if (self.NodeType != XmlNodeType.Element) return false;
            ((XmlElement) self).SetAttribute(name, value);
            return true;
        }

        public static XmlNode GetChildNode(this XmlNode self, string name)
        {
            if (self == null)
                return null;
            var childNodes = self.ChildNodes;
            for (var i = 0; i < childNodes.Count; i++)
            {
                var child = childNodes.Item(i);
                if (name.Equals(child.Name))
                    return child;
            }

            return null;
        }

        public static XmlNode GetChildNode(this XmlNode self, int pos)
        {
            if (self == null)
                return null;
            var childNodes = self.ChildNodes;
            if (pos >= 0 && pos < childNodes.Count)
                return childNodes.Item(pos);
            return null;
        }

        public static XmlNode AddChildNode(this XmlNode self, string name, string value)
        {
            if (self == null)
                return null;
            XmlElement element;
            switch (self.NodeType)
            {
                case XmlNodeType.Document:
                    element = ((XmlDocument) self).CreateElement(name);
                    break;
                case XmlNodeType.Element:
                    element = self.OwnerDocument.CreateElement(name);
                    break;
                default:
                    return null;
            }

            if (value != null)
                element.SetNodeValue(value);
            self.AppendChild(element);
            return element;
        }

        public static bool SetNodeValue(this XmlNode self, string value)
        {
            if (self == null)
                return false;
            if (self.NodeType == XmlNodeType.Text || self.NodeType == XmlNodeType.CDATA)
            {
                self.Value = value;
                return true;
            }

            var child = self.FirstChild;
            while (child != null)
            {
                if (child.NodeType == XmlNodeType.Text)
                {
                    child.Value = value;
                    return true;
                }

                child = self.NextSibling;
            }

            if (self.NodeType != XmlNodeType.Element) return false;
            child = self.OwnerDocument.CreateTextNode(StringConst.String_Empty);
            child.Value = value;
            self.AppendChild(child);
            return true;
        }

        public static void AddChildNode(this XmlNode self, Hashtable hashtable)
        {
            foreach (DictionaryEntry dictionaryEntry in hashtable)
            {
                var name = dictionaryEntry.Key.ToStringOrToDefault(StringConst.String_Empty);
                var value = dictionaryEntry.Value.ToStringOrToDefault(StringConst.String_Empty);
                if (name.Length > 0)
                    AddChildNode(self, name, value);
            }
        }
    }
}