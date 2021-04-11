namespace CsCat
{
  public class TestJsonData : ISingleton
  {
    #region ctor

    public TestJsonData()
    {
      data = new JsonData();
      data.Init(filePath);
    }

    #endregion

    #region property

    public static TestJsonData Instance => SingletonFactory.instance.Get<TestJsonData>();

    #endregion

    #region field

    public JsonData data;


    private readonly string filePath = "Data/TestJsonData";

    #endregion
  }
}