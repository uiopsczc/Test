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

		public static void Set(Animator animator, string assetPath,
		  Action<Animator, RuntimeAnimatorController> onLoadSuccessCallback = null, Action<Animator, RuntimeAnimatorController> onLoadFailCallback = null, Action<Animator, RuntimeAnimatorController> onLoadDoneCallback = null)
		{
			Set<AutoAssetSetAnimatorRuntimeAnimatorController>(animator, assetPath, (component, asset) =>
			{
				SetAnimatorRuntimeAnimatorController(component, asset);
				onLoadSuccessCallback?.Invoke(component, asset);
			}, onLoadFailCallback, onLoadDoneCallback);
		}


		public static IEnumerator SetAsync(Animator animator, string assetPath,
		  Action<Animator, RuntimeAnimatorController> onLoadSuccessCallback = null,
		  Action<Animator, RuntimeAnimatorController> onLoadFailCallback = null,
		  Action<Animator, RuntimeAnimatorController> onLoadDoneCallback = null)
		{
			var isDone = false;
			Set<AutoAssetSetAnimatorRuntimeAnimatorController>(animator, assetPath, (component, asset) =>
			{
				SetAnimatorRuntimeAnimatorController(component, asset);
				onLoadSuccessCallback?.Invoke(component, asset);
			}, onLoadFailCallback, (component, asset) =>
			{
				onLoadDoneCallback?.Invoke(component, asset);
				isDone = true;
			});
			while (!isDone)
				yield return 0;
		}
	}
}