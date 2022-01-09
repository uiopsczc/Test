using UnityEngine;

namespace CsCat
{
	public class UIGameObjectPoolCat : GameObjectPoolCat
	{
		private Cache cache = new Cache();

		private RectTransform prefab_rectTransform => this.cache.GetOrAddDefault("prefab_rectTransform",
			() => GetPrefab<GameObject>().GetComponent<RectTransform>());

		public UIGameObjectPoolCat(string pool_name, GameObject prefab, string category = null) : base(pool_name, prefab,
		  category)
		{
		}

		public override void InitParent(Object prefab, string category)
		{
			base.InitParent(prefab, category);
			rootTransform = GameObjectUtil.GetOrNewGameObject("UIPools", null).transform;
			rootTransform.gameObject.AddComponent<Canvas>();
			categoryTransform = rootTransform.GetOrNewGameObject(category).transform;
		}

		public override void Despawn(object obj)
		{
			GameObject clone = obj as GameObject;
			foreach (var clone_component in clone.GetComponents<Component>())
			{
				if (clone_component is IDespawn ispanwable)
					ispanwable.OnDespawn();
			}
			clone.SetActive(false);
			clone.transform.SetParent(categoryTransform);
			clone.GetComponent<RectTransform>().CopyFrom(prefab_rectTransform);
			base.Despawn(obj);
		}
	}
}