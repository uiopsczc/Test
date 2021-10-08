namespace CsCat
{
    public static class ObjectExtension_xlua
    {
        public static bool IsNil(this object self)
        {
            return self == null;
        }
    }
}