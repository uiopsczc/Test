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

		protected override bool IsNeedUpdateChildren()
		{
			return false;
		}

		protected override void _PostSetGameObject()
		{
			base._PostSetGameObject();
			_RemoveUnityListeners();
			_AddUnityListeners();
		}

		protected virtual void _AddUnityListeners()
		{

		}
		protected virtual void _RemoveUnityListeners()
		{
			for (var i = 0; i < _registeredUGUIEventListenerList.Count; i++)
			{
				var uguiEventListener = _registeredUGUIEventListenerList[i];
				uguiEventListener.Destroy();
			}

			_registeredUGUIEventListenerList.Clear();
		}

		protected virtual void _AddGameListeners()
		{
		}

		protected virtual void _RemoveGameListeners()
		{
		}

		protected virtual void _AddTimers()
		{
		}

		protected virtual void _RemoveTimers()
		{
		}

		


		protected void _SetImageAsync(Image image, string assetPath, Action<Image> callback = null,
		  bool isSetNativeSize = false)
		{
			GetChild<ResLoadDictTreeNode>().GetOrLoadAsset(assetPath.GetMainAssetPath(), assetCat =>
			{
				if (image == null)
					return;
				image.sprite = assetCat.Get<Sprite>(assetPath.GetSubAssetPath());
				if (isSetNativeSize)
					image.SetNativeSize();
				callback?.Invoke(image);
			}, null, null, this);
		}

		protected void _SetRawImageAsync(RawImage image, string assetPath, Action<RawImage> callback = null,
		  bool isSetNativeSize = false)
		{
			GetChild<ResLoadDictTreeNode>().GetOrLoadAsset(assetPath, assetCat =>
			{
				if (image == null)
					return;
				image.texture = assetCat.Get<Texture>(assetPath.GetSubAssetPath());
				if (isSetNativeSize)
					image.SetNativeSize();
				callback?.Invoke(image);
			}, null, null, this);
		}


		protected override void _Reset()
		{
			_RemoveUnityListeners();
			_RemoveGameListeners();
			_RemoveTimers();
			base._Reset();
		}

		protected override void _Destroy()
		{
			_RemoveUnityListeners();
			_RemoveGameListeners();
			_RemoveTimers();
			base._Destroy();
		}
	}
}



