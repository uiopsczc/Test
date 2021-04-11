

namespace CsCat
{
  public class GuidManager
  {
    public ulong key_number;

    public GuidManager(ulong current_key_number)
    {
      this.key_number = current_key_number;
    }

    public GuidManager()
    {
    }

    public string NewGuid(string id = null)
    {
      key_number++;
      return (id.IsNullOrWhiteSpace() ? "" : id) + IdConst.Rid_Infix + key_number;
    }
  }
}
