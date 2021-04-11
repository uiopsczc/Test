using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
  public partial class UIObject
  {
    private List<UGUIEventListener> registered_uguiEventListener_list = new List<UGUIEventListener>();

    protected void SaveRegisteredUGUIEventListener(UGUIEventListener uguiEventListener)
    {
      if (registered_uguiEventListener_list.Contains(uguiEventListener))
        return;
      registered_uguiEventListener_list.Add(uguiEventListener);
    }

    //////////////////////////////////////////////////////////////////////
    // OnClick
    //////////////////////////////////////////////////////////////////////
    public Action<GameObject, PointerEventData> RegisterOnClick(Component component, Action action,
      string sound_path = null)
    {
      return RegisterOnClick(component.gameObject, action, sound_path);
    }

    public Action<GameObject, PointerEventData> RegisterOnClick(GameObject gameObject, Action action,
      string sound_path = null)
    {
      Button button = gameObject.GetComponent<Button>();
      Action<GameObject, PointerEventData> result = (go, eventData) =>
      {
        if (!sound_path.IsNullOrWhiteSpace())
          Client.instance.audioManager.PlayUISound(sound_path);
        action();
      };
      UGUIEventListener.Get(button).onClick += result;
      SaveRegisteredUGUIEventListener(gameObject.GetComponent<UGUIEventListener>());
      return result;
    }

    public void UnRegisterOnClick(Component component, Action<GameObject, BaseEventData> action)
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

    public void UnRegisterOnDrag(Component component, Action<GameObject, BaseEventData> action)
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

    public void UnRegisterOnPointerDown(Component component, Action<GameObject, BaseEventData> action)
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

    public void UnRegisterOnPointerUp(Component component, Action<GameObject, BaseEventData> action)
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
    public void UnRegister(Component component)
    {
      UnRegister(component.gameObject);
    }

    public void UnRegister(GameObject gameObject)
    {
      UGUIEventListener.RemoveAllListener(gameObject);
    }
  }
}



