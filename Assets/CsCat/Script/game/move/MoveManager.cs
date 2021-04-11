using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class MoveManager : MonoBehaviour
  {
    public Dictionary<Transform, MoveInfo> moveInfo_dict = new Dictionary<Transform, MoveInfo>();
    public Dictionary<Transform, FollowInfo> followInfo_dict = new Dictionary<Transform, FollowInfo>();
    public List<Transform> delete_cache = new List<Transform>();

    void Update()
    {
      float deltaTime = Time.deltaTime;
      UpdateMove(deltaTime);
      UpdateFollow(deltaTime);
    }

    /////////////////////////////////////////////////////移动///////////////////////////////
    void UpdateMove(float delta_time)
    {
      foreach (KeyValuePair<Transform, MoveInfo> kv in moveInfo_dict)
      {
        MoveInfo moveInfo = kv.Value;
        moveInfo.current_time += delta_time;
        if (moveInfo.transform == null)
        {
          delete_cache.Add(moveInfo.transform);
        }
        else if (moveInfo.current_time < moveInfo.duration)
        {
          moveInfo.transform.position = Vector3.LerpUnclamped(moveInfo.from_pos, moveInfo.to_pos,
            moveInfo.current_time / moveInfo.duration);
        }
        else
        {
          moveInfo.transform.position = moveInfo.to_pos;
          delete_cache.Add(moveInfo.transform);
        }
      }

      if (delete_cache.Count > 0)
      {
        foreach (Transform transform in delete_cache)
        {
          moveInfo_dict.Remove(transform);
        }

        delete_cache.Clear();
      }
    }

    public void MoveTo(Transform transform, Vector3 to_pos, float duration)
    {
      MoveInfo moveInfo = null;
      bool is_contained = moveInfo_dict.TryGetValue(transform, out moveInfo);
      if (!is_contained)
      {
        moveInfo = new MoveInfo();
        moveInfo.transform = transform;
        moveInfo_dict[transform] = moveInfo;
      }

      moveInfo.from_pos = transform.position;
      moveInfo.to_pos = to_pos;
      moveInfo.duration = duration;
      moveInfo.current_time = 0;
    }

    public void StopMoveTo(Transform transfrom)
    {
      moveInfo_dict.Remove(transfrom);
    }

    /////////////////////////////////////////////////////跟随///////////////////////////////
    void UpdateFollow(float deltaTime)
    {
      foreach (KeyValuePair<Transform, FollowInfo> kv in followInfo_dict)
      {
        FollowInfo followInfo = kv.Value;
        if (followInfo.transform == null || followInfo.follow_transform == null)
        {
          delete_cache.Add(followInfo.transform);
        }
        else
        {
          followInfo.transform.position = followInfo.follow_transform.position;
        }
      }

      if (delete_cache.Count > 0)
      {
        foreach (Transform transfrom in delete_cache)
        {
          followInfo_dict.Remove(transfrom);
        }

        delete_cache.Clear();
      }
    }

    public void Follow(Transform transform, Transform follow_transform)
    {
      FollowInfo followInfo = null;
      bool is_contained = followInfo_dict.TryGetValue(transform, out followInfo);
      if (!is_contained)
      {
        followInfo = new FollowInfo();
        followInfo.transform = transform;
        followInfo_dict[transform] = followInfo;
      }

      followInfo.follow_transform = follow_transform;
    }

    public void StopFollow(Transform transform)
    {
      followInfo_dict.Remove(transform);
    }
  }
}