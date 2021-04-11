namespace CsCat
{
  public class TestCSVData : ISingleton
  {
    #region ctor

    public TestCSVData()
    {
      data = new CsvData();
      data.Init(filePath);
    }

    #endregion

    #region property

    public static TestCSVData Instance => SingletonFactory.instance.Get<TestCSVData>();

    #endregion

    #region field

    public CsvData data;


    private readonly string filePath = "Data/TestCSVData";

    #endregion
  }
}