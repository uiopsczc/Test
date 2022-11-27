using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIObject
	{
		private readonly List<UGUIEventListener> _registeredUGUIEventListenerList = new List<UGUIEventListener>();

		protected void SaveRegisteredUGUIEventListener(UGUIEventListener uguiEventListener)
		{
			if (_registeredUGUIEventListenerList.Contains(uguiEventListener))
				return;
			_registeredUGUIEventListenerList.Add(uguiEventListener);
		}

		//////////////////////////////////////////////////////////////////////
		// OnClick
		//////////////////////////////////////////////////////////////////////
		public Action<GameObject, PointerEventData> RegisterOnClick(UnityEngine.Component component, Action action,
		  string soundPath = null)
		{
			return RegisterOnClick(component.gameObject, action, soundPath);
		}

		public Action<GameObject, PointerEventData> RegisterOnClick(GameObject gameObject, Action action,
		  string soundPath = null)
		{
			Button button = gameObject.GetComponent<Button>();
			Action<GameObject, PointerEventData> result = (go, eventData) =>
			{
				if (!soundPath.IsNullOrWhiteSpace())
					Client.instance.audioManager.PlayUISound(soundPath);
				action();
			};
			UGUIEventListener.Get(button).onClick += result;
			SaveRegisteredUGUIEventListener(gameObject.GetComponent<UGUIEventListener>());
			return result;
		}

		public void UnRegisterOnClick(UnityEngine.Component component, Action<GameObject, BaseEventData> action)
		{
			UnRegisterOnClick(component.gameObject, action);
		}

		public void UnRegisterOnClick(GameObject gameObject, Action<GameObject, BaseEventData> action)
		{
			UGUIEventListener.RemoveListener(gameObject, action, "onClick");
		}

		//////////////////////////////////////////////////////////////////////
		// OnDrag
		//////////////////////////////////////////////////////////////////////
		public Action<GameObject, PointerEventData> RegisterOnDrag(GameObject gameObject, Action<PointerEventData> action)
		{
			Action<GameObject, PointerEventData> result = (go, eventData) => { action(eventData); };
			UGUIEventListener.Get(gameObject).onDrag += result;
			SaveRegisteredUGUIEventListener(gameObject.GetComponent<UGUIEventListener>());
			return result;
		}

		public void UnRegisterOnDrag(UnityEngine.Component component, Action<GameObject, BaseEventData> action)
		{
			UnRegisterOnDrag(component.gameObject, action);
		}

		public void UnRegisterOnDrag(GameObject gameObject, Action<GameObject, BaseEventData> action)
		{
			UGUIEventListener.RemoveListener(gameObject, action, "onDrag");
		}

		//////////////////////////////////////////////////////////////////////
		// OnPointerDown
		//////////////////////////////////////////////////////////////////////
		public Action<GameObject, PointerEventData> RegisterOnPointerDown(GameObject gameObject,
		  Action<PointerEventData> action)
		{
			Action<GameObject, PointerEventData> result = (go, eventData) => { action(eventData); };
			UGUIEventListener.Get(gameObject).onPointerDown += result;
			SaveRegisteredUGUIEventListener(gameObject.GetComponent<UGUIEventListener>());
			return result;
		}

		public void UnRegisterOnPointerDown(UnityEngine.Component component, Action<GameObject, BaseEventData> action)
		{
			UnRegisterOnPointerDown(component.gameObject, action);
		}

		public void UnRegisterOnPointerDown(GameObject gameObject, Action<GameObject, BaseEventData> action)
		{
			UGUIEventListener.RemoveListener(gameObject, action, "onPointerDown");
		}

		//////////////////////////////////////////////////////////////////////
		// OnPointerUp
		//////////////////////////////////////////////////////////////////////
		public Action<GameObject, PointerEventData> RegisterOnPointerUp(GameObject gameObject,
		  Action<PointerEventData> action)
		{
			Action<GameObject, PointerEventData> result = (go, eventData) => { action(eventData); };
			UGUIEventListener.Get(gameObject).onPointerUp += result;
			return result;
		}

		public void UnRegisterOnPointerUp(UnityEngine.Component component, Action<GameObject, BaseEventData> action)
		{
			UnRegisterOnPointerUp(component.gameObject, action);
		}

		public void UnRegisterOnPointerUp(GameObject gameObject, Action<GameObject, BaseEventData> action)
		{
			UGUIEventListener.RemoveListener(gameObject, action, "onPointerUp");
		}

		//////////////////////////////////////////////////////////////////////
		// UnRegister
		//////////////////////////////////////////////////////////////////////
		public void UnRegister(UnityEngine.Component component)
		{
			UnRegister(component.gameObject);
		}

		public void UnRegister(GameObject gameObject)
		{
			UGUIEventListener.RemoveAllListener(gameObject);
		}
	}
}



