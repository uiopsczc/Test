using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	public partial class LogCat : MonoBehaviour
	{
		private static List<LogMessage> messageList = new List<LogMessage>();
		private static StringBuilder stringBuilder = new StringBuilder();
		private static bool isInited = false;


		public static void Init()
		{
			if (isInited)
				return;
			isInited = true;
			Application.logMessageReceived += HandleLog;
			if (Directory.Exists(LogCatConst.LogBasePath))
			{
				//每次启动客户端删除10天前保存的Log
				DateTime time = DateTimeUtil.NowDateTime();
				DateTime passTime = time.AddDays(-10);
				var files = Directory.GetFiles(LogCatConst.LogBasePath);
				for (var i = 0; i < files.Length; i++)
				{
					string fullFileName = files[i];
					if (File.GetLastWriteTime(fullFileName) < passTime)
						File.Delete(fullFileName);
				}
			}
		}

		void Awake()
		{
			Init();
			DontDestroyOnLoad(this.gameObject);
		}

		void Update()
		{
			Flush();
		}

		//////////////////////////////////////////////////////////////////////
		// 公有方法
		//////////////////////////////////////////////////////////////////////
		public static void Log(object message)
		{
			Debug.Log(message);
		}

		public static void Log(object message, Object context)
		{
			//    Debug.Log(message, context); //统一到同一个地方，所以放弃context
			Log(message);
		}

		public static void LogFormat(string format, params object[] args)
		{
			Log(string.Format(format, args));
		}

		public static void Log(object message, Color color)
		{
			Log(GetFormatString(message, color));
		}

		public static void log(params object[] args)
		{
			Log(GetLogString(args));
		}


		public static void LogWarning(object message)
		{
			Debug.LogWarning(message);
		}

		public static void LogWarning(object message, Object context)
		{
			//    Debug.LogWarning(message, context);//统一到同一个地方，所以放弃context
			LogWarning(message);
		}

		public static void LogWarningFormat(string format, params object[] args)
		{
			LogWarning(string.Format(format, args));
		}

		public static void LogWarning(object message, Color color)
		{
			LogWarning(GetFormatString(message, color));
		}

		public static void warn(params object[] args)
		{
			LogWarning(GetLogString(args));
		}



		public static void LogError(object message)
		{
			Debug.LogError(message);
		}

		public static void LogError(object message, Object context)
		{
			//    Debug.LogError(message, context);//统一到同一个地方，所以放弃context
			LogError(message);
		}

		public static void LogErrorFormat(string format, params object[] args)
		{
			LogError(string.Format(format, args));
		}

		public static void LogError(object msg, Color color)
		{
			LogError(GetFormatString(msg, color));
		}

		public static void error(params object[] args)
		{
			LogError(GetLogString(args));
		}



		public static void ClearLogs()
		{
			Assembly assembly = AssemblyUtil.GetAssembly("UnityEditor");
			Type type = assembly.GetType("UnityEditor.LogEntries");
			MethodInfo methodInfo = type.GetMethodInfo2("Clear");
			methodInfo.Invoke(new object(), null);
		}

		//////////////////////////////////////////////////////////////////////
		// 私有方法
		//////////////////////////////////////////////////////////////////////
		private static string GetFormatString(object msg, Color color)
		{
			string colorString = color.ToHtmlStringRGB();
			return string.Format("<color=#{0}>{1}</color>", colorString, msg);
		}

		public static string GetLogString(params object[] args)
		{
			StringBuilder result = new StringBuilder("");
			if (args == null)
				return result.ToString();
			for (var i = 0; i < args.Length; i++)
			{
				var arg = args[i];
				if (arg == null)
					result.Append("null ");
				else
					result.Append(arg.ToString2() + " ");
			}

			return result.ToString();
		}

		private static void HandleLog(string message, string stackTrace, LogType logType)
		{
			messageList.Add(new LogMessage(message, stackTrace, LogCatConst.LogType_2_LogCatType_Dict[logType]));
			if (!Application.isPlaying)
				Flush();
		}

		private static void Flush()
		{
			if (messageList.Count > 0)
			{
				var nowTimeSpan = DateTimeUtil.NowDateTime().TimeOfDay;
				for (var i = 0; i < messageList.Count; i++)
				{
					var message = messageList[i];
					if (message.logType < LogCatConst.Write_Log_Level && message.logType < LogCatConst.GUI_Log_Level)
						continue;

					string logContent = string.Format("[{0}][{1}] {2}\n{3}", nowTimeSpan,
						message.logType,
						message.message, message.stackTrace);

					if (message.logType >= LogCatConst.Write_Log_Level)
						stringBuilder.Append(logContent + "\n");

					if (message.logType >= LogCatConst.GUI_Log_Level)
					{
						guiMessageList.Add(string.Format("<color=#{0}>{1}</color>",
							LogCatConst.LogCatTypeInfo_Dict[message.logType].color.ToHtmlStringRGBA(), logContent));
					}
				}

				WriteLogFile(stringBuilder.ToString());
				messageList.Clear();
				stringBuilder.Clear();
			}
		}

		private static void WriteLogFile(string content)
		{
			if (content.IsNullOrWhiteSpace())
				return;
			string day = DateTimeUtil.GetDateTime("date", DateTimeUtil.NowTicks());
			string filePath = LogCatConst.LogBasePath + day + ".txt";
			StdioUtil.WriteTextFile(filePath, content, false, true);
		}
	}
}