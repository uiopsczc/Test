
using System;
using System.Collections.Generic;
using System.IO;

namespace CsCat
{
	/// <summary>
	/// #xxxxx 注释行
	/// name=chenzhiquan
	/// age=18
	/// </summary>
	public class PropertyConfig
	{
		#region field

		private Dictionary<string, string> prop_dict = new Dictionary<string, string>();

		#endregion

		#region ctor

		public PropertyConfig(Dictionary<string, string> prop_dict)
		{
			foreach (string key in prop_dict.Keys)
			{
				string value = prop_dict[key];
				this.prop_dict[key] = value;
			}
		}

		public PropertyConfig(FileInfo file)
		{
			Load(file);
		}

		public PropertyConfig(string file) : this(new FileInfo(file))
		{
		}

		public PropertyConfig()
		{
		}

		#endregion

		#region public method

		public void Load(FileInfo file)
		{
			FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
			StreamReader streamReader = new StreamReader(fileStream);
			string line;
			while ((line = streamReader.ReadLine()) != null)
			{
				line = line.Trim();
				if (line.StartsWith("#"))
					continue;
				string[] contents = line.Split('=');
				if (contents.Length != 2)
					continue;
				this.prop_dict[contents[0]] = contents[1];
			}

			streamReader.Close();
		}

		public void Save(FileInfo file)
		{
			FileStream fileStream = new FileStream(file.FullName, FileMode.Truncate, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			foreach (string key in prop_dict.Keys)
				streamWriter.WriteLine(string.Format("{0}={1}", key, this.prop_dict[key]));
			streamWriter.Close();
		}

		public Dictionary<string, string> GetProperties()
		{
			return this.prop_dict;
		}

		public string[] ConfigNames()
		{
			string[] config_names = new String[this.prop_dict.Count];
			int i = 0;
			foreach (string name in this.prop_dict.Keys)
			{
				config_names[i] = name;
				i++;
			}

			return config_names;
		}

		public void Clear()
		{
			this.prop_dict.Clear();
		}

		public string GetConfigString(string name)
		{
			return GetConfigString(name, "");
		}

		public string GetConfigString(string name, string dv)
		{
			return this.prop_dict.GetOrAddDefault(name, () => dv);
		}

		public void SetConfigString(string name, string value)
		{
			this.prop_dict[name] = value;
		}

		public int GetConfigInt(string name)
		{
			return GetConfigInt(name, 0);
		}

		public int GetConfigInt(string name, int dv)
		{
			return int.Parse(this.prop_dict.GetOrAddDefault(name, () => dv.ToString()));
		}

		public void SetConfigInt(string name, int value)
		{
			this.prop_dict[name] = value.ToString();
		}

		#endregion
	}
}
