using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class MoveManager : MonoBehaviour
	{
		public Dictionary<Transform, MoveInfo> moveInfoDict = new Dictionary<Transform, MoveInfo>();
		public Dictionary<Transform, FollowInfo> followInfoDict = new Dictionary<Transform, FollowInfo>();
		public List<Transform> deleteCache = new List<Transform>();

		void Update()
		{
			float deltaTime = Time.deltaTime;
			UpdateMove(deltaTime);
			UpdateFollow(deltaTime);
		}

		/////////////////////////////////////////////////////移动///////////////////////////////
		void UpdateMove(float deltaTime)
		{
			foreach (KeyValuePair<Transform, MoveInfo> kv in moveInfoDict)
			{
				MoveInfo moveInfo = kv.Value;
				moveInfo.currentTime += deltaTime;
				if (moveInfo.transform == null)
				{
					deleteCache.Add(moveInfo.transform);
				}
				else if (moveInfo.currentTime < moveInfo.duration)
				{
					moveInfo.transform.position = Vector3.LerpUnclamped(moveInfo.fromPos, moveInfo.toPos,
						moveInfo.currentTime / moveInfo.duration);
				}
				else
				{
					moveInfo.transform.position = moveInfo.toPos;
					deleteCache.Add(moveInfo.transform);
				}
			}

			if (deleteCache.Count > 0)
			{
				for (var i = 0; i < deleteCache.Count; i++)
				{
					Transform transform = deleteCache[i];
					moveInfoDict.Remove(transform);
				}

				deleteCache.Clear();
			}
		}

		public void MoveTo(Transform transform, Vector3 toPos, float duration)
		{
			MoveInfo moveInfo = null;
			bool isContained = moveInfoDict.TryGetValue(transform, out moveInfo);
			if (!isContained)
			{
				moveInfo = new MoveInfo();
				moveInfo.transform = transform;
				moveInfoDict[transform] = moveInfo;
			}

			moveInfo.fromPos = transform.position;
			moveInfo.toPos = toPos;
			moveInfo.duration = duration;
			moveInfo.currentTime = 0;
		}

		public void StopMoveTo(Transform transform)
		{
			moveInfoDict.Remove(transform);
		}

		/////////////////////////////////////////////////////跟随///////////////////////////////
		void UpdateFollow(float deltaTime)
		{
			foreach (KeyValuePair<Transform, FollowInfo> kv in followInfoDict)
			{
				FollowInfo followInfo = kv.Value;
				if (followInfo.transform == null || followInfo.followTransform == null)
				{
					deleteCache.Add(followInfo.transform);
				}
				else
				{
					followInfo.transform.position = followInfo.followTransform.position;
				}
			}

			if (deleteCache.Count > 0)
			{
				for (var i = 0; i < deleteCache.Count; i++)
				{
					Transform transform = deleteCache[i];
					followInfoDict.Remove(transform);
				}

				deleteCache.Clear();
			}
		}

		public void Follow(Transform transform, Transform followTransform)
		{
			FollowInfo followInfo = null;
			bool isContained = followInfoDict.TryGetValue(transform, out followInfo);
			if (!isContained)
			{
				followInfo = new FollowInfo();
				followInfo.transform = transform;
				followInfoDict[transform] = followInfo;
			}

			followInfo.followTransform = followTransform;
		}

		public void StopFollow(Transform transform)
		{
			followInfoDict.Remove(transform);
		}
	}
}