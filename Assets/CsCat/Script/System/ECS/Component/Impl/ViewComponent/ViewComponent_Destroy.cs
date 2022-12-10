namespace CsCat
{
	public partial class ViewComponent
	{
		protected override void _Destroy()
		{
			_Destroy_();
			_Destroy_Refresh();
			_Destroy_Transform();
			base._Destroy();
		}
	}
}