using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class MountTimelinableTestEditorWindow
	{
		void DrawLeft()
		{
			using (new GUILayout.AreaScope(this._resizableRects.rects[0]))
			{
				GUILayout.Space(20);
				using (new GUILayoutBeginVerticalScope(EditorStyles.helpBox))
				{
					TimelinableEditorWindowUtil.DrawGUISetting_Sequence(ref _sequence);
					if (_sequence == null)
						return;
					_sequence.DrawGUISettingDetail();
				}

				if (_sequence.tracks.Length <= 0)
					return;
				_sequence.DrawGUISettingTracks(_timelineRect.playTime);
			}
		}
	}
}