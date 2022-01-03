using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		private Dictionary<string, Transform> socket_transform_dict = new Dictionary<string, Transform>();

		public Transform GetSocketTransform(string socket_name, bool is_ignore_error = false)
		{
			if (graphicComponent.gameObject == null)
				return null;
			if (socket_name.IsNullOrWhiteSpace() || "main".Equals(socket_name))
				return graphicComponent.transform;
			Transform socket_transform = null;
			if (!this.socket_transform_dict.ContainsKey(socket_name))
			{
				socket_transform = graphicComponent.transform.FindChildRecursive(socket_name);
				if (socket_transform != null)
					this.socket_transform_dict[socket_name] = socket_transform;
			}

			if (socket_transform == null)
			{
				if (!is_ignore_error)
					LogCat.LogErrorFormat("Can't find socket({0}) in unit({1})", socket_name, this.unit_id);
				else
					LogCat.LogWarningFormat("Can't find socket({0}) in unit({1})", socket_name, this.unit_id);
				return graphicComponent.transform;
			}

			return socket_transform;
		}

		public Vector3 GetSocketPosition(string socket_name, bool is_ignore_error = false)
		{
			var socket_transform = this.GetSocketTransform(socket_name, is_ignore_error);
			if (socket_transform != null)
				return socket_transform.position;
			else
				return this.GetPosition();
		}

		public Quaternion GetSocketRotation(string socket_name, bool is_ignore_error = false)
		{
			var socket_transform = this.GetSocketTransform(socket_name, is_ignore_error);
			if (socket_transform != null)
				return socket_transform.rotation;
			else
				return this.GetRotation();
		}
	}
}