using Com.Test;
using Google.Protobuf.Collections;

namespace CsCat
{
	public static class TestProtoTest
	{
		public static void Test()
		{
			TestProto testProto = new TestProto();
			testProto.Account = "aaa";
			testProto.Password = "456";
			testProto.Dict["aa"] = "cc";
			testProto.Adresses.Add("abc");
			testProto.Adresses.Add("efg");
			testProto.TestSubProto = new TestSubProto();
			testProto.TestSubProto.Account = "uiop";
			//      LogCat.log(testProto);
			//序列化  
			byte[] data_bytes = testProto.Serialize();

			//反序列化  
			TestProto obj = data_bytes.Deserialize<TestProto>();

			LogCat.log(obj.Dict["aa"]);
			LogCat.log(obj.TestSubProto.Account);
		}
	}
}