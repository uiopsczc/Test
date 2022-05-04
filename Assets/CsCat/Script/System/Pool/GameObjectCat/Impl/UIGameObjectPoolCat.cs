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
			rootTransform = GameObjectUtil.GetOrNewGameObject("UIPools", null).transform;
			rootTransform.gameObject.AddComponent<Canvas>();
			categoryTransform = rootTransform.GetOrNewGameObject(category).transform;
		}
		
	}
}