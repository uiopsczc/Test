using System;
using System.Collections.Generic;

namespace CsCat
{
	public partial class AbstractEntity
	{
		public bool RemoveChild(AbstractEntity child)
		{
			if (child.IsDestroyed())
				return false;

			child.DoDestroy();
			_RemoveChildRelationship(child);
			_DespawnChildKey(child);
			child.Despawn();
			return true;
		}

		public bool RemoveChild(string childKey)
		{
			if (!keyToChildDict.ContainsKey(childKey))
				return false;
			var child = keyToChildDict[childKey];
			return RemoveChild(child);
		}


		public bool RemoveChild(Type childType)
		{
			var child = this.GetChild(childType);
			if (child != null)
			{
				this.RemoveChild(child);
				return true;
			}
			return false;
		}

		public bool RemoveChild<T>() where T : AbstractEntity
		{
			return RemoveChild(typeof(T));
		}

		public bool RemoveChildStrictly(Type childType)
		{
			var child = this.GetChildStrictly(childType);
			if (child != null)
			{
				RemoveChild(child);
				return true;
			}
			return false;
		}

		public bool RemoveChildStrictly<T>() where T : AbstractEntity
		{
			return RemoveChildStrictly(typeof(T));
		}

		public bool RemoveChildren(Type childType)
		{
			var children = this.GetChildren(childType);
			if (!children.IsNullOrEmpty())
			{
				for (var i = 0; i < children.Length; i++)
				{
					var child = children[i];
					this.RemoveChild(child);
				}
				return true;
			}

			return false;
		}

		public bool RemoveChildren<T>() where T : AbstractEntity
		{
			return RemoveChildren(typeof(T));
		}


		public bool RemoveChildrenStrictly(Type childType)
		{
			var children = this.GetChildrenStrictly(childType);
			if (!children.IsNullOrEmpty())
			{
				for (var i = 0; i < children.Length; i++)
				{
					var child = children[i];
					this.RemoveChild(child);
				}
				return true;
			}

			return false;
		}

		public bool RemoveChildrenStrictly<T>() where T : AbstractEntity
		{
			return RemoveChildrenStrictly(typeof(T));
		}


		public void RemoveAllChildren()
		{
			for (var i = 0; i < childKeyList.Count; i++)
			{
				var childKey = childKeyList[i];
				RemoveChild(childKey);
			}
		}

		////////////////////////////////////////////////////////////////////
		

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
				_CheckDestroyedComponents();
				isHasDestroyedComponent = false;
			}
		}
	}
}