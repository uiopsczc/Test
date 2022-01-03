namespace CsCat
{
	public partial class SpellManager : TickObject
	{


		public void RegisterListener(string type, Unit unit, object obj, string tag, MethodInvoker methodInvoker)
		{
			if (!this.listener_dict.ContainsKey(type))
			{
				LogCat.error("Register Listener with undefine type()!", type);
				return;
			}

			var spellListenerInfo = new SpellListenerInfo();
			spellListenerInfo.type = type;
			spellListenerInfo.unit = unit;
			spellListenerInfo.obj = obj;
			spellListenerInfo.tag = tag;
			spellListenerInfo.methodInvoker = methodInvoker;
			this.listener_dict[type].Add(spellListenerInfo);
		}

		public void UnRegisterListener(string type, Unit unit, object obj, string tag)
		{
			if (!this.listener_dict.ContainsKey(type))
			{
				LogCat.error("Unregister Listener with undefine type()!", type);
				return;
			}

			for (int i = this.listener_dict[type].Count - 1; i >= 0; i--)
			{
				var listenerInfo = this.listener_dict[type][i];
				if (listenerInfo.unit == unit && listenerInfo.obj == obj && ObjectUtil.Equals(listenerInfo.tag, tag))
				{
					this.listener_dict[type].RemoveAt(i);
					break;
				}
			}
		}

		public void RemoveListenersByObj(object obj)
		{
			foreach (var listenerInfo_list in this.listener_dict.Values)
			{
				for (int i = listenerInfo_list.Count - 1; i >= 0; i--)
				{
					if (ObjectUtil.Equals(listenerInfo_list[i].obj, obj))
						listenerInfo_list.RemoveAt(i);
				}
			}
		}

		public void ListenerCallback(SpellListenerInfo listener, Unit source_unit, params object[] args)
		{
			listener.methodInvoker.Invoke(args);
		}

		public void BeforeHit(Unit source_unit, Unit target_unit, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["before_hit"])
			{
				if (listenerInfo.unit == source_unit)
					this.ListenerCallback(listenerInfo, target_unit, args);
			}

			foreach (var listenerInfo in this.listener_dict["before_be_hit"])
			{
				if (listenerInfo.unit == target_unit)
					this.ListenerCallback(listenerInfo, target_unit, args);
			}
		}

		public void OnHit(Unit source_unit, Unit target_unit, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["be_hit"])
			{
				if (listenerInfo.unit == target_unit)
					this.ListenerCallback(listenerInfo, target_unit, spell, args);
			}

			//!注意：触发回调的过程中可能再次插入或者remove listener
			foreach (var listenerInfo in this.listener_dict["on_hit"])
			{
				if (listenerInfo.unit == source_unit)
					this.ListenerCallback(listenerInfo, target_unit, spell, args);
			}

			foreach (var listenerInfo in this.listener_dict["on_cur_spell_hit"])
			{
				if (listenerInfo.unit == source_unit && spell == listenerInfo.obj)
					this.ListenerCallback(listenerInfo, target_unit, spell, args);
			}

			if ("普攻".Equals(spell.cfgSpellData.type))
			{
				foreach (var listenerInfo in this.listener_dict["normal_attack"])
				{
					if (listenerInfo.unit == source_unit)
						this.ListenerCallback(listenerInfo, target_unit, spell, args);
				}
			}
		}


		public void OnKillTarget(Unit source_unit, Unit target_unit, SpellBase spell, params object[] args)
		{
			if (spell == null)
				return;
			foreach (var listenerInfo in this.listener_dict["on_kill_target"])
			{
				if (listenerInfo.unit == source_unit)
					this.ListenerCallback(listenerInfo, target_unit, spell, args);
			}
		}

		public void BeforeDead(Unit source_unit, Unit dead_unit, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["before_dead"])
			{
				if (listenerInfo.unit == dead_unit)
					this.ListenerCallback(listenerInfo, dead_unit, args);
			}
		}

		public void OnHurt(Unit source_unit, Unit target_unit, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["on_hurt"])
			{
				if (listenerInfo.unit == target_unit)
					this.ListenerCallback(listenerInfo, target_unit, args);
			}

			foreach (var listenerInfo in this.listener_dict["on_hurt_target"])
			{
				if (listenerInfo.unit == source_unit)
					this.ListenerCallback(listenerInfo, target_unit, args);
			}
		}


		public void OnHpChange(Unit source_unit, Unit target_unit, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["on_hp_change"])
			{
				if (listenerInfo.unit == target_unit)
					this.ListenerCallback(listenerInfo, target_unit, args);
			}
		}

		public void OnSpellStart(Unit source_unit, Unit target_unit, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["on_start"])
			{
				if (listenerInfo.unit == source_unit && listenerInfo.obj == spell)
					this.ListenerCallback(listenerInfo, target_unit, spell, args);
			}
		}

		public void OnSpellCast(Unit source_unit, Unit target_unit, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["on_cast"])
			{
				if (listenerInfo.unit == source_unit && listenerInfo.obj == spell)
					this.ListenerCallback(listenerInfo, target_unit, spell, args);
			}
		}

		public void OnMissileReach(Unit source_unit, EffectEntity missileEffect, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listener_dict["on_missile_reach"])
			{
				if (listenerInfo.unit == source_unit && listenerInfo.obj == spell)
					this.ListenerCallback(listenerInfo, source_unit, missileEffect, spell, args);
			}
		}
	}
}