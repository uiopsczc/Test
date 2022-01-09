namespace CsCat
{
	public class BalanceGroupDefinition
	{
		public string openRegexPattern;
		public string closeRegexPattern;

		public BalanceGroupDefinition(string openRegexPattern, string closeRegexPattern)
		{
			this.openRegexPattern = openRegexPattern;
			this.closeRegexPattern = closeRegexPattern;
		}
	}
}