namespace CsCat
{
	public interface IEventDispatcher
	{
		void IRemoveListener(string eventName, object handler);
		void RemoveAllListeners();
	}
}