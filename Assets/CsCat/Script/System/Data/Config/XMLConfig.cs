
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

		protected HashSet<string> _cryptoPropSet = new HashSet<string>();
		protected Dictionary<string, string> _propDict = new Dictionary<string, string>();
		protected Dictionary<string, string> _attrDict = new Dictionary<string, string>();



		private XMLConfig _parentConfig = null;
		private FileInfo _configFile = null;
		private List<XMLConfig> _configList = new List<XMLConfig>();
		protected string _name = null;
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
			return this._cryptoPropSet.Contains(name);
		}

		public void SetCryptoProperty(string name)
		{
			this._cryptoPropSet.Add(name);
		}

		public string GetProperty(string name)
		{
			return this._propDict[name];
		}

		public string GetProperty(string name, string dv)
		{
			return this._propDict.GetOrAddDefault(name, () => dv);
		}

		public void SetProperty(string name, string value)
		{
			this._propDict[name] = value;
		}

		public string RemoveProperty(string name)
		{
			string value = this._propDict[name];
			this._propDict.Remove(name);
			return value;
		}

		public bool HasProperty(string name)
		{
			return this._propDict.ContainsKey(name);
		}

		public Dictionary<string, string>.KeyCollection GetPropertyNames()
		{
			return this._propDict.Keys;
		}

		public Dictionary<string, string> GetProperties()
		{
			return this._propDict;
		}

		public string GetAttribute(string name)
		{
			return this._attrDict[name];
		}

		public string GetAttribute(string name, string dv)
		{
			return this._propDict.GetOrAddDefault(name, () => dv);
		}

		public void SetAttribute(string name, string value)
		{
			this._attrDict[name] = value;
		}

		public string RemoveAttribute(string name)
		{
			string value = this._attrDict[name];
			this._attrDict.Remove(name);
			return value;
		}

		public bool HasAttribute(string name)
		{
			return this._attrDict.ContainsKey(name);
		}

		public Dictionary<string, string>.KeyCollection GetAttributeName()
		{
			return this._attrDict.Keys;
		}

		public Dictionary<string, string> GetAttributes()
		{
			return this._attrDict;
		}

		public bool Reloadable()
		{
			return this._configFile != null;
		}

		public void Reload()
		{
			if (Reloadable())
			{
				this._configList.Clear();
				this._propDict.Clear();
				this._attrDict.Clear();
				this._parentConfig = null;
				this._name = null;

				LoadFrom(this._configFile);
				return;
			}

			throw new IOException("无法重新载入配置信息");
		}

		public FileInfo GetConfigFile()
		{
			return this._configFile;
		}

		public void SetConfigFile(FileInfo file)
		{
			this._configFile = file;
		}

		public FileInfo GetSuperConfigFile()
		{
			if (this._parentConfig != null)
				return this._parentConfig._configFile ?? this._parentConfig.GetSuperConfigFile();

			return null;
		}

		public XMLConfig RootConfig()
		{
			if (this._parentConfig == null)
				return this;
			return this._parentConfig.RootConfig();
		}

		public XMLConfig SuperConfig()
		{
			return this._parentConfig;
		}

		public string GetName()
		{
			return this._name;
		}

		public void SetName(string name)
		{
			this._name = name;
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
			if (this._name == null)
			{
				string name = node.Name;
				if (name != null)
					this._name = name.Trim();
			}

			Dictionary<string, string> nodeAttrDict = XMLUtil.GetNodeAttrs(node);
			foreach (var kv in nodeAttrDict)
			{
				string key = kv.Key;
				if (this._attrDict.ContainsKey(key))
					continue;
				string value = nodeAttrDict.GetOrAddDefault(key, () => "");
				this._attrDict[key] = value;
			}

			XmlNode child = node.FirstChild;
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
						XMLConfig subConfig = null;
						if (parseClassString.Length > 0)
							subConfig = (XMLConfig)Activator.CreateInstance(Type.GetType(parseClassString));
						else
							subConfig = new XMLConfig();
						subConfig._parentConfig = this;
						subConfig._name = childNodeName;
						subConfig.LoadFrom(child); // 不断循环下一层
						this._configList.Add(subConfig);
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
			return this._configList.Count;
		}

		public XMLConfig GetConfig(int index)
		{
			return this._configList[index];
		}

		public bool HasConfig(string tag)
		{
			for (int i = 0; i < this._configList.Count; i++)
			{
				XMLConfig conf = this._configList[i];
				if (tag == conf.GetName())
					return true;
			}

			return false;
		}

		public XMLConfig GetConfig(string tag)
		{
			for (int i = 0; i < this._configList.Count; i++)
			{
				XMLConfig config = this._configList[i];
				if (tag == config.GetName())
					return config;
			}

			return null;
		}

		public XMLConfig[] GetConfigs(string tag)
		{
			List<XMLConfig> configList = new List<XMLConfig>();
			for (int i = 0; i < this._configList.Count; i++)
			{
				XMLConfig config = this._configList[i];
				if (tag.Equals(config.GetName()))
					configList.Add(config);
			}

			XMLConfig[] configs = new XMLConfig[configList.Count];
			configs = configList.ToArray();
			return configs;
		}

		public XMLConfig[] GetConfigs()
		{
			XMLConfig[] configs = new XMLConfig[this._configList.Count];
			configs = this._configList.ToArray();
			return configs;
		}

		public XMLConfig GetConfig(string attrName, string attrValue)
		{
			for (int i = 0; i < GetConfigCount(); i++)
			{
				XMLConfig config = this._configList[i];
				if (config.GetAttribute(attrName) == attrValue)
					return config;
			}

			return null;
		}

		public XMLConfig MatchesConfig(string attrName, string attrValue)
		{
			Regex regex = new Regex(attrValue, RegexOptions.IgnoreCase);
			for (int i = 0; i < GetConfigCount(); i++)
			{
				XMLConfig config = this._configList[i];
				if (config.HasAttribute(attrName))
				{
					Match match = regex.Match(config.GetAttribute(attrName));
					if (match.Success)
						return config;
				}
			}

			return null;
		}

		public XMLConfig AddConfig(string tag)
		{
			XMLConfig subConfig = new XMLConfig {_parentConfig = this};
			subConfig.SetName(tag);
			this._configList.Add(subConfig);
			return subConfig;
		}

		public XMLConfig AddConfig(int index, string tag)
		{
			XMLConfig subConfig = new XMLConfig {_parentConfig = this};
			subConfig.SetName(tag);
			this._configList.Insert(index, subConfig);
			return subConfig;
		}

		public void AddConfig(XMLConfig subConfig)
		{
			subConfig._parentConfig = this;
			this._configList.Add(subConfig);
		}

		public void AddConfig(int index, XMLConfig subConfig)
		{
			subConfig._parentConfig = this;
			this._configList.Insert(index, subConfig);
		}

		public XMLConfig RemoveConfig(int index)
		{
			XMLConfig config = this._configList[index];
			this._configList.RemoveAt(index);
			return config;
		}

		public XMLConfig RemoveConfig(string tag)
		{
			XMLConfig config = GetConfig(tag);
			if (config != null)
				this._configList.Remove(config);
			return config;
		}

		public XMLConfig removeConfig(string attrName, string attrValue)
		{
			XMLConfig config = GetConfig(attrName, attrValue);
			if (config != null)
				this._configList.Remove(config);
			return config;
		}

		public XMLConfig[] RemoveConfigs(string tag)
		{
			XMLConfig[] configs = GetConfigs(tag);
			for (int i = 0; i < configs.Length; i++)
				this._configList.Remove(configs[i]);
			return configs;
		}

		public void ClearConfigs()
		{
			this._configList.Clear();
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
				if (!this._propDict.ContainsKey(key))
				{
					string value = XMLUtil.GetNodeAttrValue(node, Value_Key, "");
					if (value.Length == 0)
						value = XMLUtil.GetNodeValue(node, "").Trim();
					this._propDict[key] = value;
				}
			}
		}


		public static XmlNode ToXMLNode(XmlNode parent, XMLConfig config)
		{
			XmlNode root = XMLUtil.AddChildNode(parent, config._name, "");
			foreach (var kv in config._attrDict)
			{
				string key = kv.Key;
				if (key == Config_Path_Key)
					continue;
				string value = kv.Value;
				XMLUtil.SetNodeAttrValue(root, key, value);
			}

			foreach (var kv in config._propDict)
			{
				string key = kv.Key;
				string value = kv.Value;
				XmlNode child = XMLUtil.AddChildNode(root, Property_Key, "");
				XMLUtil.SetNodeAttrValue(child, Name_Key, key);
				if (value.Length < 100 && value.IndexOf('\n') == -1 && value.IndexOf('\r') == -1)
					XMLUtil.SetNodeAttrValue(child, Value_Key, value);
				else
					XMLUtil.SetNodeValue(child, value);
			}
			
			for (int i = 0; i < config._configList.Count; i++)
				ToXMLNode(root, (XMLConfig)config._configList[i]);
			return root;
		}

		#endregion









	}
}