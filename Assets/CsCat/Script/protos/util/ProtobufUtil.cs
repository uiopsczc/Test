
using System.IO;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace CsCat
{
	public static class ProtobufUtil
	{
		//将对象序列化为byte[]
		public static byte[] Serialize<T>(T t) where T : Google.Protobuf.IMessage
		{
			return t.ToByteArray();
		}

		//将对象序列化到文件  
		public static void SerializeToFilePath<T>(T t, string file_path) where T : Google.Protobuf.IMessage
		{
			using (File.Create(file_path))
			{
				StdioUtil.WriteFile(new FileInfo(file_path), Serialize(t));
			}
		}


		//将byte[]转化为对象  
		public static T Deserialize<T>(byte[] data_bytes) where T : Google.Protobuf.IMessage, new()
		{
			T result = new T();
			result = (T)typeof(T).GetPropertyValue<MessageDescriptor>("Descriptor").Parser.ParseFrom(data_bytes);
			return result;
		}

		//将文件数据转化为对象  
		public static T DeserializeFromFilePath<T>(string file_path) where T : Google.Protobuf.IMessage, new()
		{
			byte[] data_bytes = StdioUtil.ReadFile(file_path);
			return Deserialize<T>(data_bytes);
		}
	}
}