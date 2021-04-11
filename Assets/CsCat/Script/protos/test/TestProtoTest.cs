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
      //序列化  
      byte[] data_bytes = testProto.Serialize();

      //反序列化  
      TestProto obj = data_bytes.Deserialize<TestProto>();

      LogCat.log(obj.Dict["aa"]);
    }
  }
}