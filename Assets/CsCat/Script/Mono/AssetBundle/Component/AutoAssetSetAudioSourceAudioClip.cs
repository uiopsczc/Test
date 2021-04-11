using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
  public class AutoAssetSetAudioSourceAudioClip : AutoAssetRelease<AudioSource, AudioClip>
  {
    private static void SetAudioSourceAudioClip(AudioSource component, AudioClip audioClip)
    {
      component.clip = audioClip;
    }

    public static void Set(AudioSource audioSource, string asset_path,
      Action<AudioSource, AudioClip> on_load_success_callback = null,
      Action<AudioSource, AudioClip> on_load_fail_callback = null,
      Action<AudioSource, AudioClip> on_load_done_callback = null)
    {
      ;
      Set<AutoAssetSetAudioSourceAudioClip>(audioSource, asset_path, (component, asset) =>
      {
        SetAudioSourceAudioClip(component, asset);
        on_load_success_callback?.Invoke(component, asset);
      }, on_load_fail_callback, on_load_done_callback);
    }


    public static IEnumerator SetAsync(AudioSource audioSource, string assetPath,
      Action<AudioSource, AudioClip> on_load_success_callback = null,
      Action<AudioSource, AudioClip> on_load_fail_callback = null,
    Action<AudioSource, AudioClip> on_load_done_callback = null)
    {
      var is_done = false;
      Set<AutoAssetSetAudioSourceAudioClip>(audioSource, assetPath, (component, audioClip) =>
      {
        SetAudioSourceAudioClip(component, audioClip);
        on_load_success_callback?.Invoke(component, audioClip);
      }, on_load_fail_callback, (component, audioClip) =>
      {
        on_load_done_callback?.Invoke(audioSource, audioClip);
        is_done = true;
      });
      while (!is_done)
        yield return 0;
    }
  }
}