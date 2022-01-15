using System.Collections.Generic;

namespace CsCat
{
	public partial class UnitManager
	{
		private Dictionary<string, Dictionary<string, Unit>> factionUnitDict;
		private Dictionary<string, Dictionary<string, FactionState>> factionStateDict;

		private void InitFactionUnitDict()
		{
			this.factionUnitDict = new Dictionary<string, Dictionary<string, Unit>>();
			for (var i = 0; i < FactionConst.Faction_List.Count; i++)
			{
				var faction = FactionConst.Faction_List[i];
				factionUnitDict[faction] = new Dictionary<string, Unit>();
			}
		}

		private void InitFactionStateInfoDict()
		{
			//初始化阵营间能否攻击，加血等
			this.factionStateDict = new Dictionary<string, Dictionary<string, FactionState>>();

			this.SetFactionStateIsCanAttack(FactionConst.A_Faction, FactionConst.A_Faction, false);
			this.SetFactionStateIsCanHelp(FactionConst.A_Faction, FactionConst.A_Faction, true);
			this.SetFactionStateIsCanAttack(FactionConst.A_Faction, FactionConst.B_Faction, true);
			this.SetFactionStateIsCanHelp(FactionConst.A_Faction, FactionConst.B_Faction, false);

			this.SetFactionStateIsCanAttack(FactionConst.B_Faction, FactionConst.A_Faction, true);
			this.SetFactionStateIsCanHelp(FactionConst.B_Faction, FactionConst.A_Faction, false);
			this.SetFactionStateIsCanAttack(FactionConst.B_Faction, FactionConst.B_Faction, false);
			this.SetFactionStateIsCanHelp(FactionConst.B_Faction, FactionConst.B_Faction, true);
		}

		// 如果找敌人，找“不是自己阵营且单向阵营关系can_attack = true”
		// 如果是找自己人，找“阵营相同，或者单向阵营关系can_help = true”
		// check_state 参数：
		// 1. enemy 判断是否敌人
		// 2. friend 判断是否自己人
		public bool CheckFaction(string faction1, string faction2, string checkState)
		{
			var factionState = this.factionStateDict[faction1][faction2];
			//找敌人
			if (checkState.Equals("enemy"))
				return !faction1.Equals(faction2) && factionState.isCanAttack;
			if (checkState.Equals("friend")) // 找自己人
				return faction1.Equals(faction2) || factionState.isCanHelp;
			return true; // all
		}

		public void SetFactionState(string faction1, string faction2, string stateKey, object stateValue,
			bool isBothSet = false)
		{
			if (!faction1.IsNullOrWhiteSpace() && !faction2.IsNullOrWhiteSpace())
			{
				this.factionStateDict.GetOrAddDefault(faction1, () => new Dictionary<string, FactionState>());
				this.factionStateDict[faction1].GetOrAddDefault(faction2, () => new FactionState());

				this.factionStateDict[faction1][faction2].SetFieldValue(stateKey, stateValue);

				if (isBothSet)
					SetFactionState(faction2, faction1, stateKey, stateValue);
			}
		}

		public void SetFactionStateIsCanAttack(string faction1, string faction2, bool isCanAttack,
			bool isBothSet = false)
		{
			this.SetFactionState(faction1, faction2, "is_can_attack", isCanAttack, isBothSet);
		}

		public void SetFactionStateIsCanHelp(string faction1, string faction2, bool isCanHelp,
			bool isBothSet = false)
		{
			this.SetFactionState(faction1, faction2, "is_can_help", isCanHelp, isBothSet);
		}

		public void OnUnitFactionChange(string unitGuid, string oldFaction, string newFaction)
		{
			var unit = this.GetUnit(unitGuid);
			if (unit != null && !oldFaction.Equals(newFaction))
			{
				this.factionUnitDict[oldFaction].Remove(unitGuid);
				this.factionUnitDict[newFaction][unitGuid] = unit;
			}
		}

		public List<string> GetMatchFactionList(string faction, string checkScope)
		{
			List<string> factionList = new List<string>();
			for (var i = 0; i < FactionConst.Faction_List.Count; i++)
			{
				var curFaction = FactionConst.Faction_List[i];
				if (this.CheckFaction(curFaction, faction, checkScope))
					factionList.Add(curFaction);
			}

			return factionList;
		}

		public List<Unit> GetFactionUnitList(List<string> factionList)
		{
			var factionUnitList = new List<Unit>();
			for (var i = 0; i < factionList.Count; i++)
			{
				var faction = factionList[i];
				foreach (var keyValue in this.factionUnitDict[faction])
				{
					var unit = keyValue.Value;
					factionUnitList.Add(unit);
				}
			}

			return factionUnitList;
		}
	}
}