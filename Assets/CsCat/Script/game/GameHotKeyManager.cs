using System;
using System.Collections.Generic;
using System.Text;
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

        public string KK()
        {
            using (var scope = PoolCatManagerUtil.SpawnScope<StringBuilderScope>(s => s.Init(5)))
            {
                scope.stringBuilder.Append("4566");
                return scope.stringBuilder.ToString();
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
                
                //                string[] ss = {"a", "b", "a", "d"};
                //                string[] ss2 = { "e", "g", "f"};
                string[] _;
//                List<string> list = ss.ToList();
                int[] ss = {1,3,4,2,7,5,9,0,8,6};
                var list = ss.ToList();
                list.QuickSortWithCompareRules((a,b)=> a - b);
				LogCat.warn(list);
//                LogCat.warn(list);
//                (_,ss) = ss.RemoveRangeAt2(3,1);
//                LogCat.warn(_);
//                LogCat.warn(ss);
                //        LogCat.log(Lang.GetText("陈智权"));
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
                LogCat.warn(PoolCatManagerUtil.GetPool<StringBuilderScope>().GetSpawnedCount());
                LogCat.warn(PoolCatManagerUtil.GetPool<StringBuilderScope>().GetDespawnedCount());
                //        LogCat.warn(3333);
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