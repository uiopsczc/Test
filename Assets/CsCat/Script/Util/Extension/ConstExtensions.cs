namespace CsCat
{
  public static class ConstExtensions
  {
    public const string Date_Regex =
      "(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)";

    public static readonly char[] Digits =
    {
      '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    };

    public static readonly char[] Chars_Big =
    {
      'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
      'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
      'U', 'V', 'W', 'X', 'Y', 'Z'
    };

    public static readonly char[] Chars_Small =
    {
      'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
      'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w',
      'x', 'y', 'z'
    };

    public static char[] GetCharsAll()
    {
      return Chars_Big.Combine(Chars_Small);
    }

    public static char[] GetDigitsAndCharsBig()
    {
      return Digits.Combine(Chars_Big);
    }

    public static char[] GetDigitsAndCharsSmall()
    {
      return Digits.Combine(Chars_Small);
    }

    public static char[] GetDigitsAndCharsAll()
    {
      return Digits.Combine(GetCharsAll());
    }
  }

  public enum DigitSign
  {
    Negative = -1,
    All = 0,
    Positive = 1
  }
}