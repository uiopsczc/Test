using UnityEngine;

namespace CsCat
{
	public interface IPosition
	{
		Vector3 GetPosition();
		Transform GetTransform();
		void SetSocketName(string socketName);
		bool IsValid();
	}
}