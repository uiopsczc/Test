using System;
using System.Collections.Generic;

namespace CsCat
{
	public class DoerEventFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.DoerEvent";

		private Dictionary<string, DoerEvent> doerEventDict = new Dictionary<string, DoerEvent>();

		protected override string GetClassPath(string id)
		{
			return this.GetCfgDoerEventData(id).classPathCS.IsNullOrWhiteSpace()
				? base.GetClassPath(id)
				: GetCfgDoerEventData(id).classPathCS;
		}

		public CfgDoerEventData GetCfgDoerEventData(string id)
		{
			return CfgDoerEvent.Instance.get_by_id(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new DoerEventDBase(idOrRid);
		}

		//////////////////////////////////////////////////////////////////////////
		public override void Init()
		{
			base.Init();
			foreach (var cfgDoerEventData in CfgDoerEvent.Instance.All())
				LoadDoerEvent(cfgDoerEventData.id);
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
			doerEventDict[id] = doerEvent;
		}

		public DoerEvent GetDoerEvent(string id)
		{
			return doerEventDict.ContainsKey(id) ? this.doerEventDict[id] : null;
		}
	}
}