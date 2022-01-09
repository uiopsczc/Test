using System;
using System.Collections.Generic;

namespace CsCat
{
	public class CommandManager : ICommandManager
	{
		protected Dictionary<string, Type> commandDict = new Dictionary<string, Type>();


		public virtual void ExecuteCommand(ICommandMessage commandMessage)
		{
			Type commandType = null;
			if (commandDict.ContainsKey(commandMessage.name))
				commandType = commandDict[commandMessage.name];

			if (commandType != null)
			{
				var commandInstance = Activator.CreateInstance(commandType);
				if (commandInstance is ICommand command) command.Execute(commandMessage);
			}
		}

		public virtual void RegisterCommand<HandleCommandType>(string commandName)
		{
			commandDict[commandName] = typeof(HandleCommandType);
		}

		public virtual bool HasCommand(string commandName)
		{
			return commandDict.ContainsKey(commandName);
		}

		public virtual void RemoveCommand(string commandName)
		{
			if (commandDict.ContainsKey(commandName)) commandDict.Remove(commandName);
		}

		public void SendMessageCommand(string message, object body = null)
		{
			ExecuteCommand(new CommandMessage(message, body));
		}
	}
}