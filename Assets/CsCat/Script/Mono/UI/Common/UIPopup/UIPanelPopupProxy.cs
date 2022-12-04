
using System;
using UnityEngine;

public class UIPanelPopupProxy
{
	private readonly UIPanelPopup _uiPanelPopup;
	public bool isPlaying;//(showing or hiding)
	public bool isHiding;

	public UIPanelPopupProxy(UIPanelPopup uiPanelPopup)
	{
		this._uiPanelPopup = uiPanelPopup;
	}

	public string playAnimationName
	{
		get => this._uiPanelPopup.playAnimationName;
		set => this._uiPanelPopup.playAnimationName = value;
	}

	public void EnableAnimator(bool isEnabled)
	{
		_uiPanelPopup.EnableAnimator(isEnabled);
	}

	public void SetOnShowStartAction(Action action)
	{
		this._uiPanelPopup.onShowStartAction = action;
	}

	public void SetOnShowFinishAction(Action action)
	{
		this._uiPanelPopup.onShowFinishAction = action;
	}

	public void SetOnHideStartAction(Action action)
	{
		this._uiPanelPopup.onHideStartAction = action;
	}

	public void SetOnHideFinishAction(Action action)
	{
		this._uiPanelPopup.onHideFinishAction = action;
	}

}
