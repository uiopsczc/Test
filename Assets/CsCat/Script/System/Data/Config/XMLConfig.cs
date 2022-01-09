
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

		protected HashSet<string> cryptoPropSet = new HashSet<string>();
		protected Dictionary<string, string> propDict = new Dictionary<string, string>();
		protected Dictionary<string, string> attrDict = new Dictionary<string, string>();



		private XMLConfig parentConfig = null;
		private FileInfo configFile = null;
		private List<XMLConfig> configList = new List<XMLConfig>();
		protected string name = null;
		private const string Config_Path_Key = "config_path";
		private const string Name_Key = "name";
		private const string Property_Key = "property";
		private const string Value_Key = "value";

		#endregion

		#region ctor

		public XMLConfig(string configFilePath)
		  : this(new FileInfo(configFilePath))
		{
		}

		public XMLConfig(FileInfo configFile)
		{
			SetConfigFile(configFile);
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
			return this.cryptoPropSet.Contains(name);
		}

		public void SetCryptoProperty(string name)
		{
			this.cryptoPropSet.Add(name);
		}

		public string GetProperty(string name)
		{
			return this.propDict[name];
		}

		public string GetProperty(string name, string dv)
		{
			return this.propDict.GetOrAddDefault(name, () => dv);
		}

		public void SetProperty(string name, string value)
		{
			this.propDict[name] = value;
		}

		public string RemoveProperty(string name)
		{
			string value = this.propDict[name];
			this.propDict.Remove(name);
			return value;
		}

		public bool HasProperty(string name)
		{
			return this.propDict.ContainsKey(name);
		}

		public Dictionary<string, string>.KeyCollection GetPropertyNames()
		{
			return this.propDict.Keys;
		}

		public Dictionary<string, string> GetProperties()
		{
			return this.propDict;
		}

		public string GetAttribute(string name)
		{
			return this.attrDict[name];
		}

		public string GetAttribute(string name, string dv)
		{
			return this.propDict.GetOrAddDefault(name, () => dv);
		}

		public void SetAttribute(string name, string value)
		{
			this.attrDict[name] = value;
		}

		public string RemoveAttribute(string name)
		{
			string value = this.attrDict[name];
			this.attrDict.Remove(name);
			return value;
		}

		public bool HasAttribute(string name)
		{
			return this.attrDict.ContainsKey(name);
		}

		public Dictionary<string, string>.KeyCollection GetAttributeName()
		{
			return this.attrDict.Keys;
		}

		public Dictionary<string, string> GetAttributes()
		{
			return this.attrDict;
		}

		public bool Reloadable()
		{
			return this.configFile != null;
		}

		public void Reload()
		{
			if (Reloadable())
			{
				this.configList.Clear();
				this.propDict.Clear();
				this.attrDict.Clear();
				this.parentConfig = null;
				this.name = null;

				LoadFrom(this.configFile);
				return;
			}

			throw new IOException("无法重新载入配置信息");
		}

		public FileInfo GetConfigFile()
		{
			return this.configFile;
		}

		public void SetConfigFile(FileInfo file)
		{
			this.configFile = file;
		}

		public FileInfo GetSuperConfigFile()
		{
			if (this.parentConfig != null)
				return this.parentConfig.configFile ?? this.parentConfig.GetSuperConfigFile();

			return null;
		}

		public XMLConfig RootConfig()
		{
			if (this.parentConfig == null)
				return this;
			return this.parentConfig.RootConfig();
		}

		public XMLConfig SuperConfig()
		{
			return this.parentConfig;
		}

		public string GetName()
		{
			return this.name;
		}

		public void SetName(string name)
		{
			this.name = name;
		}

		public void LoadFrom(string filePath)
		{
			LoadFrom(new FileInfo(filePath));
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

			Dictionary<string, string> nodeAttrDict = XMLUtil.GetNodeAttrs(node);
			foreach (string key in nodeAttrDict.Keys)
			{
				if (this.attrDict.ContainsKey(key))
					continue;
				string value = nodeAttrDict.GetOrAddDefault(key, () => "");
				this.attrDict[key] = value;
			}

			XmlNode child = node.FirstChild;
			XMLConfig subConfig = null;
			while (child != null)
			{
				if (child.NodeType == XmlNodeType.Element)
				{
					if (IsPropertyNode(child))
						LoadProperty(child);
					else
					{
						string childNodeName = child.Name;
						if (childNodeName == null)
							continue;
						childNodeName = childNodeName.Trim();
						string parseClassString = XMLUtil.GetNodeAttrValue(child, "parseClass", "");
						if (parseClassString.Length > 0)
							subConfig = (XMLConfig)Activator.CreateInstance(Type.GetType(parseClassString));
						else
							subConfig = new XMLConfig();
						subConfig.parentConfig = this;
						subConfig.name = childNodeName;
						subConfig.LoadFrom(child); // 不断循环下一层
						this.configList.Add(subConfig);
					}
				}

				child = child.NextSibling;
			}

			string configPath = nodeAttrDict.GetOrAddDefault(Config_Path_Key, () => "");
			if (configPath.Length > 0)
				LoadFrom(configPath);
		}

		public int GetConfigCount()
		{
			return this.configList.Count;
		}

		public XMLConfig GetConfig(int index)
		{
			return (XMLConfig)this.configList[index];
		}

		public bool HasConfig(string tag)
		{
			for (int i = 0; i < this.configList.Count; i++)
			{
				XMLConfig conf = (XMLConfig)this.configList[i];
				if (tag == conf.GetName())
					return true;
			}

			return false;
		}

		public XMLConfig GetConfig(string tag)
		{
			for (int i = 0; i < this.configList.Count; i++)
			{
				XMLConfig config = (XMLConfig)this.configList[i];
				if (tag == config.GetName())
					return config;
			}

			return null;
		}

		public XMLConfig[] GetConfigs(string tag)
		{
			List<XMLConfig> configList = new List<XMLConfig>();
			for (int i = 0; i < this.configList.Count; i++)
			{
				XMLConfig config = (XMLConfig)this.configList[i];
				if (tag == config.GetName())
					configList.Add(config);
			}

			XMLConfig[] configs = new XMLConfig[configList.Count];
			configs = configList.ToArray();
			return configs;
		}

		public XMLConfig[] GetConfigs()
		{
			XMLConfig[] configs = new XMLConfig[this.configList.Count];
			configs = this.configList.ToArray();
			return configs;
		}

		public XMLConfig GetConfig(string attrName, string attrValue)
		{
			for (int i = 0; i < GetConfigCount(); i++)
			{
				XMLConfig config = (XMLConfig)this.configList[i];
				if (config.GetAttribute(attrName) == attrValue)
					return config;
			}

			return null;
		}

		public XMLConfig MatchesConfig(string attrName, string attrValue)
		{
			Regex r = new Regex(attrValue, RegexOptions.IgnoreCase);
			for (int i = 0; i < GetConfigCount(); i++)
			{
				XMLConfig config = (XMLConfig)this.configList[i];
				if (config.HasAttribute(attrName))
				{
					Match m = r.Match(config.GetAttribute(attrName));
					if (m.Success)
						return config;
				}
			}

			return null;
		}

		public XMLConfig AddConfig(string tag)
		{
			XMLConfig subConfig = new XMLConfig();
			subConfig.parentConfig = this;
			subConfig.SetName(tag);
			this.configList.Add(subConfig);
			return subConfig;
		}

		public XMLConfig AddConfig(int index, string tag)
		{
			XMLConfig subConfig = new XMLConfig();
			subConfig.parentConfig = this;
			subConfig.SetName(tag);
			this.configList.Insert(index, subConfig);
			return subConfig;
		}

		public void AddConfig(XMLConfig subConfig)
		{
			subConfig.parentConfig = this;
			this.configList.Add(subConfig);
		}

		public void AddConfig(int index, XMLConfig subConfig)
		{
			subConfig.parentConfig = this;
			this.configList.Insert(index, subConfig);
		}

		public XMLConfig RemoveConfig(int index)
		{
			XMLConfig config = this.configList[index];
			this.configList.RemoveAt(index);
			return config;
		}

		public XMLConfig RemoveConfig(string tag)
		{
			XMLConfig config = GetConfig(tag);
			if (config != null)
				this.configList.Remove(config);
			return config;
		}

		public XMLConfig removeConfig(string attrName, string attrValue)
		{
			XMLConfig config = GetConfig(attrName, attrValue);
			if (config != null)
				this.configList.Remove(config);
			return config;
		}

		public XMLConfig[] RemoveConfigs(string tag)
		{
			XMLConfig[] configs = GetConfigs(tag);
			for (int i = 0; i < configs.Length; i++)
				this.configList.Remove(configs[i]);
			return configs;
		}

		public void ClearConfigs()
		{
			this.configList.Clear();
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
				if (!this.propDict.ContainsKey(key))
				{
					string value = XMLUtil.GetNodeAttrValue(node, Value_Key, "");
					if (value.Length == 0)
						value = XMLUtil.GetNodeValue(node, "").Trim();
					this.propDict[key] = value;
				}
			}
		}


		public static XmlNode ToXMLNode(XmlNode parent, XMLConfig config)
		{
			XmlNode root = XMLUtil.AddChildNode(parent, config.name, "");
			foreach (string key in config.attrDict.Keys)
			{
				if (key == Config_Path_Key)
					continue;
				string value = config.attrDict[key];
				XMLUtil.SetNodeAttrValue(root, key, value);
			}

			foreach (string key in config.propDict.Keys)
			{
				string value = config.propDict[key];
				XmlNode child = XMLUtil.AddChildNode(root, Property_Key, "");
				XMLUtil.SetNodeAttrValue(child, Name_Key, key);
				if (value.Length < 100 && value.IndexOf('\n') == -1 && value.IndexOf('\r') == -1)
					XMLUtil.SetNodeAttrValue(child, Value_Key, value);
				else
					XMLUtil.SetNodeValue(child, value);
			}

			for (int i = 0; i < config.configList.Count; i++)
				ToXMLNode(root, (XMLConfig)config.configList[i]);
			return root;
		}

		#endregion









	}
}