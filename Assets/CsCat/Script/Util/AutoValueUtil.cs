namespace CsCat
{
    public class AutoValueUtil
    {
        public static AutoSetValue<T> SetValue<T>(ref T preValue, T postValue)
        {
            return AutoSetValue.SetValue(ref preValue, postValue);
        }
    }
}