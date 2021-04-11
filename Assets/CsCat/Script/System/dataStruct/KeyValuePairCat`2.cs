namespace CsCat
{
  public class KeyValuePairCat<K, V> : IToString2,ISpawnable
  {
    public K key { set; get; }
    public V value { set; get; }

    public KeyValuePairCat()
    {

    }
    public KeyValuePairCat(K key, V value)
    {
      this.Init(key,value);
    }

    public KeyValuePairCat<K, V> Init(K key, V value)
    {
      this.key = key;
      this.value = value;
      return this;
    }

    
    

    public override bool Equals(object obj)
    {
      var other = obj as KeyValuePairCat<K, V>;
      if (other == null)
        return false;
      if (ObjectUtil.Equals(key,other.key) && ObjectUtil.Equals(value,other.value))
        return true;
      return false;
    }

    public override int GetHashCode()
    {
      return ObjectUtil.GetHashCode(key, value);
    }

    public string ToString2(bool is_fill_string_with_double_quote = false)
    {
      return string.Format("[{0},{1}]", key.ToString(), value.ToString());
    }

    public void OnDespawn()
    {
      this.key = default(K);
      this.value = default(V);
    }


  }
}