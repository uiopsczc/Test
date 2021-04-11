using UnityEngine;

namespace CsCat
{
  public static class RedDotTest
  {
    ////////////////////////////////////////////////////////////////
    public static bool ShowRedDotTest0()
    {
      return true;
    }

    public static bool ShowRedDotTest1(string arg1)
    {
      if (arg1.Equals("show_red_dot"))
        return true;
      return false;
    }

    public static bool ShowRedDotTest2(string arg1, string arg2)
    {
      if (arg1.Equals("show_red_dot") && arg2.Equals("show_red_dot"))
        return true;
      return false;
    }

    ////////////////////////////////////////////////////////////////
    public static object[] ShowRedDotTest1AllParmasListFunc()
    {
      return new object[]
      {
        new string[] {"show_red_dot"},
        new string[] {"not_show_red_dot"}
      };
    }

    public static object[] ShowRedDotTest2AllParmasListFunc()
    {
      return new object[]
      {
        new string[] {"show_red_dot", "show_red_dot"},
        new string[] {"not_show_red_dot", "show_red_dot"}
      };
    }

    ////////////////////////////////////////////////////////////////
    public static void Test()
    {
      GameObject gm_btn_gameObject = GameObject.Find("gm_btn");
      GameObject test_btn_gameObject = GameObject.Find("test_btn");
      GameObject icon_gameObject = GameObject.Find("icon");
      GameObject UITestPanel_gameObject = GameObject.Find("UITestPanel");

      Client.instance.redDotManager.AddRedDot(gm_btn_gameObject, "Test0任务");
      Client.instance.redDotManager.AddRedDot(test_btn_gameObject, "Test1任务", null, "show_red_dot");
      Client.instance.redDotManager.AddRedDot(icon_gameObject, "Test2任务", null, "show_red_dot", "show_red_dot");
      Client.instance.redDotManager.AddRedDot(UITestPanel_gameObject, "Test任务");

      //    Client.instance.Broadcast("OnShowRedDotTest0");
//    Client.instance.Broadcast("OnShowRedDotTest1");
//    Client.instance.Broadcast("OnShowRedDotTest2");
    }
  }
}