using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace CsCat
{
  public static class XmlNodeExtension
  {
    public static string GetNodeValue(this XmlNode self, string dv = null)
    {
      if (self == null)
        return dv;
      string v = null;
      if (self.NodeType == XmlNodeType.Text || self.NodeType == XmlNodeType.CDATA)
      {
        v = self.Value;
      }
      else
      {
        var child = self.FirstChild;
        while (child != null)
        {
          if (child.NodeType == XmlNodeType.Text)
          {
            v = child.Value;
            break;
          }

          child = self.NextSibling;
        }
      }

      if (v != null)
        return v;
      return dv;
    }

    public static string GetNodeCDataValue(this XmlNode self, string dv = null)
    {
      if (self == null)
        return dv;
      string v = null;
      if (self.NodeType == XmlNodeType.CDATA)
      {
        v = self.Value;
      }
      else
      {
        var child = self.FirstChild;
        while (child != null)
        {
          if (child.NodeType == XmlNodeType.CDATA)
          {
            v = child.Value;
            break;
          }

          child = self.NextSibling;
        }
      }

      if (v != null)
        return v;
      return dv;
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

      if (self.NodeType == XmlNodeType.Element)
      {
        child = self.OwnerDocument.CreateCDataSection(value);
        self.AppendChild(child);
        return true;
      }

      return false;
    }

    public static XmlAttribute GetNodeAttr(this XmlNode self, string attribute_name)
    {
      if (self == null)
        return null;
      if (self.NodeType == XmlNodeType.Element)
        return ((XmlElement) self).GetAttributeNode(attribute_name);
      return null;
    }

    public static string GetNodeAttrValue(this XmlNode self, string name, string dv = null)
    {
      string str = null;
      var attr = self.GetNodeAttr(name);
      if (attr != null)
        str = attr.Value;
      if (str != null)
        return str;
      return dv;
    }

    public static Dictionary<string, string> GetNodeAttrs(this XmlNode self)
    {
      var property_dict = new Dictionary<string, string>();
      var attrs = self.Attributes;

      if (attrs != null)
        for (var i = 0; i < attrs.Count; i++)
        {
          var attr = (XmlAttribute) attrs.Item(i);
          property_dict[Convert.ToString(attr.Name)] = Convert.ToString(attr.Value);
        }

      return property_dict;
    }

    public static bool SetNodeAttrValue(this XmlNode self, string name, string value)
    {
      if (self == null)
        return false;
      if (self.NodeType == XmlNodeType.Element)
      {
        ((XmlElement) self).SetAttribute(name, value);
        return true;
      }

      return false;
    }

    public static XmlNode GetChildNode(this XmlNode self, string name)
    {
      if (self == null)
        return null;
      var child_nodes = self.ChildNodes;
      for (var i = 0; i < child_nodes.Count; i++)
      {
        var child = child_nodes.Item(i);
        if (name == child.Name)
          return child;
      }

      return null;
    }

    public static XmlNode GetChildNode(this XmlNode self, int pos)
    {
      if (self == null)
        return null;
      var child_nodes = self.ChildNodes;
      if (pos >= 0 && pos < child_nodes.Count)
        return child_nodes.Item(pos);
      return null;
    }

    public static XmlNode AddChildNode(this XmlNode self, string name, string value)
    {
      if (self == null)
        return null;
      XmlElement element;
      if (self.NodeType == XmlNodeType.Document)
        element = ((XmlDocument) self).CreateElement(name);
      else if (self.NodeType == XmlNodeType.Element)
        element = self.OwnerDocument.CreateElement(name);
      else
        return null;
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

      if (self.NodeType == XmlNodeType.Element)
      {
        child = self.OwnerDocument.CreateTextNode("");
        child.Value = value;
        self.AppendChild(child);
        return true;
      }

      return false;
    }

    public static void AddChildNode(this XmlNode self, Hashtable ht)
    {
      foreach (DictionaryEntry de in ht)
      {
        var name = de.Key.ToStringOrToDefault("");
        var value = de.Value.ToStringOrToDefault("");
        if (name.Length > 0)
          AddChildNode(self, name, value);
      }
    }
  }
}