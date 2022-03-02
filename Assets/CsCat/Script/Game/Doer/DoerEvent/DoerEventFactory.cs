using System;
using System.Collections.Generic;

namespace CsCat
{
	public class DoerEventFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.DoerEvent";

		private readonly Dictionary<string, DoerEvent> _doerEventDict = new Dictionary<string, DoerEvent>();

		protected override string GetClassPath(string id)
		{
			return this.GetCfgDoerEventData(id).classPathCs.IsNullOrWhiteSpace()
				? base.GetClassPath(id)
				: GetCfgDoerEventData(id).classPathCs;
		}

		public CfgDoerEventData GetCfgDoerEventData(string id)
		{
			return CfgDoerEvent.Instance.GetById(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new DoerEventDBase(idOrRid);
		}

		//////////////////////////////////////////////////////////////////////////
		public override void Init()
		{
			base.Init();
			var cfgDoerEventDataList = CfgDoerEvent.Instance.All();
			for (var i = 0; i < cfgDoerEventDataList.Count; i++)
			{
				var cfgDoerEventData = cfgDoerEventDataList[i];
				LoadDoerEvent(cfgDoerEventData.id);
			}
		}

		private void LoadDoerEvent(string id)
		{
			var classPath = GetClassPath(id);
			Type type = TypeUtil.GetType(classPath);
			DoerEvent doerEvent = this.AddChildWithoutInit(null, type) as DoerEvent;
			DBase doerEventDBase = this._NewDBase(id);
			doerEvent.factory = this;
			doerEvent.SetDBase(doerEventDBase);
			doerEventDBase.SetDoer(doerEvent);
			doerEvent.Init();
			doerEvent.PostInit();
			doerEvent.SetIsEnabled(true, false);
			_doerEventDict[id] = doerEvent;
		}

		public DoerEvent GetDoerEvent(string id)
		{
			return _doerEventDict.ContainsKey(id) ? this._doerEventDict[id] : null;
		}
	}
}