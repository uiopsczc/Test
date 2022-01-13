
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class SkinnedMeshRendererTimelinableItemInfoLibrary : TimelinableItemInfoLibraryBase
	{
		[SerializeField]
		private SkinnedMeshRendererTimelinableItemInfo[] _itemInfoes = new SkinnedMeshRendererTimelinableItemInfo[0];

		public override TimelinableItemInfoBase[] itemInfoes
		{
			get => _itemInfoes;
			set { _itemInfoes = value as SkinnedMeshRendererTimelinableItemInfo[]; }
		}
	}
}



