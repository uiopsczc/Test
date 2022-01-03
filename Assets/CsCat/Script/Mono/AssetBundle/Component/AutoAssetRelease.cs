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
		  string asset_path) where TargetComponent_AssetObjectType : AutoAssetRelease<TargetComponent, AssetObjectType>
		{
			if (component == null || assetCat == null)
				return;

			var ai = component.GetComponent<TargetComponent_AssetObjectType>();
			if (ai != null)
				ai.ReleaseAll();
			else
				ai = component.gameObject.AddComponent<TargetComponent_AssetObjectType>();

			ai.SetCurAssetCat(assetCat, asset_path);
			assetCat.AddRefCount();
		}


		protected static bool Set<TargetComponent_AssetObjectType>(TargetComponent component, string asset_path,
		  Action<TargetComponent, AssetObjectType> on_load_success_callback,
		  Action<TargetComponent, AssetObjectType> on_load_fail_callback,
		  Action<TargetComponent, AssetObjectType> on_load_done_callback)
		  where TargetComponent_AssetObjectType : AutoAssetRelease<TargetComponent, AssetObjectType>
		{
			if (component == null)
			{
				LogCat.LogError("component == null ap:" + asset_path);
				return false;
			}

			TargetComponent_AssetObjectType auto_asset_release;
			if (asset_path.IsNullOrWhiteSpace())
			{
				auto_asset_release = component.GetComponent<TargetComponent_AssetObjectType>();
				if (auto_asset_release != null)
					auto_asset_release.ReleaseAll();
				on_load_success_callback(component, null);
				on_load_done_callback(component, null);
				return true;
			}

			auto_asset_release = component.gameObject.GetOrAddComponent<TargetComponent_AssetObjectType>();

			var cur_assetCat = auto_asset_release.cur_assetCat;
			var cur_asset_path = auto_asset_release.cur_asset_path;
			var new_assetCat = Client.instance.assetBundleManager.GetOrLoadAssetCat(asset_path.GetMainAssetPath());
			var new_asset_path = asset_path;


			if (cur_assetCat != null)
			{
				if (cur_asset_path.Equals(asset_path)) //cur_asset_path和asset_path相同
				{
					//加载成功的情况
					if (cur_assetCat.IsLoadSuccess())
					{
						auto_asset_release.ReleasePreAssetCat(); // 先清除之前的记录
						on_load_success_callback(component, cur_assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath()));
						on_load_done_callback(component, cur_assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath()));
						return true;
					}
					else
					{
						cur_assetCat.AddOnLoadSuccessCallback(asset_cat =>
						  on_load_success_callback(component, asset_cat.Get<AssetObjectType>(asset_path.GetSubAssetPath())), auto_asset_release);
						cur_assetCat.AddOnLoadFailCallback(asset_cat => on_load_fail_callback(component, null), auto_asset_release);
						cur_assetCat.AddOnLoadDoneCallback(asset_cat => on_load_done_callback(component,
						  cur_assetCat.IsLoadFail() ? null : asset_cat.Get<AssetObjectType>(asset_path.GetSubAssetPath())), auto_asset_release);
						return false;
					}
				}
				else //autoAssetRelease.assetCatCur.asset_path不等于assetPath
				{
					//加载成功的情况
					if (cur_assetCat.IsLoadSuccess())
					{
						auto_asset_release.ReleasePreAssetCat(); // 先清除之前的记录
						auto_asset_release.SetPreAssetCat(auto_asset_release.cur_assetCat, auto_asset_release.cur_asset_path);
					}
					else
					{
						//当前的没加载出来，直接进行ReleaseAssetCatCur,而assetCatPre不需要处理，因为当前显示为上次的assetCatPre，不然会出现空的情况
						auto_asset_release.ReleaseCurAssetCat();
					}
				}
			}


			auto_asset_release.SetCurAssetCat(new_assetCat, new_asset_path);
			if (new_assetCat != null)
			{
				//加载成功
				if (new_assetCat.IsLoadSuccess())
				{
					new_assetCat.AddRefCount();
					auto_asset_release.ReleasePreAssetCat();
					on_load_success_callback(component, new_assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath()));
					on_load_done_callback(component, new_assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath()));
					return true;
				}

				//加载中
				new_assetCat.AddOnLoadSuccessCallback(assetCat =>
				{
					assetCat.AddRefCount();
					if (auto_asset_release.cur_assetCat != assetCat)
						return;
					auto_asset_release.ReleasePreAssetCat();
					on_load_success_callback(component, assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath()));
				}, auto_asset_release);
				new_assetCat.AddOnLoadFailCallback(assetCat =>
				{
					on_load_fail_callback(component, assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath()));
				}, auto_asset_release);
				new_assetCat.AddOnLoadDoneCallback(assetCat =>
				{
					on_load_done_callback(component, assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath()));
				}, auto_asset_release);
				return false;
			}


			Client.instance.assetBundleManager.LoadAssetAsync(asset_path, assetCat =>
			  {
				  assetCat.AddRefCount();
				  if (auto_asset_release.cur_assetCat != assetCat)
					  return;
				  auto_asset_release.ReleasePreAssetCat();
				  on_load_success_callback(component, assetCat.Get<AssetObjectType>());
			  },
			  assetCat => { on_load_fail_callback(component, null); },
			  assetCat => { on_load_done_callback(component, assetCat.Get<AssetObjectType>(asset_path.GetSubAssetPath())); }, auto_asset_release);

			return false;
		}
	}
}