using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class GameHotKeyManager : TickObject
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
    private Timer t;
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
        LogCat.log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
//        Client.instance.Goto<StageTest>(0.5f);
      }

      if (Input.GetKeyDown("f2"))
      {
        LogCat.log(Lang.GetText("陈智权"));
        //        Dictionary<string, Dictionary<string, int>> dict = new Dictionary<string, Dictionary<string,int>>();
        //        var t = dict.GetType();
        //        LogCat.log(t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>));
        //        var t = CfgTest.Instance.get_by_id("2").get__age_dict();
        //        LogCat.log(CfgTest.Instance.get_by_id("2")._age_dict["星哥"][1]);
        //        t = this.AddTimer(args =>
        //        {
        //          LogCat.warn(Time.time);
        //          return true;
        //        }, 0, 1);
        //        panel = Client.instance.uiManager.CreateChildPanel(null, default(UIGMTestPanel2));
      }

      if (Input.GetKeyDown("f3"))
      {
        LogCat.warn(3333);
//        this.RemoveTimer(t);
//        panel.SetToBottom();
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