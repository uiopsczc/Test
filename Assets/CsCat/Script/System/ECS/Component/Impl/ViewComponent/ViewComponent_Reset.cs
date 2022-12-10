namespace CsCat
{
	public partial class ViewComponent
	{
		protected override void _Reset()
		{
			_Reset_();
			_Reset_Refresh();
			_Reset_Transform();
			base._Reset();
		}
	}
}