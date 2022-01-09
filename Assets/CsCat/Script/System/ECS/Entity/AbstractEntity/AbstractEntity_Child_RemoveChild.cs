using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public AbstractEntity RemoveChild(AbstractEntity child)
		{
			if (child.IsDestroyed())
				return null;

			child.Destroy();
			if (!this.isNotDeleteChildRelationshipImmediately)
			{
				_RemoveChildRelationship(child);
				_DespawnChildKey(child);
				child.Despawn();
			}
			else
				_MarkHasDestroyedChild();

			return child;
		}

		public AbstractEntity RemoveChild(string childKey)
		{
			if (!keyToChildDict.ContainsKey(childKey))
				return null;
			var child = keyToChildDict[childKey];
			return RemoveChild(child);
		}


		public AbstractEntity RemoveChild(Type childType)
		{
			var child = this.GetChild(childType);
			if (child != null)
				this.RemoveChild(child);
			return child;
		}

		public T RemoveChild<T>() where T : AbstractEntity
		{
			return RemoveChild(typeof(T)) as T;
		}

		public AbstractEntity RemoveChildStrictly(Type childType)
		{
			var child = this.GetChildStrictly(childType);
			if (child != null)
				RemoveChild(child);
			return child;
		}

		public T RemoveChildStrictly<T>() where T : AbstractEntity
		{
			return (T)RemoveChildStrictly(typeof(T));
		}

		public AbstractEntity[] RemoveChildren(Type childType)
		{
			var children = this.GetChildren(childType);
			if (!children.IsNullOrEmpty())
			{
				for (var i = 0; i < children.Length; i++)
				{
					var child = children[i];
					this.RemoveChild(child);
				}
			}

			return children;
		}

		public T[] RemoveChildren<T>() where T : AbstractEntity
		{
			return (T[])RemoveChildren(typeof(T));
		}


		public AbstractEntity[] RemoveChildrenStrictly(Type child_type)
		{
			var children = this.GetChildrenStrictly(child_type);
			if (!children.IsNullOrEmpty())
			{
				for (var i = 0; i < children.Length; i++)
				{
					var child = children[i];
					this.RemoveChild(child);
				}
			}

			return children;
		}

		public T[] RemoveChildrenStrictly<T>() where T : AbstractEntity
		{
			return (T[])RemoveChildrenStrictly(typeof(T));
		}


		public void RemoveAllChildren()
		{
			var toRemoveChildKeyList = PoolCatManagerUtil.Spawn<List<string>>();
			toRemoveChildKeyList.Capacity = this.childKeyList.Count;
			toRemoveChildKeyList.AddRange(this.childKeyList);
			for (var i = 0; i < toRemoveChildKeyList.Count; i++)
			{
				var childKey = toRemoveChildKeyList[i];
				RemoveChild(childKey);
			}

			toRemoveChildKeyList.Clear();
			PoolCatManagerUtil.Despawn(toRemoveChildKeyList);
		}

		////////////////////////////////////////////////////////////////////
		private void _MarkHasDestroyedChild()
		{
			if (!this.isHasDestroyedChild)
			{
				this.isHasDestroyedChild = true;
				_parent?._MarkHasDestroyedChild();
			}
		}

		private void _RemoveChildRelationship(AbstractEntity child)
		{
			this.keyToChildDict.Remove(child.key);
			this.childKeyList.Remove(child.key);
			this.typeToChildListDict[child.GetType()].Remove(child);
		}

		private void _DespawnChildKey(AbstractEntity child)
		{
			if (child.isKeyUsingParentIdPool)
			{
				childKeyIdPool.Despawn(child.key);
				child.isKeyUsingParentIdPool = false;
			}
		}

		public void CheckDestroyed()
		{
			//有【子孙】child中有要从child_key_list和children_dict中删除关联关系
			//或者有【子孙】child的component要从从component_list和component_dict中删除关联关系
			if (isHasDestroyedChild || isHasDestroyedChildComponent)
			{
				string childKey;
				AbstractEntity child;
				for (int i = childKeyList.Count - 1; i >= 0; i--)
				{
					childKey = childKeyList[i];
					child = keyToChildDict[childKey];
					child.CheckDestroyed();
					if (child.IsDestroyed()) //该child自身要被delete
					{
						_RemoveChildRelationship(child);
						_DespawnChildKey(child);
						child.Despawn();
					}
				}

				isHasDestroyedChild = false;
				isHasDestroyedChildComponent = false;
			}

			if (this.isHasDestroyedComponent)
			{
				__CheckDestroyedComponents();
				isHasDestroyedComponent = false;
			}
		}
	}
}