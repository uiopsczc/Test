using System;

namespace CsCat
{
	public class EditorModeScope : IDisposable
	{
		private bool org_is_editor_mode;

		public EditorModeScope(bool is_editor_mode)
		{
			org_is_editor_mode = EditorModeConst.IsEditorMode;
			EditorModeConst.IsEditorMode = is_editor_mode;
		}

		public void Dispose()
		{
			EditorModeConst.IsEditorMode = org_is_editor_mode;
		}
	}
}