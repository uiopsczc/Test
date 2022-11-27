
using System;
using CsCat;
using NPOI.SS.Formula.Functions;
using UnityEngine;

public class UIPanelPopup : MonoBehaviour
{
	[HideInInspector]
	public Action onShowStartAction;
	[HideInInspector]
	public Action onShowFinishAction;
	[HideInInspector]
	public Action onHideStartAction;
	[HideInInspector]
	public Action onHideFinishAction;

	public GameObject targetGameObject = null;
	private Animator _popupAnimator = null;

	void Awake()
	{
		var gameObject = targetGameObject == null ? this.gameObject : targetGameObject;
		_popupAnimator = gameObject.GetComponent<Animator>();
	}

	public string playAnimationName
	{
		get => _popupAnimator.GetBool("IsAlertness")?AnimationNameConst.Enter:AnimationNameConst.Quit;
		set
		{
			var boolValue = value == AnimationNameConst.Enter;
			if (_popupAnimator.gameObject.activeInHierarchy && _popupAnimator.GetBool("IsAlertness") != boolValue)
			{
				EnableAnimator(true);
				_popupAnimator.SetBool("IsAlertness", boolValue);
			}
		}
	}

	public void OnShowStart()
	{
		onShowStartAction?.Invoke();
	}

	public void OnShowFinish()
	{
		onShowFinishAction?.Invoke();
	}

	public void OnHideStart()
	{
		onHideStartAction?.Invoke();
	}

	public void OnHideFinish()
	{
		onHideFinishAction?.Invoke();
	}

	public void EnableAnimator(bool isEnabled)
	{
		_popupAnimator.enabled = isEnabled;
	}

	public void OnDestroy()
	{
	}
}
