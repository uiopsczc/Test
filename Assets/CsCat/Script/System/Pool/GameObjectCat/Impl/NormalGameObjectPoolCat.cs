using UnityEngine;

namespace CsCat
{
	public class NormalGameObjectPoolCat : GameObjectPoolCat
	{
		public NormalGameObjectPoolCat(string poolName, GameObject prefab, string category = null) : base(poolName,
			prefab, category)
		{
		}

		public override void InitParentTransform(GameObject prefab, string category)
		{
			base.InitParentTransform(prefab, category);
			_rootTransform = GameObjectUtil.GetOrNewGameObject("Pools", null).transform;
			_categoryTransform = _rootTransform.GetOrNewGameObject(category).transform;
		}
	}
}