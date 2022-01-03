using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CsCat
{
	public partial class UIObject : GameEntity
	{
		public UIObject parent_uiObject
		{
			get { return cache.GetOrAddDefault("parent_uiObject", () => parent as UIObject); }
		}

		public UIPanel parent_uiPanel
		{
			get { return cache.GetOrAddDefault("parent_uiPanel", () => parent as UIPanel); }
		}

		protected override GraphicComponent CreateGraphicComponent()
		{
			return this.AddComponent<UIGraphicComponent>(null, this.resLoadComponent);
		}

		public virtual void Open()
		{
			this.AddUntiyEvnts();
			this.AddGameEvents();
		}

		protected virtual void AddUntiyEvnts()
		{
		}
		protected virtual void AddGameEvents()
		{
		}


		public void SetImageAsync(Image image, string asset_path, Action<Image> callbak = null,
		  bool is_setNativeSize = true)
		{
			resLoadComponent.GetOrLoadAsset(asset_path.GetMainAssetPath(), (assetCat) =>
			{
				if (image == null)
					return;
				image.sprite = assetCat.Get<Sprite>(asset_path.GetSubAssetPath());
				if (is_setNativeSize)
					image.SetNativeSize();
				callbak?.Invoke(image);
			}, null, null, this);
		}

		public void SetRawImageAsync(RawImage image, string asset_path, Action<RawImage> callbak = null,
		  bool is_setNativeSize = true)
		{
			resLoadComponent.GetOrLoadAsset(asset_path, (assetCat) =>
			{
				if (image == null)
					return;
				image.texture = assetCat.Get<Texture>(asset_path.GetSubAssetPath());
				if (is_setNativeSize)
					image.SetNativeSize();
				callbak?.Invoke(image);
			}, null, null, this);
		}



		protected override void _Reset()
		{
			base._Reset();
			foreach (var uguiEventListener in registered_uguiEventListener_list)
				uguiEventListener.Destroy();
			registered_uguiEventListener_list.Clear();
		}

		protected override void _Destroy()
		{
			base._Destroy();
			foreach (var uguiEventListener in registered_uguiEventListener_list)
				uguiEventListener.Destroy();
			registered_uguiEventListener_list.Clear();
		}
	}
}



