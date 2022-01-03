namespace CsCat
{
	public class SerializeTestData : SerializeData<SerializeTestData>
	{
		[Serialize] public SerializeTestSubData serializeTestSubData = new SerializeTestSubData();

		protected override void AddDataList()
		{
			AddToDataList(serializeTestSubData);
		}
	}
}