namespace CsCat
{
	public class ItemFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Item";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgItemData(id).classPathCs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgItemData(id).classPathCs;
		}

		public CfgItemData GetCfgItemData(string id)
		{
			return CfgItem.Instance.GetById(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new ItemDBase(idOrRid);
		}

	}
}