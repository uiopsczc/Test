namespace CsCat
{
	public interface IPropNotify
	{
		/// <summary>
		/// 通知name属性更改了
		/// </summary>
		/// <param name="propertyName"></param>
		/// <param name="oldValue">更改前的值</param>
		/// <param name="newValue">更改后的值</param>
		void NotifyPropChanged(string propertyName, object oldValue, object newValue);
	}
}