using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public AbstractEntity AddChildWithoutInit(string childKey, Type childType)
		{
			if (childKey != null && keyToChildDict.ContainsKey(childKey))
			{
				LogCat.error("duplicate add child:{0},{1}", childKey, childType);
				return null;
			}

			bool isKeyUsingParentIdPool = childKey == null;
			if (isKeyUsingParentIdPool)
			{
				childKey = childKeyIdPool.Get().ToString();
				//再次检查键值
				if (keyToChildDict.ContainsKey(childKey))
				{
					LogCat.error("duplicate add child:{0},{1}", childKey, childType);
					return null;
				}
			}

			var child = PoolCatManagerUtil.Spawn(childType) as AbstractEntity;
			child.key = childKey;
			child.isKeyUsingParentIdPool = isKeyUsingParentIdPool;
			return AddChild(child);
		}

		public T AddChildWithoutInit<T>(string childKey) where T : AbstractEntity
		{
			return AddChildWithoutInit(childKey, typeof(T)) as T;
		}

		public AbstractEntity AddChild(AbstractEntity child)
		{
			if (keyToChildDict.ContainsKey(child.key))
			{
				LogCat.error("duplicate add child:{0}", child.key, child.GetType());
				return null;
			}

			child._parent = this;
			__AddChildRelationship(child);
			return child;
		}

		public virtual AbstractEntity AddChild(string childKey, Type childType, params object[] initArgs)
		{
			var child = AddChildWithoutInit(childKey, childType);
			if (child == null) //没有加成功
				return null;
			child.InvokeMethod("Init", false, initArgs);
			child.PostInit();
			child.SetIsEnabled(true, false);
			return child;
		}

		public T AddChild<T>(string childKey, params object[] initArgs) where T : AbstractEntity
		{
			return AddChild(childKey, typeof(T), initArgs) as T;
		}


		void __AddChildRelationship(AbstractEntity child)
		{
			keyToChildDict[child.key] = child;
			typeToChildListDict.GetOrAddDefault(child.GetType(), () => PoolCatManagerUtil.Spawn<List<AbstractEntity>>())
				.Add(child);
			childKeyList.Add(child.key);
			if (!childTypeList.Contains(child.GetType()))
				childTypeList.Add(child.GetType());
		}
	}
}