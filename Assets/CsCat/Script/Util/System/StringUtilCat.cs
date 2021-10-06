namespace CsCat
{
    public class StringUtilCat
    {
        public static string[] SplitIgnore(string self, string split = StringConst.StringComma, string ignoreLeft = StringConst.StringRegexDoubleQuotes,
            string ignoreRight = null)
        {
            return self.SplitIgnore(split, ignoreLeft, ignoreRight);
        }

        public static bool IsNumber(string self)
        {
            return self.IsNumber();
        }
    }
}