namespace CsCat
{
  public class TestJsonData : ISingleton
  {
    #region ctor

    public TestJsonData()
    {
      DataCat = new JsonDataCat();
      DataCat.Init(filePath);
    }

    #endregion

    #region property

    public static TestJsonData Instance => SingletonFactory.instance.Get<TestJsonData>();

    #endregion

    #region field

    public JsonDataCat DataCat;


    private readonly string filePath = "Data/TestJsonData";

    #endregion
  }
}