namespace CsCat
{
  public interface ICommandManager
  {
    void RegisterCommand<CommandHandleType>(string message_name);
    void ExecuteCommand(ICommandMessage commandMessage);
    void RemoveCommand(string message_name);
    bool HasCommand(string message_name);
  }
}
