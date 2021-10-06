namespace CsCat
{
    public class TestCSVData : ISingleton
    {
        public void SingleInit()
        {
        }

        public TestCSVData()
        {
            data = new CsvData();
            data.Init(filePath);
        }


        public static TestCSVData Instance => SingletonFactory.instance.Get<TestCSVData>();


        public CsvData data;


        private readonly string filePath = "Data/TestCSVData";
    }
}