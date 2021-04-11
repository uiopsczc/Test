using System.Collections;
using System.Collections.Generic;

namespace CsCat
{
  public class Buff : TickObject
  {
    //因为有些buff可以同时存在多个，但效果只有一个生效
    private List<BuffCache> buffCache_list = new List<BuffCache>(); //效果不累加
    private BuffManager buffManager;
    public BuffDefinition buffDefinition;
    private List<EffectEntity> effect_list = new List<EffectEntity>(); // 一个buff可能有多个特效
    public string buff_id;
    private string trigger_spell_guid;

    public  void Init(BuffManager buffManager, string buff_id)
    {
      base.Init();
      this.buffManager = buffManager;
      this.buff_id = buff_id;

      buffDefinition = DefinitionManager.instance.buffDefinition.GetData(this.buff_id);

    }

    public Buff CreateBuffCache(float duration, Unit source_unit, SpellBase source_spell, Hashtable arg_dict)
    {
      var buffCache = new BuffCache(duration, source_unit, source_spell, arg_dict);
      buffCache_list.Add(buffCache);
      if (buffCache_list.Count == 1) //第一个的时候才添加
      {
        this.AddEffects();
        this.AddPropertyDict();
        this.AddTriggerSpell();
        this.buffManager.AddState(this.buffDefinition.state);
      }

      return this;
    }

    public void RemoveBuffCache(string source_unit_guid, string soruce_spell_guid)
    {
      for (int i = this.buffCache_list.Count - 1; i >= 0; i--)
      {
        var buffCache = this.buffCache_list[i];
        var is_this_unit = source_unit_guid.IsNullOrWhiteSpace() ||
                           (buffCache.source_unit != null && buffCache.source_unit.GetGuid().Equals(source_unit_guid));
        var is_this_spell = soruce_spell_guid.IsNullOrWhiteSpace() ||
                            (buffCache.source_spell != null &&
                             buffCache.source_spell.GetGuid().Equals(soruce_spell_guid));
        if (is_this_unit && is_this_spell)
          this.buffCache_list.RemoveAt(i);
      }

      if (this.buffCache_list.IsNullOrEmpty())
        this.buffManager.RemoveBuffByBuff(this);
    }


    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      for (var i = buffCache_list.Count - 1; i >= 0; i--)
      {
        var buffCache = buffCache_list[i];
        buffCache.remain_duration -= deltaTime;
        if (buffCache.remain_duration <= 0)
          buffCache_list.RemoveAt(i);
      }

      if (buffCache_list.Count == 0)
        buffManager.RemoveBuffByBuff(this);
    }


    private void AddEffects()
    {
      var effect_ids = buffDefinition.effect_ids;
      if (effect_ids.IsNullOrEmpty())
        return;
      foreach (var effect_id in effect_ids)
      {
        var effectDefinition = DefinitionManager.instance.effectDefinition.GetData(effect_id);
        var effect =
          Client.instance.combat.effectManager.CreateAttachEffectEntity(effect_id, this.buffManager.unit,
            effectDefinition.duration); //TODO 如何初始化effectBase
        effect_list.Add(effect);
      }
    }

    private void RemoveEffects()
    {
      foreach (var effect in effect_list)
        Client.instance.combat.effectManager.RemoveEffectEntity(effect.key);
    }

    private void AddPropertyDict()
    {
      var new_property_dict = DoerAttrParserUtil.ConvertTableWithTypeString(this.buffDefinition.property_dict)
        .ToDict<string, float>();
      if (!new_property_dict.IsNullOrEmpty())
      {
        var propertyComp = this.buffManager.unit.propertyComp;
        propertyComp.StartChange();
        propertyComp.AddPropSet(new_property_dict, "buff", this.GetGuid());
        propertyComp.EndChange();
      }
    }

    private void RemovePropertyDict()
    {
      var new_property_dict = DoerAttrParserUtil.ConvertTableWithTypeString(this.buffDefinition.property_dict)
        .ToDict<string, float>();
      if (!new_property_dict.IsNullOrEmpty())
      {
        var propertyComp = this.buffManager.unit.propertyComp;
        propertyComp.StartChange();
        propertyComp.RemovePropSet("buff", this.GetGuid());
        propertyComp.EndChange();
      }
    }

    private void AddTriggerSpell()
    {
      var trigger_spell_id = this.buffDefinition.trigger_spell_id;
      if (trigger_spell_id.IsNullOrWhiteSpace())
        return;
      var spell = Client.instance.combat.spellManager.CastSpell(
        this.buffCache_list[0].source_unit ?? this.buffManager.unit,
        trigger_spell_id, this.buffManager.unit);
      this.trigger_spell_guid = spell.GetGuid();
    }

    private void RemoveTriggerSpell()
    {
      if (!this.trigger_spell_guid.IsNullOrWhiteSpace())
      {
        var spell = Client.instance.combat.spellManager.GetSpell(this.trigger_spell_guid);
        if (spell == null)
          return;
        if (spell.IsHasMethod("OnBuffRemove"))
        {
          spell.InvokeMethod("OnBuffRemove", false, this);
          return;
        }

        Client.instance.combat.spellManager.RemoveSpell(this.trigger_spell_guid);
      }
    }

    protected override void __Destroy()
    {
      base.__Destroy();
      this.RemoveEffects();
      this.RemovePropertyDict();
      this.RemoveTriggerSpell();
      this.buffManager.RemoveState(this.buffDefinition.state);
    }
  }
}