using System;

namespace CsCat
{
	public partial class UIPanel
	{
		public bool _isPlayingPopupAnimation;
		private bool _isHiding;
		private Action _onPopupShowStartAction;
		private Action _onPopupShowFinishAction;
		private Action _onPopupHideStartAction;
		private Action _onPopupHideFinishAction;

		protected void _CheckPopupAnimation()
		{
			var uiPanelPopup = this.GetGameObject().GetComponent<UIPanelPopup>();
			if (uiPanelPopup == null)
				return;
			var uiPanelPopupProxyTreeNode = this.AddChild<UIPanelPopupProxyTreeNode>(null, uiPanelPopup);
			uiPanelPopupProxyTreeNode.SetOnShowStartAction(_OnPopupShowStart);
			uiPanelPopupProxyTreeNode.SetOnShowFinishAction(_OnPopupShowFinish);
			uiPanelPopupProxyTreeNode.SetOnHideStartAction(_OnPopupHideStart);
			uiPanelPopupProxyTreeNode.SetOnHideFinishAction(_OnPopupHideFinish);
			_PlayPopupAnimation(AnimationNameConst.Enter);
		}

		private void _PlayPopupAnimation(string animationName)
		{
			if (animationName.Equals(AnimationNameConst.Quit))
				_isHiding = true;
			Client.instance.uiManager.PopupPreparedCheck(this);
			this.GetChild<UIPanelPopupProxyTreeNode>().playAnimationName = animationName;
			this._isPlayingPopupAnimation = true;
		}

		private bool _IsHasPopup()
		{
			return this.GetChild<UIPanelPopupProxyTreeNode>() != null;
		}

		private void _OnPopupShowStart()
		{
			_onPopupShowStartAction?.Invoke();
		}

		public void SetPopupShowAction(Action action)
		{
			_onPopupShowStartAction = action;
		}

		private void _OnPopupShowFinish()
		{
			this._isPlayingPopupAnimation = false;
			_onPopupShowFinishAction?.Invoke();
			Client.instance.uiManager.PopupPreparedCheck(this);
		}

		public void SetPopupFinishAction(Action action)
		{
			_onPopupShowFinishAction = action;
		}

		private void _OnPopupHideStart()
		{
			_onPopupHideStartAction?.Invoke();
		}

		public void SetPopupHideStartAction(Action action)
		{
			_onPopupHideStartAction = action;
		}

		private void _OnPopupHideFinish()
		{
			this._isPlayingPopupAnimation = false;
			this._isHiding = false;
			var onPopupHideFinishAction = this._onPopupHideFinishAction;
			Client.instance.uiManager.CloseChildPanel(this.GetKey());
			onPopupHideFinishAction?.Invoke();
		}

		public void SetPopupHideFinishAction(Action action)
		{
			_onPopupHideFinishAction = action;
		}


		protected void _Reset_Popup()
		{
			this._isPlayingPopupAnimation = false;
			this._isHiding = false;
			this._onPopupShowStartAction = null;
			this._onPopupShowFinishAction = null;
			this._onPopupHideStartAction = null;
			this._onPopupHideFinishAction = null;
	}

		protected void _Destroy_Popup()
		{
			this._isHiding = false;
			this._onPopupShowStartAction = null;
			this._onPopupShowFinishAction = null;
			this._onPopupHideStartAction = null;
			this._onPopupHideFinishAction = null;
		}
	}
}


