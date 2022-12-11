using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CsCat
{
	public class BalanceGroupManager
	{
		private readonly BalanceGroupDefinition[] _balanceGroupDefinitions;

		public BalanceGroupManager(params BalanceGroupDefinition[] balanceGroupDefinitions)
		{
			this._balanceGroupDefinitions = balanceGroupDefinitions;
		}

		public void Parse(string content)
		{
			int startIndex = 0;
			Stack<BalanceGroup> balanceGroupStack = new Stack<BalanceGroup>();
			while (startIndex != content.Length - 1)
			{
				foreach (var balanceGroupDefinition in _balanceGroupDefinitions)
				{
					var regex = new Regex(balanceGroupDefinition.openRegexPattern);
					var match = regex.Match(content, startIndex);
					if (match.Index == startIndex)
					{
						BalanceGroup balanceGroup = new BalanceGroup();
						balanceGroup.SetOpenIndexes(startIndex, startIndex + match.Length);
						balanceGroupStack.Push(balanceGroup);
						startIndex = startIndex + match.Length;
						break;
					}

					regex = new Regex(balanceGroupDefinition.closeRegexPattern);
					match = regex.Match(content, startIndex);
					if (match.Index == startIndex)
					{
						BalanceGroup balanceGroup = balanceGroupStack.Pop();
						balanceGroup.SetCloseIndexes(startIndex, startIndex + match.Length);
						startIndex = startIndex + match.Length;
						break;
					}
				}
			}
		}
	}
}