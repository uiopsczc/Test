using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class UnitManager : TickObject
	{
		private Dictionary<string, Unit> unitDict = new Dictionary<string, Unit>();

		public override void Init()
		{
			base.Init();
			var gameObject = GameObject.Find("UnitManager") ?? new GameObject("UnitManager");
			graphicComponent.SetGameObject(gameObject, true);
			this.InitFactionUnitDict();
			this.InitFactionStateInfoDict();

			this.AddListener<string, string>(null, UnitEventNameConst.On_Unit_Guid_Change, this.OnUnitGuidChange);
			this.AddListener<string, string, string>(null, UnitEventNameConst.On_Unit_Faction_Change, this.OnUnitFactionChange);
		}

		protected override void _Update(float deltaTime = 0, float unscaledDeltaTime = 0)
		{
			base._Update(deltaTime, unscaledDeltaTime);
			foreach (var keyValue in this.unitDict)
			{
				var unit = keyValue.Value;
				if (!unit.IsDead() && !unit.IsDestroyed())
					unit.ReduceSpellCooldown(deltaTime);
			}
		}

		public Unit CreateUnit(Hashtable argDict)
		{
			string guid = argDict.Get<string>("guid");
			var oldUnit = this.GetUnit(guid);
			if (oldUnit != null)
				this.RemoveUnit(oldUnit.GetGuid());
			Unit unit = this.AddChild<Unit>(guid);
			unit.Build(argDict);
			this.unitDict[unit.key] = unit;
			this.factionUnitDict[unit.GetFaction()][unit.GetGuid()] = unit;
			if (!unit.cfgUnitData.aiClassPathCS.IsNullOrWhiteSpace())
				unit.RunAI(unit.cfgUnitData.aiClassPathCS);
			return unit;
		}

		public void UpdateUnit(Unit unit, Hashtable argDict)
		{
			var newGuid = argDict.Get<string>("guid");
			var oldGuid = unit.GetGuid();
			if (!newGuid.IsNullOrWhiteSpace() && !oldGuid.IsNullOrWhiteSpace())
				this.Broadcast(null, UnitEventNameConst.On_Unit_Guid_Change, oldGuid, newGuid);
			if (!newGuid.IsNullOrWhiteSpace())
				argDict.Remove(newGuid);
			unit.UpdateUnit(argDict);
		}

		public Unit GetUnit(string guid)
		{
			return this.GetChild<Unit>(guid);
		}

		public Dictionary<string, Unit> GetUnitDict()
		{
			return this.unitDict;
		}

		public void RemoveUnit(string guid)
		{
			Unit unit = this.GetUnit(guid);
			if (unit != null)
			{
				this.RemoveChild(guid);
				this.unitDict.Remove(guid);
				if (!unit.GetFaction().IsNullOrWhiteSpace())
					this.factionUnitDict[unit.GetFaction()].Remove(guid);
			}
		}

		public void OnUnitGuidChange(string oldGuid, string newGuid)
		{
			Unit unit = this.GetUnit(oldGuid);
			if (unit != null && !oldGuid.Equals(newGuid))
			{
				this.unitDict.Remove(oldGuid);
				this.keyToChildDict.Remove(oldGuid);
				int index = this.childKeyList.IndexOf(oldGuid);

				this.unitDict[newGuid] = unit;
				this.keyToChildDict[newGuid] = unit;
				this.childKeyList[index] = newGuid;
			}
		}
	}
}