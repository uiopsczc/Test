namespace CsCat
{
	public interface ICommandMessage
	{
		string name { get; }
		object body { get; set; }
		string type { get; set; }
		string ToString();
	}
}
