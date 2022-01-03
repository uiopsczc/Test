using System;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
	public partial class TimelineRect
	{
		public Action onDrawTracksLeftSideCallback; //画Tracks的左边,需要外部添加具体实现
		public Action onDrawTracksRightSideCallback; //画Tracks的右边,需要外部添加具体实现

		public Func<Vector2, bool> isMouseDownOfSelectedItem; //当前鼠标是否是在selected_item中点下,需要外部添加具体实现
		public Func<Vector2, bool> tryToSelectUnselectedItemCallback; //尝试选中未选中的item, 需要外部添加具体实现
		public Action<Rect> updateSelectedItemsCallback; //更新选中的item,需要外部需要添加具体实现
		public Action<float> onDraggingSelectedCallback; //拖动选中的items触发的callback,需要外部需要添加具体实现
		public Func<bool> isHasSelectedItem; //是否有的item,需要外部添加具体实现

		public Action<Rect> onMouseRightButtonClickCallback; //右键点击的时候触发的callback,需要添加具体实现

		public Action<EditorMouseInputStatus> onEditorMouseInputStatusChangeCallback; //需要添加具体实现
		public Action<EditorCommand> onDoEditorCommandCallback; //触发EditorCommand时触发的callback,需要外部添加具体实现

		public Action onPlayCallback; //播放触发的callback,需要外部添加具体实现
		public Action onPauseCallback; //暂停播放触发的callback,需要添加具体实现

		public Action<float> onPlayTimeChangeCallback; //当playtime改变时候触发的action,需要添加具体实现
		public Action<float> onAnimatingCallback; //当animating时触发的callback,需要外部添加具体实现

		private Func<Rect> _totalRectFunc;
		private Vector2 _scrollPosition;
		public Rect scrollRect;
		private Rect _viewRect;
		public float duration;
		public HorizontalResizableRects resizableRects;

		private float _trackOffsetY = 2 * TimelineRectConst.Timeline_Track_Height;

		public Rect totalRect => _totalRectFunc();

		public Rect leftRect => this.resizableRects.rects[0];

		public Rect leftPaddingRect => this.resizableRects.paddingRects[0];

		public Rect rightRect => this.resizableRects.rects[1];

		public Rect rightPaddingRect => this.resizableRects.paddingRects[1];


		public TimelineRect(Func<Rect> totalRectFunc, float duration = 0)
		{
			this._totalRectFunc = totalRectFunc;
			this.resizableRects = new HorizontalResizableRects(totalRectFunc, new[] {140f});
			//      this.resizableRects.SetCanResizable(false);
			SetDuration(duration);
		}

		public void OnEnable()
		{
			EditorMouseInput.onStatusChanged += OnEditorMouseInputStatusChanged;
			EditorMouseInput.status = EditorMouseInputStatus.Normal;
		}

		public void OnDisable()
		{
			if (AnimationMode.InAnimationMode())
				AnimationMode.StopAnimationMode();
			EditorMouseInput.onStatusChanged -= OnEditorMouseInputStatusChanged;
		}

		public void SetDuration(float duration)
		{
			if (duration >= this.duration)
			{
				if (duration < scrollRect.width / widthPerSecond)
					this.duration = scrollRect.width / widthPerSecond;
				else
					this.duration = duration;
			}
		}

		public void OnGUI()
		{
			this.resizableRects.OnGUI();
			DrawLeft();
			DrawRight();
			OnUpdatePlaying();
			HandleInput();
		}

		void OnUpdatePlaying()
		{
			if (_isPlaying)
			{
				float deltaTime = Time.realtimeSinceStartup - this._preRealtimeSinceStartupOfPlayTime;
				this._preRealtimeSinceStartupOfPlayTime = Time.realtimeSinceStartup;
				this.playTime += deltaTime * _playSpeed;

				//根据play_time自动设置scroll_postion
				if (playTime > duration * 0.75f)
					SetDuration(playTime / 0.75f); //duration 每次增长为原来的(1/0.75f)陪
				_scrollPosition.x = Mathf.Max(_scrollPosition.x, playTime * widthPerSecond - scrollRect.width * 0.5f);
			}
		}


		void HandleInput()
		{
			if (this.totalRect.Contains(Event.current.mousePosition))
			{
				if (Event.current.isKey && Event.current.type == EventType.KeyDown)
				{
					switch (Event.current.keyCode)
					{
						case KeyCode.P:
							SwitchPlay();
							Event.current.Use();
							break;
						case KeyCode.LeftArrow:
							playTime -= _frameDuration;
							break;
						case KeyCode.RightArrow:
							playTime += _frameDuration;
							break;
					}
				}
			}
		}


		public Rect GetLeftTrackRect(int trackIndex)
		{
			Rect rect = new Rect();
			rect.x = 0;
			rect.y = _trackOffsetY + TimelineRectConst.Track_Height * trackIndex;
			rect.height = TimelineRectConst.Track_Height;
			rect.width = this.resizableRects.rects[0].width;
			return rect;
		}


		public Rect GetRightTrackRect(float time, float duration, int trackIndex)
		{
			Rect rect = new Rect();
			rect.x = time * widthPerSecond;
			rect.y = _trackOffsetY + TimelineRectConst.Track_Height * trackIndex;
			rect.height = TimelineRectConst.Track_Height;
			rect.width = duration * widthPerSecond;
			return rect;
		}

		public Rect GetRightTrackRect(int trackIndex)
		{
			Rect rect = new Rect();
			rect.x = 0;
			rect.y = _trackOffsetY + TimelineRectConst.Track_Height * trackIndex;
			rect.height = TimelineRectConst.Track_Height;
			rect.width = _viewRect.width;
			return rect;
		}
	}
}