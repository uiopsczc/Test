  using System.Text.RegularExpressions;

  public static class MetaConst
  {
    public static Regex guid_regex = new Regex("(?<=(guid: ))[\\s\\S]*?(?=(,))", RegexOptions.Multiline | RegexOptions.Singleline);
    public static Regex fileID_regex = new Regex("(?<=(fileID: ))[\\s\\S]*?(?=(,))", RegexOptions.Multiline | RegexOptions.Singleline);
    public static Regex font_regex = new Regex("(?<=(m_Font: {))[\\s\\S]*?(?=(}))", RegexOptions.Multiline | RegexOptions.Singleline);
    public static Regex sprite_regex = new Regex("(?<=(m_Sprite: {))[\\s\\S]*?(?=(}))", RegexOptions.Multiline | RegexOptions.Singleline);

  }
