using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
    public class NormalGameObjectPoolCat : GameObjectPoolCat
    {
        public NormalGameObjectPoolCat(string poolName, GameObject prefab, string category = null) : base(poolName,
            prefab, category)
        {
        }

        public override void InitParent(Object prefab, string category)
        {
            base.InitParent(prefab, category);
            rootTransform = GameObjectUtil.GetOrNewGameObject("Pools", null).transform;
            categoryTransform = rootTransform.GetOrNewGameObject(category).transform;
        }

        public override void Despawn(object obj)
        {
            GameObject clone = obj as GameObject;
            foreach (var cloneComponent in clone.GetComponents<Component>())
            {
                var spawnable = cloneComponent as IDespawn;
                spawnable?.OnDespawn();
            }

            clone.SetActive(false);
            clone.transform.SetParent(categoryTransform);
            clone.transform.CopyFrom((prefab as GameObject).transform);
            base.Despawn(obj);
        }
    }
}