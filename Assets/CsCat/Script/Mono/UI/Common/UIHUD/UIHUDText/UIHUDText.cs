namespace CsCat
{
	public class UIHUDText : UIHUDTextBase
	{
		public void SetText(string text)
		{
			InvokeAfterAllAssetsLoadDone(() => { this.textComp.text = text; });
		}
	}
}