//using System.Collections.Generic;
//
//namespace CsCat
//{
//  public static class RedDotConst
//  {
//    public static string Red_Dot_Image_AssetPath = "Assets/Resources/common/ui/texture/red_dot.png:red_dot";
//    public static string Red_Dot_Name = "red_dot";
//
//    public static List<RedDotInfo> Red_Dot_Info_List = new List<RedDotInfo>()
//    {
//      new RedDotInfo("Test0任务", new MethodInvoker(typeof(RedDotTest), "ShowRedDotTest0"),
//        new List<string>() {"OnShowRedDotTest0"}),
//      new RedDotInfo("Test1任务", new MethodInvoker(typeof(RedDotTest), "ShowRedDotTest1"),
//        new List<string>() {"OnShowRedDotTest1"}),
//      new RedDotInfo("Test2任务", new MethodInvoker(typeof(RedDotTest), "ShowRedDotTest2"),
//        new List<string>() {"OnShowRedDotTest2"}),
//      new RedDotInfo("Test任务", null, null, new List<string>() {"Test0任务", "Test1任务", "Test2任务"},
//        new Dictionary<string, MethodInvoker>()
//        {
//          {"Test1任务", new MethodInvoker(typeof(RedDotTest), "ShowRedDotTest1AllParmasListFunc")},
//          {"Test2任务", new MethodInvoker(typeof(RedDotTest), "ShowRedDotTest2AllParmasListFunc")}
//        }),
//    };
//
//  }
//}