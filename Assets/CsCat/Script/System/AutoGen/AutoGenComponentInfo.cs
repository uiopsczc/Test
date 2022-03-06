using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class AutoGenComponentInfo
	{
		public string name;
		public string type;
		public string path;

		public AutoGenComponentInfo(string name, string type, string path)
		{
			this.name = name;
			this.type = type;
			this.path = path;
		}



	}
}