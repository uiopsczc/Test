using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class SkinnedMeshRendererTimelinableItemInfo : TimelinableItemInfoBase
	{
		[NonSerialized] public List<SkinnedMeshRenderer> skinnedMeshRendererList = new List<SkinnedMeshRenderer>();
		public int skinnedMeshRendererIndex;

		public int blendShapeIndex;
		public float blendShapeWeight;

		public List<SkinnedMeshRendererSubBlendShapeInfo> skinnedMeshRendererSubBlendShapeInfoList =
		  new List<SkinnedMeshRendererSubBlendShapeInfo>();

		[NonSerialized]
		private readonly Dictionary<Vector2Int, float> _fixedWeightBlendShapeDict = new Dictionary<Vector2Int, float>();

		public float blendDuration => this.duration;

		public SkinnedMeshRendererTimelinableItemInfo()
		{
			duration = 0.1f;
		}

		public SkinnedMeshRendererTimelinableItemInfo(SkinnedMeshRendererTimelinableItemInfo other)
		{
			CopyFrom(other);
		}

		public override void CopyTo(object dest)
		{
			var destSkinnedMeshRendererTimelinableItemInfo = dest as SkinnedMeshRendererTimelinableItemInfo;
			destSkinnedMeshRendererTimelinableItemInfo.skinnedMeshRendererIndex = skinnedMeshRendererIndex;
			destSkinnedMeshRendererTimelinableItemInfo.blendShapeIndex = blendShapeIndex;
			destSkinnedMeshRendererTimelinableItemInfo.blendShapeWeight = blendShapeWeight;
#if UNITY_EDITOR
			skinnedMeshRendererSubBlendShapeInfoReorderableListInfo.CopyTo(destSkinnedMeshRendererTimelinableItemInfo.skinnedMeshRendererSubBlendShapeInfoReorderableListInfo);
#endif
			base.CopyTo(dest);
		}

		public override void CopyFrom(object source)
		{
			var sourceSkinnedMeshRendererTimelinableItemInfo = source as SkinnedMeshRendererTimelinableItemInfo;
			skinnedMeshRendererIndex = sourceSkinnedMeshRendererTimelinableItemInfo.skinnedMeshRendererIndex;
			blendShapeIndex = sourceSkinnedMeshRendererTimelinableItemInfo.blendShapeIndex;
			blendShapeWeight = sourceSkinnedMeshRendererTimelinableItemInfo.blendShapeWeight;
#if UNITY_EDITOR
			skinnedMeshRendererSubBlendShapeInfoReorderableListInfo.CopyFrom(sourceSkinnedMeshRendererTimelinableItemInfo.skinnedMeshRendererSubBlendShapeInfoReorderableListInfo);
#endif
			base.CopyFrom(sourceSkinnedMeshRendererTimelinableItemInfo);
		}


		public override void Play(params object[] args)
		{
			var track = args[args.Length - 1] as SkinnedMeshRendererTimelinableTrack;
			int thisItemInfoIndexInTrack = track.itemInfoes.IndexOf(this); //当前的itemInfo在track中的index
			int thisItemInfoPreIndexInTrack = thisItemInfoIndexInTrack - 1; //当前的itemInfo的前一个itemInfo在track中的index
			float curTime = track.curTime;
			//处理前一个itemInfo
			if (thisItemInfoPreIndexInTrack >= 0)
			{
				var preItemInfo = track.itemInfoes[thisItemInfoPreIndexInTrack] as SkinnedMeshRendererTimelinableItemInfo;

				if (!track.toPlayItemInfoIndexList.Contains(thisItemInfoPreIndexInTrack))
				{
					UpdateBlendShape(curTime, preItemInfo.skinnedMeshRendererIndex, preItemInfo.blendShapeIndex,
					  blendDuration, time, preItemInfo.blendShapeWeight, 0);
					for (int i = 0; i < preItemInfo.skinnedMeshRendererSubBlendShapeInfoList.Count; i++)
					{
						var preSkinnedMeshRendererSubBlendShapeInfo = preItemInfo.skinnedMeshRendererSubBlendShapeInfoList[i];
						UpdateBlendShape(curTime, preSkinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex,
						  preSkinnedMeshRendererSubBlendShapeInfo.blendShapeIndex,
						  blendDuration,
						  time, preItemInfo.blendShapeWeight, 0);
					}
				}
			}

			//处理当前的itemInfo
			if (blendDuration > 0)
			{
				UpdateBlendShape(curTime, skinnedMeshRendererIndex, blendShapeIndex,
				  blendDuration, time,
				  GetBlendShapeWeight(skinnedMeshRendererIndex, blendShapeIndex),
				  blendShapeWeight);
				for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfoList.Count; i++)
				{
					var skinnedMeshRendererSubBlendShapeInfo = skinnedMeshRendererSubBlendShapeInfoList[i];
					UpdateBlendShape(curTime, skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex,
					  skinnedMeshRendererSubBlendShapeInfo.blendShapeIndex,
					  blendDuration, time,
					  GetBlendShapeWeight(skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex,
						skinnedMeshRendererSubBlendShapeInfo.blendShapeIndex), blendShapeWeight);
				}
			}
			else
			{
				UpdateBlendShape(skinnedMeshRendererIndex, blendShapeIndex,
				  blendShapeWeight);
				for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfoList.Count; i++)
				{
					var skinnedMeshRendererSubBlendShapeInfo = skinnedMeshRendererSubBlendShapeInfoList[i];
					UpdateBlendShape(skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex,
					  skinnedMeshRendererSubBlendShapeInfo.blendShapeIndex, blendShapeWeight);
				}
			}

			base.Play(args);
		}

		public override void Stop(params object[] args)
		{
			ClearBlendShape();
			base.Stop(args);
		}

		//////////////////////////////////////////////////////////////////////////////////////
		void _RegisterFixedWeightBlendShape(int skinnedMeshRendererIndex, int blendShapeIndex, float weight)
		{
			var key = new Vector2Int(skinnedMeshRendererIndex, blendShapeIndex);
			_fixedWeightBlendShapeDict[key] = weight;
			UpdateBlendShape(skinnedMeshRendererIndex, blendShapeIndex, weight, false);
		}

		bool _IsContainsFixedWeightBlendShape(int skinnedMeshRendererIndex, int blendShapeIndex)
		{
			var key = new Vector2Int(skinnedMeshRendererIndex, blendShapeIndex);
			return _fixedWeightBlendShapeDict.ContainsKey(key);
		}

		private void _ResetBlendShapes(SkinnedMeshRenderer skinnedMeshRenderer, int skinnedMeshRendererIndex)
		{
			if (skinnedMeshRenderer != null && skinnedMeshRenderer.sharedMesh != null)
			{
				for (int blendShapeIndex = 0;
				  blendShapeIndex < skinnedMeshRenderer.sharedMesh.blendShapeCount;
				  blendShapeIndex++)
				{
					if (!_IsContainsFixedWeightBlendShape(skinnedMeshRendererIndex, blendShapeIndex))
						skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, 0);
				}
			}
		}

		public float GetBlendShapeWeight(int skinnedMeshRendererIndex, int blendShapeIndex)
		{
			if (skinnedMeshRendererIndex >= 0 && skinnedMeshRendererIndex < skinnedMeshRendererList.Count)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRendererList[skinnedMeshRendererIndex];
				if (skinnedMeshRenderer != null)
				{
					if (skinnedMeshRenderer.sharedMesh != null && blendShapeIndex >= 0 &&
						blendShapeIndex < skinnedMeshRenderer.sharedMesh.blendShapeCount)
						return skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex);
				}
			}

			return 0;
		}

		public void UpdateBlendShape()
		{
			UpdateBlendShape(skinnedMeshRendererIndex, blendShapeIndex, blendShapeWeight);
			for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfoList.Count; i++)
			{
				var skinnedMeshRendererSubBlendShapeInfo = skinnedMeshRendererSubBlendShapeInfoList[i];
				UpdateBlendShape(skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRendererIndex,
				  skinnedMeshRendererSubBlendShapeInfo.blendShapeIndex, blendShapeWeight);
			}
		}

		public void UpdateBlendShape(float time, int skinnedMeshRendererIndex, int blendShapeIndex, float blendDuration,
		  float itemInfoTime, float fromWeight, float toWeight)
		{
			if (fromWeight != toWeight)
			{
				float weight = toWeight;
				if (blendDuration > 0)
					weight = Mathf.Lerp(fromWeight, toWeight, (time - itemInfoTime) / blendDuration);
				UpdateBlendShape(skinnedMeshRendererIndex, blendShapeIndex, weight);
			}
		}

		public void UpdateBlendShape(int skinnedMeshRendererIndex, int blendShapeIndex, float weight,
		  bool isFixedWeight = true)
		{
			if (!_IsContainsFixedWeightBlendShape(skinnedMeshRendererIndex, blendShapeIndex) || !isFixedWeight)
			{
				if (skinnedMeshRendererIndex >= 0 && skinnedMeshRendererIndex < skinnedMeshRendererList.Count)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRendererList[skinnedMeshRendererIndex];
					if (skinnedMeshRenderer != null && skinnedMeshRenderer.sharedMesh != null)
					{
						if (blendShapeIndex >= 0 && blendShapeIndex < skinnedMeshRenderer.sharedMesh.blendShapeCount)
						{
							if (skinnedMeshRenderer.GetBlendShapeWeight(blendShapeIndex) != weight)
								skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, weight);
						}
					}
				}
			}
		}

		public void ClearBlendShape()
		{
			UpdateBlendShape(skinnedMeshRendererIndex, blendShapeIndex, 0);
			for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfoList.Count; i++)
				UpdateBlendShape(skinnedMeshRendererSubBlendShapeInfoList[i].skinnedMeshRendererIndex,
				  skinnedMeshRendererSubBlendShapeInfoList[i].blendShapeIndex, 0);
		}
	}
}