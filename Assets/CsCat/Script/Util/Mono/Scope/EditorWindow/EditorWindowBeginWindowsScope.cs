#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CsCat
{
	public class EditorWindowBeginWindowsScope : IDisposable
	{
		private readonly EditorWindow self;

		public EditorWindowBeginWindowsScope(EditorWindow self)
		{
			this.self = self;
			self.BeginWindows();
		}

		public void Dispose()
		{
			self.EndWindows();
		}
	}
}
#endif