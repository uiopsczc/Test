using System.Text.RegularExpressions;

public static class MetaConst
{
	public static Regex Guid_Regex =
		new Regex("(?<=(guid: ))[\\s\\S]*?(?=(,))", RegexOptions.Multiline | RegexOptions.Singleline);

	public static Regex FileID_Regex =
		new Regex("(?<=(fileID: ))[\\s\\S]*?(?=(,))", RegexOptions.Multiline | RegexOptions.Singleline);

	public static Regex Font_Regex = new Regex("(?<=(m_Font: {))[\\s\\S]*?(?=(}))",
		RegexOptions.Multiline | RegexOptions.Singleline);

	public static Regex Sprite_Regex = new Regex("(?<=(m_Sprite: {))[\\s\\S]*?(?=(}))",
		RegexOptions.Multiline | RegexOptions.Singleline);
}