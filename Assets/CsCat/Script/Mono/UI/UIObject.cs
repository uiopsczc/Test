using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIObject : GameEntity
	{
		public UIObject parentUIObject => _cache.GetOrAddDefault("parent_uiObject", () => parent as UIObject);

		public UIPanel parentUIPanel => _cache.GetOrAddDefault("parent_uiPanel", () => parent as UIPanel);

		protected override GraphicComponent CreateGraphicComponent()
		{
			return this.AddComponent<UIGraphicComponent>(null, this.resLoadComponent);
		}

		public virtual void Open()
		{
			this.AddUnityEvents();
			this.AddGameEvents();
		}

		protected virtual void AddUnityEvents()
		{
		}
		protected virtual void AddGameEvents()
		{
		}


		public void SetImageAsync(Image image, string assetPath, Action<Image> callbak = null,
		  bool isSetNativeSize = true)
		{
			resLoadComponent.GetOrLoadAsset(assetPath.GetMainAssetPath(), (assetCat) =>
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
		  bool isSetNativeSize = true)
		{
			resLoadComponent.GetOrLoadAsset(assetPath, (assetCat) =>
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
			for (var i = 0; i < registeredUGUIEventListenerList.Count; i++)
			{
				var uguiEventListener = registeredUGUIEventListenerList[i];
				uguiEventListener.Destroy();
			}

			registeredUGUIEventListenerList.Clear();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			for (var i = 0; i < registeredUGUIEventListenerList.Count; i++)
			{
				var uguiEventListener = registeredUGUIEventListenerList[i];
				uguiEventListener.Destroy();
			}

			registeredUGUIEventListenerList.Clear();
		}
	}
}



