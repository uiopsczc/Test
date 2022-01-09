using System;
using UnityEngine;
using XLua;

namespace CsCat
{
	// 是在Lua/luacat/common/event.lua.txt 中的 Update,LateUpdate,FixedUpdate定义的
	// 具体使用在Lua/luacat/updateManager/UpdateManager.lua.txt
	public class XLuaUpdater : MonoBehaviour
	{
		private Action<float, float> luaFixedUpdate;
		private Action luaLateUpdate;
		private Action<float, float> luaUpdate;


		public void OnInit(LuaEnv luaEnv)
		{
			Restart(luaEnv);
		}

		public void Restart(LuaEnv luaEnv)
		{
			luaUpdate = luaEnv.Global.Get<Action<float, float>>("Update");
			luaLateUpdate = luaEnv.Global.Get<Action>("LateUpdate");
			luaFixedUpdate = luaEnv.Global.Get<Action<float, float>>("FixedUpdate");
		}

		private void Update()
		{
			if (luaUpdate != null)
				try
				{
					luaUpdate(Time.deltaTime, Time.unscaledDeltaTime);
				}
				catch (Exception ex)
				{
					LogCat.LogError(string.Format("luaUpdate err : {0}\n{1}", ex.Message, ex.StackTrace));
				}
		}

		private void LateUpdate()
		{
			if (luaLateUpdate != null)
				try
				{
					luaLateUpdate();
				}
				catch (Exception ex)
				{
					LogCat.LogError(string.Format("luaLateUpdate err : {0}\n{1}", ex.Message, ex.StackTrace));
				}
		}

		private void FixedUpdate()
		{
			if (luaFixedUpdate != null)
				try
				{
					luaFixedUpdate(Time.fixedDeltaTime, Time.fixedUnscaledDeltaTime);
				}
				catch (Exception ex)
				{
					LogCat.LogError(string.Format("luaFixedUpdate err : {0}\n{1}", ex.Message, ex.StackTrace));
				}
		}

		private void OnDestroy()
		{
			OnDispose();
		}

		public void OnDispose()
		{
			luaUpdate = null;
			luaLateUpdate = null;
			luaFixedUpdate = null;
		}
	}
}