using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CsCat
{
  public class AutoAssetSetImageSprite : AutoAssetRelease<Image, Sprite>
  {
    private static void SetImageSprite(Image component, Sprite asset, bool is_set_native_size, Vector2 new_size)
    {
      component.sprite = asset;
      if (is_set_native_size)
        component.SetNativeSize();
      else
        component.GetComponent<RectTransform>().sizeDelta = new_size;
    }

    public static void Set(Image image, string asset_path, bool is_set_native_size, Vector2 new_size,
      Action<Image, Sprite> on_load_success_callback = null,
      Action<Image, Sprite> on_load_fail_callback = null,
      Action<Image, Sprite> on_load_done_callback = null)
    {
      Set<AutoAssetSetImageSprite>(image, asset_path, (component, asset) =>
      {
        SetImageSprite(component, asset, is_set_native_size, new_size);
        on_load_success_callback?.Invoke(component, asset);
      }, on_load_fail_callback, on_load_done_callback);
    }

    public static void Set(Image image, string asset_path, bool is_set_native_size = false,
      Action<Image, Sprite> on_load_success_callback = null,
      Action<Image, Sprite> on_load_fail_callback = null,
      Action<Image, Sprite> on_load_done_callback = null)
    {
      Set(image, asset_path, is_set_native_size, image.GetComponent<RectTransform>().sizeDelta, on_load_success_callback, on_load_fail_callback, on_load_done_callback);
    }

    public static IEnumerator SetAsync(Image image, string asset_path, bool is_set_native_size, Vector2 new_size,
      Action<Image, Sprite> on_load_success_callback = null,
      Action<Image, Sprite> on_load_fail_callback = null,
      Action<Image, Sprite> on_load_done_callback = null)
    {
      var is_done = false;
      Set<AutoAssetSetImageSprite>(image, asset_path, (component, asset) =>
      {
        SetImageSprite(component, asset, is_set_native_size, new_size);
        on_load_success_callback?.Invoke(component, asset);
      }, on_load_fail_callback, (component, asset) => { is_done = true; });
      while (!is_done)
        yield return 0;
    }


    public static IEnumerator SetAsync(Image image, string asset_path, bool is_set_native_size = false)
    {
      return SetAsync(image, asset_path, is_set_native_size, image.GetComponent<RectTransform>().sizeDelta);
    }
  }
}