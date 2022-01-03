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
		public static void LuaBatchLog(List<Dictionary<string, string>> message_info_list)
		{
			foreach (var message_info in message_info_list)
			{
				string logType = message_info["logType"];
				string message = message_info["message"];
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