using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public static class LogCatConst
  {
    public static string Log_Base_Path = Application.persistentDataPath + "/Log/";
    public static LogCatType GUI_Log_Level = LogCatType.Error;//正式版本的时候这里改成LogCatType.None
    public static LogCatType Write_Log_Level = LogCatType.Error;

    public static Dictionary<LogType, LogCatType> LogType_2_LogCatType_Dict = new Dictionary<LogType, LogCatType>()
    {
      {LogType.Log, LogCatType.Log},
      {LogType.Warning, LogCatType.Warn},
      {LogType.Error, LogCatType.Error},
      {LogType.Exception, LogCatType.Exception},
      {LogType.Assert, LogCatType.Assert},
    };

    public static Dictionary<LogCatType, LogCatTypeInfo> LogCatTypeInfo_Dict =
      new Dictionary<LogCatType, LogCatTypeInfo>()
      {
        {LogCatType.Log, new LogCatTypeInfo(LogCatType.Log, Color.white)},
        {LogCatType.Warn, new LogCatTypeInfo(LogCatType.Warn, Color.yellow)},
        {LogCatType.Error, new LogCatTypeInfo(LogCatType.Error, Color.red)},
        {LogCatType.Exception, new LogCatTypeInfo(LogCatType.Exception, Color.red)},
        {LogCatType.Assert, new LogCatTypeInfo(LogCatType.Assert, Color.red)},
      };
  }
}