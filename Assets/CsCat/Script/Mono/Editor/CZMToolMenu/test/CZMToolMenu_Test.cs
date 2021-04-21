using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  /// <summary>
  ///   CZM工具菜单
  /// </summary>
  public partial class CZMToolMenu : MonoBehaviour
  {
    private static int i;
    [MenuItem(CZMToolConst.MenuRoot + "Test")]
    public static void Test()
    {
      //      ScriptableObjectTest.CreateInstance();
      //      EditorWindow.GetWindow<AnimationTimelinableTestEditorWindow>();
      //      EditorWindow.GetWindow<MountTimelinableTestEditorWindow>();
      EditorWindow.GetWindow<SkinnedMeshRendererTimelinableTestEditorWindow>();
      //      List<AC> list = new List<AC>();
      //      list.Add(new AC("chen"));
      //      list.Add(list[0].Clone());
      //      list[0].name = "quan";
      //      LogCat.log(list);
      //      ScriptableObjectTest.CreateInstance();
      //    var obj = Selection.activeObject;
      //    LogCat.log(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(obj)));
      //    LogCat.log(AssetDatabase.GUIDToAssetPath(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(obj))));
    }


    [UnityEditor.MenuItem(CZMToolConst.MenuRoot + "Test2")]
    public static void Test2()
    {
      //      Dictionary<string,string> dict = new Dictionary<string, string>();
      //      dict["kk"] = "ff";
      //      LogCat.log("before:", dict.GetHashCode());
      //      dict["hh"] = "gg";
      //      LogCat.log("after:", dict.GetHashCode());
    }


    public static void T1()
    {
      LogCat.log("t1");
    }

    public static void T2()
    {
      LogCat.log("t2");
    }


    [UnityEditor.MenuItem(CZMToolConst.MenuRoot + "TestEditorWindow")]
    public static void TestEditor()
    {
      EditorWindow.GetWindow<TestEditorWindow>(false, "TestEditorWindow").minSize = new Vector2(720f, 480f);
    }
  }

}