using System;
using System.Collections.Generic;

namespace CsCat
{
  public class DoerEventFactory : DoerFactory
  {
    protected override string default_doer_class_path
    {
      get { return "CsCat.DoerEvent"; }
    }

    private Dictionary<string, DoerEvent> doerEvent_dict = new Dictionary<string, DoerEvent>();

    public override ExcelAssetBase GetDefinitions()
    {
      return DefinitionManager.instance.doerEventDefinition;
    }

    public override ExcelAssetBase GetDefinition(string id)
    {
      return GetDefinitions().GetData<DoerEventDefinition>(id);
    }

    public DoerEventDefinition GetDoerEventDefinition(string id)
    {
      return GetDefinition(id) as DoerEventDefinition;
    }

    protected override DBase __NewDBase(string id_or_rid)
    {
      return new DoerEventDBase(id_or_rid);
    }

    //////////////////////////////////////////////////////////////////////////
    public override void Init()
    {
      base.Init();
      var definitions = GetDefinitions() as DoerEventDefinition;
      foreach (var id in definitions.GetIdList())
        LoadDoerEvent(id);
    }

    private void LoadDoerEvent(string id)
    {
      var definition = this.GetDoerEventDefinition(id);
      var class_path = definition.class_path_cs.IsNullOrWhiteSpace() ? default_doer_class_path : definition.class_path_cs;
      Type type = TypeUtil.GetType(class_path);
      DoerEvent doerEvent = this.AddChildWithoutInit(null, type) as DoerEvent;
      DBase doerEventDBase = this.__NewDBase(id);
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