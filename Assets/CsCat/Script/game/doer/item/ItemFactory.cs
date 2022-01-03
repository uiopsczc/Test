namespace CsCat
{
	public class ItemFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Item";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgItemData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgItemData(id).class_path_cs;
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