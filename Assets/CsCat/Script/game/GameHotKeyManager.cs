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


		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
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
			}

			if (Input.GetKeyDown("f2"))
			{
			}

			if (Input.GetKeyDown("f3"))
			{
			}

			if (Input.GetKeyDown("f4"))
			{
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