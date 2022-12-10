namespace CsCat
{
	public class UIHUDText : UIHUDTextBase
	{
		public void SetText(string text)
		{
			InvokePostPrefabLoad(() => { this._TxtC_This.text = text; });
		}
	}
}