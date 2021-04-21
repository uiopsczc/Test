using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class ColliderGroup
  {
    public List<ColliderCat> collider_list = new List<ColliderCat>();
    public Dictionary<ColliderType, List<ColliderCat>> collider_list_dict =
      new Dictionary<ColliderType, List<ColliderCat>>();

    private List<ColliderType> _tmp_check_colliderType_list_1 = new List<ColliderType>();
    private List<ColliderType> _tmp_check_colliderType_list_2 = new List<ColliderType>();
    private List<ColliderCat> _tmp_to_check_collider_list_1 = new List<ColliderCat>();
    private List<ColliderCat> _tmp_to_check_collider_list_2 = new List<ColliderCat>();

    public bool IsIntersect(ColliderGroup colliderGroup2, List<ColliderType> check_colliderType_list_1,
      List<ColliderType> check_colliderType_list_2)
    {
      _tmp_to_check_collider_list_1.Clear();
      _tmp_to_check_collider_list_2.Clear();

      foreach (var colliderType in check_colliderType_list_1)
        _tmp_to_check_collider_list_1.AddRange(collider_list_dict[colliderType]);
      foreach (var colliderType in check_colliderType_list_2)
        _tmp_to_check_collider_list_2.AddRange(colliderGroup2.collider_list_dict[colliderType]);
      foreach (var collider1 in _tmp_to_check_collider_list_1)
        foreach (var collider2 in _tmp_to_check_collider_list_2)
          if (collider1.IsIntersect(collider2))
            return true;
      return false;
    }

    public bool IsIntersect(ColliderGroup colliderGroup2, ColliderType check_colliderType_1,
      ColliderType check_colliderType_2)
    {
      _tmp_check_colliderType_list_1.Clear();
      _tmp_check_colliderType_list_1.Add(check_colliderType_1);

      _tmp_check_colliderType_list_2.Clear();
      _tmp_check_colliderType_list_2.Add(check_colliderType_2);

      return IsIntersect(colliderGroup2, _tmp_check_colliderType_list_1, _tmp_check_colliderType_list_2);
    }

    public void AddCollider(ColliderCat collider)
    {
      collider_list.Add(collider);
      collider_list_dict.GetOrAddDefault(collider.collider_type, () => new List<ColliderCat>()).Add(collider);
    }

    public void RemoveCollider(ColliderCat collider)
    {
      if (!collider_list_dict.ContainsKey(collider.collider_type))
        return;
      collider_list.Remove(collider);
      collider_list_dict[collider.collider_type].Remove(collider);
    }

    public void RemoveAllColliders()
    {
      collider_list.RemoveAll();
      collider_list_dict.Clear();
    }


    public void DebugDraw()
    {
      foreach (var collider in collider_list)
        collider.DebugDraw(Vector3.zero);
    }
  }
}