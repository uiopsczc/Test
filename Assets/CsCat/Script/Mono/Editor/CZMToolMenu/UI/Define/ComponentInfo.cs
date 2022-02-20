using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class ComponentInfo
	{
		public string name;
		public string type;
		public string path;

		public ComponentInfo(string name, string type, string path)
		{
			this.name = name;
			this.type = type;
			this.path = path;
		}



	}
}