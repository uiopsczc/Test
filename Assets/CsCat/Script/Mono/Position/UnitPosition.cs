using UnityEngine;

namespace CsCat
{
	public class UnitPosition : IPosition
	{
		public Unit unit;
		public string socketName;

		public UnitPosition(Unit unit, string socketName = null)
		{
			this.unit = unit;
			this.socketName = socketName;
		}

		public Vector3 GetPosition()
		{
			return GetTransform().position;
		}

		public Transform GetTransform()
		{
			return !socketName.IsNullOrWhiteSpace() ? this.unit.graphicComponent.transform.GetSocketTransform(socketName) : this.unit.graphicComponent.transform;
		}

		public void SetSocketName(string socketName)
		{
			this.socketName = socketName;
		}


		public bool IsValid()
		{
			return !this.unit.IsDead();
		}
	}
}