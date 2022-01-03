using System;
using System.Collections.Generic;

namespace CsCat
{
	public class DoerEventFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.DoerEvent";

		private Dictionary<string, DoerEvent> doerEvent_dict = new Dictionary<string, DoerEvent>();

		protected override string GetClassPath(string id)
		{
			return this.GetCfgDoerEventData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgDoerEventData(id).class_path_cs;
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
			var class_path = GetClassPath(id);
			Type type = TypeUtil.GetType(class_path);
			DoerEvent doerEvent = this.AddChildWithoutInit(null, type) as DoerEvent;
			DBase doerEventDBase = this._NewDBase(id);
			doerEvent.factory = this;
			doerEvent.SetDBase(doerEventDBase);
			doerEventDBase.SetDoer(doerEvent);
			doerEvent.Init();
			doerEvent.PostInit();
			doerEvent.SetIsEnabled(true, false);
			doerEvent_dict[id] = doerEvent;
		}

		public DoerEvent GetDoerEvent(string id)
		{
			if (doerEvent_dict.ContainsKey(id))
				return this.doerEvent_dict[id];
			else
				return null;
		}
	}
}