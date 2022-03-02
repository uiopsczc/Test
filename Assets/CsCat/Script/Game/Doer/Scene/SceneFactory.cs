namespace CsCat
{
	public class SceneFactory : DoerFactory
	{
		protected override string defaultDoerClassPath => "CsCat.Scene";

		protected override string GetClassPath(string id)
		{
			return this.GetCfgSceneData(id).classPathCs.IsNullOrWhiteSpace()
				? base.GetClassPath(id)
				: GetCfgSceneData(id).classPathCs;
		}

		public CfgSceneData GetCfgSceneData(string id)
		{
			return CfgScene.Instance.GetById(id);
		}

		protected override DBase _NewDBase(string idOrRid)
		{
			return new SceneDBase(idOrRid);
		}
	}
}