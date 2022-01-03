using System;

namespace CsCat
{
	public class BehaviourTreeNode
	{
		#region field

		public BehaviourTreeNode parent;
		private BehaviourTreeNodeStatus _status = BehaviourTreeNodeConst.Default_Status;

		#endregion

		#region delegate

		public Action onSuccess;
		public Action onFail;

		#endregion

		#region property

		public BehaviourTreeNodeStatus status
		{
			get { return _status; }
			set
			{
				_status = value;
				if (_status == BehaviourTreeNodeStatus.Success && onSuccess != null)
					onSuccess();
				else if (_status == BehaviourTreeNodeStatus.Fail && onFail != null)
					onFail();
			}
		}

		#endregion

		#region virtual method

		public virtual BehaviourTreeNodeStatus Update()
		{
			return BehaviourTreeNodeStatus.Fail;
		}



		public virtual T GetChild<T>(bool loop = false) where T : BehaviourTreeNode
		{
			return null;
		}

		public virtual void RestStatus()
		{
			this.status = BehaviourTreeNodeConst.Default_Status;
		}

		public virtual void Interrupt()
		{
			this.status = BehaviourTreeNodeStatus.WaitingToRun;

		}

		#endregion

	}
}