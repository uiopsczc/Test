namespace CsCat
{
  public class StringUtilCat
  {
    public static string[] SplitIgnore(string self, string split = ",", string ignore_left = "\\\"",
      string ignore_right = null)
    {
      return self.SplitIgnore(split, ignore_left, ignore_right);
    }

    public static bool IsNumber(string self)
    {
      return self.IsNumber();
    }

  }
}



