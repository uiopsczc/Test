using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class Unit
	{
		private Dictionary<string, Transform> socketTransformDict = new Dictionary<string, Transform>();

		public Transform GetSocketTransform(string socketName, bool isIgnoreError = false)
		{
			if (graphicComponent.gameObject == null)
				return null;
			if (socketName.IsNullOrWhiteSpace() || "main".Equals(socketName))
				return graphicComponent.transform;
			Transform socketTransform = null;
			if (!this.socketTransformDict.ContainsKey(socketName))
			{
				socketTransform = graphicComponent.transform.FindChildRecursive(socketName);
				if (socketTransform != null)
					this.socketTransformDict[socketName] = socketTransform;
			}

			if (socketTransform == null)
			{
				if (!isIgnoreError)
					LogCat.LogErrorFormat("Can't find socket({0}) in unit({1})", socketName, this.unitId);
				else
					LogCat.LogWarningFormat("Can't find socket({0}) in unit({1})", socketName, this.unitId);
				return graphicComponent.transform;
			}

			return socketTransform;
		}

		public Vector3 GetSocketPosition(string socketName, bool isIgnoreError = false)
		{
			var socketTransform = this.GetSocketTransform(socketName, isIgnoreError);
			return socketTransform != null ? socketTransform.position : this.GetPosition();
		}

		public Quaternion GetSocketRotation(string socketName, bool isIgnoreError = false)
		{
			var socketTransform = this.GetSocketTransform(socketName, isIgnoreError);
			return socketTransform != null ? socketTransform.rotation : this.GetRotation();
		}
	}
}