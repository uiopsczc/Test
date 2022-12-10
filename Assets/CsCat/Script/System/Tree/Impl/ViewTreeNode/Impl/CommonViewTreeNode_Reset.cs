namespace CsCat
{
	public partial class CommonViewTreeNode
	{
		
		protected override void _Reset()
		{
			this._Reset_GameObject();
			this._Reset_Prefab();
			base._Reset();
		}
	}
}