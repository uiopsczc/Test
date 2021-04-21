using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class AutoAssetSetAnimatorRuntimeAnimatorController : AutoAssetRelease<Animator, RuntimeAnimatorController>
  {
    private static void SetAnimatorRuntimeAnimatorController(Animator component, RuntimeAnimatorController asset)
    {
      component.runtimeAnimatorController = asset;
    }

    public static void Set(Animator animator, string asset_path,
      Action<Animator, RuntimeAnimatorController> on_load_success_callback = null, Action<Animator, RuntimeAnimatorController> on_load_fail_callback = null, Action<Animator, RuntimeAnimatorController> on_load_done_callback = null)
    {
      Set<AutoAssetSetAnimatorRuntimeAnimatorController>(animator, asset_path, (component, asset) =>
      {
        SetAnimatorRuntimeAnimatorController(component, asset);
        on_load_success_callback?.Invoke(component, asset);
      }, on_load_fail_callback, on_load_done_callback);
    }


    public static IEnumerator SetAsync(Animator animator, string asset_path,
      Action<Animator, RuntimeAnimatorController> on_load_success_callback = null,
      Action<Animator, RuntimeAnimatorController> on_load_fail_callback = null,
      Action<Animator, RuntimeAnimatorController> on_load_done_callback = null)
    {
      var is_done = false;
      Set<AutoAssetSetAnimatorRuntimeAnimatorController>(animator, asset_path, (component, asset) =>
      {
        SetAnimatorRuntimeAnimatorController(component, asset);
        on_load_success_callback?.Invoke(component, asset);
      }, on_load_fail_callback, (component, asset) =>
      {
        on_load_done_callback?.Invoke(component, asset);
        is_done = true;
      });
      while (!is_done)
        yield return 0;
    }
  }
}