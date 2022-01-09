using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class PathManager : TickObject
	{
		public List<Vector3> GetPath(Vector3 from_pos, Vector3 to_pos, Hashtable filter_arg_dict = null)
		{
			List<Vector3> path = new List<Vector3>();
			path.Add(from_pos);
			path.Add(to_pos);
			return path;
		}

		// 是否能达到
		// 返回空时说明不能到达目标地
		public bool CanReach(Vector3 from_pos, Vector3 to_pos, Hashtable filter_arg_dict = null)
		{
			var path = this.GetPath(from_pos, to_pos, filter_arg_dict);
			if (path.IsNullOrEmpty())
				return false;
			if (to_pos == path.Last())
				return true;
			return false;
		}

		public Vector3 GetGroundPos(Vector3 pos)
		{
			return pos;
		}

		//返回from_pos, to_pos间碰撞的点
		public Vector3? Raycast(Vector3 from_pos, Vector3 to_pos, Hashtable filter_arg_dict = null)
		{
			return null;
		}
	}
}