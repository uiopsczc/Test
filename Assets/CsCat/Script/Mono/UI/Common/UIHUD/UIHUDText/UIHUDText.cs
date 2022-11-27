namespace CsCat
{
	public class UIHUDText : UIHUDTextBase
	{
		public void SetText(string text)
		{
			InvokeAfterPrefabLoadDone(() => { this._TxtC_This.text = text; });
		}
	}
}