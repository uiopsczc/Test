namespace CsCat
{
  public partial class Unit
  {
    public string faction;

    public void SetFaction(string new_faction)
    {
      var old_faction = this.faction;
      this.faction = new_faction;
      this.OnFactionChange(old_faction, new_faction);
    }

    public string GetFaction()
    {
      return this.faction;
    }

    private void OnFactionChange(string old_value, string new_value)
    {
      if (!new_value.Equals(old_value))
        this.Broadcast(UnitEventNameConst.On_Unit_Faction_Change, this.GetGuid(), old_value, new_value);
    }
  }
}