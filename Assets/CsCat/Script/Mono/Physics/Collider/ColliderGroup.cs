using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class ColliderGroup
	{
		public List<ColliderCat> colliderList = new List<ColliderCat>();
		public Dictionary<ColliderType, List<ColliderCat>> colliderListDict =
		  new Dictionary<ColliderType, List<ColliderCat>>();

		private List<ColliderType> _tmpCheckColliderTypeList1 = new List<ColliderType>();
		private List<ColliderType> _tmpCheckColliderTypeList2 = new List<ColliderType>();
		private List<ColliderCat> _tmpToCheckColliderList1 = new List<ColliderCat>();
		private List<ColliderCat> _tmpToCheckColliderList2 = new List<ColliderCat>();

		public bool IsIntersect(ColliderGroup colliderGroup2, List<ColliderType> checkColliderTypeList1,
		  List<ColliderType> checkColliderTypeList2)
		{
			_tmpToCheckColliderList1.Clear();
			_tmpToCheckColliderList2.Clear();

			for (var i = 0; i < checkColliderTypeList1.Count; i++)
			{
				var colliderType = checkColliderTypeList1[i];
				_tmpToCheckColliderList1.AddRange(colliderListDict[colliderType]);
			}

			for (var i = 0; i < checkColliderTypeList2.Count; i++)
			{
				var colliderType = checkColliderTypeList2[i];
				_tmpToCheckColliderList2.AddRange(colliderGroup2.colliderListDict[colliderType]);
			}

			for (var i = 0; i < _tmpToCheckColliderList1.Count; i++)
			{
				var collider1 = _tmpToCheckColliderList1[i];
				for (var j = 0; j < _tmpToCheckColliderList2.Count; j++)
				{
					var collider2 = _tmpToCheckColliderList2[j];
					if (collider1.IsIntersect(collider2))
						return true;
				}
			}

			return false;
		}

		public bool IsIntersect(ColliderGroup colliderGroup2, ColliderType checkColliderType1,
		  ColliderType checkColliderType2)
		{
			_tmpCheckColliderTypeList1.Clear();
			_tmpCheckColliderTypeList1.Add(checkColliderType1);

			_tmpCheckColliderTypeList2.Clear();
			_tmpCheckColliderTypeList2.Add(checkColliderType2);

			return IsIntersect(colliderGroup2, _tmpCheckColliderTypeList1, _tmpCheckColliderTypeList2);
		}

		public void AddCollider(ColliderCat collider)
		{
			colliderList.Add(collider);
			colliderListDict.GetOrAddDefault(collider.colliderType, () => new List<ColliderCat>()).Add(collider);
		}

		public void RemoveCollider(ColliderCat collider)
		{
			if (!colliderListDict.ContainsKey(collider.colliderType))
				return;
			colliderList.Remove(collider);
			colliderListDict[collider.colliderType].Remove(collider);
		}

		public void RemoveAllColliders()
		{
			colliderList.Clear();
			colliderListDict.Clear();
		}


		public void DebugDraw()
		{
			for (var i = 0; i < colliderList.Count; i++)
			{
				var collider = colliderList[i];
				collider.DebugDraw(Vector3.zero);
			}
		}
	}
}