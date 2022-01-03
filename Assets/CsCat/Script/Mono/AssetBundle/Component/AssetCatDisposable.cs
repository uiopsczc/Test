using UnityEngine;

namespace CsCat
{
	public class AssetCatDisposable : MonoBehaviour
	{
		public string cur_asset_path;

		public string pre_asset_path;

		//当前使用的资源
		protected AssetCat cur_assetCat; //可能是加载完成状态或者加载中状态

		// 上一次使用的资源，只有当assetCatCur加载出asset后且在下次进行assetCatCur赋予新值，才会进行assetCatPre.SubRefCnt（并不是assetCatCur加载出asset后就进行assetCatPre.SubRefCnt，而是在assetCatCur下次进行新的赋值时进行assetCatPre.SubRefCnt）
		protected AssetCat pre_assetCat; //只能是加载完成个状态


		private void OnDestroy()
		{
			ReleaseAll();
		}

		public void SetCurAssetCat(AssetCat cur_assetCat, string cur_asset_path)
		{
			this.cur_assetCat = cur_assetCat;
			this.cur_asset_path = cur_asset_path;
		}

		public void SetPreAssetCat(AssetCat pre_assetCat, string pre_asset_path)
		{
			this.pre_assetCat = pre_assetCat;
			this.pre_asset_path = pre_asset_path;
		}

		public void ReleaseCurAssetCat()
		{
			if (cur_assetCat != null)
			{
				if (!cur_assetCat.IsLoadSuccess())
					cur_assetCat.RemoveCallback(this);
				cur_assetCat.SubRefCount(1, true);
			}

			cur_asset_path = null;
			cur_assetCat = null;
		}

		public void ReleasePreAssetCat()
		{
			if (pre_assetCat != null)
			{
				if (!pre_assetCat.IsLoadSuccess())
					pre_assetCat.RemoveCallback(this);
				pre_assetCat.SubRefCount(1, true);
			}

			pre_asset_path = null;
			pre_assetCat = null;
		}

		public void ReleaseAll()
		{
			ReleaseCurAssetCat();
			ReleasePreAssetCat();
		}
	}
}