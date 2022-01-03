namespace CsCat
{
	public class AutoSetValue
	{
		public static AutoSetValue<T> SetValue<T>(ref T preValue, T postValue)
		{
			var self = new AutoSetValue<T> { preValue = preValue, postValue = postValue };
			preValue = postValue;
			return self;
		}
	}
}