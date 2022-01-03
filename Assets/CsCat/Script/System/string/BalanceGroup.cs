using System.Collections.Generic;

namespace CsCat
{
	public class BalanceGroup
	{
		private int _openStartIndex;
		private int _openEndIndex;

		private int _closeStartIndex;
		private int _closeEndIndex;

		private List<BalanceGroup> _childrenList = new List<BalanceGroup>();

		public BalanceGroup()
		{
		}

		public void SetOpenIndexes(int openStartIndex, int openEndIndex)
		{
			this._openStartIndex = openStartIndex;
			this._openEndIndex = openEndIndex;
		}

		public void SetCloseIndexes(int closeStartIndex, int closeEndIndex)
		{
			this._closeStartIndex = closeStartIndex;
			this._closeEndIndex = closeEndIndex;
		}

		public string GetContent(string s)
		{
			return s.Substring(_openEndIndex + 1, _closeStartIndex - _openEndIndex - 1);
		}
	}
}