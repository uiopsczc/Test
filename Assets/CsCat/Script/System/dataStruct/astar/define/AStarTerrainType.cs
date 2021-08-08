namespace CsCat
{
  public class AStarTerrainType
  {
    public string name;
    public int value;
    public float cost;


    public AStarTerrainType(string name, int value, float cost)
    {
      this.name = name;
      this.value = value;
      this.cost = cost;
    }
  }
}
