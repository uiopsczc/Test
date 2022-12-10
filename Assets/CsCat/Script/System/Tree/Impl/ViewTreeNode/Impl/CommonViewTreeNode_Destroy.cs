namespace CsCat
{
	public partial class CommonViewTreeNode
	{
		protected override void _Destroy()
		{
			this._Destroy_GameObject();
			this._Destroy_Prefab();
			base._Destroy();
		}
	}
}