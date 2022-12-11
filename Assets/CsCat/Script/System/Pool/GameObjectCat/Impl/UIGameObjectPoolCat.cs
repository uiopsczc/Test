using UnityEngine;

namespace CsCat
{
	public class UIGameObjectPoolCat : GameObjectPoolCat
	{
		private readonly Cache cache = new Cache();

		private RectTransform prefabRectTransform => this.cache.GetOrAddDefault("prefabRectTransform",
			() => GetPrefab().GetComponent<RectTransform>());

		public UIGameObjectPoolCat(string poolName, GameObject prefab, string category = null) : base(poolName, prefab,
		  category)
		{
		}

		public override void InitParentTransform(GameObject prefab, string category)
		{
			base.InitParentTransform(prefab, category);
			_rootTransform = GameObjectUtil.GetOrNewGameObject("UIPools", null).transform;
			_rootTransform.gameObject.AddComponent<Canvas>();
			_categoryTransform = _rootTransform.GetOrNewGameObject(category).transform;
		}
		
	}
}