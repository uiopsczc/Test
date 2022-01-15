namespace CsCat
{
	public partial class SpellManager : TickObject
	{


		public void RegisterListener(string type, Unit unit, object obj, string tag, MethodInvoker methodInvoker)
		{
			if (!this.listenerDict.ContainsKey(type))
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
			this.listenerDict[type].Add(spellListenerInfo);
		}

		public void UnRegisterListener(string type, Unit unit, object obj, string tag)
		{
			if (!this.listenerDict.ContainsKey(type))
			{
				LogCat.error("Unregister Listener with undefine type()!", type);
				return;
			}

			for (int i = this.listenerDict[type].Count - 1; i >= 0; i--)
			{
				var listenerInfo = this.listenerDict[type][i];
				if (listenerInfo.unit == unit && listenerInfo.obj == obj && ObjectUtil.Equals(listenerInfo.tag, tag))
				{
					this.listenerDict[type].RemoveAt(i);
					break;
				}
			}
		}

		public void RemoveListenersByObj(object obj)
		{
			foreach (var keyValue in this.listenerDict)
			{
				var listenerInfoList = keyValue.Value;
				for (int i = listenerInfoList.Count - 1; i >= 0; i--)
				{
					if (ObjectUtil.Equals(listenerInfoList[i].obj, obj))
						listenerInfoList.RemoveAt(i);
				}
			}
		}

		public void ListenerCallback(SpellListenerInfo listener, Unit sourceUnit, params object[] args)
		{
			listener.methodInvoker.Invoke(args);
		}

		public void BeforeHit(Unit sourceUnit, Unit targetUnit, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["before_hit"])
			{
				if (listenerInfo.unit == sourceUnit)
					this.ListenerCallback(listenerInfo, targetUnit, args);
			}

			foreach (var listenerInfo in this.listenerDict["before_be_hit"])
			{
				if (listenerInfo.unit == targetUnit)
					this.ListenerCallback(listenerInfo, targetUnit, args);
			}
		}

		public void OnHit(Unit sourceUnit, Unit targetUnit, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["be_hit"])
			{
				if (listenerInfo.unit == targetUnit)
					this.ListenerCallback(listenerInfo, targetUnit, spell, args);
			}

			//!注意：触发回调的过程中可能再次插入或者remove listener
			foreach (var listenerInfo in this.listenerDict["on_hit"])
			{
				if (listenerInfo.unit == sourceUnit)
					this.ListenerCallback(listenerInfo, targetUnit, spell, args);
			}

			foreach (var listenerInfo in this.listenerDict["on_cur_spell_hit"])
			{
				if (listenerInfo.unit == sourceUnit && spell == listenerInfo.obj)
					this.ListenerCallback(listenerInfo, targetUnit, spell, args);
			}

			if ("普攻".Equals(spell.cfgSpellData.type))
			{
				foreach (var listenerInfo in this.listenerDict["normal_attack"])
				{
					if (listenerInfo.unit == sourceUnit)
						this.ListenerCallback(listenerInfo, targetUnit, spell, args);
				}
			}
		}


		public void OnKillTarget(Unit sourceUnit, Unit targetUnit, SpellBase spell, params object[] args)
		{
			if (spell == null)
				return;
			foreach (var listenerInfo in this.listenerDict["on_kill_target"])
			{
				if (listenerInfo.unit == sourceUnit)
					this.ListenerCallback(listenerInfo, targetUnit, spell, args);
			}
		}

		public void BeforeDead(Unit sourceUnit, Unit deadUnit, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["before_dead"])
			{
				if (listenerInfo.unit == deadUnit)
					this.ListenerCallback(listenerInfo, deadUnit, args);
			}
		}

		public void OnHurt(Unit sourceUnit, Unit targetUnit, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["on_hurt"])
			{
				if (listenerInfo.unit == targetUnit)
					this.ListenerCallback(listenerInfo, targetUnit, args);
			}

			foreach (var listenerInfo in this.listenerDict["on_hurt_target"])
			{
				if (listenerInfo.unit == sourceUnit)
					this.ListenerCallback(listenerInfo, targetUnit, args);
			}
		}


		public void OnHpChange(Unit sourceUnit, Unit targetUnit, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["on_hp_change"])
			{
				if (listenerInfo.unit == targetUnit)
					this.ListenerCallback(listenerInfo, targetUnit, args);
			}
		}

		public void OnSpellStart(Unit sourceUnit, Unit targetUnit, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["on_start"])
			{
				if (listenerInfo.unit == sourceUnit && listenerInfo.obj == spell)
					this.ListenerCallback(listenerInfo, targetUnit, spell, args);
			}
		}

		public void OnSpellCast(Unit sourceUnit, Unit targetUnit, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["on_cast"])
			{
				if (listenerInfo.unit == sourceUnit && listenerInfo.obj == spell)
					this.ListenerCallback(listenerInfo, targetUnit, spell, args);
			}
		}

		public void OnMissileReach(Unit sourceUnit, EffectEntity missileEffect, SpellBase spell, params object[] args)
		{
			foreach (var listenerInfo in this.listenerDict["on_missile_reach"])
			{
				if (listenerInfo.unit == sourceUnit && listenerInfo.obj == spell)
					this.ListenerCallback(listenerInfo, sourceUnit, missileEffect, spell, args);
			}
		}
	}
}