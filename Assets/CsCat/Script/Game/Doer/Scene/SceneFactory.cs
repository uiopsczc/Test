namespace CsCat
{
	public class SceneFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Scene";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgSceneData(id).class_path_cs.IsNullOrWhiteSpace() ? base.GetClassPath(id) : GetCfgSceneData(id).class_path_cs;
		}

		public CfgSceneData GetCfgSceneData(string id)
		{
			return CfgScene.Instance.get_by_id(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new SceneDBase(idOrRid);
		}
	}
}