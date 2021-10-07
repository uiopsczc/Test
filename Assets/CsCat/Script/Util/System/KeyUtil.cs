namespace CsCat
{
    public static class KeyUtil
    {
        public static string GetCombinedKey(string separator, params object[] keys)
        {
            return keys.Join(separator);
        }

        public static string GetCombinedKey(char separator, params object[] keys)
        {
            return keys.Join(separator.ToString());
        }
    }
}