/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using System;
using UnityEngine;
using XLua;

namespace CsCat
{
	public class LuaBehaviour : MonoBehaviour
	{
		public TextAsset luaFile;
		private Action luaAwake;
		private Action luaOnDestroy;
		private Action luaStart;
		private Action luaUpdate;
		private LuaTable scriptEnv;

		private void Awake()
		{
			scriptEnv = XLuaManager.instance.luaEnv.NewScriptEnv(this, luaFile.text);
			InitScriptEnv();
			luaAwake?.Invoke();
		}

		private void InitScriptEnv()
		{
			scriptEnv.Get("Awake", out luaAwake);
			scriptEnv.Get("Start", out luaStart);
			scriptEnv.Get("Update", out luaUpdate);
			scriptEnv.Get("OnDestroy", out luaOnDestroy);
		}

		private void ResetScriptEnv()
		{
			luaAwake = null;
			luaStart = null;
			luaUpdate = null;
			luaOnDestroy = null;
			scriptEnv.Dispose();
		}

		private void Start()
		{
			luaStart?.Invoke();
		}

		private void Update()
		{
			luaUpdate?.Invoke();
		}

		private void OnDestroy()
		{
			luaOnDestroy?.Invoke();
			ResetScriptEnv();
		}
	}
}