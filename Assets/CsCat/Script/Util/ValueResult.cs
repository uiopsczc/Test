namespace CsCat
{
  public class ValueResult<V>
  {
    public bool is_has_value;
    public V value;

    public ValueResult(bool is_has_value, V value)
    {
      this.is_has_value = is_has_value;
      this.value = value;
    }
  }
}