using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public static class UIAutoGenConst
	{
		public const string Cs_Line_Cfg_Part_Starts_With = "//CsAutoGen:";
		public const string Lua_Line_Cfg_Part_Starts_With = "--LuaAutoGen:";
	}
}