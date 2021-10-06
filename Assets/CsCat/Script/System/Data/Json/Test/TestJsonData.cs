namespace CsCat
{
    public class TestJsonData : ISingleton
    {
        public void SingleInit()
        {
        }

        public TestJsonData()
        {
            DataCat = new JsonDataCat();
            DataCat.Init(filePath);
        }


        public static TestJsonData Instance => SingletonFactory.instance.Get<TestJsonData>();


        public JsonDataCat DataCat;


        private readonly string filePath = "Data/TestJsonData";
    }
}