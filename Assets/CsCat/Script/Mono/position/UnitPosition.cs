using UnityEngine;

namespace CsCat
{
  public class UnitPosition : IPosition
  {
    public Unit unit;
    public string socket_name;

    public UnitPosition(Unit unit, string socket_name = null)
    {
      this.unit = unit;
      this.socket_name = socket_name;
    }

    public Vector3 GetPosition()
    {
      return GetTransform().position;
    }

    public Transform GetTransform()
    {
      return !socket_name.IsNullOrWhiteSpace() ? this.unit.graphicComponent.transform.GetSocketTransform(socket_name) : this.unit.graphicComponent.transform;
    }

    public void SetSocketName(string socket_name)
    {
      this.socket_name = socket_name;
    }


    public bool IsValid()
    {
      return !this.unit.IsDead();
    }
  }
}