
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace CsCat
{
  //config形式如：
  //<nodeName attr=xxxx>
  //	<property name=xxxx value=yyyy/>
  //</nodeName/>
  //props 保存有<property name="xxx" value="yyy">属性的标签
  //attrs 保存像to="xxx"的属性
  public class XMLConfig
  {
    #region field

    protected HashSet<string> crypto_prop_set = new HashSet<string>();
    protected Dictionary<string, string> prop_dict = new Dictionary<string, string>();
    protected Dictionary<string, string> attr_dict = new Dictionary<string, string>();



    private XMLConfig parent_config = null;
    private FileInfo config_file = null;
    private List<XMLConfig> config_list = new List<XMLConfig>();
    protected string name = null;
    private const string Config_Path_Key = "config_path";
    private const string Name_Key = "name";
    private const string Property_Key = "property";
    private const string Value_Key = "value";

    #endregion

    #region ctor

    public XMLConfig(string config_file_path)
      : this(new FileInfo(config_file_path))
    {
    }

    public XMLConfig(FileInfo config_file)
    {
      SetConfigFile(config_file);
      LoadFrom(GetConfigFile());
    }

    public XMLConfig(System.IO.Stream stream)
    {
      LoadFrom(stream);
    }

    public XMLConfig()
    {
    }

    #endregion



    #region public method

    public bool IsCryptoProperty(string name)
    {
      return this.crypto_prop_set.Contains(name);
    }

    public void SetCryptoProperty(string name)
    {
      this.crypto_prop_set.Add(name);
    }

    public string GetProperty(string name)
    {
      return this.prop_dict[name];
    }

    public string GetProperty(string name, string dv)
    {
      return this.prop_dict.GetOrAddDefault(name, () => dv);
    }

    public void SetProperty(string name, string value)
    {
      this.prop_dict[name] = value;
    }

    public string RemoveProperty(string name)
    {
      string value = this.prop_dict[name];
      this.prop_dict.Remove(name);
      return value;
    }

    public bool HasProperty(string name)
    {
      return this.prop_dict.ContainsKey(name);
    }

    public Dictionary<string, string>.KeyCollection GetPropertyNames()
    {
      return this.prop_dict.Keys;
    }

    public Dictionary<string, string> GetProperties()
    {
      return this.prop_dict;
    }

    public string GetAttribute(string name)
    {
      return this.attr_dict[name];
    }

    public string GetAttribute(string name, string dv)
    {
      return this.prop_dict.GetOrAddDefault(name, () => dv);
    }

    public void SetAttribute(string name, string value)
    {
      this.attr_dict[name] = value;
    }

    public string RemoveAttribute(string name)
    {
      string value = this.attr_dict[name];
      this.attr_dict.Remove(name);
      return value;
    }

    public bool HasAttribute(string name)
    {
      return this.attr_dict.ContainsKey(name);
    }

    public Dictionary<string, string>.KeyCollection GetAttributeName()
    {
      return this.attr_dict.Keys;
    }

    public Dictionary<string, string> GetAttributes()
    {
      return this.attr_dict;
    }

    public bool Reloadable()
    {
      return this.config_file != null;
    }

    public void Reload()
    {
      if (Reloadable())
      {
        this.config_list.Clear();
        this.prop_dict.Clear();
        this.attr_dict.Clear();
        this.parent_config = null;
        this.name = null;

        LoadFrom(this.config_file);
        return;
      }

      throw new IOException("无法重新载入配置信息");
    }

    public FileInfo GetConfigFile()
    {
      return this.config_file;
    }

    public void SetConfigFile(FileInfo file)
    {
      this.config_file = file;
    }

    public FileInfo GetSuperConfigFile()
    {
      if (this.parent_config != null)
      {
        if (this.parent_config.config_file != null)
          return this.parent_config.config_file;
        return this.parent_config.GetSuperConfigFile();
      }

      return null;
    }

    public XMLConfig RootConfig()
    {
      if (this.parent_config == null)
        return this;
      return this.parent_config.RootConfig();
    }

    public XMLConfig SuperConfig()
    {
      return this.parent_config;
    }

    public string GetName()
    {
      return this.name;
    }

    public void SetName(string name)
    {
      this.name = name;
    }

    public void LoadFrom(string file_path)
    {
      LoadFrom(new FileInfo(file_path));
    }

    public void LoadFrom(FileInfo file)
    {
      if (!file.Exists && GetSuperConfigFile() != null)
      {
        DirectoryInfo basePathInfo = GetSuperConfigFile().Parent();
        file = new FileInfo(string.Format("{0}/{1}", basePathInfo.FullName, file.Name));
      }

      LoadFrom(new FileStream(file.FullName, FileMode.Open, FileAccess.Read));
    }

    public void LoadFrom(System.IO.Stream stream)
    {
      XmlDocument doc = new XmlDocument();
      doc.Load(stream);
      XmlNode child = doc.FirstChild;
      if (child == null)
        throw new IOException("XML配置文件不含根节点");
      LoadFrom(child);
    }

    public void LoadFrom(XmlNode node)
    {
      if (this.name == null)
      {
        string name = node.Name;
        if (name != null)
          this.name = name.Trim();
      }

      Dictionary<string, string> node_attr_dict = XMLUtil.GetNodeAttrs(node);
      foreach (string key in node_attr_dict.Keys)
      {
        if (this.attr_dict.ContainsKey(key))
          continue;
        string value = node_attr_dict.GetOrAddDefault(key, () => "");
        this.attr_dict[key] = value;
      }

      XmlNode child = node.FirstChild;
      XMLConfig sub_config = null;
      while (child != null)
      {
        if (child.NodeType == XmlNodeType.Element)
        {
          if (IsPropertyNode(child))
            LoadProperty(child);
          else
          {
            string child_node_name = child.Name;
            if (child_node_name == null)
              continue;
            child_node_name = child_node_name.Trim();
            string parse_class_string = XMLUtil.GetNodeAttrValue(child, "parseClass", "");
            if (parse_class_string.Length > 0)
              sub_config = (XMLConfig)Activator.CreateInstance(Type.GetType(parse_class_string));
            else
              sub_config = new XMLConfig();
            sub_config.parent_config = this;
            sub_config.name = child_node_name;
            sub_config.LoadFrom(child); // 不断循环下一层
            this.config_list.Add(sub_config);
          }
        }

        child = child.NextSibling;
      }

      string config_path = node_attr_dict.GetOrAddDefault(Config_Path_Key, () => "");
      if (config_path.Length > 0)
        LoadFrom(config_path);
    }

    public int GetConfigCount()
    {
      return this.config_list.Count;
    }

    public XMLConfig GetConfig(int index)
    {
      return (XMLConfig)this.config_list[index];
    }

    public bool HasConfig(string tag)
    {
      for (int i = 0; i < this.config_list.Count; i++)
      {
        XMLConfig conf = (XMLConfig)this.config_list[i];
        if (tag == conf.GetName())
          return true;
      }

      return false;
    }

    public XMLConfig GetConfig(string tag)
    {
      for (int i = 0; i < this.config_list.Count; i++)
      {
        XMLConfig config = (XMLConfig)this.config_list[i];
        if (tag == config.GetName())
          return config;
      }

      return null;
    }

    public XMLConfig[] GetConfigs(string tag)
    {
      List<XMLConfig> config_list = new List<XMLConfig>();
      for (int i = 0; i < this.config_list.Count; i++)
      {
        XMLConfig config = (XMLConfig)this.config_list[i];
        if (tag == config.GetName())
          config_list.Add(config);
      }

      XMLConfig[] configs = new XMLConfig[config_list.Count];
      configs = config_list.ToArray();
      return configs;
    }

    public XMLConfig[] GetConfigs()
    {
      XMLConfig[] configs = new XMLConfig[this.config_list.Count];
      configs = this.config_list.ToArray();
      return configs;
    }

    public XMLConfig GetConfig(string attr_name, string attr_value)
    {
      for (int i = 0; i < GetConfigCount(); i++)
      {
        XMLConfig config = (XMLConfig)this.config_list[i];
        if (config.GetAttribute(attr_name) == attr_value)
          return config;
      }

      return null;
    }

    public XMLConfig MatchesConfig(string attr_name, string attr_value)
    {
      Regex r = new Regex(attr_value, RegexOptions.IgnoreCase);
      for (int i = 0; i < GetConfigCount(); i++)
      {
        XMLConfig config = (XMLConfig)this.config_list[i];
        if (config.HasAttribute(attr_name))
        {
          Match m = r.Match(config.GetAttribute(attr_name));
          if (m.Success)
            return config;
        }
      }

      return null;
    }

    public XMLConfig AddConfig(string tag)
    {
      XMLConfig sub_config = new XMLConfig();
      sub_config.parent_config = this;
      sub_config.SetName(tag);
      this.config_list.Add(sub_config);
      return sub_config;
    }

    public XMLConfig AddConfig(int index, string tag)
    {
      XMLConfig sub_config = new XMLConfig();
      sub_config.parent_config = this;
      sub_config.SetName(tag);
      this.config_list.Insert(index, sub_config);
      return sub_config;
    }

    public void AddConfig(XMLConfig sub_config)
    {
      sub_config.parent_config = this;
      this.config_list.Add(sub_config);
    }

    public void AddConfig(int index, XMLConfig sub_config)
    {
      sub_config.parent_config = this;
      this.config_list.Insert(index, sub_config);
    }

    public XMLConfig RemoveConfig(int index)
    {
      XMLConfig config = this.config_list[index];
      this.config_list.RemoveAt(index);
      return config;
    }

    public XMLConfig RemoveConfig(string tag)
    {
      XMLConfig config = GetConfig(tag);
      if (config != null)
        this.config_list.Remove(config);
      return config;
    }

    public XMLConfig removeConfig(string attr_name, string attr_value)
    {
      XMLConfig config = GetConfig(attr_name, attr_value);
      if (config != null)
        this.config_list.Remove(config);
      return config;
    }

    public XMLConfig[] RemoveConfigs(string tag)
    {
      XMLConfig[] configs = GetConfigs(tag);
      for (int i = 0; i < configs.Length; i++)
        this.config_list.Remove(configs[i]);
      return configs;
    }

    public void ClearConfigs()
    {
      this.config_list.Clear();
    }

    protected bool IsPropertyNode(XmlNode node)
    {
      return Property_Key == node.Name;
    }

    protected void LoadProperty(XmlNode node)
    {
      string key = XMLUtil.GetNodeAttrValue(node, Name_Key, "").Trim();
      if (key.Length > 0)
      {
        if (!this.prop_dict.ContainsKey(key))
        {
          string value = XMLUtil.GetNodeAttrValue(node, Value_Key, "");
          if (value.Length == 0)
            value = XMLUtil.GetNodeValue(node, "").Trim();
          this.prop_dict[key] = value;
        }
      }
    }


    public static XmlNode ToXMLNode(XmlNode parent, XMLConfig config)
    {
      XmlNode root = XMLUtil.AddChildNode(parent, config.name, "");
      foreach (string key in config.attr_dict.Keys)
      {
        if (key == Config_Path_Key)
          continue;
        string value = config.attr_dict[key];
        XMLUtil.SetNodeAttrValue(root, key, value);
      }

      foreach (string key in config.prop_dict.Keys)
      {
        string value = config.prop_dict[key];
        XmlNode child = XMLUtil.AddChildNode(root, Property_Key, "");
        XMLUtil.SetNodeAttrValue(child, Name_Key, key);
        if (value.Length < 100 && value.IndexOf('\n') == -1 && value.IndexOf('\r') == -1)
          XMLUtil.SetNodeAttrValue(child, Value_Key, value);
        else
          XMLUtil.SetNodeValue(child, value);
      }

      for (int i = 0; i < config.config_list.Count; i++)
        ToXMLNode(root, (XMLConfig)config.config_list[i]);
      return root;
    }

    #endregion









  }
}