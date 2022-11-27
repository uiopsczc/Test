using System;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIObject : CommonViewTreeNode
	{
		public UIObject parentUIObject => _cache.GetOrAddDefault("parentUIObject", () => GetParent<UIObject>());

		public UIPanel parentUIPanel => _cache.GetOrAddDefault("parentUIPanel", () => GetParent<UIPanel>());

		protected virtual Transform _contentTransform
		{
			get{return _cache.GetOrAddDefault("contentTransform", () => GetTransform().Find("Nego_Content"));}
		}

		protected virtual void AddUnityListeners()
		{

		}
		protected virtual void RemoveUnityListeners()
		{
			for (var i = 0; i < _registeredUGUIEventListenerList.Count; i++)
			{
				var uguiEventListener = _registeredUGUIEventListenerList[i];
				uguiEventListener.Destroy();
			}

			_registeredUGUIEventListenerList.Clear();
		}

		protected virtual void AddGameListeners()
		{
		}

		protected virtual void RemoveGameListeners()
		{
		}

		protected virtual void AddTimers()
		{
		}

		protected virtual void RemoveTimers()
		{
		}

		


		public void SetImageAsync(Image image, string assetPath, Action<Image> callbak = null,
		  bool isSetNativeSize = false)
		{
			GetChild<ResLoadDictTreeNode>().GetOrLoadAsset(assetPath.GetMainAssetPath(), assetCat =>
			{
				if (image == null)
					return;
				image.sprite = assetCat.Get<Sprite>(assetPath.GetSubAssetPath());
				if (isSetNativeSize)
					image.SetNativeSize();
				callbak?.Invoke(image);
			}, null, null, this);
		}

		public void SetRawImageAsync(RawImage image, string assetPath, Action<RawImage> callbak = null,
		  bool isSetNativeSize = false)
		{
			GetChild<ResLoadDictTreeNode>().GetOrLoadAsset(assetPath, assetCat =>
			{
				if (image == null)
					return;
				image.texture = assetCat.Get<Texture>(assetPath.GetSubAssetPath());
				if (isSetNativeSize)
					image.SetNativeSize();
				callbak?.Invoke(image);
			}, null, null, this);
		}



		protected override void _Reset()
		{
			base._Reset();
			RemoveUnityListeners();
			RemoveGameListeners();
			RemoveTimers();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			RemoveUnityListeners();
			RemoveGameListeners();
			RemoveTimers();
		}
	}
}



