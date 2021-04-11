using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public partial class SpellBase
  {
    //起手特效
    public List<string> CreateHandEffect(float duration)
    {
      if (this.spellDefinition.hand_effect_ids.IsNullOrEmpty())
        return null;
      var effect_ids = this.spellDefinition.hand_effect_ids;
      var guid_list = new List<string>();
      foreach (var effect_id in effect_ids)
      {
        var effect = Client.instance.combat.effectManager.CreateAttachEffectEntity(effect_id, this.source_unit, duration);
        guid_list.Add(effect.GetGuid());
      }

      return guid_list;
    }

    public List<string> CreateGoEffect(float duration)
    {
      if (this.spellDefinition.go_effect_ids.IsNullOrEmpty())
        return null;
      var effect_ids = this.spellDefinition.go_effect_ids;
      var guid_list = new List<string>();
      foreach (var effect_id in effect_ids)
      {
        var effect = Client.instance.combat.effectManager.CreateAttachEffectEntity(effect_id, this.source_unit, duration);
        guid_list.Add(effect.GetGuid());
      }

      return guid_list;
    }

    //击中特效
    public List<string> CreateHitEffect(Unit source_unit, Unit target_unit, float? duration = null,
      float sector_angle = 0, List<string> force_effect_id_list = null)
    {
      if (this.spellDefinition.hit_effect_ids.IsNullOrEmpty() && force_effect_id_list.IsNullOrEmpty())
        return null;
      Vector3? force_dir = null;
      if (source_unit != null)
      {
        var force_rotation = Quaternion.LookRotation(target_unit.GetPosition() - source_unit.GetPosition());
        if (!force_rotation.IsZero())
          force_dir = force_rotation.eulerAngles;
      }

      var effect_ids = force_effect_id_list != null
        ? force_effect_id_list.ToArray()
        : this.spellDefinition.hit_effect_ids;
      var guid_list = new List<string>();
      foreach (var effect_id in effect_ids)
      {
        var effect =
          Client.instance.combat.effectManager.CreateAttachEffectEntity(effect_id, this.source_unit, duration, force_dir,
            sector_angle);
        guid_list.Add(effect.GetGuid());
      }

      return guid_list;
    }

    //地面特效
    public List<string> CreateGroundEffect(Vector3? position, Vector3? eulerAngles, float duration,
      List<string> force_effect_id_list = null, Vector3? force_position = null, bool is_hide = false)
    {
      if (this.spellDefinition.ground_effect_ids.IsNullOrEmpty() && force_effect_id_list.IsNullOrEmpty())
        return null;
      Vector3 _position = force_position.GetValueOrDefault(position.GetValueOrDefault(this.source_unit.GetPosition()));
      Vector3 _eulerAngles =
        eulerAngles.GetValueOrDefault(Quaternion.LookRotation(_position - this.source_unit.GetPosition()).GetNotZero()
          .eulerAngles);
      var effect_ids = force_effect_id_list != null
        ? force_effect_id_list.ToArray()
        : this.spellDefinition.ground_effect_ids;
      var guid_list = new List<string>();
      if (force_position == null)
        _position = Client.instance.combat.pathManager.GetGroundPos(_position);

      foreach (var effect_id in effect_ids)
      {
        var effect = Client.instance.combat.effectManager.CreateGroundEffectEntity(effect_id, this.source_unit, _position,
          _eulerAngles, duration, is_hide);
        guid_list.Add(effect.GetGuid());
      }

      return guid_list;
    }

    //line特效
    public List<string> CreateLineEffect(IPosition target_iposition, float speed, float acc_speed)
    {
      if (this.spellDefinition.line_effect_ids.IsNullOrEmpty())
        return null;
      if (target_iposition == null || !target_iposition.IsValid())
        return null;
      var effect_ids = this.spellDefinition.line_effect_ids;
      var guid_list = new List<string>();
      foreach (var effect_id in effect_ids)
      {
        var effect = Client.instance.combat.effectManager.CreateLineEffectEntity(effect_id, this.source_unit,
          this.source_unit.ToUnitPosition(), target_iposition, speed, acc_speed);
        guid_list.Add(effect.GetGuid());
      }

      return guid_list;
    }


    public void RemoveEffect(List<string> effect_guid_list)
    {
      if (effect_guid_list.IsNullOrEmpty())
        return;
      foreach (var effect_guid in effect_guid_list)
        RemoveEffect(effect_guid);
    }

    public void RemoveEffect(string effect_guid)
    {
      Client.instance.combat.effectManager.RemoveEffectEntity(effect_guid);
    }
  }
}