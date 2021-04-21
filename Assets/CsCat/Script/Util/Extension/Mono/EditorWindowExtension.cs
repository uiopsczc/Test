#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static class EditorWindowExtension
  {
    public static void ShowNotificationAndLog(this EditorWindow self, params object[] args)
    {
      self._ShowNotificationAndCallback(() => LogCat.log(args), args);
    }

    public static void ShowNotificationAndWarn(this EditorWindow self, params object[] args)
    {
      self._ShowNotificationAndCallback(() => LogCat.warn(args), args);
    }

    public static void ShowNotificationAndError(this EditorWindow self, params object[] args)
    {
      self._ShowNotificationAndCallback(() => LogCat.error(args), args);
    }

    public static void ShowNotification2(this EditorWindow self, params object[] args)
    {
      self.ShowNotification(LogCat.GetLogString(args).ToGUIContent());
    }

    private static void _ShowNotificationAndCallback(this EditorWindow self, Action action, params object[] args)
    {
      self.ShowNotification2(args);
      action();
    }








  }
}
#endif