using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class AnimEvent : MonoBehaviour
  {
    private static readonly AnimationEvent[] empty_event = new AnimationEvent[0];
    private static readonly string on_trigger_string = "OnTrigger";
    /// <summary>
    ///   每个切片上的所有事件(key:该Animator上的ClipName)
    /// </summary>
    private readonly Dictionary<string, List<string>> clip_event_dict = new Dictionary<string, List<string>>();
    /// <summary>
    ///   每个事件对应回调(key:clipName + percentage)
    /// </summary>
    private readonly Dictionary<string, List<DelegateStruct>> event_callback_dict =
      new Dictionary<string, List<DelegateStruct>>();


    private Animator animator => GetComponent<Animator>();
    private AnimationClip[] clips => animator.runtimeAnimatorController.animationClips;


    #region private method

    private void OnTrigger(string event_key)
    {
      if (!event_callback_dict.ContainsKey(event_key))
      {
        LogCat.LogWarningFormat("{0}:不存在eventCallbacks {1}", GetType().Name, event_key);
        return;
      }

      foreach (var callback_struct in event_callback_dict[event_key])
        callback_struct.Call();
    }

    #endregion

    #region public method

    public string GetEventKey(string clip_name, float percentage)
    {
      return string.Format("{0}_{1}", clip_name, percentage);
    }

    #region add

    //只添加一个AnimationEvent到AnimationClip指定时间的上，然后触发的时候，调用strOnComplete方法（里面会调用eventCallbacks对应的方法（key：clipName + percentage）触发callBackList)
    //如何将Delegate作为参数   使用lamba表达式,()=>{} 作为参数要将该delegate设置为Callback等，如 (Action)((args)=>{LogCat.Log(args);})
    //例子: AddEvents((Action<string, string>)((a, b) => { LogCat.LogWarning(a+b);}), "TestAnimationClip", 1f,"aabbcc","ddff");
    public AnimEvent AddEvents(Delegate callback, string clip_name, float percentage, params object[] callback_args)
    {
      clip_event_dict.GetOrAddDefault(clip_name, () => new List<string>());

      var event_key = GetEventKey(clip_name, percentage);
      var has_event_callback = event_callback_dict.ContainsKey(event_key);
      event_callback_dict.GetOrAddDefault(event_key, () => new List<DelegateStruct>());
      event_callback_dict[event_key].Add(new DelegateStruct(callback, callback_args));
      if (clip_event_dict[clip_name].FindIndex(a => a.Equals(event_key)) == -1)
        clip_event_dict[clip_name].Add(event_key);

      LogCat.LogWarning(clip_event_dict[clip_name].Count);

      foreach (var clip in clips)
      {
        if (!clip.name.Equals(clip_name)) continue;
        if (has_event_callback) continue;
        var animationEvent = new AnimationEvent();
        animationEvent.functionName = on_trigger_string;
        animationEvent.messageOptions = SendMessageOptions.RequireReceiver;
        animationEvent.time = clip.length * percentage;
        animationEvent.stringParameter = event_key;
        clip.AddEvent(animationEvent);
      }

      return this;
    }

    public AnimEvent AddEvents(Action callback, string clip_name, float percentage)
    {
      return AddEvents((Delegate)callback, clip_name, percentage);
    }

    #endregion

    #region replace

    public AnimEvent ReplaceEvents(Delegate callback, string clip_name, float percentage, params object[] callback_args)
    {
      var event_key = GetEventKey(clip_name, percentage);
      if (event_callback_dict.ContainsKey(event_key))
        event_callback_dict[event_key].Clear(); //确保只有一个
      return AddEvents(callback, clip_name, percentage, callback_args);
    }

    public AnimEvent ReplaceEvents(Action callback, string clip_name, float percentage)
    {
      return ReplaceEvents((Delegate)callback, clip_name, percentage);
    }

    #endregion

    #region remove

    public AnimEvent RemoveClipEvents(string clip_name)
    {
      if (clips != null)
        foreach (var clip in clips)
        {
          if (clip == null) continue;
          if (!clip.name.Equals(clip_name)) continue;
          clip.events = empty_event;
        }

      List<string> event_list = null;
      if (clip_event_dict.TryGetValue(clip_name, out event_list))
      {
        foreach (var event_name in event_list)
          event_callback_dict.Remove(event_name);

        clip_event_dict.Remove(clip_name);
      }

      return this;
    }

    public void RemoveAllEvent(bool destroy = false)
    {
      var clip_event_Keys = new List<string>(clip_event_dict.Keys);
      foreach (var clip_name in clip_event_Keys)
        RemoveClipEvents(clip_name);
      if (destroy) this.Destroy();
    }

    #endregion

    #endregion
  }
}