using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CsCat
{
	public class UGUIEventListener : MonoBehaviour,
	  IPointerClickHandler,
	  IPointerDownHandler,
	  IPointerUpHandler,
	  IPointerEnterHandler,
	  IPointerExitHandler,
	  ISubmitHandler,
	  ICancelHandler,
	  IMoveHandler,
	  IDragHandler,
	  IInitializePotentialDragHandler,
	  IBeginDragHandler,
	  IEndDragHandler,
	  IDropHandler,
	  ISelectHandler,
	  IDeselectHandler,
	  IUpdateSelectedHandler,
	  IScrollHandler
	{
		private bool is_down;
		private readonly bool is_long_press_repeat = false; //是否repeate
		private PointerEventData long_press_eventData;
		private readonly float long_press_trigger_period = 1; //长按周期
		private float long_press_trigger_remain_duration; // 长按触发剩余时间
		public Action<GameObject, PointerEventData> onBeforeBeginDrag;
		public Action<GameObject, PointerEventData> onBeginDrag;
		public Action<GameObject, BaseEventData> onCancel;
		public Action<GameObject, PointerEventData> onClick;
		public Action<GameObject, BaseEventData> onDeselect;
		public Action<GameObject, PointerEventData> onPointerDown;

		public Action<GameObject, PointerEventData> onDrag;
		public Action<GameObject, PointerEventData> onDrop;
		public Action<GameObject, PointerEventData> onEndDrag;
		public Action<GameObject, PointerEventData> onPointerEnter;
		public Action<GameObject, PointerEventData> onPointerExit;

		public Action<GameObject, PointerEventData> onLongPress;
		public Action<GameObject, AxisEventData> onMove;

		public Action<GameObject, BaseEventData> onScroll;

		public Action<GameObject, BaseEventData> onSelect;

		public Action<GameObject, BaseEventData> onSubmit;
		public Action<GameObject, PointerEventData> onPointerUp;
		public Action<GameObject, BaseEventData> onUpdateSelected;

		public void OnBeginDrag(PointerEventData eventData)
		{
			onBeginDrag?.Invoke(gameObject, eventData);
		}

		public void OnCancel(BaseEventData eventData)
		{
			onCancel?.Invoke(gameObject, eventData);
		}

		public void OnDeselect(BaseEventData eventData)
		{
			onDeselect?.Invoke(gameObject, eventData);
		}


		public void OnDrag(PointerEventData eventData)
		{
			onDrag?.Invoke(gameObject, eventData);
		}

		public void OnDrop(PointerEventData eventData)
		{
			onDrop?.Invoke(gameObject, eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			onEndDrag?.Invoke(gameObject, eventData);
		}

		public void OnInitializePotentialDrag(PointerEventData eventData)
		{
			onBeforeBeginDrag?.Invoke(gameObject, eventData);
		}

		public void OnMove(AxisEventData eventData)
		{
			onMove?.Invoke(gameObject, eventData);
		}


		public void OnPointerClick(PointerEventData eventData)
		{
			onClick?.Invoke(gameObject, eventData);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			onPointerDown?.Invoke(gameObject, eventData);
			HandleLongPressDown(eventData);
			is_down = true;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			onPointerEnter?.Invoke(gameObject, eventData);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			onPointerExit?.Invoke(gameObject, eventData);
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			onPointerUp?.Invoke(gameObject, eventData);
			is_down = false;
		}


		public void OnScroll(PointerEventData eventData)
		{
			onScroll?.Invoke(gameObject, eventData);
		}


		public void OnSelect(BaseEventData eventData)
		{
			onSelect?.Invoke(gameObject, eventData);
		}


		public void OnSubmit(BaseEventData eventData)
		{
			onSubmit?.Invoke(gameObject, eventData);
		}

		public void OnUpdateSelected(BaseEventData eventData)
		{
			onUpdateSelected?.Invoke(gameObject, eventData);
		}


		private void HandleLongPressDown(PointerEventData eventData)
		{
			if (onLongPress == null)
				return;
			if (!is_down)
				long_press_trigger_remain_duration = long_press_trigger_period;
			long_press_trigger_remain_duration -= Time.deltaTime;
			if (!(long_press_trigger_remain_duration < 0)) return;
			onLongPress(gameObject, long_press_eventData);
			long_press_trigger_remain_duration = is_long_press_repeat ? long_press_trigger_period : float.MaxValue;
		}


		public static UGUIEventListener Get(GameObject gameObject)
		{
			return gameObject.GetOrAddComponent<UGUIEventListener>();
		}

		public static UGUIEventListener Get<T>(T component) where T : Component
		{
			return Get(component.gameObject);
		}

		public static void RemoveAllListener(GameObject gameObject)
		{
			var listener = Get(gameObject);
			listener.onClick = null;
			listener.onPointerDown = null;
			listener.onPointerUp = null;
			listener.onPointerEnter = null;
			listener.onPointerExit = null;

			listener.onSubmit = null;
			listener.onCancel = null;
			listener.onMove = null;

			listener.onDrag = null;
			listener.onBeforeBeginDrag = null;
			listener.onBeginDrag = null;
			listener.onEndDrag = null;
			listener.onDrag = null;

			listener.onSelect = null;
			listener.onDeselect = null;
			listener.onUpdateSelected = null;

			listener.onScroll = null;

			listener.onLongPress = null;
		}

		public static void RemoveAllListener(Component component)
		{
			RemoveAllListener(component.gameObject);
		}

		public static void RemoveListener(GameObject gameObject, Action<GameObject, BaseEventData> action,
		  string action_type = null)
		{
			var listener = Get(gameObject);
			switch (action_type)
			{
				case null:
					listener.onClick -= action;
					listener.onPointerDown -= action;
					listener.onPointerUp -= action;
					listener.onPointerEnter -= action;
					listener.onPointerExit -= action;

					listener.onSubmit -= action;
					listener.onCancel -= action;
					listener.onMove -= action;

					listener.onDrag -= action;
					listener.onBeforeBeginDrag -= action;
					listener.onBeginDrag -= action;
					listener.onEndDrag -= action;
					listener.onDrag -= action;

					listener.onSelect -= action;
					listener.onDeselect -= action;
					listener.onUpdateSelected -= action;

					listener.onScroll -= action;

					listener.onLongPress -= action;
					break;

				case "onClick":
					listener.onClick -= action;
					break;
				case "onPointerDown":
					listener.onPointerDown -= action;
					break;
				case "onPointerUp":
					listener.onPointerUp -= action;
					break;
				case "onPointerEnter":
					listener.onPointerEnter -= action;
					break;
				case "onPointerExit":
					listener.onPointerExit -= action;
					break;

				case "onSubmit":
					listener.onSubmit -= action;
					break;
				case "onCancel":
					listener.onCancel -= action;
					break;
				case "onMove":
					listener.onMove -= action;
					break;

				case "onDrag":
					listener.onDrag -= action;
					break;
				case "onBeforeBeginDrag":
					listener.onBeforeBeginDrag -= action;
					break;
				case "onBeginDrag":
					listener.onBeginDrag -= action;
					break;
				case "onEndDrag":
					listener.onEndDrag -= action;
					break;
				case "onDrop":
					listener.onDrop -= action;
					break;


				case "onSelect":
					listener.onSelect -= action;
					break;
				case "onDeselect":
					listener.onDeselect -= action;
					break;
				case "onUpdateSelected":
					listener.onUpdateSelected -= action;
					break;
				case "onScroll":
					listener.onScroll -= action;
					break;

				case "onLongPress":
					listener.onLongPress -= action;
					break;
			}
		}
	}
}