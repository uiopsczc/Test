using System;
using System.Collections.Generic;

namespace CsCat
{
  public class CommandManager : ICommandManager
  {
    protected Dictionary<string, Type> command_dict = new Dictionary<string, Type>();


    public virtual void ExecuteCommand(ICommandMessage commandMessage)
    {
      Type command_type = null;
      if (command_dict.ContainsKey(commandMessage.name))
        command_type = command_dict[commandMessage.name];

      if (command_type != null)
      {
        var command_instance = Activator.CreateInstance(command_type);
        if (command_instance is ICommand) ((ICommand)command_instance).Execute(commandMessage);
      }
    }

    public virtual void RegisterCommand<HandleCommandType>(string command_name)
    {
      command_dict[command_name] = typeof(HandleCommandType);
    }

    public virtual bool HasCommand(string command_name)
    {
      return command_dict.ContainsKey(command_name);
    }

    public virtual void RemoveCommand(string command_name)
    {
      if (command_dict.ContainsKey(command_name)) command_dict.Remove(command_name);
    }

    public void SendMessageCommand(string message, object body = null)
    {
      ExecuteCommand(new CommandMessage(message, body));
    }
  }
}