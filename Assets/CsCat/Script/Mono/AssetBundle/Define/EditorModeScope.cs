using System;

namespace CsCat
{
	public class EditorModeScope : IDisposable
	{
		private bool orgIsEditorMode;

		public EditorModeScope(bool isEditorMode)
		{
			orgIsEditorMode = EditorModeConst.IsEditorMode;
			EditorModeConst.IsEditorMode = isEditorMode;
		}

		public void Dispose()
		{
			EditorModeConst.IsEditorMode = orgIsEditorMode;
		}
	}
}