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

		public static void Set(AudioSource audioSource, string assetPath,
			Action<AudioSource, AudioClip> onLoadSuccessCallback = null,
			Action<AudioSource, AudioClip> onLoadFailCallback = null,
			Action<AudioSource, AudioClip> onLoadDoneCallback = null)
		{
			;
			Set<AutoAssetSetAudioSourceAudioClip>(audioSource, assetPath, (component, asset) =>
			{
				SetAudioSourceAudioClip(component, asset);
				onLoadSuccessCallback?.Invoke(component, asset);
			}, onLoadFailCallback, onLoadDoneCallback);
		}


		public static IEnumerator SetAsync(AudioSource audioSource, string assetPath,
			Action<AudioSource, AudioClip> onLoadSuccessCallback = null,
			Action<AudioSource, AudioClip> onLoadFailCallback = null,
			Action<AudioSource, AudioClip> onLoadDoneCallback = null)
		{
			var isDone = false;
			Set<AutoAssetSetAudioSourceAudioClip>(audioSource, assetPath, (component, audioClip) =>
			{
				SetAudioSourceAudioClip(component, audioClip);
				onLoadSuccessCallback?.Invoke(component, audioClip);
			}, onLoadFailCallback, (component, audioClip) =>
			{
				onLoadDoneCallback?.Invoke(audioSource, audioClip);
				isDone = true;
			});
			while (!isDone)
				yield return 0;
		}
	}
}