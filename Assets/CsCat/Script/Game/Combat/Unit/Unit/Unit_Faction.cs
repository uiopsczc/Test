namespace CsCat
{
	public partial class Unit
	{
		public string faction;

		public void SetFaction(string newFaction)
		{
			var oldFaction = this.faction;
			this.faction = newFaction;
			this.OnFactionChange(oldFaction, newFaction);
		}

		public string GetFaction()
		{
			return this.faction;
		}

		private void OnFactionChange(string oldValue, string newValue)
		{
			if (!newValue.Equals(oldValue))
				this.Broadcast(null, UnitEventNameConst.On_Unit_Faction_Change, this.GetGuid(), oldValue, newValue);
		}
	}
}