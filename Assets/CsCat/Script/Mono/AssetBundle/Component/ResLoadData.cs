using System;

namespace CsCat
{
	public class ResLoadData
	{
		public AssetCat assetCat;
		public int refCount;

		public ResLoadData(AssetCat assetCat)
		{
			this.assetCat = assetCat;
			assetCat.AddRefCount();
		}

		public void AddRefCount()
		{
			refCount++;
		}

		public void SubRefCount(int subValue = 1)
		{
			subValue = Math.Abs(subValue);
			refCount = refCount - subValue;
		}

		public void Destroy()
		{
			refCount = 0;
			assetCat.SubRefCount(1, true);
		}
	}
}