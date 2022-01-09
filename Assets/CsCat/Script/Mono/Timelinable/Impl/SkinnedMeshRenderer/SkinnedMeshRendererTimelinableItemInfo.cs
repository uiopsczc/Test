using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	[Serializable]
	public partial class SkinnedMeshRendererTimelinableItemInfo : TimelinableItemInfoBase
	{
		[NonSerialized] public List<SkinnedMeshRenderer> skinnedMeshRenderer_list = new List<SkinnedMeshRenderer>();
		public int skinnedMeshRenderer_index;

		public int blendShape_index;
		public float blendShape_weight;

		public List<SkinnedMeshRendererSubBlendShapeInfo> skinnedMeshRendererSubBlendShapeInfo_list =
		  new List<SkinnedMeshRendererSubBlendShapeInfo>();

		[NonSerialized]
		private readonly Dictionary<Vector2Int, float> fixed_weight_blendShape_dict = new Dictionary<Vector2Int, float>();

		public float blend_duration
		{
			get { return this.duration; }
		}

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
			var _dest = dest as SkinnedMeshRendererTimelinableItemInfo;
			_dest.skinnedMeshRenderer_index = skinnedMeshRenderer_index;
			_dest.blendShape_index = blendShape_index;
			_dest.blendShape_weight = blendShape_weight;
#if UNITY_EDITOR
			skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo.CopyTo(_dest.skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo);
#endif
			base.CopyTo(dest);
		}

		public override void CopyFrom(object source)
		{
			var _source = source as SkinnedMeshRendererTimelinableItemInfo;
			skinnedMeshRenderer_index = _source.skinnedMeshRenderer_index;
			blendShape_index = _source.blendShape_index;
			blendShape_weight = _source.blendShape_weight;
#if UNITY_EDITOR
			skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo.CopyFrom(_source.skinnedMeshRendererSubBlendShapeInfo_reorderableListInfo);
#endif
			base.CopyFrom(_source);
		}


		public override void Play(params object[] args)
		{
			var track = args[args.Length - 1] as SkinnedMeshRendererTimelinableTrack;
			int this_itemInfo_index_in_track = track.itemInfoes.IndexOf(this); //当前的itemInfo在track中的index
			int this_itemInfo_pre_index_in_track = this_itemInfo_index_in_track - 1; //当前的itemInfo的前一个itemInfo在track中的index
			float cur_time = track.curTime;
			//处理前一个itemInfo
			if (this_itemInfo_pre_index_in_track >= 0)
			{
				var pre_itemInfo = track.itemInfoes[this_itemInfo_pre_index_in_track] as SkinnedMeshRendererTimelinableItemInfo;

				if (!track.to_play_itemInfo_index_list.Contains(this_itemInfo_pre_index_in_track))
				{
					UpdateBlendShape(cur_time, pre_itemInfo.skinnedMeshRenderer_index, pre_itemInfo.blendShape_index,
					  blend_duration, time, pre_itemInfo.blendShape_weight, 0);
					for (int i = 0; i < pre_itemInfo.skinnedMeshRendererSubBlendShapeInfo_list.Count; i++)
					{
						var pre_skinnedMeshRendererSubBlendShapeInfo = pre_itemInfo.skinnedMeshRendererSubBlendShapeInfo_list[i];
						UpdateBlendShape(cur_time, pre_skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRenderer_index,
						  pre_skinnedMeshRendererSubBlendShapeInfo.blendShape_index,
						  blend_duration,
						  time, pre_itemInfo.blendShape_weight, 0);
					}
				}
			}

			//处理当前的itemInfo
			if (blend_duration > 0)
			{
				UpdateBlendShape(cur_time, skinnedMeshRenderer_index, blendShape_index,
				  blend_duration, time,
				  GetBlendShapeWeight(skinnedMeshRenderer_index, blendShape_index),
				  blendShape_weight);
				for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfo_list.Count; i++)
				{
					var skinnedMeshRendererSubBlendShapeInfo = skinnedMeshRendererSubBlendShapeInfo_list[i];
					UpdateBlendShape(cur_time, skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRenderer_index,
					  skinnedMeshRendererSubBlendShapeInfo.blendShape_index,
					  blend_duration, time,
					  GetBlendShapeWeight(skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRenderer_index,
						skinnedMeshRendererSubBlendShapeInfo.blendShape_index), blendShape_weight);
				}
			}
			else
			{
				UpdateBlendShape(skinnedMeshRenderer_index, blendShape_index,
				  blendShape_weight);
				for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfo_list.Count; i++)
				{
					var skinnedMeshRendererSubBlendShapeInfo = skinnedMeshRendererSubBlendShapeInfo_list[i];
					UpdateBlendShape(skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRenderer_index,
					  skinnedMeshRendererSubBlendShapeInfo.blendShape_index, blendShape_weight);
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
		void RegisterFixedWeightBlendShape(int skinnedMeshRenderer_index, int blendShape_index, float weight)
		{
			var key = new Vector2Int(skinnedMeshRenderer_index, blendShape_index);
			fixed_weight_blendShape_dict[key] = weight;
			UpdateBlendShape(skinnedMeshRenderer_index, blendShape_index, weight, false);
		}

		bool IsContainsFixedWeightBlendShape(int skinnedMeshRenderer_index, int blendShape_index)
		{
			var key = new Vector2Int(skinnedMeshRenderer_index, blendShape_index);
			return fixed_weight_blendShape_dict.ContainsKey(key);
		}

		private void ResetBlendShapes(SkinnedMeshRenderer skinnedMeshRenderer, int skinnedMeshRenderer_index)
		{
			if (skinnedMeshRenderer != null && skinnedMeshRenderer.sharedMesh != null)
			{
				for (int blendShape_index = 0;
				  blendShape_index < skinnedMeshRenderer.sharedMesh.blendShapeCount;
				  blendShape_index++)
				{
					if (!IsContainsFixedWeightBlendShape(skinnedMeshRenderer_index, blendShape_index))
						skinnedMeshRenderer.SetBlendShapeWeight(blendShape_index, 0);
				}
			}
		}

		public float GetBlendShapeWeight(int skinnedMeshRenderer_index, int blendShape_index)
		{
			if (skinnedMeshRenderer_index >= 0 && skinnedMeshRenderer_index < skinnedMeshRenderer_list.Count)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRenderer_list[skinnedMeshRenderer_index];
				if (skinnedMeshRenderer != null)
				{
					if (skinnedMeshRenderer.sharedMesh != null && blendShape_index >= 0 &&
						blendShape_index < skinnedMeshRenderer.sharedMesh.blendShapeCount)
						return skinnedMeshRenderer.GetBlendShapeWeight(blendShape_index);
				}
			}

			return 0;
		}

		public void UpdateBlendShape()
		{
			UpdateBlendShape(skinnedMeshRenderer_index, blendShape_index, blendShape_weight);
			for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfo_list.Count; i++)
			{
				var skinnedMeshRendererSubBlendShapeInfo = skinnedMeshRendererSubBlendShapeInfo_list[i];
				UpdateBlendShape(skinnedMeshRendererSubBlendShapeInfo.skinnedMeshRenderer_index,
				  skinnedMeshRendererSubBlendShapeInfo.blendShape_index, blendShape_weight);
			}
		}

		public void UpdateBlendShape(float time, int skinnedMeshRenderer_index, int blendShape_index, float blend_duration,
		  float itemInfo_time, float from_weight, float to_weight)
		{
			if (from_weight != to_weight)
			{
				float weight = to_weight;
				if (blend_duration > 0)
					weight = Mathf.Lerp(from_weight, to_weight, (time - itemInfo_time) / blend_duration);
				UpdateBlendShape(skinnedMeshRenderer_index, blendShape_index, weight);
			}
		}

		public void UpdateBlendShape(int skinnedMeshRenderer_index, int blendShap_index, float weight,
		  bool is_fixed_weight = true)
		{
			if (!IsContainsFixedWeightBlendShape(skinnedMeshRenderer_index, blendShap_index) || !is_fixed_weight)
			{
				if (skinnedMeshRenderer_index >= 0 && skinnedMeshRenderer_index < skinnedMeshRenderer_list.Count)
				{
					SkinnedMeshRenderer skinnedMeshRenderer = skinnedMeshRenderer_list[skinnedMeshRenderer_index];
					if (skinnedMeshRenderer != null && skinnedMeshRenderer.sharedMesh != null)
					{
						if (blendShap_index >= 0 && blendShap_index < skinnedMeshRenderer.sharedMesh.blendShapeCount)
						{
							if (skinnedMeshRenderer.GetBlendShapeWeight(blendShap_index) != weight)
								skinnedMeshRenderer.SetBlendShapeWeight(blendShap_index, weight);
						}
					}
				}
			}
		}

		public void ClearBlendShape()
		{
			UpdateBlendShape(skinnedMeshRenderer_index, blendShape_index, 0);
			for (int i = 0; i < skinnedMeshRendererSubBlendShapeInfo_list.Count; i++)
				UpdateBlendShape(skinnedMeshRendererSubBlendShapeInfo_list[i].skinnedMeshRenderer_index,
				  skinnedMeshRendererSubBlendShapeInfo_list[i].blendShape_index, 0);
		}
	}
}