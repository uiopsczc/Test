
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public class MountTimelinableItemInfoLibrary : TimelinableItemInfoLibraryBase
	{
		[SerializeField]
		private MountTimelinableItemInfo[] _itemInfoes = new MountTimelinableItemInfo[0];

		public override TimelinableItemInfoBase[] itemInfoes
		{
			get { return _itemInfoes; }
			set { _itemInfoes = value as MountTimelinableItemInfo[]; }
		}
	}
}



