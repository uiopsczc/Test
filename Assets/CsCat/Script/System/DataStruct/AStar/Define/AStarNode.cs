using System;
using UnityEngine;

namespace CsCat
{
	public class AStarNode : IDeSpawn
	{
		public Vector2Int pos;
		public AStarNode parent;
		public float g; //当前消耗值
		public float h; //预估还需的消耗值
		public float f; //当前消耗值 + 预估还需的消耗值
		public AStarNodeInListType astarInListType;


		public void Init(int x, int y)
		{
			this.pos = new Vector2Int(x, y);
		}

		public void OnDeSpawn()
		{
			pos = default;
			parent = null;
			g = 0;
			h = 0;
			f = 0;
			astarInListType = default;
		}

		public override int GetHashCode()
		{
			return pos.GetHashCode();
		}


		public static int Compare(AStarNode data1, AStarNode data2)
		{
			float value = data1.f - data2.f;
			return value == 0 ? 0 : value < 0 ? -1 : 1;
		}
	}
}