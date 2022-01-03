using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public class GenericMenuCatTestEditorWindow : EditorWindow
	{
		[GenericMenuItem("test/Example1/Ex1", false, 8)]
		public static void Example2()
		{
			Debug.LogError("hello");
		}

		[GenericMenuItem("test/Example2/Ex1", false, 13)]
		public static void Example31()
		{
			Debug.LogError("world1");
		}

		[GenericMenuItem("test/Example2/Ex2", false, 12)]
		public static void Example32()
		{
			Debug.LogError("world2");
		}

		[GenericMenuItem("test/Example2/Ex3", false, 11)]
		public void Example33(object userData)
		{
			Debug.LogError(userData);
		}

		//Test的时候将这行去掉
		//    [MenuItem("GenericMenuCatTestEditorWindow/Test")]
		public static void Load()
		{
			GetWindow<GenericMenuCatTestEditorWindow>();
		}

		public void OnGUI()
		{
			if (GUILayout.Button("test"))
			{
				//如果GenericMenuItem是非静态函数，需要提供source_object,如果调用的函数有userData，也需要提供userData到GenericMenuItemInfoInvokParams中；其中args中string是GenericMenuItem的路径
				//如果没有以上的需求，则不用提供args
				GenericMenuCatUtil.dict["test"].Show(("test/Example2/Ex3", this, "chen"));
			}
		}
	}
}