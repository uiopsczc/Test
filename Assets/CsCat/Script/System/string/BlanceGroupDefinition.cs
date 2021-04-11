namespace CsCat
{
  public class BlanceGroupDefinition
  {
    public string open_regex_pattern;
    public string close_regex_pattern;

    public BlanceGroupDefinition(string open_regex_pattern, string close_regex_pattern)
    {
      this.open_regex_pattern = open_regex_pattern;
      this.close_regex_pattern = close_regex_pattern;
    }

  }
}