using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class AutoAssetSetAudioSourceAudioClip : AutoAssetRelease<AudioSource, AudioClip>
	{
		private static void _SetAudioSourceAudioClip(AudioSource component, AudioClip audioClip)
		{
			component.clip = audioClip;
		}

		public static void Set(AudioSource audioSource, string assetPath,
			Action<AudioSource, AudioClip> onLoadSuccessCallback = null,
			Action<AudioSource, AudioClip> onLoadFailCallback = null,
			Action<AudioSource, AudioClip> onLoadDoneCallback = null)
		{
			;
			_Set<AutoAssetSetAudioSourceAudioClip>(audioSource, assetPath, (component, asset) =>
			{
				_SetAudioSourceAudioClip(component, asset);
				onLoadSuccessCallback?.Invoke(component, asset);
			}, onLoadFailCallback, onLoadDoneCallback);
		}


		public static IEnumerator SetAsync(AudioSource audioSource, string assetPath,
			Action<AudioSource, AudioClip> onLoadSuccessCallback = null,
			Action<AudioSource, AudioClip> onLoadFailCallback = null,
			Action<AudioSource, AudioClip> onLoadDoneCallback = null)
		{
			var isDone = false;
			_Set<AutoAssetSetAudioSourceAudioClip>(audioSource, assetPath, (component, audioClip) =>
			{
				_SetAudioSourceAudioClip(component, audioClip);
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