namespace CsCat
{
	public class ItemFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Item";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgItemData(id).classPathCS.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgItemData(id).classPathCS;
		}

		public CfgItemData GetCfgItemData(string id)
		{
			return CfgItem.Instance.get_by_id(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new ItemDBase(idOrRid);
		}

	}
}