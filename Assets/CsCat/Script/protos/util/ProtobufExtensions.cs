using Google.Protobuf;

namespace CsCat
{
  public static class ProtobufExtension
  {
    //将对象序列化为byte[]
    public static byte[] Serialize(this IMessage self)
    {
      return ProtobufUtil.Serialize(self);
    }

    //将byte[]转化为对象  
    public static T Deserialize<T>(this byte[] self) where T : Google.Protobuf.IMessage, new()
    {
      return ProtobufUtil.Deserialize<T>(self);
    }
  }
}