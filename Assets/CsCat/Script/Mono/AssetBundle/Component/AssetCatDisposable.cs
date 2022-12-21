using UnityEngine;

namespace CsCat
{
	public class AssetCatDisposable : MonoBehaviour
	{
		public string curAssetPath;

		public string preAssetPath;

		//当前使用的资源
		protected AssetCat _curAssetCat; //可能是加载完成状态或者加载中状态

		// 上一次使用的资源，只有当assetCatCur加载出asset后且在下次进行assetCatCur赋予新值，才会进行assetCatPre.SubRefCnt（并不是assetCatCur加载出asset后就进行assetCatPre.SubRefCnt，而是在assetCatCur下次进行新的赋值时进行assetCatPre.SubRefCnt）
		protected AssetCat _preAssetCat; //只能是加载完成个状态


		private void OnDestroy()
		{
			ReleaseAll();
		}

		public void SetCurAssetCat(AssetCat curAssetCat, string curAssetPath)
		{
			this._curAssetCat = curAssetCat;
			this.curAssetPath = curAssetPath;
		}

		public void SetPreAssetCat(AssetCat preAssetCat, string preAssetPath)
		{
			this._preAssetCat = preAssetCat;
			this.preAssetPath = preAssetPath;
		}

		public void ReleaseCurAssetCat()
		{
			if (_curAssetCat != null)
			{
				if (!_curAssetCat.IsLoadSuccess())
					_curAssetCat.RemoveCallback(this);
				_curAssetCat.SubRefCount(1, true);
			}

			curAssetPath = null;
			_curAssetCat = null;
		}

		public void ReleasePreAssetCat()
		{
			if (_preAssetCat != null)
			{
				if (!_preAssetCat.IsLoadSuccess())
					_preAssetCat.RemoveCallback(this);
				_preAssetCat.SubRefCount(1, true);
			}

			preAssetPath = null;
			_preAssetCat = null;
		}

		public void ReleaseAll()
		{
			ReleaseCurAssetCat();
			ReleasePreAssetCat();
		}
	}
}