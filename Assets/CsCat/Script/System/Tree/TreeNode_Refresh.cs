namespace CsCat
{
	public partial class TreeNode
	{
		private bool _isCanNotRefresh = false;
		public virtual bool _IsCanRefresh()
		{
			return !_isCanNotRefresh;
		}

		public virtual void Refresh(bool isInit = false)
		{
			if (!_IsCanRefresh())
				return;
			_Refresh(isInit);
		}

		protected virtual bool _Refresh(bool isInit = false)
		{
			return true;
		}

		void _OnReset_Refresh()
		{
			_isCanNotRefresh = false;
		}

		void _OnDespawn_Refresh()
		{
			_isCanNotRefresh = false;
		}
	}
}