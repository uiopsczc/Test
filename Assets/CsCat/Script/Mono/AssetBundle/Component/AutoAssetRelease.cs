using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
	//基类，该类不是组件供使用
	public class AutoAssetRelease<TargetComponent, AssetObjectType> : AssetCatDisposable
		where TargetComponent : Component where AssetObjectType : Object
	{
		protected static void Set<TargetComponent_AssetObjectType>(TargetComponent component, AssetCat assetCat,
			string assetPath) where TargetComponent_AssetObjectType : AutoAssetRelease<TargetComponent, AssetObjectType>
		{
			if (component == null || assetCat == null)
				return;

			var ai = component.GetComponent<TargetComponent_AssetObjectType>();
			if (ai != null)
				ai.ReleaseAll();
			else
				ai = component.gameObject.AddComponent<TargetComponent_AssetObjectType>();

			ai.SetCurAssetCat(assetCat, assetPath);
			assetCat.AddRefCount();
		}


		protected static bool Set<TargetComponent_AssetObjectType>(TargetComponent component, string assetPath,
			Action<TargetComponent, AssetObjectType> onLoadSuccessCallback,
			Action<TargetComponent, AssetObjectType> onLoadFailCallback,
			Action<TargetComponent, AssetObjectType> onLoadDoneCallback)
			where TargetComponent_AssetObjectType : AutoAssetRelease<TargetComponent, AssetObjectType>
		{
			if (component == null)
			{
				LogCat.LogError("component == null ap:" + assetPath);
				return false;
			}

			TargetComponent_AssetObjectType autoAssetRelease;
			if (assetPath.IsNullOrWhiteSpace())
			{
				autoAssetRelease = component.GetComponent<TargetComponent_AssetObjectType>();
				if (autoAssetRelease != null)
					autoAssetRelease.ReleaseAll();
				onLoadSuccessCallback(component, null);
				onLoadDoneCallback(component, null);
				return true;
			}

			autoAssetRelease = component.gameObject.GetOrAddComponent<TargetComponent_AssetObjectType>();

			var curAssetCat = autoAssetRelease.curAssetCat;
			var curAssetPath = autoAssetRelease.curAssetPath;
			var newAssetCat = Client.instance.assetBundleManager.GetOrLoadAssetCat(assetPath.GetMainAssetPath());
			var newAssetPath = assetPath;


			if (curAssetCat != null)
			{
				if (curAssetPath.Equals(assetPath)) //cur_asset_path和asset_path相同
				{
					//加载成功的情况
					if (curAssetCat.IsLoadSuccess())
					{
						autoAssetRelease.ReleasePreAssetCat(); // 先清除之前的记录
						onLoadSuccessCallback(component, curAssetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
						onLoadDoneCallback(component, curAssetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
						return true;
					}

					curAssetCat.AddOnLoadSuccessCallback(assetCat =>
							onLoadSuccessCallback(component,
								assetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath())),
						autoAssetRelease);
					curAssetCat.AddOnLoadFailCallback(assetCat => onLoadFailCallback(component, null),
						autoAssetRelease);
					curAssetCat.AddOnLoadDoneCallback(assetCat => onLoadDoneCallback(component,
							curAssetCat.IsLoadFail()
								? null
								: assetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath())),
						autoAssetRelease);
					return false;
				}

				//autoAssetRelease.assetCatCur.asset_path不等于assetPath
				//加载成功的情况
				if (curAssetCat.IsLoadSuccess())
				{
					autoAssetRelease.ReleasePreAssetCat(); // 先清除之前的记录
					autoAssetRelease.SetPreAssetCat(autoAssetRelease.curAssetCat, autoAssetRelease.curAssetPath);
				}
				else
				{
					//当前的没加载出来，直接进行ReleaseAssetCatCur,而assetCatPre不需要处理，因为当前显示为上次的assetCatPre，不然会出现空的情况
					autoAssetRelease.ReleaseCurAssetCat();
				}
			}


			autoAssetRelease.SetCurAssetCat(newAssetCat, newAssetPath);
			if (newAssetCat != null)
			{
				//加载成功
				if (newAssetCat.IsLoadSuccess())
				{
					newAssetCat.AddRefCount();
					autoAssetRelease.ReleasePreAssetCat();
					onLoadSuccessCallback(component, newAssetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
					onLoadDoneCallback(component, newAssetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
					return true;
				}

				//加载中
				newAssetCat.AddOnLoadSuccessCallback(assetCat =>
				{
					assetCat.AddRefCount();
					if (autoAssetRelease.curAssetCat != assetCat)
						return;
					autoAssetRelease.ReleasePreAssetCat();
					onLoadSuccessCallback(component, assetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
				}, autoAssetRelease);
				newAssetCat.AddOnLoadFailCallback(
					assetCat =>
					{
						onLoadFailCallback(component, assetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
					}, autoAssetRelease);
				newAssetCat.AddOnLoadDoneCallback(
					assetCat =>
					{
						onLoadDoneCallback(component, assetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
					}, autoAssetRelease);
				return false;
			}


			Client.instance.assetBundleManager.LoadAssetAsync(assetPath, assetCat =>
				{
					assetCat.AddRefCount();
					if (autoAssetRelease.curAssetCat != assetCat)
						return;
					autoAssetRelease.ReleasePreAssetCat();
					onLoadSuccessCallback(component, assetCat.Get<AssetObjectType>());
				},
				assetCat => { onLoadFailCallback(component, null); },
				assetCat =>
				{
					onLoadDoneCallback(component, assetCat.Get<AssetObjectType>(assetPath.GetSubAssetPath()));
				}, autoAssetRelease);

			return false;
		}
	}
}