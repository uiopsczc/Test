using System;
using System.Collections.Generic;

namespace CsCat
{
	public class CommandManager : ICommandManager
	{
		protected Dictionary<string, Type> _commandDict = new Dictionary<string, Type>();


		public virtual void ExecuteCommand(ICommandMessage commandMessage)
		{
			Type commandType = null;
			if (_commandDict.ContainsKey(commandMessage.name))
				commandType = _commandDict[commandMessage.name];

			if (commandType != null)
			{
				var commandInstance = Activator.CreateInstance(commandType);
				if (commandInstance is ICommand command) command.Execute(commandMessage);
			}
		}

		public virtual void RegisterCommand<HandleCommandType>(string commandName)
		{
			_commandDict[commandName] = typeof(HandleCommandType);
		}

		public virtual bool HasCommand(string commandName)
		{
			return _commandDict.ContainsKey(commandName);
		}

		public virtual void RemoveCommand(string commandName)
		{
			if (_commandDict.ContainsKey(commandName)) _commandDict.Remove(commandName);
		}

		public void SendMessageCommand(string message, object body = null)
		{
			ExecuteCommand(new CommandMessage(message, body));
		}
	}
}