#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public class ResizableRectsBase
	{
		protected Func<Rect> totalRectFunc;
		protected Rect[] splitLineRects;
		protected Rect[] resizeSplitRects;
		protected int resizingSplitLineRectIndex = ResizableRectsConst.Not_Resizing_Split_Line_Rect_Index;


		public Rect[] rects;
		public Rect[] paddingRects;

		public Rect total_rect => totalRectFunc();

		public bool isResizing => resizingSplitLineRectIndex != ResizableRectsConst.Not_Resizing_Split_Line_Rect_Index;

		private bool isCanResizable = true;

		protected bool isUsingSplitPixels = false;
		protected float[] splitPixels;
		protected float[] splitPCTs;

		public string name;

		public ResizableRectsBase(Func<Rect> totalRectFunc, float[] splitPixels, float[] splitPcTs = null)
		{
			this.totalRectFunc = totalRectFunc;
			this.splitPixels = splitPixels;
			this.splitPCTs = splitPcTs;
			this.isUsingSplitPixels = !splitPixels.IsNullOrEmpty();
			if (!isUsingSplitPixels && splitPcTs.IsNullOrEmpty())
				this.splitPCTs = new float[] {0.3f};

			int splitCount = isUsingSplitPixels ? this.splitPixels.Length : this.splitPCTs.Length;
			this.resizeSplitRects = new Rect[splitCount];


			this.splitLineRects = new Rect[splitCount];
			UpdateSplitLineRects();


			this.rects = new Rect[splitCount + 1];
			this.paddingRects = new Rect[splitCount + 1];
			SetRectListSize();
		}

		public virtual void UpdateSplitLineSetting(int splitLineIndex, float delta)
		{
		}

		public virtual void SetSplitLinePosition(int splitLineIndex, float position)
		{
		}

		protected virtual void UpdateSplitLineRects()
		{
		}

		protected virtual void SetRectListSize()
		{
		}

		public void SetCanResizable(bool isCanResizable)
		{
			this.isCanResizable = isCanResizable;
		}

		public void OnGUI()
		{
			UpdateSplitLineRects();
			foreach (var splitLineRect in splitLineRects)
				EditorGUI.DrawRect(splitLineRect, Color.grey);
			SetRectListSize();
			Resizing();
		}

		void Resizing()
		{
			ResizingSplitLineRects();
			if (isCanResizable)
			{
				ResizingResizeSplitRects();
				HandleMouseResizingEvent();
			}
		}

		protected virtual void ResizingSplitLineRects()
		{
		}

		protected virtual void ResizingResizeSplitRects()
		{
		}

		protected virtual void HandleMouseDragEvent()
		{
		}

		protected void HandleMouseResizingEvent()
		{
			if (Event.current.isMouse)
			{
				if (Event.current.type == EventType.MouseDown)
				{
					for (int i = 0; i < resizeSplitRects.Length; i++)
					{
						if (resizeSplitRects[i].Contains(Event.current.mousePosition))
						{
							resizingSplitLineRectIndex = i;
							Event.current.Use();
							break;
						}
					}
				}
				else if (Event.current.type == EventType.MouseDrag)
				{
					if (isResizing)
					{
						HandleMouseDragEvent();
						Event.current.Use();
					}
				}
				else if (Event.current.type == EventType.MouseUp)
				{
					if (isResizing)
					{
						resizingSplitLineRectIndex = ResizableRectsConst.Not_Resizing_Split_Line_Rect_Index;
						Event.current.Use();
					}
				}
			}
		}
	}
}
#endif