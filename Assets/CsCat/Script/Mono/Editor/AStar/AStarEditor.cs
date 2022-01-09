using UnityEditor;

namespace CsCat
{
	[CustomEditor(typeof(AStarMonoBehaviour))]
	public partial class AStarEditor : Editor
	{
		private AStarMonoBehaviour _target => base.target as AStarMonoBehaviour;
		private Tool _orgEditorToolSelected;
		private AStarBrush _brush = new AStarBrush();

		void OnEnable()
		{
			_orgEditorToolSelected = Tools.current;
			Tools.current = Tool.None;

			_brush.astarMonoBehaviour = _target;
		}

		void OnDisable()
		{
			Tools.current = _orgEditorToolSelected;
		}
	}
}