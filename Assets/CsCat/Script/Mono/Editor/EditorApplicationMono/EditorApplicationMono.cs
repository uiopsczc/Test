using UnityEditor;

namespace CsCat
{
	[InitializeOnLoad]
	public partial class EditorApplicationMono
	{
		static EditorApplicationMono()
		{
			LogCat.Init();


			EditorApplication.delayCall += DelayCall;
			EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
			EditorApplication.modifierKeysChanged += ModifierKeysChanged;
			EditorApplication.projectWindowItemOnGUI += ProjectWindowItemOnGUI;
			EditorApplication.searchChanged += SearchChanged;
			EditorApplication.update += Update;


			EditorApplication.hierarchyChanged += HierarchyChanged;
			EditorApplication.pauseStateChanged += PauseStateChanged;
			EditorApplication.playModeStateChanged += PlayModeStateChanged;
			EditorApplication.projectChanged += ProjectChanged;
			EditorApplication.quitting += Quitting;
			EditorApplication.wantsToQuit += WantsToQuit;
		}
	}
}