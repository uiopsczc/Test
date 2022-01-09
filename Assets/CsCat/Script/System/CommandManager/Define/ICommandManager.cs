namespace CsCat
{
	public interface ICommandManager
	{
		void RegisterCommand<CommandHandleType>(string commandName);
		void ExecuteCommand(ICommandMessage commandMessage);
		void RemoveCommand(string commandName);
		bool HasCommand(string commandName);
	}
}
