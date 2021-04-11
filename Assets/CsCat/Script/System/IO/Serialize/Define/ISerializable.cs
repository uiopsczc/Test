
namespace CsCat
{
  public interface ISerializable
  {
    void Deserialize(SerializationInfo info, object context);

    void Serialize(SerializationInfo info, object context);
  }
}