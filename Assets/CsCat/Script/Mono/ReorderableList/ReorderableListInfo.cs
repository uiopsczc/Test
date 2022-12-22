#if UNITY_EDITOR
using UnityEditorInternal;
using System.Collections;

namespace CsCat
{
	public class ReorderableListInfo : ICopyable
	{
		public IList toReorderList;
		private readonly GUIToggleTween _toggleTween = new GUIToggleTween();
		public ReorderableList _reorderableList;
		public ReorderableList reorderableList
		{
			get
			{
				toReorderList.ToReorderableList(ref _reorderableList);
				return _reorderableList;
			}
		}

		public ReorderableListInfo(IList toReorderList)
		{
			this.toReorderList = toReorderList;
		}

		public void SetElementHeight(float elementHeight)
		{
			this.reorderableList.SetElementHeight(elementHeight);
		}

		public void DrawGUI(string title)
		{
			this.reorderableList.DrawGUI(_toggleTween, title);
		}

		public void CopyTo(object dest)
		{
			var destReorderableListInfo = dest as ReorderableListInfo;
			destReorderableListInfo.toReorderList.Clear();
			for (var i = 0; i < toReorderList.Count; i++)
			{
				var toReorderElement = toReorderList[i];
				if (toReorderElement is ICopyable toReorderElement1)
				{
					var destCloneElement = toReorderElement1.GetType().CreateInstance<ICopyable>();
					toReorderElement1.CopyTo(destCloneElement);
					destReorderableListInfo.toReorderList.Add(destCloneElement);
				}
				else
					destReorderableListInfo.toReorderList.Add(toReorderElement);
			}

			destReorderableListInfo.toReorderList.ToReorderableList(ref destReorderableListInfo._reorderableList);
		}

		public void CopyFrom(object source)
		{
			var sourceReorderableListInfo = source as ReorderableListInfo;
			toReorderList.Clear();
			for (var i = 0; i < sourceReorderableListInfo.toReorderList.Count; i++)
			{
				var sourceToReorderElement = sourceReorderableListInfo.toReorderList[i];
				if (sourceToReorderElement is ICopyable sourceToReorderElementClone)
				{
					var cloneElement = sourceToReorderElementClone.GetType().CreateInstance<ICopyable>();
					cloneElement.CopyFrom(sourceToReorderElementClone);
					toReorderList.Add(cloneElement);
				}
				else
					toReorderList.Add(sourceToReorderElement);
			}

			toReorderList.ToReorderableList(ref _reorderableList);
		}
	}
}
#endif