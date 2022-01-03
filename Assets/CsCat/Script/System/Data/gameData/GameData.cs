namespace CsCat
{
	public class GameData : SerializeData<GameData>
	{
		[Serialize] public ulong guid_current = 0;
		[Serialize] public AudioData audioData = new AudioData();
		[Serialize] public LangData langData = new LangData();
		[Serialize] public CurrencyData currencyData = new CurrencyData();
		[Serialize] public long quit_time_ticks;

		protected override void AddDataList()
		{
			AddToDataList(currencyData);
		}
	}
}