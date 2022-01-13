using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class LogCat
	{
		//给Lua端调用
		public static void LuaBatchLog(List<Dictionary<string, string>> messageInfoList)
		{
			for (var i = 0; i < messageInfoList.Count; i++)
			{
				var messageInfo = messageInfoList[i];
				string logType = messageInfo["logType"];
				string message = messageInfo["message"];
				switch (logType)
				{
					case "log":
						Log(message);
						break;
					case "warn":
						LogWarning(message);
						break;
					case "error":
						LogError(message);
						break;
				}
			}
		}
	}
}