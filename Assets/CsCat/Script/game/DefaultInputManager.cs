using System;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public class DefaultInputManager : TickObject
  {
    public Action gui_callback;

    private HFSMComponent<TestCoroutineHFSM> hfsmComponent;
    public override void Init()
    {
      base.Init();
      //      TestCoroutineHFSM hfsm = new TestCoroutineHFSM(this);
      //      hfsm.Init();
      //      hfsmComponent = this.AddComponent<HFSMComponent<TestCoroutineHFSM>>("HFSMComponent", hfsm);
      //      hfsmComponent.hfsm.Start();
    }

    private Dictionary<EventName, string> dict = new Dictionary<EventName, string>();

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      HandleMouseEvent();
      HandleKeyInputEvent();
    }

    private static int i;
    private UIGMTestPanel2 panel;
    private void HandleMouseEvent()
    {
      if (Input.GetMouseButtonDown(0))
      {
      }

      if (Input.GetMouseButtonUp(0))
      {
      }
    }

    private void HandleKeyInputEvent()
    {
      if (Input.GetKeyDown("f1"))
      {
        Client.instance.Goto<StageTest>(0.5f);
      }

      if (Input.GetKeyDown("f2"))
      {
        //        panel = Client.instance.uiManager.CreateChildPanel(null, default(UIGMTestPanel2));
        LogCat.warn(Selection.activeTransform.GetRelativePath(GameObject.Find("EffectPool").transform));
      }

      if (Input.GetKeyDown("f3"))
      {
        panel.SetToBottom();
        //        LogCat.log(dict);
      }

      if (Input.GetKeyDown("f4"))
      {
        panel.SetToTop();
        //        PoolCatManagerUtil.GetPool<GameObject>().Trim();
        //        UIBloodTest.Test4();
      }

      if (Input.GetKeyDown("f5"))
      {
      }

      if (Input.GetKeyDown("f6"))
      {
        Client.instance.Rebort();
      }

      if (Input.GetKeyDown("f7"))
      {
      }

      if (Input.GetKeyDown("f8"))
      {
      }

      if (Input.GetKeyDown("f9"))
      {
      }

      if (Input.GetKeyDown("f10"))
      {
      }

      if (Input.GetKeyDown("f11"))
      {
      }

      if (Input.GetKeyDown("f12"))
      {
      }
    }
  }
}