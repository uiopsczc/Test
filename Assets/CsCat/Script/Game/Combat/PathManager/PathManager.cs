using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class PathManager : TickObject
	{
		public List<Vector3> GetPath(Vector3 fromPos, Vector3 toPos, Hashtable filterArgDict = null)
		{
			List<Vector3> path = new List<Vector3>();
			path.Add(fromPos);
			path.Add(toPos);
			return path;
		}

		// 是否能达到
		// 返回空时说明不能到达目标地
		public bool CanReach(Vector3 fromPos, Vector3 toPos, Hashtable filterArgDict = null)
		{
			var path = this.GetPath(fromPos, toPos, filterArgDict);
			if (path.IsNullOrEmpty())
				return false;
			if (toPos == path.Last())
				return true;
			return false;
		}

		public Vector3 GetGroundPos(Vector3 pos)
		{
			return pos;
		}

		//返回from_pos, to_pos间碰撞的点
		public Vector3? Raycast(Vector3 fromPos, Vector3 toPos, Hashtable filterArgDict = null)
		{
			return null;
		}
	}
}