using System;

namespace CsCat
{
	public class EditorModeScope : IDisposable
	{
		private readonly bool _orgIsEditorMode;

		public EditorModeScope(bool isEditorMode)
		{
			_orgIsEditorMode = EditorModeConst.IsEditorMode;
			EditorModeConst.IsEditorMode = isEditorMode;
		}

		public void Dispose()
		{
			EditorModeConst.IsEditorMode = _orgIsEditorMode;
		}
	}
}