using System;
using System.Collections.Generic;

namespace CsCat
{
	public class ResLoadDataInfo
	{
		public ResLoadData resLoadData;
		public Dictionary<object, bool> callbackCauseDict = new Dictionary<object, bool>();
		private bool isNotCheckDestroy;

		public ResLoadDataInfo(ResLoadData resLoadData, bool isNotCheckDestroy)
		{
			this.resLoadData = resLoadData;
			this.isNotCheckDestroy = isNotCheckDestroy;
		}

		public void AddCallbackCause(object callbackCause)
		{
			if (this.callbackCauseDict.ContainsKey(callbackCause.GetNotNullKey())) //不重复
				return;
			this.callbackCauseDict[callbackCause] = true;
		}

		//callback_cause==null时是全部删除
		public void RemoveCallbackCause(object callbackCause)
		{
			this.callbackCauseDict.Remove(callbackCause.GetNotNullKey());
			this.resLoadData.assetCat.RemoveCallback(callbackCause.GetNullableKey());
			if (!isNotCheckDestroy)
				CheckDestroy();
		}

		public void RemoveAllCallbackCauses()
		{
			foreach (var keyValue in callbackCauseDict)
			{
				var callbackCause = keyValue.Key;
				this.resLoadData.assetCat.RemoveCallback(callbackCause.GetNullableKey());
			}

			this.callbackCauseDict.Clear();
			if (!isNotCheckDestroy)
				CheckDestroy();
		}


		void CheckDestroy()
		{
			if (this.callbackCauseDict.Count == 0)
				resLoadData.Destroy();
		}

		public void Destroy()
		{
			foreach (var keyValue in callbackCauseDict)
			{
				var callbackCause = keyValue.Key;
				this.resLoadData.assetCat.RemoveCallback(callbackCause.GetNullableKey());
			}
			this.callbackCauseDict.Clear();
			this.resLoadData.Destroy();
		}
	}
}