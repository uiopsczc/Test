
using System;
using CsCat;
using UnityEngine;

public class UIPanelPopupProxyTreeNode:TreeNode
{
	private UIPanelPopupProxy _uiPanelPopupProxy;

	public bool isPlaying => this._uiPanelPopupProxy.isPlaying;
	public bool isHiding => this._uiPanelPopupProxy.isHiding;

	protected void _Init(UIPanelPopup uiPanelPopup)
	{
		base._Init();
		this._uiPanelPopupProxy = new UIPanelPopupProxy(uiPanelPopup);
	}

	public string playAnimationName
	{
		get => this._uiPanelPopupProxy.playAnimationName;
		set => this._uiPanelPopupProxy.playAnimationName = value;
	}

	public void EnableAnimator(bool isEnabled)
	{
		_uiPanelPopupProxy.EnableAnimator(isEnabled);
	}

	public void SetOnShowStartAction(Action action)
	{
		this._uiPanelPopupProxy.SetOnShowStartAction(action);
	}

	public void SetOnShowFinishAction(Action action)
	{
		this._uiPanelPopupProxy.SetOnShowFinishAction(action);
	}

	public void SetOnHideStartAction(Action action)
	{
		this._uiPanelPopupProxy.SetOnHideStartAction(action);
	}

	public void SetOnHideFinishAction(Action action)
	{
		this._uiPanelPopupProxy.SetOnHideFinishAction(action);
	}

}
