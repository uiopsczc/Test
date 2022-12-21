using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
	public class AutoAssetSetImageSprite : AutoAssetRelease<Image, Sprite>
	{
		private static void _SetImageSprite(Image component, Sprite asset, bool isSetNativeSize, Vector2 newSize)
		{
			component.sprite = asset;
			if (isSetNativeSize)
				component.SetNativeSize();
			else
				component.GetComponent<RectTransform>().sizeDelta = newSize;
		}

		public static void Set(Image image, string assetPath, bool isSetNativeSize, Vector2 newSize,
		  Action<Image, Sprite> onLoadSuccessCallback = null,
		  Action<Image, Sprite> onLoadFailCallback = null,
		  Action<Image, Sprite> onLoadDoneCallback = null)
		{
			_Set<AutoAssetSetImageSprite>(image, assetPath, (component, asset) =>
			{
				_SetImageSprite(component, asset, isSetNativeSize, newSize);
				onLoadSuccessCallback?.Invoke(component, asset);
			}, onLoadFailCallback, onLoadDoneCallback);
		}

		public static void Set(Image image, string assetPath, bool isSetNativeSize = false,
		  Action<Image, Sprite> onLoadSuccessCallback = null,
		  Action<Image, Sprite> onLoadFailCallback = null,
		  Action<Image, Sprite> onLoadDoneCallback = null)
		{
			Set(image, assetPath, isSetNativeSize, image.GetComponent<RectTransform>().sizeDelta, onLoadSuccessCallback, onLoadFailCallback, onLoadDoneCallback);
		}

		public static IEnumerator SetAsync(Image image, string assetPath, bool isSetNativeSize, Vector2 newSize,
		  Action<Image, Sprite> onLoadSuccessCallback = null,
		  Action<Image, Sprite> onLoadFailCallback = null,
		  Action<Image, Sprite> onLoadDoneCallback = null)
		{
			var is_done = false;
			_Set<AutoAssetSetImageSprite>(image, assetPath, (component, asset) =>
			{
				_SetImageSprite(component, asset, isSetNativeSize, newSize);
				onLoadSuccessCallback?.Invoke(component, asset);
			}, onLoadFailCallback, (component, asset) => { is_done = true; });
			while (!is_done)
				yield return 0;
		}


		public static IEnumerator SetAsync(Image image, string assetPath, bool isSetNativeSize = false)
		{
			return SetAsync(image, assetPath, isSetNativeSize, image.GetComponent<RectTransform>().sizeDelta);
		}
	}
}