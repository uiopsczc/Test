using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace CsCat
{
  public class XMLUtil
  {
    #region static method

    public static string GetNodeValue(XmlNode node, string dv)
    {
      if (node == null)
        return dv;
      string v = null;
      if (node.NodeType == XmlNodeType.Text || node.NodeType == XmlNodeType.CDATA)
        v = node.Value;
      else
      {
        XmlNode child = node.FirstChild;
        while (child != null)
        {
          if (child.NodeType == XmlNodeType.Text)
          {
            v = child.Value;
            break;
          }

          child = node.NextSibling;
        }
      }

      if (v != null)
        return v;
      return dv;
    }

    public static bool SetNodeValue(XmlNode node, String value)
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

      if (node.NodeType == XmlNodeType.Element)
      {
        child = node.OwnerDocument.CreateTextNode("");
        child.Value = value;
        node.AppendChild(child);
        return true;
      }

      return false;
    }

    public static string GetNodeCDataValue(XmlNode node, string dv)
    {
      if (node == null)
        return dv;
      String v = null;
      if (node.NodeType == XmlNodeType.CDATA)
        v = node.Value;
      else
      {
        XmlNode child = node.FirstChild;
        while (child != null)
        {
          if (child.NodeType == XmlNodeType.CDATA)
          {
            v = child.Value;
            break;
          }

          child = node.NextSibling;
        }
      }

      if (v != null)
        return v;
      return dv;
    }

    public static bool SetNodeCDataValue(XmlNode node, String value)
    {
      if (node == null)
        return false;
      if (node.NodeType == XmlNodeType.CDATA)
      {
        node.Value = value;
        return true;
      }

      XmlNode child = node.FirstChild;
      while (child != null)
      {
        if (child.NodeType == XmlNodeType.CDATA)
        {
          child.Value = value;
          return true;
        }

        child = node.NextSibling;
      }

      if (node.NodeType == XmlNodeType.Element)
      {
        child = node.OwnerDocument.CreateCDataSection(value);
        node.AppendChild(child);
        return true;
      }

      return false;
    }

    public static XmlAttribute GetNodeAttr(XmlNode node, String name)
    {
      if (node == null)
        return null;
      if (node.NodeType == XmlNodeType.Element)
        return ((XmlElement)node).GetAttributeNode(name);
      return null;
    }

    public static string GetNodeAttrValue(XmlNode node, string name, String dv)
    {
      string str = null;
      XmlAttribute attr = GetNodeAttr(node, name);
      if (attr != null)
        str = attr.Value;
      if (str != null)
        return str;
      return dv;
    }

    public static Dictionary<string, string> GetNodeAttrs(XmlNode node)
    {
      Dictionary<string, string> properties = new Dictionary<string, string>();
      XmlAttributeCollection attrs = node.Attributes;

      if (attrs != null)
      {
        for (int i = 0; i < attrs.Count; i++)
        {
          XmlAttribute attr = (XmlAttribute)attrs.Item(i);
          properties[Convert.ToString(attr.Name)] = Convert.ToString(attr.Value);
        }
      }

      return properties;
    }

    public static bool SetNodeAttrValue(XmlNode node, string name, string value)
    {
      if (node == null)
        return false;
      if (node.NodeType == XmlNodeType.Element)
      {
        ((XmlElement)node).SetAttribute(name, value);
        return true;
      }

      return false;
    }

    public static XmlNode GetChildNode(XmlNode node, string name)
    {
      if (node == null)
        return null;
      XmlNodeList childNodes = node.ChildNodes;
      for (int i = 0; i < childNodes.Count; i++)
      {
        XmlNode child = childNodes.Item(i);
        if (name == child.Name)
          return child;
      }

      return null;
    }

    public static XmlNode GetChildNode(XmlNode node, int pos)
    {
      if (node == null)
        return null;
      XmlNodeList childNodes = node.ChildNodes;
      if (pos >= 0 && pos < childNodes.Count)
        return childNodes.Item(pos);
      return null;
    }

    public static XmlNode AddChildNode(XmlNode node, string name, string value)
    {
      if (node == null)
        return null;
      XmlElement element;
      if (node.NodeType == XmlNodeType.Document)
        element = ((XmlDocument)node).CreateElement(name);
      else if (node.NodeType == XmlNodeType.Element)
        element = node.OwnerDocument.CreateElement(name);
      else
        return null;
      if (value != null)
        SetNodeValue(element, value);
      node.AppendChild(element);
      return element;
    }

    public static void AddChildNode(XmlNode node, Hashtable ht)
    {
      foreach (DictionaryEntry de in ht)
      {
        string name = de.Key.ToStringOrToDefault("");
        string value = de.Value.ToStringOrToDefault("");
        if (name.Length > 0)
          AddChildNode(node, name, value);
      }
    }

    #endregion

  }
}

