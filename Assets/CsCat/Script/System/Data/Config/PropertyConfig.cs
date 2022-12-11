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

		private Dictionary<string, string> _propDict = new Dictionary<string, string>();

		#endregion

		#region ctor

		public PropertyConfig(Dictionary<string, string> propDict)
		{
			foreach (var kv in propDict)
			{
				string key = kv.Key;
				string value = kv.Value;
				this._propDict[key] = value;
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
				this._propDict[contents[0]] = contents[1];
			}

			streamReader.Close();
		}

		public void Save(FileInfo file)
		{
			FileStream fileStream = new FileStream(file.FullName, FileMode.Truncate, FileAccess.Write);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			foreach (var kv in _propDict)
			{
				string key = kv.Key;
				string value = kv.Value;
				streamWriter.WriteLine(string.Format("{0}={1}", key, value));
			}
			streamWriter.Close();
		}

		public Dictionary<string, string> GetProperties()
		{
			return this._propDict;
		}

		public string[] ConfigNames()
		{
			string[] configNames = new string[this._propDict.Count];
			int i = 0;
			foreach (var kv in this._propDict)
			{
				var name = kv.Key;
				configNames[i] = name;
				i++;
			}

			return configNames;
		}

		public void Clear()
		{
			this._propDict.Clear();
		}

		public string GetConfigString(string name)
		{
			return GetConfigString(name, "");
		}

		public string GetConfigString(string name, string dv)
		{
			return this._propDict.GetOrAddDefault(name, () => dv);
		}

		public void SetConfigString(string name, string value)
		{
			this._propDict[name] = value;
		}

		public int GetConfigInt(string name)
		{
			return GetConfigInt(name, 0);
		}

		public int GetConfigInt(string name, int dv)
		{
			return int.Parse(this._propDict.GetOrAddDefault(name, () => dv.ToString()));
		}

		public void SetConfigInt(string name, int value)
		{
			this._propDict[name] = value.ToString();
		}

		#endregion
	}
}