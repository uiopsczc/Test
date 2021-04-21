using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class TestEditorWindow : EditorWindow
  {
    void Awake()
    {
      //      EditorWindow.GetWindow<TimelineRectTestEditorWindow>();
    }

    void OnEnable()
    {
      //      LogCat.log("当窗口enable时调用一次");
      //      初始化
      //      GameObject go = Selection.activeGameObject;
    }

    void OnDisable()
    {
      //      LogCat.log("当窗口disable时调用一次");
    }

    void OnFocus()
    {
      //      LogCat.log("当窗口获得焦点时调用一次");
    }

    void OnLostFocus()
    {
      //      LogCat.log("当窗口丢失焦点时调用一次");
    }

    void OnHierarchyChange()
    {
      //      LogCat.log("当Hierarchy视图中的任何对象发生改变时调用一次");
    }

    void OnProjectChange()
    {
      //      LogCat.log("当Project视图中的资源发生改变时调用一次");
    }

    void OnInspectorUpdate()
    {

      //      LogCat.log("窗口面板的更新");
      //      这里开启窗口的重绘，不然窗口信息不会刷新
      this.Repaint();
    }

    void OnDestroy()
    {
      //      LogCat.log("当窗口关闭时调用");
    }

    void OnGUI()
    {
      //      using (new GUILayout.AreaScope(new Rect(100, 100, 100, 100)))
      //      {
      //        
      //      }


      this.Repaint();
    }

    void OnSelectionChange()
    {
      //      当窗口处于开启状态，并且在Hierarchy视图中选择某游戏对象时调用
      //      foreach (Transform t in Selection.transforms)
      //      {
      //         //有可能是多选，这里开启一个循环打印选中游戏对象的名称
      //          LogCat.log("OnSelectionChange" , t.name);
      //      }
    }
  }
}