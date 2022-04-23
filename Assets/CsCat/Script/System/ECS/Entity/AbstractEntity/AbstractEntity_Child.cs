using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		protected Dictionary<string, AbstractEntity> keyToChildDict = new Dictionary<string, AbstractEntity>();

		protected Dictionary<Type, List<AbstractEntity>> typeToChildListDict =
			new Dictionary<Type, List<AbstractEntity>>(); //准确的类型


		protected List<string> childKeyList = new List<string>();
		protected List<Type> childTypeList = new List<Type>();
		protected AbstractEntity parent;

		protected IdPool childKeyIdPool = new IdPool();
		protected bool isKeyUsingParentIdPool;

		public IEnumerable<AbstractEntity> ForeachChild()
		{
			for (int i = 0; i < childKeyList.Count; i++)
			{
				var child = GetChild(childKeyList[i]);
				if (child != null)
					yield return child;
			}
		}

		public IEnumerable<AbstractEntity> ForeachChild(Type childType)
		{
			for (int i = 0; i < childKeyList.Count; i++)
			{
				var child = GetChild(childKeyList[i]);
				if (child != null && childType.IsInstanceOfType(child))
					yield return child;
			}
		}

		public IEnumerable<T> ForeachChild<T>() where T : AbstractEntity
		{
			for (int i = 0; i < childKeyList.Count; i++)
			{
				if (GetChild(childKeyList[i]) is T child)
					yield return child;
			}
		}


		void _OnDespawn_Child()
		{
			keyToChildDict.Clear();
			foreach (var keyValue in typeToChildListDict)
			{
				var childList = keyValue.Value;
				childList.Clear();
				PoolCatManagerUtil.Despawn(childList);
			}
			typeToChildListDict.Clear();
			childKeyList.Clear();
			childTypeList.Clear();
			parent = null;
			isKeyUsingParentIdPool = false;
		}
	}
}